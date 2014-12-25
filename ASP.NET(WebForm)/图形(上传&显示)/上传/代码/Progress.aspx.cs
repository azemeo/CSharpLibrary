using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Openlab.Web.Upload;
namespace Upload
{
	/// <summary>
	/// 上传进度反映页面
	/// </summary>
	public class Progress : System.Web.UI.Page
	{

		private void Page_Load(object sender, System.EventArgs e)
		{
			string uploadId = Request.QueryString["UploadID"];
			string scriptText = "";
			string scriptUploading = "pb.setSize({0}, {1});";
			string scriptClearTimer = "ClearTimer();";
			string scriptUploadComplete = "pb.UploadComplete();" + scriptClearTimer;
			string scriptUploadError = "pb.UploadError();";

			string length = "";
			string read = "";

			Openlab.Web.Upload.Progress progress = HttpUploadModule.GetProgress(uploadId, Application);
			if (progress != null)
			{
				// 如果正在接收数据，利用脚本来通知前端进度条
				//
				if (progress.State == UploadState.ReceivingData)
				{
					length = (progress.ContentLength / 1024).ToString();
					read = (progress.BytesRead / 1024).ToString();
					scriptText = string.Format(scriptUploading, length, read);
				}
				else if(progress.State == UploadState.Complete)
				{
					scriptText = scriptUploadComplete;
				}
				else
				{
					scriptText = scriptUploadError;
				}
			}
			else
			{
				//scriptText = scriptUploadError;
			}
			Response.Clear();
			Response.Write(scriptText);
			Response.End();
		}

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
