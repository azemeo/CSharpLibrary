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
	/// Goods 的摘要说明。
	/// </summary>
	public class Goods : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button Btnselectgoods;
		protected System.Web.UI.WebControls.Button Btnupdategoods;
		protected System.Web.UI.WebControls.Button Btndeletegoods;
		protected System.Web.UI.WebControls.Button Btnselectgoodsprice;
		protected System.Web.UI.WebControls.Button Btnupdategoodsprice;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!Page.IsPostBack)
			{
				DataSet dsTemp=(DataSet)Session["UserInfo"];
				string RoleID=dsTemp.Tables[0].Rows[0]["RoleID"].ToString();

				DataAccess da=new DataAccess("server=172.16.1.26;uid=sa;pwd=;database=Test");
				string strSql="Select * From troleInfo Where InnerId="+RoleID;
				DataSet ds=da.GetSelect(strSql);
				if(ds.Tables[0].Rows.Count>0)	
				{
					string RoleValue=ds.Tables[0].Rows[0]["RoleValue"].ToString();
					Btnselectgoods.Enabled=(RoleValue.Substring(0,1)=="1")?true:false;
					Btnupdategoods.Enabled=(RoleValue.Substring(1,1)=="1")?true:false;
					Btndeletegoods.Enabled=(RoleValue.Substring(2,1)=="1")?true:false;
					Btnselectgoodsprice.Enabled=(RoleValue.Substring(3,1)=="1")?true:false;
					Btnupdategoodsprice.Enabled=(RoleValue.Substring(4,1)=="1")?true:false;
				}


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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
