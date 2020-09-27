using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace GameCommon.Utils
{
    class IOUtils
    {
        public static bool SaveToFile_Binary(string filePath, String content)
        {
            FileStream file = new FileStream(filePath, FileMode.Create);
            // 二进制 写入
            BinaryWriter writer = new BinaryWriter(file);
            writer.Write(content);
            file.Close();
            return true;
        }

        public static string LoadFromFile_Binary(string filePath)
        {
            FileStream file = null;
            try
            {
                file = new FileStream(filePath, FileMode.Open);
            }
            catch (FileNotFoundException e)
            {
                Debug.WriteLine("FileNotFoundException " + e.FileName);
            }

            if (file == null)
            {
                return default;
            }

            // 二进制读
            BinaryReader reader = new BinaryReader(file);
            StringBuilder stringBuilder = new StringBuilder();
            while (file.Position < file.Length)
            {
                stringBuilder.Append(reader.ReadString());

            }
            file.Close();
            return stringBuilder.ToString();
        }
    }
}
