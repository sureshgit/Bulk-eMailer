using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net.Mail;
using System.Drawing;

namespace SBM.WebForms
{
    public partial class index : System.Web.UI.Page
    {

        string bccIds = "", txtserverPath, state = "", INvalidrecp = "";
        int inValidCount=0, i = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(UPdatebtn, this.GetType(), "init", "tinymce.init({selector: \"textarea\"});", true);
            //ScriptManager.RegisterOnSubmitStatement(this, this.GetType(), "", "tinyMCE.triggerSave();");
            if (Session["user"] == null)
            {
                Response.Redirect("login.aspx?&ssn=exp&stae=tr");
            }
            if (!IsPostBack)
            {
                lblReport.Text = "Welcome to S Bulk Mailer Please send your feedback to \"support@suresh.com\"";
                string done = null;
                if (Request.QueryString["done"] != null)
                {
                    done = Request.QueryString["done"].ToString();                    
                }
                if (done == "validuser")
                {
                    lblReport.Text = "Welcome to S Bulk Mailer Please send your feedback to \"support@suresh.com\"";
                }
                
                    notifier.Style.Add("display", "block");
                    lblReport.Visible = true;
                    Page.ClientScript.RegisterStartupScript(GetType(), "Key1", "hidealert(100000);", true);
            }
            txtFrom.Focus();
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string serverpath = null, filePath = "";       
            try
            {
                MailMessage mailmsg = new MailMessage();
                if (Session["bccIds"]==null && txtBcc.Text != "")
                {
                    bccIds = txtBcc.Text;
                }
                else
                {
                    bccIds = Session["bccIds"].ToString();
                    Session["bccIds"] = null;
                }
                if (attachment.HasFile)
                {
                    HttpPostedFile attFile = attachment.PostedFile;
                    filePath = System.IO.Path.GetFileName(attachment.PostedFile.FileName);
                    string fileExtn = System.IO.Path.GetExtension(filePath);
                    int attachFileLength = attFile.ContentLength;
                    if (attachFileLength > 0)
                    {
                        serverpath = filePath;
                        /* Save the file on the server */
                        attachment.PostedFile.SaveAs(Server.MapPath("Attachments/" + filePath));
                        /* Create the email attachment with the uploaded file */
                        Attachment attach = new Attachment(Server.MapPath("Attachments/" + filePath));
                        mailmsg.Attachments.Add(attach);
                        filePath = Server.MapPath("Attachments/" + filePath);
                    }
                }
                string result;
                SBMServiceReference.SendBulkeMailSoapClient GDSBMclient = new SBMServiceReference.SendBulkeMailSoapClient();
                GDSBMclient.Open();
                result = GDSBMclient.GDSSendBulkeMail(txtFrom.Text, txtTo.Text, bccIds, txtCc.Text, txtbody.Text, txtSubject.Text, filePath, "Iwjil");
                string[] rset = result.Split(',');
                if (inValidCount != 0)
                {
                    if(Session["inValidCount"] !=null){
                        inValidCount += int.Parse(Session["inValidCount"].ToString());
                        INvalidrecp += Session["INvalidrecp"].ToString();
                        Session["inValidCount"] = null;
                        Session["INvalidrecp"] = null;
                    }
                }
                GDSBMclient.Close();
                if (rset[0] == "sucess")
                {
                    alert.Attributes.Add("class", "alertgreen");
                    lblReport.Text = rset[1];
                    txtTo.Text = "";
                    txtCc.Text = "";
                    txtBcc.Text = "";
                    txtSubject.Text = "";
                    txtbody.Text = "";

                }
                else {
                    alert.Attributes.Add("class", "alertred");
                    lblReport.Text = result +" Please try again";
                    if (bccIds != "")
                    { txtBcc.Text = bccIds; bccIds = ""; }
                    lblReport.Visible = true;
                    Page.ClientScript.RegisterStartupScript(GetType(), "Key1", "hidealert(60000);", true);
                }                
            }
            catch (Exception excep)
            {
               lblReport.Text = excep.Message;
            }
        }

        protected void lnkbtnSignout_Click(object sender, EventArgs e)
        {
            Session["user"] = null;
            Response.Redirect("login.aspx?&stae=fal&ssn=nxp");
        }

        protected void txtFileuploaded(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            if (state != "false" && Session["user"] != null)
            {
                //string txtfileName = System.IO.Path.GetFileName(e.FileName); ;
                string fileName = System.IO.Path.GetFileName(e.FileName);
                string txtfileExtn = System.IO.Path.GetExtension(fileName);
                if (txtfileExtn == ".txt" && fileName.Length > 0)
                {
                    txtserverPath = "Attachments/" + fileName;
                    AsyncFileUpload1.PostedFile.SaveAs(Server.MapPath(txtserverPath));
                    bccIds = System.IO.File.ReadAllText(Server.MapPath(txtserverPath));
                    //calling method for splitting and validating email ids one by one
                    Session["bccIds"] = validate_eMaillist(bccIds);
                    if (inValidCount != 0)
                    {
                        Session["inValidCount"] = inValidCount;
                        Session["INvalidrecp"] = INvalidrecp;
                    }
                    System.IO.File.Delete(Server.MapPath(txtserverPath));                        
                }
            }
        }

        private string validate_eMaillist(string emailList) 
        {
            string strData = emailList;
            emailList = "";
            //splitting and validating email ids one by one
            string[] separator = new string[] { ",", ";", "/", ":", " ", "\r\n","*","#","|","!","'","$","%","^","&","(",")","+","=","~","?","<",">","`" };
            string[] strSplitArr = strData.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            bool iSvalid = true;
            foreach (string arrStr in strSplitArr)
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
+ @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
+ @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
+ @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$");
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
                    //if ((i == strSplitArr.Length - 1))
                    //{
                    //    emailList += arrStr;
                    //}
                    //else 
                    { emailList += arrStr + ","; }
                }
                else { inValidCount++; INvalidrecp += arrStr + "<br>"; }
                i++;
            }
            emailList = emailList.TrimEnd(',', ';');            
            return emailList;
        }


        protected void lnkRemovetxt_Click(object sender, EventArgs e)
        {       
            bccIds = "";
            inValidCount = 0;
            txtBcc.Text = "";
            state = "false";
            //notifier.Style.Add("display", "none");

            if (Session["bccIds"] != null) {
                        Session["bccIds"] = null;
            }

            if (Session["inValidCount"] != null || Session["INvalidrecp"]!=null)
            {
                Session["inValidCount"] = null;
                Session["INvalidrecp"] = null;
            }

            //info.jmasters.www1.DecryptImplService cl1 = new info.jmasters.www1.DecryptImplService();
            //test += "-:-" + cl1.decryptText("email@gmail.com", "lqFEZMEO", "IwjilFvLAVF1/WX8B9bmqqAiWEMo8A==");
            
            //lblReport.Visible = true;
            //lblReport.Text = test;
            //Page.ClientScript.RegisterStartupScript(GetType(), "Key1", "hidealert(60000);", true);

        }
        protected void OnAsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
        {
            ScriptManager1.AsyncPostBackErrorMessage = e.Exception.Message;
        }      
    }
}