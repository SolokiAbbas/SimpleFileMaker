using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileMaker
{
    class TextMaker
    {
        private string Path { get; set; }
        private string FileName { get; set; }
        
        private readonly int ConvertToKb = 1024;

        public TextMaker(string _path, string _fileName)
        {
            Path = _path;
            FileName = _fileName;
        }

        /// <summary>
        /// Creates a NON empty text
        /// </summary>
        /// <param name="maxFileSize"></param> in MB
        public void TextCreateByFilling(string maxFileSize)
        {

            try
            {
                var resultByte = String.Empty;
                var fileSize = ConvertToMb(maxFileSize);

                // Create the file, or overwrite if the file exists.
                using (FileStream fs = File.Create(Path + FileName))
                {
                    for (int i = 0; i < fileSize; i++)
                    {
                        resultByte += ByteList;
                    }
                    byte[] info = new UTF8Encoding(true).GetBytes(resultByte);
                    // Add some information to the file.                    
                    fs.Write(info, 0, info.Length);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Creates Text that you dont care what is inside.
        /// </summary>
        /// <param name="setLength"></param> In MB
        public void TextCreateBySetLength(string setLength)
        {

            try
            {
                var resultByte = String.Empty;
                // Create the file, or overwrite if the file exists.
                using (FileStream fs = File.Create(Path + FileName))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes("Initial Text");
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                    // Set length to anything but fills it with empty spaces
                    fs.SetLength(ConvertToMb(setLength) * 1024);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private int ConvertToMb (string fileLengthInput)
        {
            return (int)Math.Round(double.Parse(fileLengthInput) * ConvertToKb);
        }

        // 1000 bytes
        private readonly string ByteList = "This is a test file.This is a test file that is written in c sharpwithThis is a test file that is written in c sharpwith. " +
                "These are the words of a powerful person who said this and that they are using csharp to do stuff and write code. that is allbamThis is a test file." +
                "This is a test file that is written in c sharpwithThis is a test file that is written in c sharpwith. These are the words of a powerful person who said this " +
                "and that they are using csharp to do stuff and write code. that is allbamThis is a test file.This is a test file that is written in c sharpwithThis is a test " +
                "file that is written in c sharpwith. These are the words of a powerful person who said this and that they are using csharp to do stuff and write code. that is " +
                "allbamThis is a test file.This is a test file that is written in c sharpwithThis is a test file that is written in c sharpwith. These are the words of a powerful " +
                "person who said this and that they are using csharp to do stuff and write code. that is allbam";
        
    }
}
