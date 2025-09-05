using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace NDCWeb.Infrastructure.Helpers.Email
{
    public class SendEmailHelper
    {
        public static bool EmailSend(string SenderEmail, string Subject, string Message, bool IsBodyHtml = false)
        {
            bool status = false;
            try
            {
                //string HostAddress = ConfigurationManager.AppSettings["Host"].ToString();
                //string FormEmailId = ConfigurationManager.AppSettings["MailFrom"].ToString();
                //string Password = ConfigurationManager.AppSettings["Password"].ToString();
                //string Port = ConfigurationManager.AppSettings["Port"].ToString();
                //SenderEmail = "ar@gmail.com";
                string HostAddress = "smtp.gmail.com";
                string FormEmailId = "ar@gmail.com";
                string Password = "******";
                string Port = "587";
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(FormEmailId);
                mailMessage.Subject = Subject;
                mailMessage.Body = Message;
                mailMessage.IsBodyHtml = IsBodyHtml;
                mailMessage.To.Add(new MailAddress(SenderEmail));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = HostAddress;
                smtp.EnableSsl = true;
                NetworkCredential networkCredential = new NetworkCredential();
                networkCredential.UserName = mailMessage.From.Address;
                networkCredential.Password = Password;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = networkCredential;
                smtp.Port = Convert.ToInt32(Port);
                smtp.Send(mailMessage);
                status = true;
                return status;
            }
            catch (Exception e)
            {
                return status;
            }
        }

        public static bool EmailSendToMutipleRecipient(List<string> SenderEmail, string Subject, string Message, bool IsBodyHtml = false)
        {
            bool status = false;
            try
            {
                //string HostAddress = ConfigurationManager.AppSettings["Host"].ToString();
                //string FormEmailId = ConfigurationManager.AppSettings["MailFrom"].ToString();
                //string Password = ConfigurationManager.AppSettings["Password"].ToString();
                //string Port = ConfigurationManager.AppSettings["Port"].ToString();

                //SenderEmail = "ajayratudi1997@gmail.com";
                string HostAddress = "smtp.gmail.com";
                string FormEmailId = "ar@gmail.com";
                string Password = "******";
                string Port = "587";
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(FormEmailId);
                mailMessage.Subject = Subject;
                mailMessage.Body = Message;
                mailMessage.IsBodyHtml = IsBodyHtml;
                //mailMessage.To.Add(new MailAddress(SenderEmail));
                foreach (var address in SenderEmail)
                {
                    mailMessage.To.Add(address);
                }
                SmtpClient smtp = new SmtpClient();
                smtp.Host = HostAddress;
                smtp.EnableSsl = true;
                NetworkCredential networkCredential = new NetworkCredential();
                networkCredential.UserName = mailMessage.From.Address;
                networkCredential.Password = Password;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = networkCredential;
                smtp.Port = Convert.ToInt32(Port);
                smtp.Send(mailMessage);
                status = true;
                return status;
            }
            catch (Exception e)
            {
                return status;
            }
        }
    }
}