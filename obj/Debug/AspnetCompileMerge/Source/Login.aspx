<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="R2MD.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        table#ChirrpManagerUserLogin {
            padding: 200px 0px;
            width: 300px;
            height: 200px;
            background-color: white;
            position: absolute;
            top: 0;
            bottom: 0;
            left: 0;
            right: 0;
            margin: auto;
            border-radius: 10px;
        }
        table {
            margin: auto;
        }
    </style>
</head>
<body style="margin:0">
    <form id="form1" runat="server">
    <div style="background-color: rgba(0, 0, 0, 0.42); width: 100%; height: 100vh;">
        <asp:Login ID="ChirrpManagerUserLogin" runat="server" OnAuthenticate="ValidateUser"></asp:Login>
    </div>
    </form>
</body>
</html>
