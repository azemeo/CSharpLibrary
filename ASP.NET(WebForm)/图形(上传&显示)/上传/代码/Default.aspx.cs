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
	/// �ϴ�����ҳ��
	/// </summary>
	public class _Default : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btnUpload;
		protected System.Web.UI.HtmlControls.HtmlInputFile uploadFile;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// ʹ��UploadID��Ψһ��ʶ�ǵ�ǰ���ϴ�
			//
			if (Request.QueryString["UploadID"] == null)
				Response.Redirect("Default.aspx?UploadID=" + Guid.NewGuid().ToString());

			// ���ύ��ʱ�򣬿�ʼ���ؽ�����
			//
			btnUpload.Attributes.Add("onclick", "window.setTimeout('LoadProgressInfo()', 500)");
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
			this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		private void uploadButton_ServerClick(object sender, System.EventArgs e)
		{
		}

		private void btnUpload_Click(object sender, System.EventArgs e)
		{
			// ��˴���������ͨ���ϴ���һ����
			//
			string filename = this.uploadFile.PostedFile.FileName;
			filename = filename.Substring(filename.LastIndexOf("\\"));
			string FullPath=Server.MapPath("") + filename;
			this.uploadFile.PostedFile.SaveAs(FullPath);
			Response.Write("�ϴ��ļ���" + filename);
			
			// �ϴ����ʹ�ýű�֪ͨǰ�˽�����
			//
			Response.Write("<script>parent.pb.UploadComplete();parent.ClearTimer();</script>");
			Response.End();
		}
	}
}
