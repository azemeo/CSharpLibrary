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

namespace Upload
{
	/// <summary>
	/// 上传测试页面
	/// </summary>
	public class _Default : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btnUpload;
		protected System.Web.UI.HtmlControls.HtmlInputFile uploadFile;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 使用UploadID来唯一标识是当前的上传
			//
			if (Request.QueryString["UploadID"] == null)
				Response.Redirect("Default.aspx?UploadID=" + Guid.NewGuid().ToString());

			// 当提交的时候，开始加载进度条
			//
			btnUpload.Attributes.Add("onclick", "window.setTimeout('LoadProgressInfo()', 500)");
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
			this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		private void uploadButton_ServerClick(object sender, System.EventArgs e)
		{
		}

		private void btnUpload_Click(object sender, System.EventArgs e)
		{
			// 后端处理方法和普通的上传是一样的
			//
			string filename = this.uploadFile.PostedFile.FileName;
			filename = filename.Substring(filename.LastIndexOf("\\"));
			string FullPath=Server.MapPath("") + filename;
			this.uploadFile.PostedFile.SaveAs(FullPath);
			Response.Write("上传文件：" + filename);
			
			// 上传完后使用脚本通知前端进度条
			//
			Response.Write("<script>parent.pb.UploadComplete();parent.ClearTimer();</script>");
			Response.End();
		}
	}
}
