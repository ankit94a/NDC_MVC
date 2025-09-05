using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace NDCWeb.Infrastructure.Constants
{
    public class secConst
    {
        private static string csalt; // field
        private static string ccaptext; // field
        public static string cSalt   // property
        {
            get { return csalt; }
            set { csalt = value; }
        }
        public static string cCaptext   // property
        {
            get { return ccaptext; }
            set { ccaptext = value; }
        }
        public static string GetRandomText()
        {
            StringBuilder randomText = new StringBuilder();
            string alphabets = "012345679ACEFGHKLMNPRSWXZabcdefghijkhlmnopqrstuvwxyz";
            Random r = new Random();
            for (int j = 0; j <= 5; j++)
            {
                randomText.Append(alphabets[r.Next(alphabets.Length)]);
            }
            return randomText.ToString();
        }
    }
}