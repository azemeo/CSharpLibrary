<%@ Page language="c#" Codebehind="Default.aspx.cs" AutoEventWireup="false" Inherits="Upload._Default" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Simple Upload Sample</title>
		<style>.progressBar {
	WIDTH: 250px; HEIGHT: 18px
}
.progressInfo {
	BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; OVERFLOW: hidden; BORDER-LEFT: #000000 1px solid; WIDTH: 250px; PADDING-TOP: 1px; BORDER-BOTTOM: #000000 1px solid; POSITION: absolute; HEIGHT: 18px; TEXT-ALIGN: center
}
.progress {
	OVERFLOW: hidden; WIDTH: 0%; HEIGHT: 18px; BACKGROUND-COLOR: #5fff3f
}
</style>
		<script src="xmlLib.js"></script>
		<script>
		var r = "传输: {0}K 还未完成";
		var s = "您的文件已经上传完成";
		var t = "上传失败";
		function progressBar()
		{

			this.totalSize = 100;
			this.sizeCompleted = 0;
			this.percentDone = "0%";
			this.setSize = function(totalSize, size)
			{
				var oProgressInfo = document.getElementById("progressInfo");
				var oProgress = document.getElementById("progress");
				if (oProgress == null || oProgressInfo == null)
					return;

				if (totalSize <= 0)
					return;

				this.totalSize = totalSize;
				this.sizeCompleted = size;
				if (size < 0)
					this.sizeCompleted = 0;
				else if (size > this.totalSize)
					this.sizeCompleted = this.totalSize;

				var sizeLeft = 0;
				var progressInfoText = "";
				sizeLeft = this.totalSize - this.sizeCompleted;

				this.percentDone = Math.round(size / this.totalSize * 100) + "%";
				oProgress.style.width = this.percentDone;
				
				if (sizeLeft > 0)
					progressInfoText = r.replace("{0}", sizeLeft);
				else
					progressInfoText = s;

				oProgressInfo.innerHTML = progressInfoText;
			}
			this.UploadError = function()
			{
				var oProgressInfo = document.getElementById("progressInfo");
				var oProgress = document.getElementById("progress");
				if (oProgressInfo != null)
					oProgressInfo.innerHTML = t;
				if (oProgress != null)
					oProgress.style.width = "0";
			}
			this.UploadComplete = function()
			{
				var oProgressInfo = document.getElementById("progressInfo");
				var oProgress = document.getElementById("progress");
				if (oProgressInfo != null)
					oProgressInfo.innerHTML = s;
				if (oProgress != null)
					oProgress.style.width = "100%";
			}
		}
		var iTimerID = null;
		var xmlHttp = XmlHttpPool.pick();
		var url = "progress.aspx?UploadID=<%=Request.QueryString["UploadID"]%>"
		var pb = new progressBar();	
		function LoadProgressInfo()
		{
			try
			{
				xmlHttp.open("GET", url + "&t=" + Math.random(), true);
				xmlHttp.send(null);
				xmlHttp.onreadystatechange = function()
				{
					LoadData(xmlHttp);
				}
			}
			catch(e)
			{
				alert(e)
			}
		}

		function LoadData(xmlhttp)
		{
			if (xmlhttp.readyState == 4)
			{
				iTimerID = window.setTimeout("LoadProgressInfo()", 500); 
				try{
					eval(xmlhttp.responseText);
				}
				catch(e)
				{
					alert(e)
				}
			}
		}
		
		function ClearTimer()
		{
			if (iTimerID != null)
			{
				window.clearTimeout(iTimerID);
				iTimerID = null;
			}
		}
		
		function UploadCancel()
		{
			location.href = location.href;
		}

		</script>
	</HEAD>
	<body>
		<form method="post" target="uploadData" runat="server">
			<P><INPUT id="uploadFile" type="file" runat="server">
			</P>
			<div class="progressBar" id="progressBar">
				<div class="progressInfo" onselectstart="return false;" id="progressInfo"></div>
				<FONT face="宋体"></FONT>
				<div class="progress" id="progress"></div>
			</div>
			<P><asp:button id="btnUpload" runat="server" Text="  上传  "></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;
				<INPUT id="btnUpload" onclick="UploadCancel()" type="button" value="  取消  ">
			</P>
			<iframe name="uploadData" src="about:blank"></iframe></form>
	</body>
</HTML>
