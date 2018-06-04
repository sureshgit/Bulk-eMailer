<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="SBM.WebForms.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>S Bulk Mailer | Login</title>
    <link href="Styles/composemail.css" rel="Stylesheet" />
        <script type="text/javascript">
            function hidealert(time) {
                var tsec = time;
                var ide = document.getElementById('notifier');
                setTimeout(function () { ide.style.display = 'none'; }, tsec); return false
            }
 </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div class="loginframe">
    <div class="loginheader">S Bulk Mailer</div>
    <div class="loginnotifier" id="notifier" runat="server">
    <div class="loginalert" id="alert" runat="server" ><asp:Label ID="lblReport" runat="server" Text="Label" Visible="false"></asp:Label></div>
     <div class="clr"></div>
    </div> 
    <div class="fields">
    <div class="loginleft" >User Name</div>
    <div class="loginright">
        <asp:TextBox ID="txtumane" runat="server"></asp:TextBox></div>
    <div class="clr"></div>
    </div>
    <div class="fields">
        <div class="loginleft" >Password</div>
    <div class="loginright"><asp:TextBox ID="txtpwd" runat="server" TextMode="Password"></asp:TextBox></div>
    <div class="clr"></div>
    </div>
    <div class="clr"></div> 
    <div class="btns">
    <div class="loginbtn">
        <asp:Button ID="btnSignin" runat="server" Text="Sign In" CssClass="btnsend" 
            onclick="btnSignin_Click" /></div>
    </div>  
    <div class="poweredby">
    <div>Developed by <a href="" target="_blank" title="Suresh">Suresh</a></div>
    </div> 
    </div>
    </div>
    </form>
</body>
</html>
