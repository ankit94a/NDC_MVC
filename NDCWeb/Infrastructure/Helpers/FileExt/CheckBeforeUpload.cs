using Microsoft.AspNetCore.Hosting.Server;
using NDCWeb.Infrastructure.Constants;
using NDCWeb.Infrastructure.Helpers.FileDirectory;

//using Spire.Doc;
//using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
//using PDfFormat = Spire.Pdf.FileFormat;
//using DocFormat = Spire.Doc.FileFormat;

namespace NDCWeb.Infrastructure.Helpers.FileExt
{
    public class CheckBeforeUpload
    {
        public string ErrorMessage { get; set; }
        public decimal filesize { get; set; }
        public string UploadUserFile(HttpPostedFileBase file)
        {
            try
            {
                var supportedTypes = new[] { "doc", "docx", "pdf", "xls", "xlsx" };
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                if (!supportedTypes.Contains(fileExt))
                {
                    ErrorMessage = "File Extension Is InValid - Only Upload WORD/PDF/EXCEL File";
                    return ErrorMessage;
                }
                else if (file.ContentLength > (filesize * 1024))
                {
                    ErrorMessage = "File size Should Be UpTo " + filesize + "KB only";
                    return ErrorMessage;
                }
                else
                {
                    ErrorMessage = "File Is Successfully Uploaded";
                    return ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "Upload Container Should Not Be Empty or Contact Admin";
                return ErrorMessage;
            }
        }
        public string UploadImageFile(HttpPostedFileBase file)
        {
            string path = "";
            try
            {
                if (filesize == 0)
                    filesize = 550;

                var supportedTypes = new[] { "jpg", "png", "jpeg" };
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                var fileName = Path.GetFileName(file.FileName);
                Guid guid = Guid.NewGuid();
                path = System.Web.HttpContext.Current.Server.MapPath(ServerRootConsts.TEMP_ROOT + guid + Path.GetExtension(fileName));
                file.SaveAs(path);  //Upload to temp
                if (!supportedTypes.Contains(fileExt))
                {
                    if (File.Exists(path))
                        System.IO.File.Delete(path);

                    ErrorMessage = "File Extension is Invalid - Only Upload JPG/PNG/JPEG File";
                    return ErrorMessage;
                }
                else if (file.ContentLength > (filesize * 1024))
                {
                    if (File.Exists(path))
                        System.IO.File.Delete(path);

                    ErrorMessage = "File size Should Be Upto " + filesize + "KB only";
                    return ErrorMessage;
                }
                else if (!file.ContentType.StartsWith("image/"))
                {
                    if (File.Exists(path))
                        System.IO.File.Delete(path);

                    ErrorMessage = "Invalid content type.";
                    return ErrorMessage;
                }
                else if (!supportedTypes.Contains(fileExt))
                {
                    if (File.Exists(path))
                        System.IO.File.Delete(path);

                    ErrorMessage = "Invalid file extension.";
                    return ErrorMessage;
                }
                else if (!ValidExtension(file.FileName))
                {
                    if (File.Exists(path))
                        System.IO.File.Delete(path);

                    ErrorMessage = "File contains double extension.";
                    return ErrorMessage;
                }
                else if (!checkImg(file, path))
                {
                    if (File.Exists(path))
                        System.IO.File.Delete(path);

                    ErrorMessage = "File is malicious not geniun.";
                    return ErrorMessage;
                }
                else if (!IsValidHeader(file,path))
                {
                    if (File.Exists(path))
                        System.IO.File.Delete(path);

                    ErrorMessage = "File is malicious.";
                    return ErrorMessage;
                }
                else
                {
                    if (File.Exists(path))
                        System.IO.File.Delete(path);

                    ErrorMessage = string.Empty;
                    return ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                if (File.Exists(path))
                    System.IO.File.Delete(path);

                ErrorMessage = "Uploaded files is invalid or does not exists.";
                return ErrorMessage;
            }
        }
        public string UploadFile(HttpPostedFileBase file)
        {
            string path = "";
            try
            {
                if (filesize == 0)
                    filesize = 550;

                var supportedTypes = new[] { "jpg", "jpeg", "png", "doc", "docx", "pdf", "xlsx" };
                var supportedMimeTypes = new[] { "image/jpg", "image/jpeg", "image/png", "application/pdf", "application/msword", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "application/vnd.ms-excel", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" };
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                var fileName = Path.GetFileName(file.FileName);
                Guid guid = Guid.NewGuid();
                path = System.Web.HttpContext.Current.Server.MapPath(ServerRootConsts.TEMP_ROOT + guid + Path.GetExtension(fileName));
                if (supportedTypes.Contains(fileExt.ToLower()))
                {
                    file.SaveAs(path);  //Upload to temp

                    if (!supportedTypes.Contains(fileExt.ToLower()))
                    {
                        if (File.Exists(path))
                            System.IO.File.Delete(path);

                        ErrorMessage = "File Extension is Invalid - Only Upload JPG/JPEG/PNG/DOC/DOCX/PDF/XLSX File";
                        return ErrorMessage;
                    }
                    else if (file.ContentLength > (filesize * 1024))
                    {
                        if (File.Exists(path))
                            System.IO.File.Delete(path);

                        ErrorMessage = "File size Should Be Upto " + filesize + "kb only";
                        return ErrorMessage;
                    }
                    else if (!supportedMimeTypes.Contains(file.ContentType))
                    {
                        if (File.Exists(path))
                            System.IO.File.Delete(path);

                        ErrorMessage = "Invalid file type.";
                        return ErrorMessage;
                    }
                    else if (!ValidExtension(file.FileName))
                    {
                        if (File.Exists(path))
                            System.IO.File.Delete(path);

                        ErrorMessage = "Invalid file, filename should not contain double extension.";
                        return ErrorMessage;
                    }
                    else if (!IsValidHeader(file, path))
                    {
                        if (File.Exists(path))
                            System.IO.File.Delete(path);

                        ErrorMessage = "File is malicious or not geniun. (HMIM)";  //Header miss match
                        return ErrorMessage;
                    }
                    else if (file.ContentType.StartsWith("image/") && !checkImg(file, path))
                    {
                        if (File.Exists(path))
                            System.IO.File.Delete(path);

                        ErrorMessage = "File is malicious or not geniun. (NIM)"; //Not image
                        return ErrorMessage;
                    }
                    else
                    {
                        //if (File.Exists(path))
                        //    System.IO.File.Delete(path);

                        ErrorMessage = string.Empty;
                        return ErrorMessage;
                    }
                }
                else
                {
                    ErrorMessage = "File Extension is Invalid - Only Upload JPG/JPEG/PNG/DOC/DOCX/PDF/XLSX File";
                    return ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                if (File.Exists(path))
                    System.IO.File.Delete(path);

                ErrorMessage = "Uploaded files is invalid or does not exists.";
                return ErrorMessage;
            }
        }
        public string UploadFile(HttpPostedFileBase file, Guid guid)
        {
            string path = "";
            try
            {
                if (filesize == 0)
                    filesize = 550;

                var supportedTypes = new[] { "jpg", "jpeg", "png", "doc", "docx", "pdf", "xlsx" };
                var supportedMimeTypes = new[] { "image/jpg", "image/jpeg", "image/png", "application/pdf", "application/msword", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "application/vnd.ms-excel", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" };
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                var fileName = Path.GetFileName(file.FileName);

                string PHOTO_PATH = "photos/";
                string CURRENT_YEAR = DateTime.Now.Year.ToString();
                string CURRENT_MONTH = DateTime.Now.ToString("MMM");

                string tPath = ServerRootConsts.TEMP_ROOT + PHOTO_PATH + CURRENT_YEAR + "/" + CURRENT_MONTH + "/";
                DirectoryHelper.CreateFolder(System.Web.HttpContext.Current.Server.MapPath(tPath));
                //Guid guid = Guid.NewGuid();
                path = System.Web.HttpContext.Current.Server.MapPath(tPath + guid + Path.GetExtension(fileName));
                if (supportedTypes.Contains(fileExt.ToLower()))
                {
                    file.SaveAs(path);  //Upload to temp

                    if (!supportedTypes.Contains(fileExt.ToLower()))
                    {
                        if (File.Exists(path))
                            System.IO.File.Delete(path);

                        ErrorMessage = "File Extension is Invalid - Only Upload JPG/JPEG/PNG/DOC/DOCX/PDF/XLSX File";
                        return ErrorMessage;
                    }
                    else if (file.ContentLength > (filesize * 1024))
                    {
                        if (File.Exists(path))
                            System.IO.File.Delete(path);

                        ErrorMessage = "File size Should Be Upto " + filesize + "kb only";
                        return ErrorMessage;
                    }
                    else if (!supportedMimeTypes.Contains(file.ContentType))
                    {
                        if (File.Exists(path))
                            System.IO.File.Delete(path);

                        ErrorMessage = "Invalid file type.";
                        return ErrorMessage;
                    }
                    else if (!ValidExtension(file.FileName))
                    {
                        if (File.Exists(path))
                            System.IO.File.Delete(path);

                        ErrorMessage = "Invalid file, filename should not contain double extension.";
                        return ErrorMessage;
                    }
                    else if (!IsValidHeader(file, path))
                    {
                        if (File.Exists(path))
                            System.IO.File.Delete(path);

                        ErrorMessage = "File is malicious or not geniun. (HMIM)";  //Header miss match
                        return ErrorMessage;
                    }
                    else if (file.ContentType.StartsWith("image/") && !checkImg(file, path))
                    {
                        if (File.Exists(path))
                            System.IO.File.Delete(path);

                        ErrorMessage = "File is malicious or not geniun. (NIM)"; //Not image
                        return ErrorMessage;
                    }
                    else
                    {
                        //if (File.Exists(path))
                        //    System.IO.File.Delete(path);

                        ErrorMessage = string.Empty;
                        return ErrorMessage;
                    }
                }
                else
                {
                    ErrorMessage = "File Extension is Invalid - Only Upload JPG/JPEG/PNG/DOC/DOCX/PDF/XLSX File";
                    return ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                if (File.Exists(path))
                    System.IO.File.Delete(path);

                ErrorMessage = "Uploaded files is invalid or does not exists.";
                return ErrorMessage;
            }
        }
        public static bool ValidExtension(string filename)
        {
            try
            {
                var regex = @".*\..*\..*";
                var temp = Regex.IsMatch(filename, regex);
                if (temp)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsValidHeader(HttpPostedFileBase file, string path)
        {
            string msg = "";
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
                string fUpload = data_as_hex.Substring(0, 11);
                string output = null;
                bool isGeniun = false;
                switch (fUpload)
                {
                    case "25-50-44-46":
                        output = "pdf";
                        isGeniun = true;
                        break;
                    case "50-4B-03-04":
                        output = "word-excel-ppt";
                        isGeniun = true;
                        break;
                    case "D0-CF-11-E0":
                        output = "doc-xls-ppt";
                        isGeniun = true;
                        break;
                    case "47-49-46-38":
                        output = "gif";
                        isGeniun = true;
                        break;
                    case "FF-D8-FF-E0":
                        output = "jpeg";
                        isGeniun = true;
                        break;
                    case "FF-D8-FF-E1":
                        output = "jpg";
                        isGeniun = true;
                        break;
                    case "89-50-4E-47":
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
                    retMsg = isGeniun;
                else
                    retMsg = isGeniun;
            }
            return retMsg;
        }

        public static bool checkImg(HttpPostedFileBase file, string path)
        {
            bool msg = false;
            
            FileInfo fileI = new FileInfo(path);
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                byte[] BytesOfPic = new byte[Convert.ToInt32(fileI.Length)];
                stream.Read(BytesOfPic, 0, Convert.ToInt32(fileI.Length));

                using (MemoryStream mStream = new MemoryStream())
                {
                    mStream.Write(BytesOfPic, 0, BytesOfPic.Length);
                    mStream.Seek(0, SeekOrigin.Begin);
                    Bitmap bm = new Bitmap(mStream);

                    msg = true;
                }
            }
            return msg;
        }
  
    }
}