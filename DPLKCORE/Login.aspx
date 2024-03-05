<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DPLKCORE.Login1" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
    <link href="bootstrap4/css/bootstrap.min.css" rel="stylesheet"/> 
    <link href="additional-file/css/global.css" rel="stylesheet"/> 
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <script src="bootstrap4/js/bootstrap.min.js"></script>
</head>

<body>
    <div class="container-fluid bg">
        <div class="row">
            <div class="col-md-4 col-sm-12"></div> 
            <div class="col-md-4 col-sm-12">   
                <form class="container-form text-black font-weight-bold" runat="server"> 
                    <div style="text-align: center;">
                        <img src="../additional-file/img/tugu.png" alt="Form Login Image" style="max-width: 30%; height: auto;">
                    </div>
                    <div class="form-group">
                        <td colspan="3" align="center"><asp:Label ID="lblMessage" runat="server"></asp:Label> <br /><br /></td>
                    </div>
                    
                    <div class="form-group">
                        <td style="font-weight: bold; font-size: medium; color: black;">Username</td> 
                        <td><asp:TextBox ID="txtUsername" runat="server" class="form-control" value='usertestone@pertalife.com'></asp:TextBox></td>
                        <td><asp:RequiredFieldValidator ID="valUsername" runat="server" ControlToValidate="txtUsername" ForeColor="red">*</asp:RequiredFieldValidator></td>
                    </div>
                    <div class="form-group">
                        <td style="font-weight: bold; font-size: medium; color: black;">Password</td> 
                        <td><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" class="form-control" value='testing123'></asp:TextBox></td>
                        <td><asp:RequiredFieldValidator ID="valPassword" runat="server" ControlToValidate="txtPassword" ForeColor="red">*</asp:RequiredFieldValidator></td>
                    </div>
                    <div class="form-group">
                        <td style="color: black;">Captcha*</td> 
                        <td><asp:TextBox ID="txtCaptcha" runat="server" width="200px" CssClass="form-control"></asp:TextBox></td>
                    </div>
                    <div class="form-group">
                        <td>
                            <cc1:CaptchaControl ID="cptCaptcha" runat="server"  
                                CaptchaBackgroundNoise="Low" CaptchaLength="5"  
                                CaptchaHeight="60" CaptchaWidth="200"  
                                CaptchaLineNoise="None" CaptchaMinTimeout="5"  
                                CaptchaMaxTimeout="240" FontColor="#529E00" />  
                            <br />  
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Required" ControlToValidate="txtCaptcha" ForeColor="black"></asp:RequiredFieldValidator> <!-- Change text color to black -->
                            <tr><td>&nbsp;&nbsp;</td></tr>
                        </td>
                    </div>
                    <asp:ImageButton ID="imgbtnLogin" class="btn btn-warning btn-block mt-4 font-weight-bold" runat="server" Text="Login" tooltip="Login"/>
                </form>
            </div>
            <div class="col-md-4 col-sm-12"></div>
        </div>
    </div>
    <script src="bootstrap4/js/bootstrap.min.js"></script>
</body>
</html>

<style>
    body {
        margin: 0;
        padding: 0;
    }

    .bg {
        background-image: url('../additional-file/img/80.jpg');
        background-repeat: no-repeat;
        background-size: cover;
        background-position: center;
        height: 100vh;
        margin: 0;
    }

    .container-form {
    border: 1px solid #A52A2A;
    margin-top: 10vh;  
    padding: 20px;     
    border-radius: 10px;
    box-shadow: 0 0 30px -9px rgba(255, 255, 255, 1);  
    background-color: rgba(255, 255, 255, 0.8);
}

</style>


