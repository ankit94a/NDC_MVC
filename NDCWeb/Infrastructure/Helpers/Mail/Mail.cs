using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace NDCWeb.Infrastructure.Helpers.Mail
{
    public static class Mail
    {
        public static void FeedbackEmail(string SenderName, string subject, string email,string mMessage,string Department)
        {
            string bodya = string.Empty;
            using (var sr = new System.IO.StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("\\Infrastructure\\Templates\\Thankyou.txt")))
            {
                bodya = sr.ReadToEnd();
            }
            string messageBody = string.Format(bodya, SenderName, email, mMessage, Department);
            Appmail(email, subject, messageBody);
        }
        public static void AlumniEmail(string SenderName, string subject, string email, string mMessage)
        {
            string bodya = string.Empty;
            using (var sr = new System.IO.StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("\\Infrastructure\\Templates\\Alumni.txt")))
            {
                bodya = sr.ReadToEnd();
            }
            string messageBody = string.Format(bodya, SenderName, email, mMessage);
            Appmail(email, subject, messageBody);
        }
        public static void Appmail(string email, string subj, string body)
        {
            try
            {
                MailMessage mailMessage = new MailMessage("aq.ndc-mod@nic.in",email);
                mailMessage.Subject = subj;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = body;
                MailAddress copy = new MailAddress("gsosystems.ndc-mod@nic.in");
                mailMessage.CC.Add(copy);
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Send(mailMessage);
            }
            catch (System.Exception)
            {
                throw;
            }

        }
    }
}