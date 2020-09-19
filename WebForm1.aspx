<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="R2MD.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <input type="image" name="ctl00$CartBtn" id="CartBtn" src="img/date.png" onclick="javascript:WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions(&quot;ctl00$CartBtn&quot;, &quot;&quot;, false, &quot;&quot;, &quot;https://www.paypal.com/cgi-bin/webscr&quot;, false, false))">
        <input type="hidden" name="business" value="wkq95131@yahoo.com">
        <input type="hidden" name="cmd" value="_cart">
        <input type="hidden" name="display" value="1">
        <input type="hidden" name="bn" value="PP-ShopCartBF">
        <input type="hidden" name="lc" value="US">
        <input type="hidden" name="currency_code" value="USD">
        <input type="hidden" name="no_shipping" value="2">
        <input type="hidden" name="amount" value="299.00">    
        <input type="hidden" name="item_number" value="500-6004">                
        <input type="hidden" name="item_name" value="Katanin, P80">
        <input type="hidden" name="add" value="1">
        <input type="image" name="ctl00$SidebarContent$OrderBtn" id="SidebarContent_OrderBtn" src="img/date.png" onclick='javascript:WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions("ctl00$SidebarContent$OrderBtn", "", false, "", "https://www.paypal.com/cgi-bin/webscr", false, false))'>
    
    </div>
    </form>
</body>
</html>
