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
	/// Home 的摘要说明。
	/// </summary>
	public class Home : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label WelcomeMessage;
		protected System.Web.UI.WebControls.Button BtnGoods;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!Page.IsPostBack)
			{
				DataSet ds=(DataSet)Session["UserInfo"];
				WelcomeMessage.Text="欢迎你:"+ds.Tables[0].Rows[0]["YourName"].ToString();

			}
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
