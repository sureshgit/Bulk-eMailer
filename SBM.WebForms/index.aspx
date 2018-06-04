<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="SBM.WebForms.index" ValidateRequest="false" Async="true" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GDS Bulk Mailer | Compose</title>
    <link href="Styles/composemail.css" rel="Stylesheet" />
    <%--<script type="text/javascript" src="js/nicEdit-latest.js"></script> <script type="text/javascript">
//<![CDATA[
  bkLib.onDomLoaded(function () { nicEditors.allTextAreas() });
  //]]>selector: 'textarea',
  </script>--%>
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <script src="js/tinymce/tinymce.min.js" type="text/javascript"></script>
<script type="text/javascript">
    tinymce.init({ selector: "textarea"});
</script>

    <script type="text/javascript">
        function hidealert(time) {
            //alert(time);
            var tsec = time;
            if (tsec == "[object Object]") {
                tsec = 100000;
             }
            var ide = document.getElementById('notifier');
            setTimeout(function () { ide.style.display = 'none'; }, tsec); return false
        }

        function hidetxtDiv() {

            var divid = document.getElementById('lblremovediv');
            divid.style.display = 'block';
            var divid1 = document.getElementById('txtuperrordiv');
            divid1.style.display = 'none';

        }
        function txtFileerror() {
            var divid = document.getElementById('txtuperrordiv');
            divid.innerHTML = "Invalid file, Try again.";
            divid.style.display = 'block';
            divid.style.color = 'red';
            var divid = document.getElementById('lblremovediv');
            divid.style.display = 'none';
        }

        function txtFileupstart(sender, args) {
            var fileExtension = args.get_fileName().toLowerCase();
            if (fileExtension.indexOf('.txt') > -1) {
                return true;
            }
            else {
                var err = new Error();
                err.name = 'Extension Error';
                err.message = 'Only Text (.txt) files are allowed and File can not be empty. Tray agian';
                txtFileerror();
                alert(err);
                args.set_cancel(true);                            //cancel upload
                args.set_errorMessage("File type must be .txt"); //set error message
                return false;
            }
        }
        

 </script>

</head>
<body>
    <form id="form1" runat="server">
   <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="600000" 
        AsyncPostBackErrorMessage="Error"  
        onasyncpostbackerror="OnAsyncPostBackError"  >
    </asp:ScriptManager>
    <div id="main">
    <div id="main-wrapeer">
    <div class="maincontainer">
    <div class="header">
    <div class="headerleft">
     <a href="" title="Suresh" target="_blank">
         <asp:Image ID="Image1" runat="server" ImageUrl="~/images/logo1.png" />
     </a>
    </div>
    <div class="headerright">

<div class="links">
        <ul>
          <li><a href="" title="Home" target="_blank">&nbsp;Home</a></li>
          <li>|</li>
          <li><a href="" title="Employee Login" target="_blank">&nbsp;cPanel Login</a></li>
          <li>|</li>
          <li><a href="" title="Webmail" target="_blank">&nbsp;Webmail</a></li>
          <li>|</li>
          <li><a href="" title="Contact Us" target="_blank">&nbsp;Contact Us</a></li>
          <li>|</li>
          <li>
              <asp:LinkButton ID="lnkbtnSignout" runat="server" onclick="lnkbtnSignout_Click">Sign Out</asp:LinkButton></li>
        </ul>
      </div>     
    </div>
    <div class="clr"></div>
    </div>
    <div class="containerbody">
<%--        <script type="text/javascript">        Sys.Application.add_load(hidealert); </script>--%>
    
    <div class="composemail">
    <div>
    <div class="composehedaer">
    Compose and Send Bulk Mail
     </div>
<%--         <asp:UpdatePanel ID="uPd2" runat="server" UpdateMode="Conditional" >
         <ContentTemplate>
          <div class="notifier" id="notifier2" runat="server">
    <div class="alert" id="alert2" runat="server" ><asp:Label ID="lblreport2" runat="server"  Visible="false"></asp:Label>
    </div>
    </div>
     <div class="clr"></div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>--%>
    <%--<asp:UpdateProgress ID="Updateprocess" runat="server" AssociatedUpdatePanelID="UPdatebtn" DynamicLayout="true">
    <ProgressTemplate>
     <div class="processbar" id="processbar" runat="server">
Processing... Please wait!<br />
<img alt="Proccessing... Please wait!" src="images/proceessing.gif" />
     </div>
    </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UPdatebtn" runat="server" UpdateMode="Conditional">
    <ContentTemplate>--%>
<%--        <script type="text/javascript">            Sys.Application.add_load(hidealert); </script>--%>
     <div class="notifier" id="notifier" runat="server">
    <div class="alert" id="alert" runat="server" ><asp:Label ID="lblReport" runat="server" Text="Label" Visible="false"></asp:Label>
    <%--<div class="closeicn">
        <img alt="Close" title="Clsoe" src="images/close_alertdiv.png" onclick="hidealert(10)" /></div>--%>
    </div>
     <div class="clr"></div>
    </div>
<%--    </ContentTemplate>
    </asp:UpdatePanel>
--%>    

     <div class="tofield">
    <div class="left">FROM*</div>
    <div class="right"><asp:TextBox ID="txtFrom" runat="server" Width="500px" TextMode="SingleLine"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup ="sendmail"
            ControlToValidate="txtFrom" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true">   
        </asp:RequiredFieldValidator> 
         </div>
    <div class="clr"></div>
    </div>
        
    <div class="ccfield">
    <div class="left">TO*</div>
    <div class="right"><asp:TextBox ID="txtTo" runat="server" Width="500px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup ="sendmail"
            ControlToValidate="txtTo" ErrorMessage="*" SetFocusOnError="true" ForeColor="Red">   
        </asp:RequiredFieldValidator></div>
    <div class="clr"></div>
    </div>
    <div class="ccfield" id="ccfield">
        <div class="left">CC</div>
    <div class="right"><asp:TextBox ID="txtCc" runat="server" Width="500px"></asp:TextBox></div>
    <div class="clr"></div>
    </div>
    <div class="bccfield">
        <div class="left">BCC*</div>
    <div class="bccright"><asp:TextBox ID="txtBcc" runat="server" Width="500px" MaxLength="3000000"></asp:TextBox>
        &nbsp;<%--<asp:FileUpload ID="fubtn" runat="server" Width="50" />--%></div>
    <div class="clr"></div>
     </div>
     <div class="ccfield" id="chooosetxtfile">
        <div class="left">Choose Contacts File</div>
    <div class="chooseContacts"><div class="asynctxtupload"><asp:AsyncFileUpload  ID="AsyncFileUpload1" OnClientUploadComplete="hidetxtDiv" runat="server" OnClientUploadError="txtFileerror" UploadingBackColor="Green" ThrobberID="imgLoading" OnClientUploadStarted="txtFileupstart"  UploaderStyle="Traditional" Width="250px"  onuploadedcomplete="txtFileuploaded" /></div><div class="imgloading"><asp:Image ID="imgLoading" runat="server" ImageUrl="~/images/imgLoading.gif" /></div><div class="lblremove" id="lblremovediv" runat="server">
        <asp:LinkButton ID="lnkRemovetxt" runat="server" onclick="lnkRemovetxt_Click" >Remove</asp:LinkButton></div><div class="" id="txtuperrordiv">
            </div> </div>   
    <div class="clr"></div>
    </div>
     <div class="ccfield">
        <div class="left">SUBJECT*</div>
    <div class="ccright"><asp:TextBox ID="txtSubject" runat="server"  Width="500px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup ="sendmail"
            ControlToValidate="txtSubject" ErrorMessage="*" SetFocusOnError="true" ForeColor="Red">   
        </asp:RequiredFieldValidator><%--<asp:RegularExpressionValidator ID="rglrexpfubtn" runat="server" ForeColor="Red"
                    ErrorMessage="&nbsp;&nbsp;&nbsp;Only txt file allowed!"
                    ControlToValidate="fubtn" Font-Size="Small" Font-Bold="false"
                    ValidationExpression=".*(\.txt)$ (([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))"
                    ValidationGroup ="sendmail" ></asp:RegularExpressionValidator>--%></div>
    <div class="clr"></div>
    </div>
    <div class="ccfield">
        <div class="left">ATTACHMENT</div>
    <div class="ccright">
        <asp:FileUpload ID="attachment" runat="server" Width="505px" /><asp:RegularExpressionValidator ID="rglrexpattch" runat="server" ForeColor="Red"
                    ErrorMessage="&nbsp;&nbsp;&nbsp;Selected file not allowed!"
                    ControlToValidate="attachment" Font-Size="Small" Font-Bold="false"
                    ValidationExpression=".*(\.rtf|\.xlsx|\.docx|\.doc|\.pdf|\.png|\.jpg|\.jpeg|\.txt|\.html|\.htm)$"
                    ValidationGroup ="sendmail" ></asp:RegularExpressionValidator></div>
    <div class="clr"></div>
    </div>
    <div class="messagefield"> 
    <div class="left">MESSAGE*</div>
    <div class="bccright">
       <asp:TextBox ID="txtbody" runat="server" TextMode="MultiLine" Rows="20" Columns="90" ></asp:TextBox>
        <%--<asp:HtmlEditorExtender ID="txtbody_HtmlEditorExtender" runat="server" DisplaySourceTab="true" EnableSanitization="false"
             Enabled="True" TargetControlID="txtbody">
            <Toolbar> 
                <asp:Undo />
                <asp:Redo />
                <asp:Bold />
                <asp:Italic />
                <asp:Underline />
                <asp:StrikeThrough />
                <asp:Subscript />
                <asp:Superscript />
                <asp:JustifyLeft />
                <asp:JustifyCenter />
                <asp:JustifyRight />
                <asp:JustifyFull />
                <asp:InsertOrderedList />
                <asp:InsertUnorderedList />
                <asp:CreateLink />
                <asp:SelectAll />
                <asp:BackgroundColorSelector />
                <asp:ForeColorSelector />
                <asp:FontNameSelector />
                <asp:FontSizeSelector />
                <asp:Indent />
                <asp:Outdent />
                <asp:InsertHorizontalRule />
                <asp:HorizontalSeparator />
            </Toolbar>
            <ProfileBindings>
            <asp:ProfilePropertyBinding ExtenderPropertyName="txtBody" />
            </ProfileBindings>
        </asp:HtmlEditorExtender>--%>
</div>
    <div class="clr"></div>
    </div>

      <div class="ccfield">
        <div class="ccleft"></div>
    <div class="ccright"><asp:Button ID="btnSend" runat="server" Text="Send" onclick="btnSend_Click" CssClass="btnsend" ValidationGroup="sendmail"/>
        &nbsp;
    <div class="clr"></div>
    </div>    <div class="clr"></div>
            <%--</ContentTemplate>
    <Triggers>

    <asp:PostBackTrigger ControlID="txtBody"/>
<asp:AsyncPostBackTrigger ControlID="lnkRemovetxt" EventName="Click"/>
<asp:AsyncPostBackTrigger ControlID="lnkbtnSignout" EventName="Click"/>
    </Triggers>
    </asp:UpdatePanel>--%>
    </div>
    
        </div>

    
    
    <div id="footer" class="footer">
<div class="footer_left">Copyright &#169; 2015 |  <span><a href="#" id="lgltrm" runat="server" title="Terms & Conditions">Terms & Conditions</a> | Developed by <a href="#" id="gds" target="_blank" runat="server" title="Developed by Suresh">Suresh </a></span>
</div>
<div class="social_right">
<div class="fb"><a href="https://www.facebook.com/yalamanchi.suresh" title="Facebok" target="_blank"><img id="imgfb" alt="Facebook" src="~/images/facebook.png" runat="server"/></a></div>
<div class="twt"><a href="https://twitter.com/yalamanchsuresh" title="Twitter" target="_blank"><img id="imgtwt" alt="Twitter" src="~/images/twitter.png" runat="server"/></a></div>
<div class="clr"></div>
</div>
<div class="clr"></div>
</div>
    
    

    </div>
    </div>
    </div>
    </div>
    </form>
</body>
</html>


