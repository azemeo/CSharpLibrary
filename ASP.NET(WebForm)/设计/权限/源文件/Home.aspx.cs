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

namespace UserCheck
{
	/// <summary>
	/// Home ��ժҪ˵����
	/// </summary>
	public class Home : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label WelcomeMessage;
		protected System.Web.UI.WebControls.Button BtnGoods;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!Page.IsPostBack)
			{
				DataSet ds=(DataSet)Session["UserInfo"];
				WelcomeMessage.Text="��ӭ��:"+ds.Tables[0].Rows[0]["YourName"].ToString();

			}
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
			this.BtnGoods.Click += new System.EventHandler(this.BtnGoods_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void BtnGoods_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("Goods.aspx");
		}
	}
}
