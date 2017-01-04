using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Configuration;
using System.Net.Mail;
using System.IO;
using NQN.DB;

namespace NQN.Bus
{
    public class EmailBusiness
    {
        public string emailServer = String.Empty;
      
        private EmailAttachment _attachment = null;
        public EmailAttachment AttachedDocument
        {
            get
            {
                return _attachment;
            }
            set
            {
                _attachment = value;
            }
        }
        

     
        public EmailBusiness()
        {
           
        }
        public void AddAttachment( MemoryStream Stream, string Title, string ContentType)
        {
            _attachment = new EmailAttachment();
            _attachment.AttachmentContent = Stream;
            _attachment.AttachmentTitle = Title;
            _attachment.AttachmentMime = ContentType;
                
        }
        public void Notify(string emailaddress, string loginid, string link, string msg, string Program)
        {
            string ServerPrefix = IsInternal(emailaddress) ? "Internal" : "External";
            string from = System.Configuration.ConfigurationManager.AppSettings.Get(ServerPrefix + "MailFrom");
            if (msg == String.Empty)
            {
                msg =String.Format( @"<p>Your new account on {0} has been created.
                Please set a password to begin using the login:</p><ul>", Program);
 
            }
            string server = HttpContext.Current.Request.Url.DnsSafeHost;
            int Port = HttpContext.Current.Request.Url.Port;
            if (Port == 80)
                server = "http://" + server;
            else if (Port == 443)
                server = "https://" + server;
            else
                server = "http://" + server + ":" + HttpContext.Current.Request.Url.Port.ToString();

            msg += String.Format(@"<li>URL:   <a href='{0}'>{0}</a></li>", server);
            msg += String.Format(@"<li>   User Name: {0}</li>",loginid) ;
            msg += String.Format(@"<li> <a href='{0}'>  Click this link to set your password.</a></li></ul>", link);
            SendMail(from, emailaddress, String.Format("{0} Notification", Program), msg, true);
            
        }


        public void SendMail(string MailTo, string Subject, string msg)
        {
            string ServerPrefix = IsInternal(MailTo) ? "Internal" : "External";
            string from = System.Configuration.ConfigurationManager.AppSettings.Get(ServerPrefix + "MailFrom");
            SendMail(from, MailTo, Subject, msg);
        }
        public void SendMail(string MailTo, string Subject, string msg, bool IsHtml)
        {
            string ServerPrefix = IsInternal(MailTo) ? "Internal" : "External";
            string from = System.Configuration.ConfigurationManager.AppSettings.Get(ServerPrefix + "MailFrom");
            SendMail(from, MailTo, Subject, msg, IsHtml);
        }
        public void SendMail(string From, string MailTo, string Subject, string msg)
        {
            SendMail(From, MailTo, Subject, msg, false);
        }
        public void SendMail(string From, string MailTo, string Subject, string msg, bool IsHtml)
         {
             if (String.IsNullOrEmpty(MailTo)) return;
             string ServerPrefix = IsInternal(MailTo) ? "Internal" : "External";
            if (From == null || From == String.Empty)
                From = ConfigurationManager.AppSettings.Get(ServerPrefix + "MailUser");
           
            if (!IsInternal(From))
                throw new Exception("Attempt to send email from another domain");
            try
            {                    
                emailServer = System.Configuration.ConfigurationManager.AppSettings.Get(ServerPrefix + "MailServer");
            }
            catch { }
           
          
            if (emailServer == String.Empty || emailServer == null) return;
            if (emailServer == "Mandrill")
            {
                MandrillBusiness mb = new MandrillBusiness(ServerPrefix);
                mb.send(From, MailTo, Subject, msg,IsHtml, _attachment);
            }
            else
            {

                MailMessage Message = new MailMessage(From, MailTo, Subject, msg);
                Message.IsBodyHtml = IsHtml;
                Message.From = new MailAddress(From);
                Message.ReplyToList.Add(new MailAddress(From));
                if (_attachment != null)
                {
                    System.Net.Mail.Attachment Doc = new System.Net.Mail.Attachment(_attachment.AttachmentContent, _attachment.AttachmentTitle, _attachment.AttachmentMime);
                    Message.Attachments.Add(Doc);
                }
                string smtpuser = ConfigurationManager.AppSettings.Get(ServerPrefix + "MailUser");
                string smtppw = ConfigurationManager.AppSettings.Get(ServerPrefix + "MailPW");
                string smtpport = ConfigurationManager.AppSettings.Get(ServerPrefix + "MailPort");
                SmtpClient SmtpMail = new SmtpClient(emailServer);
                if (smtpport != "25") SmtpMail.EnableSsl = true;

                if (smtpport != String.Empty) SmtpMail.Port = Convert.ToInt32(smtpport);
                if (smtpuser != String.Empty)
                {
                    SmtpMail.UseDefaultCredentials = false;
                    SmtpMail.Credentials = new System.Net.NetworkCredential(smtpuser, smtppw);
                }
 
                if (smtpport != String.Empty) SmtpMail.Port = Convert.ToInt32(smtpport);
                if (smtpuser != String.Empty)
                {
                    SmtpMail.UseDefaultCredentials = false;
                    SmtpMail.Credentials = new System.Net.NetworkCredential(smtpuser, smtppw);
                }
                //HttpContext.Current.Response.Write(msg);
                SmtpMail.Send(Message);
            }
        }
         private bool IsInternal(string mailto) 
        {
            string InternalMailDomain = ConfigurationManager.AppSettings["InternalMailDomain"].ToString();
            return mailto.EndsWith(InternalMailDomain);
        }
    }
    public class EmailAttachment
    {
          
        public MemoryStream AttachmentContent 
        {
            get; set;
        }
        public string AttachmentMime
        {
            get; set;
        }
        public string AttachmentTitle
        {
            get; set;
        }
    }
}
