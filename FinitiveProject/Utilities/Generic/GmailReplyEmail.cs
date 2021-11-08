using S22.Imap;
using SeleniumAutomation.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;





namespace OSVSupport.Utilities.Generic
{
    class GmailReplyEmail
    {
        private const string imapHost = "imap.gmail.com"; // e.g. imap.gmail.com
        private static string imapUser;
        private static string imapPassword;



        private const string smtpHost = "smtp.gmail.com"; // e.g. smtp.gmail.com
        private static string smtpUser;
        private static string smtpPassword;
        AutomationUtilities _autoUtils = new AutomationUtilities();
        //private const string senderName = "OSVTest";




        public GmailReplyEmail(string username, string password)
        {
            imapUser = username;
            imapPassword = password;
            smtpUser = username;
            smtpPassword = password;
        }
        private static IEnumerable<System.Net.Mail.MailMessage> GetMessages()
        {
            using (ImapClient client = new ImapClient(imapHost, 993, true))
            {
                Console.WriteLine("Connected to " + imapHost + '.');



                // Login
                client.Login(imapUser, imapPassword, AuthMethod.Auto);
                Console.WriteLine("Authenticated.");



                // Get a collection of all unseen messages in the INBOX folder
                client.DefaultMailbox = "INBOX";
                IEnumerable<uint> uids = client.Search(SearchCondition.Unseen());



                if (uids.Count() == 0)
                    return null;



                return client.GetMessages(uids);
            }
        }



        private static MailMessage CreateReply(MailMessage source, string replyingText)
        {
            MailMessage reply = new MailMessage(new MailAddress(imapUser, "Sender"), source.From);



            // Get message id and add 'In-Reply-To' header
            string id = source.Headers["Message-ID"];
            reply.Headers.Add("In-Reply-To", id);



            // Try to get 'References' header from the source and add it to the reply
            string references = source.Headers["References"];



            if (!string.IsNullOrEmpty(references))
                references += ' ';



            reply.Headers.Add("References", references + id);



            // Add subject
            if (!source.Subject.StartsWith("Re:", StringComparison.OrdinalIgnoreCase))
                reply.Subject = "Re: ";



            reply.Subject += source.Subject;



            // Add body
            StringBuilder body = new StringBuilder();
            if (source.IsBodyHtml)
            {
                body.Append("<p>" + replyingText + "</p>");
                body.Append("<br>");



                body.Append("<div>");
                if (source.Date().HasValue)
                    body.AppendFormat("On {0}, ", source.Date().Value.ToString(CultureInfo.InvariantCulture));



                if (!string.IsNullOrEmpty(source.From.DisplayName))
                    body.Append(source.From.DisplayName + ' ');



                body.AppendFormat("<<a href=\"mailto:{0}\">{0}</a>> wrote:<br/>", source.From.Address);



                if (!string.IsNullOrEmpty(source.Body))
                {
                    body.Append("<blockqoute style=\"margin: 0 0 0 5px;border-left:2px blue solid;padding-left:5px\">");
                    body.Append(source.Body);
                    body.Append("</blockquote>");
                }



                body.Append("</div>");
            }
            else
            {
                body.Append(replyingText);
                body.AppendLine();



                if (source.Date().HasValue)
                    body.AppendFormat("On {0}, ", source.Date().Value.ToString(CultureInfo.InvariantCulture));



                body.Append(source.From);
                body.AppendLine(" wrote:");



                if (!string.IsNullOrEmpty(source.Body))
                {
                    body.AppendLine();
                    body.Append("> " + source.Body.Replace("\r\n", "\r\n> "));
                }
            }



            reply.Body = body.ToString();
            reply.IsBodyHtml = source.IsBodyHtml;



            return reply;
        }



        private static void SendReplies(IEnumerable<MailMessage> replies, string HasAttachment)
        {
            using (SmtpClient client = new SmtpClient(smtpHost, 587))
            {
                // Set SMTP client properties
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(smtpUser, smtpPassword);



                // Send
                bool retry = true;
                foreach (MailMessage msg in replies)
                {
                    try
                    {
                        if (!(HasAttachment == null))
                        {
                            Console.WriteLine("Adding Attachment to the mail");
                            Attachment attachment = new Attachment(HasAttachment);
                            msg.Attachments.Add(attachment);
                        }
                        client.Send(msg);
                        retry = true;
                    }
                    catch (Exception ex)
                    {
                        if (!retry)
                        {
                            Console.WriteLine("Failed to send email reply to " + msg.To.ToString() + '.');
                            Console.WriteLine("Exception: " + ex.Message);
                            return;
                        }



                        retry = false;
                    }
                    finally
                    {
                        msg.Dispose();
                    }
                }



                Console.WriteLine("All email replies successfully sent.");
            }
        }



        public static void ReplyEmailToMailInInbox(string replyingText, string hasAttachment = null)
        {
            // Download unread messages from the server
            IEnumerable<MailMessage> messages = GetMessages();
            if (messages != null)
            {
                Console.WriteLine(messages.Count().ToString() + " new email message(s).");

                // Create message replies
                messages.First();
                List<MailMessage> replies = new List<MailMessage>();
                replies.Add(CreateReply(messages.Last(), replyingText));
                messages.First().Dispose();


                //    foreach (MailMessage msg in messages)
                //    {
                //        Console.WriteLine("In Sending Reply");
                //        replies.Add(CreateReply(msg));
                //        msg.Dispose();
                //    }



                // Send replies
                SendReplies(replies, hasAttachment);
            }
            else
            {
                Console.WriteLine("No new email messages.");
            }



        }



    }
}