using NDCWeb.Infrastructure.Helpers.FileDirectory;
using System;
using System.IO;

namespace NDCWeb.Infrastructure.Extensions
{
    public static class FileTypeCheck
    {
        public static bool CheckFile(this string path)
        {
            string msg = "";
            string temppath = "/writereaddata/tempdata/";
            bool retMsg = false;
            string[] file_hexa_signature = { "25-50-44-46-2D-31-2E", "50-4B-03-04-14-00-06", "D0-CF-11-E0-A1-B1-1A", "47-49-46-38-39-61-20", "FF-D8-FF-E0-00-10-4A", "89-50-4E-47-0D-0A-1A" };

            if (path != null && path != "")
            {
                BinaryReader reader = new BinaryReader(new FileStream(Convert.ToString(path), FileMode.Open, FileAccess.Read, FileShare.None));
                reader.BaseStream.Position = 0x0;     // The offset you are reading the data from
                byte[] data = reader.ReadBytes(0x10); // Read 16 bytes into an array         
                string data_as_hex = BitConverter.ToString(data);
                reader.Close();

                // substring to select first 20 characters from hexadecimal array
                string fUpload = data_as_hex.Substring(0, 20);
                string output = null;
                bool isGeniun = false;
                switch (fUpload)
                {
                    case "25-50-44-46-2D-31-2E":
                        output = "pdf";
                        isGeniun = true;
                        break;
                    case "50-4B-03-04-14-00-06":
                        output = "word-excel-ppt";
                        isGeniun = true;
                        break;
                    case "D0-CF-11-E0-A1-B1-1A":
                        output = "doc-xls-ppt";
                        isGeniun = true;
                        break;
                    case "47-49-46-38-39-61-20":
                        output = "gif";
                        isGeniun = true;
                        break;
                    case "FF-D8-FF-E0-00-10-4A":
                        output = "jpeg-jpg";
                        isGeniun = true;
                        break;
                    case "89-50-4E-47-0D-0A-1A":
                        output = "png";
                        isGeniun = true;
                        break;
                    case null:
                        output = "notmatched";
                        isGeniun = false;
                        break;
                }
                msg = output;

                if (!isGeniun)
                {
                    System.IO.File.Delete(path);
                }
                else
                    retMsg = isGeniun;
            }
            return retMsg;
        }
    }
}