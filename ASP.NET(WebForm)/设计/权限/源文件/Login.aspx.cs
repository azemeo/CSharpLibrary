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
using ZeroWire;
namespace UserCheck
{
	/// <summary>
	/// Login ��ժҪ˵����
	/// </summary>
	public class Login : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox TBAcco;
		protected System.Web.UI.WebControls.TextBox TBPass;
		protected System.Web.UI.WebControls.Button BtnLogin;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label LabelMessage;
		protected System.Web.UI.WebControls.Label Label2;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
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
			this.BtnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void BtnLogin_Click(object sender, System.EventArgs e)
		{
			DataAccess da=new DataAccess("server=172.16.1.26;uid=sa;pwd=;database=Test");
			string strSql="Select * From TUserInfo Where Acco='"+this.TBAcco.Text+"' AND Pass='"+this.TBPass.Text+"'";
			DataSet ds=da.GetSelect(strSql);
			if(ds.Tables[0].Rows.Count>0)	
			{
				Session["UserInfo"]=ds;
				Response.Redirect("Home.aspx");
			}
			else
				LabelMessage.Text="�ʺŻ����벻��ȷ.";
			

		}
	}
}
