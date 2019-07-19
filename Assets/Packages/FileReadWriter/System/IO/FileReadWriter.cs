using System.Text;

namespace System.IO
{
    public static class FileReadWriter
    {
        public class Result
        {
            public bool   success;
            public string content;
        }

        public static FileReadWriter.Result WriteTextToFile(string filePath, string content)
        {
            return WriteTextToFile(filePath, content, Encoding.UTF8);
        }

        public static FileReadWriter.Result WriteTextToFile(string filePath, string content, Encoding encoding)
        {
            string directoryPath = Path.GetDirectoryName(filePath);

            if (Directory.Exists(directoryPath) == false)
            {
                Directory.CreateDirectory(directoryPath);
            }

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, false, encoding))
                {
                    writer.WriteLine(content);
                }
                return new FileReadWriter.Result()
                {
                    success = true,
                    content = content,
                };
            }
            catch (Exception exception)
            {
                return new FileReadWriter.Result()
                {
                    success = false,
                    content = exception.Message,
                };
            }
        }

        public static FileReadWriter.Result ReadTextFromFile(string filePath)
        {
            return ReadTextFromFile(filePath, Encoding.UTF8);
        }

        public static FileReadWriter.Result ReadTextFromFile(string filePath, Encoding encoding)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(filePath);
                string content;

                using (StreamReader reader = new StreamReader(fileInfo.OpenRead(), encoding))
                {
                    content = reader.ReadToEnd();
                }

                return new FileReadWriter.Result()
                {
                    success = true,
                    content = content
                };
            }
            catch (Exception exception)
            {
                return new FileReadWriter.Result()
                {
                    success = false,
                    content = exception.Message
                };
            }
        }

        public static string ToRelativePath(string relativeFilePath)
        {
            if (relativeFilePath == null || relativeFilePath.Length == 0)
            {
                return "/";
            }

            if (relativeFilePath[0] == '/')
            {
                return relativeFilePath;
            }
            else
            {
                return "/" + relativeFilePath;
            }
        }
    }
}