using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Backend.Helpers
{
    public static class MailHelper
    {
        private static string _messageBody;
        static MailHelper()
        {
            var body = File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Data/SubscriptionMail.html"));
            _messageBody = body;
        }
        public static void SendWelcomeEmail(string toEmailAddress, string unsubscribeLink)
        {
            try
            {
                var fromAddress = ConfigurationManager.AppSettings[Constants.AmazonSESFrom];
                var subject = "Welcome to #charity";

                var smtpUsername = ConfigurationManager.AppSettings[Constants.AmazonSESUsername];
                var smtpPassword = ConfigurationManager.AppSettings[Constants.AmazonSESPassword];
                var host = ConfigurationManager.AppSettings[Constants.AmazonSESAddress];
                int port = 587;

                using (SmtpClient client = new SmtpClient(host, port))
                {
                    client.Credentials = new System.Net.NetworkCredential(smtpUsername, smtpPassword);
                    client.EnableSsl = true;

                    var mail = new MailMessage(fromAddress, toEmailAddress, subject, _messageBody.Replace("$unsubscribelink$", unsubscribeLink));
                    mail.IsBodyHtml = true;
                    client.Send(mail);
                }
            }
            catch
            {

            }
        }
    }
}