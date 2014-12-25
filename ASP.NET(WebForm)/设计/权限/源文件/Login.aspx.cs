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
	/// Login 的摘要说明。
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
			// 在此处放置用户代码以初始化页面
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
				LabelMessage.Text="帐号或密码不正确.";
			

		}
	}
}
