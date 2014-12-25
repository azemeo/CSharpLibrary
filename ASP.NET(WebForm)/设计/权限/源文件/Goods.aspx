<%@ Page language="c#" Codebehind="Goods.aspx.cs" AutoEventWireup="false" Inherits="UserCheck.Goods" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Goods</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:Button id="Btnselectgoods" style="Z-INDEX: 101; LEFT: 120px; POSITION: absolute; TOP: 136px"
				runat="server" Text="商品信息查询"></asp:Button>
			<asp:Button id="Btnupdategoods" style="Z-INDEX: 102; LEFT: 120px; POSITION: absolute; TOP: 200px"
				runat="server" Text="商品信息更新"></asp:Button>
			<asp:Button id="Btndeletegoods" style="Z-INDEX: 103; LEFT: 120px; POSITION: absolute; TOP: 256px"
				runat="server" Text="商品信息删除"></asp:Button>
			<asp:Button id="Btnselectgoodsprice" style="Z-INDEX: 104; LEFT: 120px; POSITION: absolute; TOP: 320px"
				runat="server" Text="商品定价信息查询"></asp:Button>
			<asp:Button id="Btnupdategoodsprice" style="Z-INDEX: 105; LEFT: 120px; POSITION: absolute; TOP: 384px"
				runat="server" Text="商品定价信息更新"></asp:Button>
		</form>
	</body>
</HTML>
