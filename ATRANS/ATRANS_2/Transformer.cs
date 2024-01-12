using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace ATRANS
{
    internal partial class Transformer
    {
        public Transformer(string input, string output, int threadCnt, int idx, string type,
                                            Action<int, string> UpdateStatusUIFunc, Action<int, bool> UpdateCnt, MainForm f)
        {
            inputPath = input;
            outputPath = output;
            threadCount = threadCnt;
            transformerIndex = idx;
            transformType = type;
            UpdateThreadStatusUI = UpdateStatusUIFunc;
            UpdateCountUI = UpdateCnt;

            form = f;
        }

        private MainForm form;
        public string inputPath { get; set; }
        public string outputPath { get; set; }
        public string savePath { get; set; }
        public int threadCount { get; set; }
        public int transformerIndex { get; set; }
        public string transformType { get; set; }

        private Task[] workers;
        private BlockingCollection<string> fileQueue = new BlockingCollection<string>();
        private Dictionary<string, string> renamedFilePath = new Dictionary<string, string>();

        private Boolean SUCCESS = true;
        private Boolean FAIL = false;
        private string ABIN = ".abin";
        private string ATXT = ".atxt";

        struct FileData
        {
            public int threadNumber;

            public string inputFilePath;
            public string inputFileName;
            public string inputFileExtension;
            public long inputFileSize;
            public string inputFileCreateTime;

            public string outputFilePath;
            public string outputFileName;
            public string outputFileExtension;
            public long outputFileSize;
            public string outputFileCreateTime;

            public string startTime;
            public string endTime;

            public Encoding encoding;

            public Boolean result;
            public string message;
            public string details;
        }

        private CancellationTokenSource[] cancellationTokenSources;

        FileSystemWatcher watcher = new FileSystemWatcher();
        Action<int, string> UpdateThreadStatusUI;
        Action<int, bool> UpdateCountUI;

        public void initTransformer()
        {
            savePath = Path.Combine(outputPath, "Atrans_save");

            cancellationTokenSources = new CancellationTokenSource[threadCount];
            workers = new Task[threadCount];
            for (int i = 0; i < threadCount; i++)
            {
                int workerIdx = i;
                int idx = workerIdx + form.t_idx;
                cancellationTokenSources[workerIdx] = new CancellationTokenSource();
                workers[workerIdx] = Task.Run(() => startTransfort(fileQueue, idx, cancellationTokenSources[workerIdx].Token), cancellationTokenSources[workerIdx].Token);
                form.errorList.Add(idx, new ObservableCollection<ListViewItem>());
            }
            form.t_idx += threadCount;

            watcher.Path = inputPath;
            watcher.Created += WatcherFileCreated;
            watcher.Renamed += WatcherFileRenamed;
            watcher.EnableRaisingEvents = true;

            if (Directory.Exists(inputPath))
            {
                string[] files = Directory.GetFiles(inputPath);
                foreach (string file in files)
                {
                    string extension = Path.GetExtension(file);
                    if (extension == ATXT || extension == ABIN)
                    {
                        fileQueue.Add(file);
                    }
                }
            }

            if (Directory.Exists(outputPath) == false)
            {
                throw new Exception($"출력 경로 : {outputPath}가 존재하지 않습니다.");
            }

        }

        private void TextToBinary(string filePath, ref FileData fileData, CancellationToken ct)
        {
            string newFilePath = GetNewFileSavePath(GetFileNameWithoutPrefix(filePath), ABIN, outputPath);
            try
            {
                using (StreamReader readTextFileStream = new StreamReader(filePath, fileData.encoding))
                using (StreamWriter writeBinaryStream = new StreamWriter(newFilePath, true))
                {
                    ct.ThrowIfCancellationRequested();
                    string header = readTextFileStream.ReadLine();
                    string newHeader = $"[ATRANS] {Path.GetFileNameWithoutExtension(newFilePath)}\n";
                    byte[] utf8HeaderData = Encoding.UTF8.GetBytes(newHeader);
                    StringBuilder binaryHeaderStringBuilder = new StringBuilder();
                    foreach (byte b in utf8HeaderData)
                    {
                        binaryHeaderStringBuilder.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
                    }
                    string binaryHeaderText = binaryHeaderStringBuilder.ToString();
                    writeBinaryStream.Write(binaryHeaderText);


                    char[] buffer = new char[4096];
                    int bytesRead;
                    while ((bytesRead = readTextFileStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ct.ThrowIfCancellationRequested();
                        byte[] utf8Data = Encoding.UTF8.GetBytes((new string(buffer, 0, bytesRead)));
                        StringBuilder binaryStringBuilder = new StringBuilder();
                        foreach (byte b in utf8Data)
                        {
                            binaryStringBuilder.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
                        }
                        string binaryText = binaryStringBuilder.ToString();
                        writeBinaryStream.Write(binaryText);
                    }
                }
                SetOutputFileData(newFilePath, ref fileData);
            } catch (OperationCanceledException)
            {
                SetOutputFileData(newFilePath, ref fileData);
                fileData.endTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                fileData.result = FAIL;
                fileData.message = "Thread Stop During Working";
                MakeLog(fileData);
                throw;
            } catch (Exception)
            {
                throw;
            }
        }

        static byte[] BinaryTextToText(string binaryText)
        {
            if (!Regex.IsMatch(binaryText, "^[01]+$"))
            {
                throw new Exception("File Error: 이진 파일이 아닙니다.");
            }

            // 8비트씩 끊어서 바이너리로 변환
            int length = binaryText.Length / 8;
            byte[] binaryData = new byte[length];

            for (int i = 0; i < length; i++)
            {
                string binaryByte = binaryText.Substring(i * 8, 8);
                binaryData[i] = Convert.ToByte(binaryByte, 2);
            }

            return binaryData;
        }

        private void BinaryToText(string filePath, ref FileData fileData, CancellationToken ct)
        {
            string newFilePath = GetNewFileSavePath(GetFileNameWithoutPrefix(filePath), ATXT, outputPath);

            string newHeader = $"[ATRANS] {Path.GetFileNameWithoutExtension(newFilePath)}";
            try
            {
                using (StreamReader readBinaryFileStream = new StreamReader(filePath, fileData.encoding))
                using (StreamWriter writeTextStream = new StreamWriter(newFilePath, true))
                {
                    ct.ThrowIfCancellationRequested();
                    char[] buffer = new char[8];
                    int bytesRead;

                    while ((bytesRead = readBinaryFileStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ct.ThrowIfCancellationRequested();
                        byte[] binaryData = BinaryTextToText(new string(buffer, 0, bytesRead));
                        string tmp = Encoding.UTF8.GetString(binaryData);
                        if (tmp == "\n")
                            break;
                    }

                    writeTextStream.WriteLine(newHeader);

                    buffer = new char[16384];

                    while ((bytesRead = readBinaryFileStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ct.ThrowIfCancellationRequested();
                        byte[] bynaryData = BinaryTextToText(new string(buffer, 0, bytesRead));
                        string text = Encoding.UTF8.GetString(bynaryData);
                        writeTextStream.Write(text);
                    }
                }
                SetOutputFileData(newFilePath, ref fileData);
            } catch (OperationCanceledException)
            {
                SetOutputFileData(newFilePath, ref fileData);
                fileData.result = FAIL;
                fileData.message = "Thread Stop During Working";
                MakeLog(fileData);
                throw;
            } catch (Exception) {
                throw;
            }
        }


        private void startTransfort(BlockingCollection<string> fileQueue, int threadIdx, CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {

                    UpdateThreadStatusUI(threadIdx, $"thread ID : {threadIdx}      WAIT");
                    cancellationToken.ThrowIfCancellationRequested();

                    string currentTransfortFilePath = fileQueue.Take(cancellationToken);
                    FileData currentFileData = new FileData();

                    if (File.Exists(currentTransfortFilePath) == false && renamedFilePath.ContainsKey(currentTransfortFilePath))
                    {
                        currentTransfortFilePath = renamedFilePath[currentTransfortFilePath];
                        renamedFilePath.Remove(currentTransfortFilePath);
                    }
                    currentFileData.startTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    currentFileData.threadNumber = threadIdx;
                    currentFileData.inputFilePath = currentTransfortFilePath;
                    currentFileData.inputFileName = Path.GetFileName(currentTransfortFilePath);
                    currentFileData.inputFileExtension = Path.GetExtension(currentTransfortFilePath);
                    UpdateThreadStatusUI(threadIdx, $"thread ID : {threadIdx}      IN PROGRESS      {Path.GetFileName(currentTransfortFilePath)}");

                    try
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        if (File.Exists(currentTransfortFilePath) == false)
                        {
                            throw new Exception("경로에 파일이 존재하지 않거나 파일에 접근할 수 없습니다.");
                        }
                        SetInputFileData(currentTransfortFilePath, ref currentFileData);
                        ValidateFile(currentTransfortFilePath, ref currentFileData);

                        if (currentFileData.inputFileExtension == ATXT)
                        {
                            TextToBinary(currentTransfortFilePath, ref currentFileData, cancellationToken);
                        }
                        else if (currentFileData.inputFileExtension == ABIN)
                        {
                            BinaryToText(currentTransfortFilePath, ref currentFileData, cancellationToken);
                        }
                        cancellationToken.ThrowIfCancellationRequested();
                        currentFileData.result = SUCCESS;
                        currentFileData.message = "SUCCESS";

                        UpdateCountUI(threadIdx, SUCCESS);

                    } catch (OperationCanceledException)
                    {
                        throw;
                    }
                    catch (Exception e)
                    {
                        currentFileData.result = FAIL;
                        currentFileData.message = e.Message;
                        currentFileData.details = $"{e.Message}\n" +
                            $"{e.StackTrace}\n" +
                            $"{e.HelpLink}\n" +
                            $"{e.Data}\n" +
                            $"{e.InnerException}" +
                            $"{e.Source}" +
                            $"{e.TargetSite}";
                        ListViewItem error = new ListViewItem(threadIdx.ToString());
                        error.SubItems.Add(Path.GetFileName(currentTransfortFilePath));
                        error.SubItems.Add(e.Message);
                        error.SubItems.Add(currentTransfortFilePath);
                        error.SubItems.Add(currentFileData.startTime);
                        error.SubItems.Add(savePath);

                        form.errorList[threadIdx].Add(error);

                        UpdateCountUI(threadIdx, FAIL);
                    }
                    finally
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        currentFileData.endTime = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss"));
                        if (!Directory.Exists(savePath))
                            Directory.CreateDirectory(savePath);

                        string destFilePath = GetNewFileSavePath(Path.GetFileNameWithoutExtension(currentFileData.inputFileName), currentFileData.inputFileExtension, savePath);
                        if (Path.GetFileNameWithoutExtension(destFilePath) != Path.GetFileNameWithoutExtension(currentFileData.inputFileName))
                        {
                            currentFileData.message = $"원본 파일 이름 변경: {Path.GetFileName(currentFileData.inputFileName)} -> {Path.GetFileName(destFilePath)}";
                            currentFileData.details = $"보관 경로에 원본 파일과 같은 이름의 파일이 존재합니다.";
                        }
                        MakeLog(currentFileData);
                        if (File.Exists(currentTransfortFilePath))
                        {
                            if (File.Exists(destFilePath))
                            {
                                // 대상 파일이 사용 중이면 대기
                                while (IsFileInUse(destFilePath))
                                {
                                    cancellationToken.ThrowIfCancellationRequested();
                                    Thread.Sleep(1000); // 1초 동안 대기
                                }
                            }
                            cancellationToken.ThrowIfCancellationRequested();
                            try
                            {
                                File.Move(currentTransfortFilePath, destFilePath);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"파일 이동 오류: {e.Message}");
                            }
                        }
                        for (int i = 10; i > 0; i--)
                        {
                            cancellationToken.ThrowIfCancellationRequested();
                            UpdateThreadStatusUI(threadIdx, $"thread ID : {threadIdx}      COMPLETE ... {i}");
                            Thread.Sleep(1000);
                        }
                    }
                }
            }
            catch (OperationCanceledException ex)
            {
                try
                {
                    while (fileQueue.Count > 0)
                    {
                        string leftFile = fileQueue.Take();
                        FileData leftFileData = new FileData();
                        leftFileData.inputFilePath = leftFile;
                        leftFileData.inputFileExtension = Path.GetExtension(leftFile);
                        leftFileData.startTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        leftFileData.endTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        leftFileData.result = FAIL;
                        leftFileData.inputFileName = Path.GetFileName(leftFile);
                        leftFileData.message = "Thread Stop";
                        MakeLog(leftFileData);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"로그 파일 오류: {e.Message}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private static bool IsFileInUse(string filePath)
        {
            try
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                    return false;
            }
            catch
            {
                return true;
            }
        }


        private void WatcherFileCreated(object sender, FileSystemEventArgs e)
        {
            string extension = Path.GetExtension(e.FullPath).ToLower();
            if (extension == ATXT || extension == ABIN)
                fileQueue.Add(Path.ChangeExtension(e.FullPath, extension)); // 소문자로 변경해서 작업 시작
        }

        private void WatcherFileRenamed(object sender, RenamedEventArgs e)
        {
            renamedFilePath.Add(e.OldFullPath, e.FullPath);
        }

        public void Stop()
        {
            if (cancellationTokenSources != null)
            {
                foreach (var cts in cancellationTokenSources)
                {
                    cts?.Cancel();
                    cts?.Dispose();
                }
                Task.WaitAll(workers);
            }

            // Stop file watcher
            watcher.EnableRaisingEvents = false;
            watcher.Created -= WatcherFileCreated;
            watcher.Renamed -= WatcherFileRenamed;

            cancellationTokenSources = null;
            workers = null;
        }

    }
}
