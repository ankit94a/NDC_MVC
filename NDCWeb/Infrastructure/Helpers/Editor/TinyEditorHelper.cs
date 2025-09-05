using NDCWeb.Infrastructure.Helpers.FileDirectory;
using NDCWeb.Infrastructure.Helpers.Routing;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace NDCWeb.Infrastructure.Helpers.Editor
{
    public static class TinyEditorHelper
    {
        /// <summary>
        /// Saves the contents of an uploaded image file.
        /// </summary>
        /// <param name="targetFolder">Location where to save the image file.</param>
        /// <param name="file">The uploaded image file.</param>
        /// <exception cref="InvalidOperationException">Invalid MIME content type.</exception>
        /// <exception cref="InvalidOperationException">Invalid file extension.</exception>
        /// <exception cref="InvalidOperationException">File size limit exceeded.</exception>
        /// <returns>The relative path where the file is stored.</returns>
        public static string SaveImageFile(string targetFolder, HttpPostedFileBase file, string rootLocation)
        {
            DirectoryHelper.CreateFolder(targetFolder);
            const int megabyte = 1024 * 1024;
            var extension = Path.GetExtension(file.FileName.ToLowerInvariant());
            string[] extensions = { ".gif", ".jpg", ".png", ".svg", ".webp" };

            if (!file.ContentType.StartsWith("image/"))
                throw new InvalidOperationException("Invalid MIME content type.");
            if (!extensions.Contains(extension))
                throw new InvalidOperationException("Invalid file extension.");
            if (file.ContentLength > (8 * megabyte))
                throw new InvalidOperationException("File size limit exceeded.");

            string fileNameSlug = Path.GetFileNameWithoutExtension(file.FileName).ToSlug();
            string fileName = fileNameSlug + extension;
            string path = Path.Combine(targetFolder, fileName);

            bool isExist = true;
            int serial = 1;
            while (isExist)
            {
                if (serial == 1)
                    path = Path.Combine(targetFolder, fileName);
                else
                {
                    fileName = fileNameSlug + "-" + serial.ToString() + extension;
                    path = Path.Combine(targetFolder, fileName);
                }
                isExist = File.Exists(path);
                serial++;
            }
            file.SaveAs(path);
            return Path.Combine(rootLocation.Replace("~/", "/"), fileName).Replace('\\', '/');
        }
        public static bool CheckEmailExists(string text)
        {
            const string MatchEmailPattern =
              @"(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
              + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
              + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
              + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})";

            Regex rx = new Regex(
              MatchEmailPattern,
              RegexOptions.Compiled | RegexOptions.IgnoreCase);

            // Find matches.
            MatchCollection matches = rx.Matches(text);

            // Report the number of matches found.
            int noOfMatches = matches.Count;
            bool emails = false;
            // Report on each match.
            foreach (Match match in matches)
            {
                //Console.WriteLine(match.Value.ToString());
                emails = true;
            }
            return emails;
        }
    }

}