using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Net.Mail;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using log4net;
using SeleniumAutomation.Utilities;
using SeleniumAutomation.Selenium;

namespace OneAtmosphere.Utilities.Generic
{
    class GmailUtility : UA
    {
        AutomationUtilities _autoUtils = new AutomationUtilities();
        private string credentialsPath;
        private string userTokenPath;
        private string userID = "me";
        private string query = "UNREAD";
        public List<string> emailData = new List<string>();
        public List<string> emailDataTimeStamp = new List<string>();
        public List<string> emailSubject = new List<string>();
        public List<string> emailFrom = new List<string>();

        ILog log = LogManager.GetLogger("GmailUtility Page");

        public object SendMail { get; private set; }

        public GmailUtility(string userID, string query, string credentialsPath, string userTokenPath)
        {
            this.userID = userID;
            this.query = query;
            this.credentialsPath = _autoUtils.GetProjectLocation() + credentialsPath;
            this.userTokenPath = _autoUtils.GetProjectLocation() + userTokenPath;
        }


        public string getTheBodyDataFromEmail(string mailText, int mailCount)
        {
            emailData = GetListMessages(userID, query, credentialsPath, userTokenPath);
            int i;
            for (i = 0; i < emailData.Count; i++)
            {
                if (emailData[i].Contains(mailText))
                {
                    break;
                }
            }
            log.Info($"Email body contains given text { mailText} in body  {emailData[i]}");
            return emailData[i];
        }
        public bool validateEmailWithBodyContent(string text)
        {
            bool flag = false;
            emailData = GetListMessages(userID, query, credentialsPath, userTokenPath);
            for (int i = 0; i < emailData.Count; i++)
            {
                if (emailData[i].Contains(text))
                {
                    log.Info("email Data Count = " + emailData.Count);
                    log.Info("emailData = " + emailData[i]);
                    flag = true;
                    break;
                }
            }
            if (flag == true)
            {
                log.Info("Email body contains given text - " + text);
            }
            else
            {
                log.Error("Email body does not contains given text - " + text);
            }
            return flag;
        }
        public bool verifyEmailWithReceivedTime(string text, string receivedTime)
        {
            bool flag = false;
            emailData = GetListMessages(userID, query, credentialsPath, userTokenPath);
            for (int i = 0; i < emailData.Count; i++)
            {
                if (emailData[i].Contains(text) && emailDataTimeStamp.Contains(receivedTime))
                {
                    flag = true;
                    break;
                }
            }
            if (flag == true)
            {
                log.Info("Email received at :" + receivedTime + " and email body contains given text : " + text);
            }
            else
            {
                log.Error("Email not received at :" + receivedTime + " or email body not contains given text : " + text);
            }
            return flag;
        }

        public bool verifyEmailWithSubject(string subject)
        {
            bool flag = false;
            string expectedEmailSubject = null;
            //waitForTime(TimeOuts.SHORTWAIT);
            GetListMessages(userID, query, credentialsPath, userTokenPath);
            for (int i = 0; i < emailSubject.Count; i++)
            {
                expectedEmailSubject = emailSubject[i];
                if (emailSubject[i].Contains(subject))
                {
                    flag = true;
                    break;
                }
            }
            if (flag == true)
            {
                log.Info("Email List contains specified subject " + "Actual Subject: " + subject + " Expected Subject: " + expectedEmailSubject);
            }
            else
            {
                log.Error("Email List does not contains specified subject " + "Actual Subject: " + subject + " Expected Subject: " + expectedEmailSubject);
            }
            return flag;
        }

        public static byte[] DecodeURLEncodedBase64EncodedString(string base64ForUrlInput)
        {
            int padChars = (base64ForUrlInput.Length % 4) == 0 ? 0 : (4 - (base64ForUrlInput.Length % 4));
            StringBuilder result = new StringBuilder(base64ForUrlInput, base64ForUrlInput.Length + padChars);
            result.Append(String.Empty.PadRight(padChars, '='));
            result.Replace('-', '+');
            result.Replace('_', '/');
            return Convert.FromBase64String(result.ToString());
        }

        public List<string> GetListMessages(string userID, string query, string credentialsPath, string userTokenPath)
        {
            string[] Scopes = { GmailService.Scope.GmailReadonly };
            string ApplicationName = "Gmail API .NET Quickstart";
            //string decodedString;
            UserCredential credential;

            log.Info("Conneting to Gmail API");

            using (var stream =
                new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(userTokenPath, true)).Result;
            }

            // Create Gmail API service.
            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            UsersResource.MessagesResource.ListRequest request1 = service.Users.Messages.List(userID);
            request1.LabelIds = query;
            request1.IncludeSpamTrash = false;
            var emailListResponse = request1.Execute();

            if (emailListResponse != null && emailListResponse.Messages != null)
            {
                //loop through each email and get what fields you want...   
                foreach (var email in emailListResponse.Messages)
                {
                    UsersResource.MessagesResource.GetRequest emailInfoRequest = service.Users.Messages.Get(userID, email.Id);
                    var emailInfoResponse = emailInfoRequest.Execute();
                    if (emailInfoResponse != null)
                    {
                        String from = "";
                        String date = "";
                        String subject = "";

                        //loop through the headers to get from,date,subject, body  
                        foreach (var mParts in emailInfoResponse.Payload.Headers)
                        {
                            if (mParts.Name == "Date")
                            {
                                emailDataTimeStamp.Add(mParts.Value);
                                date = mParts.Value;
                            }
                            else if (mParts.Name == "From")
                            {
                                emailFrom.Add(mParts.Value);
                                from = mParts.Value;
                            }
                            else if (mParts.Name == "Subject")
                            {
                                emailSubject.Add(mParts.Value);
                                subject = mParts.Value;
                            }
                            if (date != "" && from != "")
                            {
                                //foreach (MessagePart p in emailInfoResponse.Payload.Parts)
                                //{
                                //    if (p.MimeType == "text/html")
                                //    {
                                //        byte[] data = DecodeURLEncodedBase64EncodedString(p.Body.Data);
                                //        decodedString = Encoding.UTF8.GetString(data);
                                //        from = "";
                                //        date = "";
                                //        subject = "";
                                //        emailData.Add(decodedString);
                                //    }

                                //}

                                //foreach (MessagePart p in emailInfoResponse.Payload.Parts)
                                //{
                                string body = null;
                                if (emailInfoResponse.Payload.Parts == null && emailInfoResponse.Payload.Body != null)
                                {
                                    body = DecodeBase64String(emailInfoResponse.Payload.Body.Data);
                                }
                                else
                                {
                                    body = GetNestedBodyParts(emailInfoResponse.Payload.Parts, "");
                                }
                                emailData.Add(body);
                                //}

                            }
                        }
                    }
                }
            }
            else
            {
                log.Info("emailList is empty");
            }

            return emailData;
        }

        static string DecodeBase64String(string sInput)
        {
            string sBase46codedBody = sInput.Replace("-", "+").Replace("_", "/").Replace("=", String.Empty);  //get rid of URL encoding, and pull any current padding off.
            string sPaddedBase46codedBody = sBase46codedBody.PadRight(sBase46codedBody.Length + (4 - sBase46codedBody.Length % 4) % 4, '=');  //re-pad the string so it is correct length.
            byte[] data = Convert.FromBase64String(sPaddedBase46codedBody);
            return Encoding.UTF8.GetString(data);
        }

        static String GetNestedBodyParts(IList<MessagePart> part, string curr)
        {
            string str = curr;
            if (part == null)
            {
                return str;
            }
            else
            {
                foreach (var parts in part)
                {
                    if (parts.Parts == null)
                    {
                        if (parts.Body != null && parts.Body.Data != null)
                        {
                            var ts = DecodeBase64String(parts.Body.Data);
                            str += ts;
                        }
                    }
                    else
                    {
                        return GetNestedBodyParts(parts.Parts, str);
                    }
                }

                return str;
            }
        }



        public void SendEmailUsingAPI(string subject, string ToAddress, string Body, string attachmentFile = null, string CCAddress = null)
        {
            var msg = new AE.Net.Mail.MailMessage
            {
                Subject = subject,
                Body = Body,
                From = new MailAddress(userID)

            };
            msg.To.Add(new MailAddress(ToAddress));
            if (!(CCAddress == null))
            {
                msg.Cc.Add(new MailAddress(CCAddress));
            }
            if (!(attachmentFile == null))
            {
                string filePath = _autoUtils.GetProjectLocation() + attachmentFile;
                var bytes = File.ReadAllBytes(filePath);
                AE.Net.Mail.Attachment attachment = new AE.Net.Mail.Attachment(bytes, "text/html", Path.GetFileName(attachmentFile), true);
                msg.Attachments.Add(attachment);
            }
            msg.ReplyTo.Add(msg.From); // Bounces without this!!
            var msgStr = new StringWriter();
            msg.Save(msgStr);

            // Context is a separate bit of code that provides OAuth context;
            // your construction of GmailService will be different from mine.
            //var gmail = new GmailService(Context.GoogleOAuthInitializer);

            string[] Scopes = { GmailService.Scope.GmailSend };
            string ApplicationName = "Gmail API .NET Quickstart";
            UserCredential credential;

            log.Info("Conneting to Gmail API");
            using (var stream =
                new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "admin",
                    CancellationToken.None,
                    new FileDataStore(userTokenPath, true)).Result;
            }

            // Create Gmail API service.
            var gmail = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });



            var result = gmail.Users.Messages.Send(new Message
            {
                Raw = Base64UrlEncode(msgStr.ToString())
            }, userID).Execute();
            Console.WriteLine("Message ID {0} sent.", result.Id);
        }


        private static string Base64UrlEncode(string input)
        {
            var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            // Special "url-safe" base64 encode.
            return Convert.ToBase64String(inputBytes)
              .Replace('+', '-')
              .Replace('/', '_')
              .Replace("=", "");
        }


        //public void LookForAnEmailWithSubjectAndReply(string lookingSubject, string ToAddress, string Body)
        //{
        //    bool flag = false;
        //    string expectedEmailSubject = null;
        //    waitForTime(TimeOuts.MEDIUMWAIT);
        //    GetListMessages(userID, query, credentialsPath, userTokenPath);
        //    for (int i = 0; i < emailSubject.Count; i++)
        //    {
        //        expectedEmailSubject = emailSubject[i];
        //        if (emailSubject[i].Contains(lookingSubject))
        //        {
        //            flag = true;
        //            SendEmailUsingAPI("Re: " + lookingSubject, ToAddress, Body + emailData[i]);
        //            break;
        //        }
        //    }
        //    if (flag == true)
        //    {
        //        log.Info("Email List contains specified subject " + "Actual Subject: " + lookingSubject + " Expected Subject: " + expectedEmailSubject);
        //    }
        //    else
        //    {
        //        log.Error("Email List does not contains specified subject " + "Actual Subject: " + lookingSubject + " Expected Subject: " + expectedEmailSubject);
        //    }

        //}

        public void LookForAnEmailWithSubjectAndReply(string lookingSubject, string ToAddress, string Body)
        {
            //for (int i = 0; i < emailSubject.Count; i++)
            //{
            //    if (emailSubject[i].Contains(lookingSubject))
            //    {
            //        Console.WriteLine("Looking Subject Found");
            //        break;
            //    }
            //}

            var msg = new AE.Net.Mail.MailMessage
            {
                Subject = lookingSubject,
                Body = Body,
                From = new MailAddress(userID)

            };
            msg.To.Add(new MailAddress("ramesh.uppuluri@zenq.com"));
            msg.ReplyTo.Add(msg.From); // Bounces without this!!
            msg.ReplyTo.Add(new MailAddress(ToAddress));
            var msgStr = new StringWriter();
            msg.Save(msgStr);
            string[] Scopes = { GmailService.Scope.GmailSend };
            string ApplicationName = "Gmail API .NET Quickstart";
            UserCredential credential;

            log.Info("Conneting to Gmail API");
            using (var stream =
                new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "admin",
                    CancellationToken.None,
                    new FileDataStore(userTokenPath, true)).Result;
            }

            // Create Gmail API service.
            var gmail = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });



            var result = gmail.Users.Messages.Send(new Message
            {
                Raw = Base64UrlEncode(msgStr.ToString())
            }, userID).Execute();
            Console.WriteLine("Message ID {0} sent.", result.Id);


        }
        public string getTheDataFromTheEmail(string subject, int countofEmails)
        {
            bool flag = false;
            string DataReadFromEmail = null;
            GetListMessages(userID, query, credentialsPath, userTokenPath);
            for (int i = 0; i < countofEmails; i++)
            {
                if (emailSubject[i].Contains(subject))
                {
                    //log.Info("email Data Count = " + emailData.Count);
                    //log.Info("emailData = " + emailData[i]);
                    DataReadFromEmail = emailData[i];
                    flag = true;
                    break;
                }
            }
            if (flag == true)
            {
                log.Info("Email body of subject - " + subject + " is " + DataReadFromEmail);
            }
            else
            {
                log.Error("Does not contain an email with Subject");
            }
            return DataReadFromEmail;
        }
    }


}