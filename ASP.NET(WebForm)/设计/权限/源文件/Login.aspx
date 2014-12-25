<%@ Page language="c#" Codebehind="Login.aspx.cs" AutoEventWireup="false" Inherits="UserCheck.Login" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Login</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:TextBox id="TBAcco" style="Z-INDEX: 101; LEFT: 312px; POSITION: absolute; TOP: 96px" runat="server"></asp:TextBox>
			<asp:TextBox id="TBPass" style="Z-INDEX: 102; LEFT: 312px; POSITION: absolute; TOP: 136px" runat="server"></asp:TextBox>
			<asp:Button id="BtnLogin" style="Z-INDEX: 103; LEFT: 496px; POSITION: absolute; TOP: 136px"
				runat="server" Text="µ«¬º"></asp:Button>
			<asp:Label id="Label1" style="Z-INDEX: 104; LEFT: 232px; POSITION: absolute; TOP: 96px" runat="server">”√ªß’ ∫≈:</asp:Label>
			<asp:Label id="Label2" style="Z-INDEX: 105; LEFT: 240px; POSITION: absolute; TOP: 144px" runat="server">ø⁄¡Ó:</asp:Label>
			<asp:Label id="LabelMessage" style="Z-INDEX: 106; LEFT: 240px; POSITION: absolute; TOP: 200px"
				runat="server"></asp:Label>
		</form>
	</body>
</HTML>
