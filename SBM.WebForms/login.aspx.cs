using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SBM.WebForms
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string stae = null;
            string ssn = null;
            if (!IsPostBack) 
            {
                if (Request.QueryString["stae"] != null && Request.QueryString["stae"] != null)
                {
                    ssn = Request.QueryString["ssn"].ToString();
                    stae = Request.QueryString["stae"].ToString();
            if (stae == "fal") 
            {
                lblReport.Text = "You have successfully signed out!";
                notifier.Style.Add("display", "block");
                lblReport.Visible = true;
                Page.ClientScript.RegisterStartupScript(GetType(), "Key1", "hidealert(25000);", true);
            }
                    if(ssn=="exp")
                    {
                        lblReport.Text = "Your session was expired!";
                        notifier.Style.Add("display", "block");
                        lblReport.Visible = true;
                        Page.ClientScript.RegisterStartupScript(GetType(), "Key1", "hidealert(25000);", true);
                    }
                }

            }
            txtumane.Focus();
            
        }

        protected void btnSignin_Click(object sender, EventArgs e)
        {
            if (txtumane.Text.Trim() == "" || txtpwd.Text.Trim() == "")
            {
                notifier.Style.Add("display", "block");
                alert.Attributes.Add("class", "loginalertorange");
                lblReport.Visible = true;
                //txtpwd.Text = "";
                txtumane.Focus();
                lblReport.Text = "Please enter user name and/or password";
                Page.ClientScript.RegisterStartupScript(GetType(), "Key1", "hidealert(10000);", true);
            }
            if (txtumane.Text.ToLower().Trim() == "admin" && txtpwd.Text.Trim() == "admin")
            {
                Session["user"] = txtumane.Text;
                Response.Redirect("index.aspx?done=validuser");
            }
            else if (txtumane.Text.Trim() != "" && txtpwd.Text.Trim() != "")
            {
                notifier.Style.Add("display", "block");
                alert.Attributes.Add("class", "loginalertred");
                lblReport.Visible = true;
                lblReport.Text = "Invalid user name and/or password";
                txtpwd.Text = "";
                txtumane.Focus();
                Page.ClientScript.RegisterStartupScript(GetType(), "Key1", "hidealert(10000);", true);
            
            }

        }
    }
}