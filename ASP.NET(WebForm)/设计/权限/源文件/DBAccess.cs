using System;
using System.Data;
using System.Data.SqlClient;
namespace ZeroWire
{
	public class DataAccess
	{
		
		private string strConn="";
		public DataAccess(string strConn)
		{
			this.strConn=strConn;
		}
		
		public DataTable GetTable(string strTableName)
		{
			DataSet ds=new DataSet();
			SqlConnection  myConn=new SqlConnection(strConn);
			try
			{
				myConn.Open();
				SqlDataAdapter da= new SqlDataAdapter("SELECT * FROM "+strTableName,myConn);
				SqlCommandBuilder thisBuilder=new SqlCommandBuilder(da);
				da.Fill(ds,strTableName);
				return ds.Tables[strTableName];
			}
			catch(Exception ex)
			{
				string s=ex.Message;
			}
			finally
			{
				myConn.Close();
			}
			return null;
		}
		public DataSet GetSelect(string strSql)
		{
			DataSet ds=new DataSet();
			SqlConnection  myConn=new SqlConnection(strConn);
			try
			{
				myConn.Open();
				SqlDataAdapter da= new SqlDataAdapter(strSql,myConn);
				SqlCommandBuilder thisBuilder=new SqlCommandBuilder(da);
				da.Fill(ds,"tempTable");
				return ds;
			}
			catch(Exception ex)
			{
				string s=ex.Message;
			}
			finally
			{
				myConn.Close();
			}
			return null;
		}
		public bool ExcuteCmd(string strSql)
		{
			bool IsSucess=false;
			DataSet ds=new DataSet();
			SqlConnection  myConn=new SqlConnection(strConn);
			try
			{
				SqlCommand myCommand = new SqlCommand(strSql, myConn);
				myCommand.Connection.Open();
				myCommand.ExecuteNonQuery();
				IsSucess=true;
			}
			catch(Exception ex)
			{
				string s=ex.Message;
			}
			finally
			{
				myConn.Close();
			}
			return IsSucess;
		}
	}
}