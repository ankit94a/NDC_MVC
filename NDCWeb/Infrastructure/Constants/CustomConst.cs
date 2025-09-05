using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace NDCWeb.Infrastructure.Constants
{
    public class CustomConst
    {
        public static string GetUploadDirectory(string FileName)
        {
            var mimeType = MimeMapping.GetMimeMapping(FileName);
            string path = ServerRootConsts.MEDIA_ROOT;
            if (mimeType.Length > 0)
            {
                if (mimeType.ToString() == "image/jpeg" || mimeType.ToString() == "image/png" || mimeType.ToString() == "image/gif")
                {
                    path = path + "images/";
                }
                else if (mimeType.ToString() == "application/msword" || mimeType.ToString() == "application/pdf" || mimeType.ToString() == "application/vnd.ms-excel" || mimeType.ToString() == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" || mimeType.ToString() == "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
                {
                    path = path + "documents/";
                }
                else if (mimeType.ToString() == "video/mp4" || mimeType.ToString() == "application/x-mpegURL" || mimeType.ToString() == "video/x-ms-wmv" || mimeType.ToString() == "video/quicktime" || mimeType.ToString() == "video/quicktime")
                {
                    path = path + "videos/";
                }
                else
                {
                    path = "InvalidFile";
                }
            }
            return path;
        }
    }
    public static class AppSettingsKeyConsts
    {
        public const string PUBLIC_ROOT_KEY = "PublicRoot";
        public const string MEDIA_ROOT_KEY = "MediaRoot";
        public static string DefPassKey = ConfigurationManager.AppSettings["DefaultPass"] == null ? "Cspl@#$1234" : ConfigurationManager.AppSettings["DefaultPass"].ToString();
    }
    public static class ServerRootConsts
    {
        public const string PUBLIC_ROOT = "~/writereaddata/";
        public const string MEDIA_ROOT = "~/writereaddata/media/";
        public const string USER_ROOT = "~/writereaddata/user/";
        public const string ALUMNI_ROOT = "~/writereaddata/alumni/";
        public const string SPEAKER_ROOT = "~/writereaddata/speaker/";
        public const string PRODUCT_IMAGE_ROOT = "~/writereaddata/images/products/";

        public const string PAGE_CONTENT_ROOT = "~/writereaddata/";
        public const string NEWS_CONTENT_ROOT = "~/writereaddata/";
        public const string FORUMBLOG_ROOT = "~/writereaddata/forumblog/";
        public const string ALUMNIARTICLE_ROOT = "~/writereaddata/Alumniarticle/";
        public const string ACCOMODATION_ROOT = "~/writereaddata/accomodation/";
        public const string CIRCULAR_ROOT = "~/writereaddata/circular/";
        public const string TRAINING_DOC_ROOT = "~/writereaddata/trainingdocs/";
        public const string TEMP_ROOT = "~/writereaddata/tempdata/";
    }
}