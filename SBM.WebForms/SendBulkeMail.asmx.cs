using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Net.Mail;

namespace SBM.WebForms
{
    /// <summary>
    /// Summary description for SendBulkeMail
    /// </summary>
    //[WebService(Namespace = "SBM.WebForms")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SendBulkeMail : System.Web.Services.WebService
    {
        int i = 0, inValidCount = 0; string INvalidrecp = "";

        [WebMethod]
        public string GDSSendBulkeMail(string From, string To, string BCC, string CC, string Body, string Subject, string filePath, string AppKey)
        {
            string status=null;
            return status = SsendeMail(From,To,BCC,CC,Body,Subject,filePath,AppKey);
        }

        private string SsendeMail(string From, string To, string BCC, string CC, string Body, string Subject, string filePath, string AppKey)
        {
            string result = "";
            int bcccount = 0, cccount = 0, tocount = 0, errorNo = 0;
            if (From == null || To == null || BCC == null || CC == null || Body == null || Subject == null || filePath == null || AppKey == null)
            {
                return result = "Null reference exception, No input should be a null value.";
            }
            if (AppKey == "Iwjil")
            {
                try
                {
                    MailMessage mailmsg = new MailMessage();
                    if (From != "")
                    {
                        errorNo = 1;
                        From = validate_eMaillist(From);
                        string[] dispName = From.Split('@');
                        if (dispName.Length > 2) { return result = "From filed takes only one email id."; }
                        mailmsg.From = new MailAddress(validate_eMaillist(From), dispName[0].ToUpper() + " GDS");
                        errorNo = 0;
                    }
                    else { return result = "FROM filed should not be empty."; }
                    if (To != "")
                    {
                        errorNo = 2;
                        mailmsg.To.Add(validate_eMaillist(To));
                        errorNo = 0;
                        tocount = mailmsg.To.Count();
                    }
                    if (BCC != "")
                    {
                        errorNo = 3;
                        mailmsg.Bcc.Add(validate_eMaillist(BCC));
                        errorNo = 0;
                        bcccount = mailmsg.Bcc.Count();
                    }
                    if (CC != "")
                    {
                        errorNo = 4;
                        mailmsg.CC.Add(validate_eMaillist(CC));
                        errorNo = 0;
                        cccount = mailmsg.CC.Count();
                    }

                    if (Body != "")
                    {
                        mailmsg.Body = Body;
                    }
                    else { mailmsg.Body = ""; }                    
                    if (Subject != "")
                    {
                        mailmsg.Subject = Subject;
                    }
                    if (filePath != "")
                    {
                        Attachment attach = new Attachment(filePath);
                        mailmsg.Attachments.Add(attach);
                    }
                    mailmsg.BodyEncoding = System.Text.Encoding.UTF8;
                    mailmsg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                    mailmsg.ReplyToList.Add(From);
                    mailmsg.IsBodyHtml = true;

                    ///*Local host settings*/
                    SmtpClient client = new SmtpClient();
                    /*Add the Creddentials- use your own email id and password*/
                    client.Credentials = new System.Net.NetworkCredential("email@gmail.com", "xxxxxxx");
                    client.Port = 587; // Gmail works on this port<o:p />
                    client.Host = "smtp.gmail.com";
                    client.EnableSsl = true; //Gmail works on Server Secured Layer

                    /*server settings*/
                    //SmtpClient client = new SmtpClient("relay-hosting.secureserver.net", 25);
                    //client.Credentials = new System.Net.NetworkCredential("support@gdatasol.com", "Ourstrength#2013");
                    //client.Timeout = 360000;
                    //client.ServicePoint.ConnectionLeaseTimeout = 360000;
                    //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    //client.EnableSsl = false;
                    //client.ServicePoint.MaxIdleTime = 1; //without this the connection is idle too long and not terminated, times out at the server and gives sequencing errors

                    client.Send(mailmsg);
                    
                    if (inValidCount != 0)
                    {
                        MailMessage inValmailmsg = new MailMessage();
                        inValmailmsg.From = new MailAddress("support@domain.com", "Support");
                        inValmailmsg.To.Add(From);
                        inValmailmsg.Subject = "GDS Bulk Mailer: " + inValidCount + " Invalid Eamil IDs, Please verify";
                        inValmailmsg.Body = "Dear GDS Bulk Mailer User,<br>" + "<br>" + "Please find the below " + inValidCount + " INVALID email ids, and verify. <br> <br>" + INvalidrecp + "<br> <br> Thanks & Regards <br> GDS Bulk Mailer Support <br> Eamil: support@gdatasol.com";
                        inValmailmsg.IsBodyHtml = true;
                        inValmailmsg.BodyEncoding = System.Text.Encoding.UTF8;
                        //mailmsg.Attachments.Dispose();
                        //mailmsg.ReplyToList.Clear();
                        inValmailmsg.ReplyToList.Add("support@gdatasol.com");
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
                    result = "Attachment file path was incorrect, Error: "+fileexp.Message;
                }
                catch (Exception excep)
                {
                    result = excep.Message;
                }
                finally
                {

                }
            }
            else { result = "Authenticatoin failed, Invalid 'AppKey'."; }


            return result;
        }



        private string validate_eMaillist(string emailList) 
        {
            string strData = emailList;
            emailList = "";            
            string[] separator = new string[] { ",", ";", "/", ":", " ", "\r\n","*","#","|","!","'","$","%","^","&","(",")","+","=","~","?","<",">","`" };
            string[] strSplitArr = strData.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            bool iSvalid = true;
            foreach (string arrStr in strSplitArr)
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"+ @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."+ @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"+ @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$");
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
        }
    }
}
