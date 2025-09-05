using System;
using System.Collections.Generic;
using System.Linq;

namespace NDCWeb.Infrastructure.Helpers.FileExt
{
    public static class MimeTypeMap
    {
        private static readonly Lazy<IDictionary<string, string>> _mappings = new Lazy<IDictionary<string, string>>(BuildMappings);
        private static IDictionary<string, string> BuildMappings()
        {
            var mappings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) {
 
                #region Big freaking list of mime types
            
                // maps both ways,
                // extension -> mime type
                //   and
                // mime type -> extension
                //
                // any mime types on left side not pre-loaded on right side, are added automatically
                // some mime types can map to multiple extensions, so to get a deterministic mapping,
                // add those to the dictionary specifcially
                //
                // combination of values from Windows 7 Registry and
                // from C:\Windows\System32\inetsrv\config\applicationHost.config
                // some added, including .7z and .dat
                //
                // Some added based on http://www.iana.org/assignments/media-types/media-types.xhtml
                // which lists mime types, but not extensions
                //
               
                {".doc", "application/msword"},
                {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
                {".jpeg", "image/jpeg"},
                {".jpg", "image/jpeg"},
                {".png", "image/png"},
                {".pdf", "application/pdf"},
                {".ppt", "application/vnd.ms-powerpoint"},
                {".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},

                {"application/msword",".doc"},
                {"application/vnd.ms-excel",".xls"},
                {"application/vnd.openxmlformats-officedocument.wordprocessingml.document",".docx"},
                {"image/jpeg", ".jpeg"},
                {"image/jpeg", ".jpg"},
                {"image/png",".png"},
                {"application/pdf",".pdf"},
                {"application/vnd.ms-powerpoint", ".ppt"},
                {"application/vnd.openxmlformats-officedocument.presentationml.presentation",".pptx"},
                {"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",".xlsx"},
 
                #endregion
 
                };

            var cache = mappings.ToList(); // need ToList() to avoid modifying while still enumerating

            foreach (var mapping in cache)
            {
                if (!mappings.ContainsKey(mapping.Value))
                {
                    mappings.Add(mapping.Value, mapping.Key);
                }
            }

            return mappings;
        }

        public static string GetMimeType(string extension)
        {
            if (extension == null)
            {
                throw new ArgumentNullException("extension");
            }

            if (!extension.StartsWith("."))
            {
                extension = "." + extension;
            }

            string mime;

            return _mappings.Value.TryGetValue(extension, out mime) ? mime : "application/octet-stream";
        }

        public static string GetExtension(string mimeType)
        {
            if (mimeType == null)
            {
                throw new ArgumentNullException("mimeType");
            }

            if (mimeType.StartsWith("."))
            {
                throw new ArgumentException("Requested mime type is not valid: " + mimeType);
            }

            string extension;

            if (_mappings.Value.TryGetValue(mimeType, out extension))
            {
                return extension;
            }

            throw new ArgumentException("Requested mime type is not registered: " + mimeType);
        }
    }
}