using System;
using System.Collections.Generic;
using System.Configuration;
using Mandrill;
using System.Text;
using System.IO;

namespace NQN.Bus
{
    public class MandrillBusiness
    { 
        private string ServerPrefix = "External";
        public MandrillBusiness()
        {
        }
        public MandrillBusiness(string Range)
        {
            ServerPrefix = Range;
        }
        public void send(string from, string mailto, string subject, string msg, bool IsHtml, EmailAttachment att )
        {

            string ApiKey = ConfigurationManager.AppSettings.Get(ServerPrefix + "MailKey");
            if (ApiKey == null)
                return;
            Mandrill.MandrillApi ma = new MandrillApi(ApiKey);
            EmailMessage em = new EmailMessage();
            em.AddHeader("Reply-to", from);
            if (att != null)
            {

                Mandrill.attachment efa = new Mandrill.attachment();
                efa.type = att.AttachmentMime;
                efa.name = att.AttachmentTitle;
                efa.content = Convert.ToBase64String( att.AttachmentContent.ToArray());
                em.attachments = new Mandrill.attachment[1] { efa };
            }
            em.from_email = from;
            em.subject = subject;
            if (IsHtml)
            {
                em.html = msg;
                em.AddHeader("X-MC-Autotext", "true");
            }
            else
                em.text = msg;
          
            EmailAddress addr = new EmailAddress(mailto);
            em.to = new List<EmailAddress>() { addr };
            try
            {
                ma.SendMessage(em);
            }
            catch
            {
                // No use trying to email an error at this point
            }
        }   
    }
}
