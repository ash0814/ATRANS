using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace ATRANS
{
    internal partial class Transformer
    {
        private void ValidateFile(string filePath, ref FileData logData)
        {
            if (logData.inputFileSize < 18)
                throw new Exception("File Size Too Small: 변환 파일의 용량은 18byte 이상이어야 합니다.");
            if (!checkFileName(logData.inputFileName))
                throw new Exception("File Name Error: 변환 파일명은 '[TargetFileName]' 으로 시작하며 공백과 확장자를 포함하여 192자 이하여야  합니다.\n" +
                    " 또한,  { '\\', '/', ':', '*', '?', '\"', '<', '>', '|' } 는 포함할 수 없습니다.");
            SetEncoding(filePath , ref logData);
            if (!checkHeader(filePath, ref logData))
            {
                throw new Exception("File Header Error: 파일의 헤더가 잘못되었습니다.\n 헤더의 형식은 '[ATRANS](space)(TrimmedFileName)(\\n)' 입니다.");
            }
           
        }

        private static string ReadTextFirstLine(string fileName, Encoding encoding)
        {
            string header = null;

            using (StreamReader reader = new StreamReader(fileName, encoding))
                header = reader.ReadLine();
            return header;
        }

        private static string ReadBinaryFirstLine(string fileName, Encoding encoding)
        {
            StringBuilder header = new StringBuilder();

            using (StreamReader readTextFileStream = new StreamReader(fileName, encoding))
            {
                char[] buffer = new char[8192];
                int bytesRead;

                while ((bytesRead = readTextFileStream.Read(buffer, 0, buffer.Length)) > 0 && header.ToString().IndexOf('\n') == -1)
                {
                    byte[] bynaryData = BinaryTextToText(new string(buffer, 0, bytesRead));
                    string text = Encoding.UTF8.GetString(bynaryData);
                    header.Append(text);
                }
            }
            return header.ToString().Substring(0, header.ToString().IndexOf('\n'));
        }


        private Boolean checkHeader(string filePath, ref FileData fileData)
        {
            string header;
            if (fileData.inputFileExtension == ATXT)
                header = ReadTextFirstLine(filePath, fileData.encoding);
            else
                header = ReadBinaryFirstLine(filePath, fileData.encoding);
            if (header.Split(' ').Length < 2)
            {
                return false;
            }
            if (!header.StartsWith("[ATRANS] "))
            {
                return false;
            }
            int startIndex = "[ATRANS] ".Length;
            string headerFileName = header.Substring(startIndex);
            if (headerFileName != GetFileNameWithoutPrefix(filePath))
            {
                return false;
            }
            return true;
        }

        private Boolean checkFileName(string fileName)
        {
            if (!fileName.StartsWith("[TargetFileName]"))
                return false;
            // 파일 이름 길이 제한 확인
            // '[TargetFileName]과 확장자, 공백 포함 192자 이하
            if (fileName.Length > 192)
            {
                return false;
            }
            // 특수문자 확인
            char[] invalidChars = { '\\', '/', ':', '*', '?', '"', '<', '>', '|' };
            if (fileName.Any(c => invalidChars.Contains(c)))
            {
                return false;
            }
            // 모든 조건을 만족하면 유효한 파일 이름
            return true;
        }

        private static void SetEncoding(string filePath, ref FileData fileData)
        {
            // 파일의 첫 부분에서 인코딩을 확인
            byte[] buffer = new byte[4];
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                fileStream.Read(buffer, 0, 4);
            }

            if (buffer[0] == 0xFF && buffer[1] == 0xFE)
            {
                fileData.encoding = Encoding.Unicode; // UTF-16 little-endian
            }
            else if (buffer[0] == 0xFE && buffer[1] == 0xFF)
            {
                fileData.encoding =  Encoding.BigEndianUnicode; // UTF-16 big-endian
            }
            else
            {
                fileData.encoding =  Encoding.UTF8; // 기본 인코딩 UTF8 사용
            }
        }

        private string GetFileNameWithoutPrefix(string filePath)
        {
            if (filePath == null)
                throw new Exception("파일이 존재하지 않습니다.");
            string fileNameWithoutPath = Path.GetFileNameWithoutExtension(filePath);
            int startIndex = 0;
            if (fileNameWithoutPath.StartsWith("[TargetFileName]"))
                startIndex = "[TargetFileName]".Length;

            string fileName = fileNameWithoutPath.Substring(startIndex).Trim();

            return fileName;
        }

        private string GetNewFileSavePath(string fileName, string extention, string savePath)
        {
            string newFilePath = Path.Combine(savePath, fileName + extention);
            string newFileName = fileName;

            if (File.Exists(newFilePath))
            {
                // 파일 이름에 "(1)", "(2)" 등의 숫자를 붙여서 새로운 파일 이름 생성
                int counter = 1;
                string fileNameWithoutExtension = newFileName;
                do
                {
                    newFileName = $"{fileNameWithoutExtension}({counter}){extention}";
                    newFilePath = Path.Combine(savePath, newFileName);
                    counter++;
                } while (File.Exists(newFilePath));
            }

            return newFilePath;
        }

        private void MakeLog(FileData logData)
        {
            try
            {
                string logSaveFilePathName = GetNewFileSavePath(GetFileNameWithoutPrefix(logData.inputFileName), Path.GetExtension(logData.inputFileExtension) + ".log", savePath);
                string result = logData.result == true ? "SUCCESS" : "FAIL";

                DateTime startTime = DateTime.ParseExact(logData.startTime, "yyyy-MM-dd HH:mm:ss", null);
                DateTime endTime = DateTime.ParseExact(logData.endTime, "yyyy-MM-dd HH:mm:ss", null);
                TimeSpan difference = endTime - startTime;

                string logString =
                    $"ThreadNumber:            {logData.threadNumber}\n" +
                    $"Transform Result:         {result}\n" +
                    $"Transform Time :          {difference.Seconds}sec ({logData.startTime} - {logData.endTime})\n" +
                    $"\n" +
                    $"StartFileName:              {logData.inputFileName}\n" +
                    $"StartFilePath:                {logData.inputFilePath}\n" +
                    $"StartFileSize:                 {Math.Ceiling((double)(logData.inputFileSize / 1024))}KB({logData.inputFileSize}byte)\n" +
                    $"StartFileCreateTime:     {logData.inputFileCreateTime}\n" +
                    $"StartFileEncoding:        {logData.encoding}\n" +
                    $"\n" +
                    $"EndFileName:               {logData.outputFileName}\n" +
                    $"EndFilePath:                 {logData.outputFilePath}\n" +
                    $"EndFileSize:                  {logData.outputFileSize / 1024}KB({logData.outputFileSize}byte)\n" +
                    $"EndFileCreateTime:      {logData.outputFileCreateTime}\n" +
                    $"\n" +
                    $"Log Message:              {logData.message}\n" +
                    $"\n" +
                    $"\n" +
                    $"Detail Error Messages=============\n" +
                    $"{logData.details}";

                File.WriteAllText(logSaveFilePathName, logString);
            } catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void SetOutputFileData(string filePath, ref FileData fileData)
        {
            FileInfo outputFileInfo = new FileInfo(filePath);

            if (outputFileInfo.Exists == false)
                throw new Exception("OutputFileError: 출력 파일이 경로에 제대로 생성되지 않았습니다.");
            fileData.outputFilePath = outputFileInfo.DirectoryName;
            fileData.outputFileCreateTime = outputFileInfo.CreationTime.ToString(("yyyy-MM-dd HH:mm:ss"));
            fileData.outputFileSize = outputFileInfo.Length;
            fileData.outputFileName = outputFileInfo.Name;
            fileData.outputFileExtension = outputFileInfo.Extension;
        }

        private void SetInputFileData(string filePath, ref FileData fileData)
        {
            FileInfo inputFileInfo = new FileInfo(filePath);

            if (inputFileInfo.Exists == false || filePath == null)
                throw new Exception($"InputFileNotExist: 입력 파일이 {filePath}에 존재하지 않습니다.");
            fileData.inputFilePath = inputFileInfo.DirectoryName;
            fileData.inputFileCreateTime = inputFileInfo.CreationTime.ToString(("yyyy-MM-dd HH:mm:ss"));
            fileData.inputFileSize = inputFileInfo.Length;
            fileData.inputFileName = inputFileInfo.Name;
            fileData.inputFileExtension = inputFileInfo.Extension;
        }
    }
}
