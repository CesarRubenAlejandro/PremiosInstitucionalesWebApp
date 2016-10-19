<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PremiosInstitucionales.WebForms.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        
    <div style="text-align:center">
        <h3>Login</h3>
        <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label><br />
        <asp:TextBox ID="TextBox1" runat="server" placeholder="E-mail"></asp:TextBox><br /><br />
        <asp:TextBox ID="TextBox2" runat="server" TextMode="Password" placeholder="Contraseña"></asp:TextBox><br /><br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Inicia Sesion" /><br /><br />
        <asp:HyperLink ID="HyperLink1" NavigateUrl="~/WebForms/Registro.aspx" runat="server">¿No tienes cuenta? Registrate</asp:HyperLink>
        <br />
        
    </div>
        
    </form>
</body>
</html>
