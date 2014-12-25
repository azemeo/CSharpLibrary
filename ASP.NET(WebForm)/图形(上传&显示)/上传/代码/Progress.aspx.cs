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
	/// �ϴ����ȷ�ӳҳ��
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
				// ������ڽ������ݣ����ýű���֪ͨǰ�˽�����
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

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
