using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using SBM.MVC5.Models;

namespace SBM.MVC5.Models
{
    public class SendMail
    {
        int i = 0, inValidCount = 0; string INvalidrecp = "";
        public string SenDEmail(string AppKey, Mail model) 
        {
            string result = "";
            int bcccount = 0, cccount = 0, tocount = 0, errorNo = 0;
            if (AppKey == "SBMkEY")
            {
                try
                {
                    MailMessage mailmsg = new MailMessage();
                    if (model.From!= null)
                    {
                        errorNo = 1;
                        model.From = validate_eMaillist(model.From);
                        string[] dispName = model.From.Split('@');
                        if (dispName.Length > 2) { return result = "From filed takes only one email id."; }
                        mailmsg.From = new MailAddress(validate_eMaillist(model.From), dispName[0].ToUpper() + " SBM");
                        errorNo = 0;
                    }
                    else { return result = "FROM filed should not be NULL."; }
                    if (model.To != null)
                    {
                        errorNo = 2;
                        mailmsg.To.Add(validate_eMaillist(model.To));
                        errorNo = 0;
                        tocount = mailmsg.To.Count();
                    }
                    if (model.BCC != null)
                    {
                        errorNo = 3;
                        mailmsg.Bcc.Add(validate_eMaillist(model.BCC));
                        errorNo = 0;
                        bcccount = mailmsg.Bcc.Count();
                    }
                    if (model.CC != null)
                    {
                        errorNo = 4;
                        mailmsg.CC.Add(validate_eMaillist(model.CC));
                        errorNo = 0;
                        cccount = mailmsg.CC.Count();
                    }

                    if (model.Body != null)
                    {
                        mailmsg.Body = model.Body;
                    }
                    else { mailmsg.Body = ""; }
                    if (model.Subject != null)
                    {
                        mailmsg.Subject = model.Subject;
                    }
                    if (model.files != null)
                    {
                        Attachment attach = new Attachment(model.files);
                        mailmsg.Attachments.Add(attach);
                    }
                    mailmsg.BodyEncoding = System.Text.Encoding.UTF8;
                    mailmsg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                    mailmsg.ReplyToList.Add(model.From);
                    mailmsg.IsBodyHtml = true;
                    SmtpClient client = new SmtpClient();
                    if (System.Web.Configuration.WebConfigurationManager.AppSettings["SMTP"].ToString().ToLower() == "gmail")
                    {
                        ///*Local host settings*/
                        /*Add the Creddentials- use your own email id and password*/
                        client.Credentials = new System.Net.NetworkCredential("email@gmail.com", "xxxxxxxx");
                        client.Port = 587; // Gmail works on this port<o:p />
                        client.Host = "smtp.gmail.com";
                        client.EnableSsl = true; //Gmail works on Server Secured Layer
                    }
                    else
                    {
                        /*server settings*/
                        client.Host = "mx1.hostinger.in";
                        client.Port = 2525;
                        //client.UseDefaultCredentials = true;
                        client.Credentials = new System.Net.NetworkCredential("name@domain.com", "xxxxxxxx");
                        client.Timeout = 360000;
                        client.ServicePoint.ConnectionLeaseTimeout = 360000;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.EnableSsl = false;
                        client.ServicePoint.MaxIdleTime = 1; //without this the connection is idle too long and not terminated, times out at the server and gives sequencing errors
                    }
                    client.Send(mailmsg);

                    if (inValidCount != 0)
                    {
                        MailMessage inValmailmsg = new MailMessage();
                        inValmailmsg.From = new MailAddress("name@domain.com", "Support");
                        inValmailmsg.To.Add(model.From);
                        inValmailmsg.Subject = "Suresh Bulk Mailer: " + inValidCount + " Invalid Eamil IDs, Please verify";
                        inValmailmsg.Body = "Dear Suresh Bulk Mailer User,<br>" + "<br>" + "Please find the below " + inValidCount + " INVALID email ids, and verify. <br> <br>" + INvalidrecp + "<br> <br> Thanks & Regards <br> GDS Bulk Mailer Support <br> Eamil: support@gdatasol.com";
                        inValmailmsg.IsBodyHtml = true;
                        inValmailmsg.BodyEncoding = System.Text.Encoding.UTF8;
                        //mailmsg.Attachments.Dispose();
                        //mailmsg.ReplyToList.Clear();
                        inValmailmsg.ReplyToList.Add("name@domain.com");
                        client.Send(inValmailmsg);
                        inValmailmsg.Dispose();
                    }
                    mailmsg.Dispose();

                    int totalCount = (bcccount + cccount + tocount + inValidCount);
                    result = "sucess," + (totalCount - inValidCount).ToString() + "/" + totalCount + " Mails has been sent.";
                }
                catch (ArgumentException)
                {
                    result = "Undefined sender and/or recipient. ";
                    if (errorNo == 1) { result += "\"FROM\" field contains INVALID email."; }
                    if (errorNo == 2) { result += "\"TO\" field contains INVALID email/s ."; }
                    if (errorNo == 3) { result += "\"BCC\" field contains INVALID email/s."; }
                    if (errorNo == 4) { result += "\"CC\" field contains INVALID email/s."; }
                }
                catch (FormatException)
                {
                    result = "some filed/s contains invalid email id/id's.";
                    if (errorNo == 1) { result = "\"FROM\" field contains INVALID email/s ."; }
                    if (errorNo == 2) { result = "\"TO\" field contains INVALID email/s ."; }
                    if (errorNo == 3) { result = "\"BCC\" field contains INVALID email/s."; }
                    if (errorNo == 4) { result = "\"CC\" field contains INVALID email/s."; }
                }
                catch (InvalidOperationException)
                {
                    result = "Undefined SMTP server.";
                }
                catch (SmtpFailedRecipientsException ex)
                {
                    result = "Failed to deliver message to " + ex.FailedRecipient.Length + " contacts,";
                    result += " Error: " + ex;
                    for (int i = 0; i < ex.InnerExceptions.Length; i++)
                    {
                        SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                        if (status == SmtpStatusCode.MailboxBusy ||
                            status == SmtpStatusCode.MailboxUnavailable)
                        {
                            result = "Delivery failed - Tray again after few minutes";
                        }
                        else
                        {
                            result += ex.InnerExceptions[i].FailedRecipient + " , ";
                        }
                    }
                }
                catch (SmtpFailedRecipientException rec)
                {
                    result = "The mail server says that there is no mailbox for recipient/";
                    //Additional info for error
                    result += rec.FailedRecipient + rec.StackTrace;
                }
                catch (SmtpException ex)
                {
                    // Invalid hostnames result in a WebException InnerException that
                    // provides a more descriptive error, so get the base exception
                    Exception inner = ex.GetBaseException();
                    result = "Could not send message: " + inner.Message;
                    //additonal info for error
                    //result += inner.HelpLink.ToString() + inner.StackTrace.ToString() + inner.Source.ToString() + inner.InnerException.ToString();
                }
                catch (System.IO.FileNotFoundException fileexp)
                {
                    result = "Attachment file path was incorrect, Error: " + fileexp.Message;
                }
                catch (Exception excep)
                {
                    result = excep.Message;
                }
                finally
                {

                }
                return result;
            }
            else { return "Authenticatoin failed, Invalid 'AppKey'."; }
        }

        private string validate_eMaillist(string emailList)
        {
            string strData = emailList;
            emailList = "";
            string[] separator = new string[] { ",", ";", "/", ":", " ", "\r\n", "*", "#", "|", "!", "'", "$", "%", "^", "&", "(", ")", "+", "=", "~", "?", "<", ">", "`" };
            string[] strSplitArr = strData.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            bool iSvalid = true;
            foreach (string arrStr in strSplitArr)
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@" + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\." + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|" + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$");
                System.Text.RegularExpressions.Match match = regex.Match(arrStr);
                if (match.Success)
                {
                    try
                    {
                        MailAddress m = new MailAddress(arrStr);
                        iSvalid = true;
                    }
                    catch (FormatException)
                    { iSvalid = false; }
                }
                else
                { iSvalid = false; }

                if (iSvalid)
                {
                    { emailList += arrStr + ","; }
                }
                else { inValidCount++; INvalidrecp += arrStr + "<br>"; }
                i++;
            }
            emailList = emailList.TrimEnd(',', ';');
            return emailList;
            //throw new NotImplementedException();
        }


    }
}