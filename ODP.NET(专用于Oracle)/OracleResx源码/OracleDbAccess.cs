using System;
using System.Data;
using System.Data.Common;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using ZeroWire.Shared.Database;
using ZeroWire.Shared.Localization;
using System.Globalization;

namespace ZeroWire.WS.Common.Database
{
	/// <summary>
	/// Summary description for OracleDbAccess.
	/// </summary>
	public class OracleDbAccess:ZeroWire.Shared.Database.IDatabaseAccess
	{
		private   string  ConnectionString = null;
		public OracleDbAccess()
		{
			//
			// TODO: Add constructor logic here
			//
			getConnectionString();
			getResourcesFileSQL();
		}


	   //获得初始数据值

		#region Component Designer generated code

		//
		/// <summary>
		///获得数据库的连接串
		/// </summary>

		private void getConnectionString() 
		{
			//获得连接字符串(重要*****)
			ConnectionString="User Id=sfa;Password=zwsfa;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=172.16.1.6)(PORT=1521)))(CONNECT_DATA=(SID=zwora)(SERVER=DEDICATED)))";
		}

		/// <summary>
		/// 获得Resources file 
		/// </summary>
		private void getResourcesFileSQL()
		{
			//得到资源文件(重要*****)当目录的相对路径
			ResourceBundle.SetSQLResourceManager(new System.Resources.ResourceManager("TestOracle.Resources.OrclSql",System.Reflection.Assembly.GetExecutingAssembly()));
		}

		#endregion	

        //初始方法
		#region Database getConnection
		//
		/// <summary>
		/// 得到IDbConnection对象
		/// </summary>
		/// <returns></returns>
		//
		public IDbConnection getConnection()
		{			
			//定义连接
			IDbConnection conn = null;
			try
			{
				//获得Oracle连接
				conn = new OracleConnection(ConnectionString);					
			}
			catch(Exception ex)
			{	
				//	ZeroWireLog.WriteLog(ex);
				//	throws new ZeroWireExcepton (ex, "OracleDbAccess.getConnection() connString="+connString);	
				throw (ex);
			}			
			return conn;
		}
		
		//
		/// <summary>
		/// 从外部获得连接串
		/// </summary>
		/// <param name="ConnectionString">连接字符</param>
		/// <returns></returns>
		public  IDbConnection getConnection(string ConnectionString)
		{
			
			IDbConnection conn = null;
			try 
			{
				conn = new OracleConnection(ConnectionString);				
			}
			catch(Exception ex)
			{	
				//	ZeroWireLog.WriteLog(ex);
				//	throws new ZeroWireExcepton (ex);
				throw (ex);
			}			
			return conn; 
		}



		#endregion

		#region IDbTransaction getTransaction(IDbConnection conn)
		/// <summary>
		/// 的到conn的事务
		/// </summary>
		/// <param name="conn">连接对象</param>
		/// <returns></returns>
		public IDbTransaction getTransaction(IDbConnection conn)
		{	
			//返回一个事务
			return conn.BeginTransaction();
		}

		#endregion

		#region Database CreateCommand
		/// <summary>
		/// 得到OracleCommand对象
		/// </summary>
		/// <returns></returns>
		private static OracleCommand CreateCommand()
		{
			OracleCommand cmd = null;
			try 
			{
				//获得一个oracle命令对象
				cmd = new OracleCommand();	
				
			}
			catch(Exception ex)
			{	
				//	ZeroWireLog.WriteLog(ex);
				//	throws new ZeroWireExcepton (ex);			
				throw (ex);
			}
			return cmd;
			
		}

		/// <summary>
		/// 得到OracleCommand对象
		/// </summary>
		/// <param name="CommandText">SQL字符串</param>
		/// <param name="cnn">连接对象</param>
		/// <returns></returns>

		private static OracleCommand CreateCommand(string CommandText, OracleConnection cnn)
		{
			OracleCommand cmd = null;
			try 
			{
				cmd = new OracleCommand(CommandText,cnn);	
				
			}
			catch(Exception ex)
			{	
				//	ZeroWireLog.WriteLog(ex);
				//	throws new ZeroWireExcepton (ex);			
				throw (ex);
			}
			return cmd;
			
		}

		#endregion

		#region Database CreateAdapter
		/// <summary>
		/// 得到OracleDataAdapter对象
		/// </summary>
		/// <param name="cmd">传入OracleCommand对象</param>
		/// <returns></returns>
		public static OracleDataAdapter CreateAdapter(OracleCommand cmd)
		{
			OracleDataAdapter da;
			try
			{
				//获得一个OracleDataAdapter
				da = new OracleDataAdapter(cmd);					
			}
			catch(Exception ex)
			{	
				//	ZeroWireLog.WriteLog(ex);
				//	throws new ZeroWireExcepton (ex);				
				throw (ex);			  
			}	
			return da;  
		}

		#endregion

		#region public string getCommandText(string sqlKey)

		/// <summary>
		/// 获得SQL语句，用于测试用
		/// </summary>
		/// <param name="sqlKey">为Resources file 中的ＳＱＬ语句的关键字</param>
		/// <returns></returns>

		public string getCommandText(string sqlKey)
		{
			return getCommandText(sqlKey,(string[])null);

		}

		/// <summary>
		/// 获得SQL语句，用于测试用
		/// </summary>
		/// <param name="sqlKey">为Resources file 中的ＳＱＬ语句的关键字</param>
		/// <param name="parames">传入的sql语句的参数</param>
		/// <returns></returns>
		public string getCommandText(string sqlKey,string[] parames)
		{
			string commandText = "";
			if (sqlKey == "")
			{
				throw new ArgumentNullException("sqlKey");
			}

			try
			{
				commandText = ResourceBundle.GetSQL(sqlKey);
				if (parames != null)
				{
					commandText = paraSQL(commandText, parames);	
				}

			}
			catch (Exception ex)
			{
				throw (ex);

			}
			return commandText;


		}
		#endregion 

		//exexcute no query SQL

		#region public int executeNoQueryCmdString(string commandText)

		public int executeNoQueryCmdString(string commandText)
		{
			int result = 0;		

			if (commandText == "")
			{
				throw new ArgumentNullException("commandText") ;
			}

			try 
			{
				result = NoQueryDisposalCmdString(commandText);
			}
			catch(Exception ex)
			{	
				//				ZeroWireLog.WriteLog(ex);
				//				throws new ZeroWireExcepton (ex);
				throw (ex);				
			}
			return result;

		}
		#endregion

		#region int executeNonQuery(string sqlKey)

		/// <summary>
		/// 执行非查询操作
		/// </summary>
		/// <param name="sqlKey">为Resources file 中的ＳＱＬ语句的关键字</param>
		/// <returns></returns>
		/// 
		public int executeNonQuery(string sqlKey)
		{ 
          
			return executeNonQuery(sqlKey,(string[])null);
		}

		#endregion

		#region int  executeNonQuery(string sqlKey,string[]  parames)
		//
		/// <summary>
		/// 执行非查询操作
		/// </summary>
		/// <param name="sqlKey">为Resources file 中的ＳＱＬ语句的关键字</param>
		/// <param name="parames">当有参数时传入的参数数组</param>
		/// <returns></returns>
		//
		public int  executeNonQuery(string sqlKey,string[] parames)
		{	
			//返回的行数
			int rows = 0;		

			if (sqlKey == "")
			{
				throw new ArgumentNullException("sqlKey");
			}			

			try
			{
				rows = nonQueryDisposal(sqlKey, parames); 
			}
			catch(Exception ex)
			{
				//				ZeroWireLog.WriteLog(ex);
				//				throws new ZeroWireExcepton (ex);
				throw (ex);
			}	
			return   rows;
		}

		#endregion

		//execute no query SQL with transaction

		#region public int  executeNonQuery(IDbTransaction transaction ,string sqlKey)
		//
		/// <summary>
		/// 执行事务非查询操作
		/// </summary>
		/// <param name="transaction">传入的事务</param>
		/// <param name="sqlKey">为Resources file 中的ＳＱＬ语句的关键字</param>
		/// <returns></returns>
		//
		public int  executeNonQuery(IDbTransaction transaction ,string sqlKey)
		{
			return executeNonQuery(transaction, sqlKey,(string[])null);
		}

		#endregion


		#region int  executeNonQuery(IDbTransaction transaction ,string sqlKey,string[] parames)
		/// <summary>
		/// 执行事务非查询操作
		/// </summary>
		/// <param name="transaction">传入的事务</param>
		/// <param name="sqlKey">为Resources file 中的ＳＱＬ语句的关键字</param>
		/// <param name="parames">当有参数时传入的参数数组</param>
		/// <returns></returns>
		public int  executeNonQuery(IDbTransaction transaction ,string sqlKey,string[] parames)
		{
			//返回的行数
			int rows = 0;

			if (transaction == null)
			{
				throw new ArgumentNullException("transaction");
			}

			if (sqlKey == "")
			{
				throw new ArgumentNullException("sqlKey");
			}

			try
			{
				rows = TransNonQueryDisposal((OracleTransaction)transaction,sqlKey,parames);
			}
			catch(Exception ex)
			{
				//				ZeroWireLog.WriteLog(ex);
				//				throws new ZeroWireExcepton (ex);
				throw (ex);
			}	
			return   rows;
		}

		#endregion




		//execute query.return single object like string ,date, integer.

		#region object executeQueryCmdString(string commandText)
		//
		/// <summary>
		///执行查询操作 返回单个值，建议此方法不使用，只有在使用复杂的ＳＱＬ语句的时候使用此方法
		/// </summary>
		/// <param name="commandText">传入的ＳＱＬ命令语句</param>
		/// <returns></returns>
		//
		public object executeQueryCmdString(string commandText)
		{
			object result = null;		

			if (commandText == "")
			{
				throw new ArgumentNullException("commandText") ;
			}

			try 
			{
				result = QueryDisposalCmdString(commandText);
			}
			catch(Exception ex)
			{	
				//				ZeroWireLog.WriteLog(ex);
				//				throws new ZeroWireExcepton (ex);
				throw (ex);				
			}
			return result;
		}

		#endregion

		#region object executeQuery(string sqlKey)
		//
		/// <summary>
		/// 执行查询操作 返回单个值
		/// </summary>
		/// <param name="sqlKey">为Resources file 中的ＳＱＬ语句的关键字</param>
		/// <returns></returns>
		//
		public object executeQuery(string sqlKey)
		{
			return executeQuery(sqlKey,(string[])null);				
		}


		#endregion

		#region object executeQuery(string sqlKey,string[]  parames)
		//
		/// <summary>
		/// 执行查询操作 返回单个值
		/// </summary>
		/// <param name="sqlKey">为Resources file 中的ＳＱＬ语句的关键字</param>
		/// <param name="parames">传入的语句参数</param>
		/// <returns></returns>
		//
		public object executeQuery(string sqlKey,string[]  parames)
		{
			object result = null;		

			if (sqlKey == "")
			{
				throw new ArgumentNullException("sqlKey") ;
			}

			try 
			{
				result = QueryDisposal(sqlKey,parames);
			}
			catch(Exception ex)
			{	
				//				ZeroWireLog.WriteLog(ex);
				//				throws new ZeroWireExcepton (ex);
				throw (ex);				
			}
			return result;
		}

		#endregion



		//execute query. return DataSet
		#region public DataSet executeDataSetCmdString(string commandText)
		/// <summary>
		/// 执行查询操作 返回数据集，建议此方法不使用，只有在使用复杂的ＳＱＬ语句的时候使用此方法
		/// </summary>
		/// <param name="commandText">ＳＱＬ命令语句</param>
		/// <returns></returns>
		public DataSet executeDataSetCmdString(string commandText)
		{
			DataSet myDataSetGet = new DataSet();
			if (commandText == "")
			{
				throw new ArgumentNullException("commandText");
			}
								
			try
			{  	
				myDataSetGet = DataSetDisposalCmdString(commandText);				
			}	
			catch(Exception ex)
			{
				//				ZeroWireLog.WriteLog(ex);
				//				throws new ZeroWireExcepton (ex);				
				throw (ex);
			}			
			return myDataSetGet;

		}

		#endregion

		#region DataSet executeDataSet(string sqlKey)
		//
		/// <summary>
		/// 执行查询操作，返回数据集
		/// </summary>
		/// <param name="sqlKey">为Resources file 中的ＳＱＬ语句的关键字</param>
		/// <returns></returns>
		//
		public DataSet executeDataSet(string sqlKey)
		{			
			
			return executeDataSet(sqlKey, (string[])null);			
		}

		#endregion

		#region DataSet executeDataSet(string sqlKey, string[] parames)
		
		/// <summary>
		/// 执行查询操作，返回数据集
		/// </summary>
		/// <param name="sqlKey">为Resources file 中的ＳＱＬ语句的关键字</param>
		/// <param name="parames">传入的ＳＱＬ参数</param>
		/// <returns></returns>
		public DataSet executeDataSet(string sqlKey, string[] parames)
		{
			DataSet myDataSetGet = new DataSet();

			return executeDataSet(sqlKey, parames, myDataSetGet,"");
		}

		#endregion

		
		#region DataSet executeDataSet(string sqlKey,DataSet myDataSetGet,string tableName)
		/// <summary>
		/// 执行查询操作，返回数据集
		/// </summary>
		/// <param name="sqlKey">为Resources file 中的ＳＱＬ语句的关键字</param>
		/// <param name="myDataSetGet">传入的DataSet参数</param>
		/// <param name="tableName">表名称</param>
		/// <returns></returns>

		public DataSet executeDataSet(string sqlKey, DataSet myDataSetGet, string tableName)
		{
			return executeDataSet(sqlKey,(string[])null, myDataSetGet, tableName);
		}

		#endregion


		#region DataSet executeDataSet(string sqlKey, string[] parames,DataSet myDataSetGet,string tableName)
		/// <summary>
		/// 执行查询操作，返回数据集
		/// </summary>
		/// <param name="sqlKey">为Resources file 中的ＳＱＬ语句的关键字</param>
		/// <param name="parames">传入的ＳＱＬ参数</param>
		/// <param name="myDataSetGet">传入的DataSet参数</param>
		/// <param name="tableName">表名称</param>
		/// <returns></returns>
		public DataSet executeDataSet(string sqlKey, string[] parames, DataSet myDataSetGet, string tableName)
		{	
			if (sqlKey == "")
			{
				throw new ArgumentNullException("sqlKey");
			}
								
			try
			{  	
				myDataSetGet = DataSetDisposal(sqlKey, parames, myDataSetGet,tableName);				
			}	
			catch(Exception ex)
			{
				//				ZeroWireLog.WriteLog(ex);
				//				throws new ZeroWireExcepton (ex);				
				throw (ex);
			}			
			return myDataSetGet;

		}

		#endregion

		#region DataSet executeDataSet(IDbTransaction transaction, string sqlKey)
		//
		/// <summary>
		/// 执行查询事务操作，返回数据集
		/// </summary>
		/// <param name="transaction">传入的事务参数</param>
		/// <param name="sqlKey">为Resources file 中的ＳＱＬ语句的关键字</param>
		/// <returns></returns>
		//
		public DataSet executeDataSet(IDbTransaction transaction, string sqlKey)
		{		
			return executeDataSet(transaction, sqlKey, (string[])null);
		}

		#endregion

		#region DataSet executeDataSet(IDbTransaction transaction, string commandText, string[] parames)
		//
		/// <summary>
		/// 执行查询事务操作，返回数据集
		/// </summary>
		/// <param name="transaction">传入的事务参数</param>
		/// <param name="sqlKey">为Resources file 中的ＳＱＬ语句的关键字</param>
		/// <param name="parames">传入的ＳＱＬ参数</param>
		/// <returns></returns>
		//
		public DataSet executeDataSet(IDbTransaction transaction, string sqlKey, string[] parames)
		{
			
			DataSet myDataSetGet = new DataSet();	

			if (transaction == null)
			{
				throw new ArgumentNullException("transaction");
			}

			if (sqlKey == "")
			{
				throw new ArgumentNullException("sqlKey");
			}
				
			try
			{  	
				myDataSetGet = transDataSetDisposal((OracleTransaction)transaction,sqlKey, parames);				
			}	
			catch (Exception ex)
			{
				//				ZeroWireLog.WriteLog(ex);
				//				throws new ZeroWireExcepton (ex);				
				throw (ex);
			}			
			return myDataSetGet;
		}

		#endregion

		//Update DataSet 

		#region void updateDataSet(IDbDataAdapter dataAdapter, DataSet ds)
		//
		/// <summary>
		/// 更新数据集
		/// </summary>
		/// <param name="dataAdapter">传入的DataAdapter参数</param>
		/// <param name="ds">传入的 DataSet</param>
		//
		public void updateDataSet(IDbDataAdapter dataAdapter, DataSet ds)
		{		
			if(!ds.HasChanges(DataRowState.Modified))
			{
				return;
			}

			if (dataAdapter == null)
			{
				return ;
			}
		
			OracleDataAdapter  MyDataAdapter = (OracleDataAdapter)dataAdapter;
			
			DataSet MyDataSet;			
			try
			{
				//获得被修改的行
				MyDataSet = ds.GetChanges(DataRowState.Modified);
				//更新数据
				MyDataAdapter.Update(MyDataSet);
			}
			catch(Exception ex)
			{
				//				ZeroWireLog.WriteLog(ex);
				//				throws new ZeroWireExcepton (ex);				
				throw (ex);
			}			
			return;
		}

		#endregion



		//功能处理方法
		#region disposal
		
		

		#region string paraSQL(string commandText,string[] para)
		/// <summary>
		/// 处理SQL语句的参数
		/// </summary>
		/// <param name="commandText">SQL语句</param>
		/// <param name="para">传入的参数</param>
		/// <returns></returns>
		private  string paraSQL(string commandText,string[] para)
		{	
			//Para的长度
			int length = para.Length;
			//记载SQL语句中"{i}"的数量
			int count = 0;
			try
			{
				for (int j = 0; j< 20; j++)
				{
					//判断sql语句中是否还有{i}
					if (commandText.IndexOf("{" + j.ToString() + "}", 1) == -1)
					{
						j = 20;                   
					}
					count ++;
				}

				//替换参数
				for  (int i= 0;i< length;i++)
				{
					commandText = commandText.Replace("{" + i.ToString() + "}"," " + para[i]).ToString();								
				}
            
				//替换没有传入值的参数
				if (length  < count )
				{
					for  (int i= length -1 ;i<= count;i++)
					{
						commandText = commandText.Replace("{" + i.ToString() + "}","").ToString();								
					}	
				}
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			return commandText;
		}
		#endregion

		#region private object paramesDisposal(IDbCommand cmd, string parames)
		//
		//diesposal parames 
		//

		//		private OracleCommand paramesDisposal(OracleCommand cmd, object[] parames)
		//		{
		//			int length = parames.Length;
		//			try
		//			{
		//				for (int i = 0 ; i < length ; i++)
		//				{	
		//
		//        			//get parames[i] data type
		////					Type ParamesTpye = parames[i].GetType();
		////					myParameter[i] = new OracleParameter("parames" + i.ToString() , ParamesTpye);
		////					myParameter[i].Value = parames[i];
		//					//add myParameter[i] values to cmd
		//					cmd.Parameters.Add(parames[i]);
		//				}
		//			}
		//			catch(Exception ex)
		//			{
		//				throw (ex);
		//			}
		//			return cmd;
		//		}

		#endregion

		#region private int NoQueryDisposalCmdString(string commandText)
		private int NoQueryDisposalCmdString(string commandText)
		{
			OracleConnection conn = null;
			OracleCommand cmd = null;
			//记录行数
			int rows = 0;
			try
			{
				//得到了解
				conn = (OracleConnection)getConnection();
				//打开连接
				conn.Open();	
				cmd = CreateCommand(commandText,conn);
				rows = cmd.ExecuteNonQuery(); 
			}
			catch(Exception ex)
			{
				throw (ex);
			}
			finally
			{			
				conn.Close();
			}
			return rows;

		}
		#endregion

		#region private int nonQueryDisposal(string sqlKey, string[] parames)
		//
		/// <summary>
		/// 非查询语句的处理
		/// </summary>
		/// <param name="sqlKey">为Resources file 中的ＳＱＬ语句的关键字</param>
		/// <param name="parames">传入的SQL语句参数</param>
		/// <returns></returns>
		//
		private int nonQueryDisposal(string sqlKey, string[] parames)
		{
			string commandText = "";
			OracleConnection conn = null;
			OracleCommand cmd = null;
			//记录行数
			int rows = 0;
			//得到sql语句
			commandText = ResourceBundle.GetSQL(sqlKey);
			if (commandText == "") throw new ArgumentNullException("commandText");

			try
			{
				//得到了解
				conn = (OracleConnection)getConnection();
				//打开连接
				conn.Open();	
				if (parames != null)
				{
					commandText = paraSQL(commandText, parames);	
				}
				cmd = CreateCommand(commandText,conn);

				rows = cmd.ExecuteNonQuery(); 
			}
			catch(Exception ex)
			{
				throw (ex);
			}
			finally
			{			
				conn.Close();
			}
			return rows;
		}

		#endregion		

		#region private int  TransNonQueryDisposal(OracleTransaction transaction ,string commandText,object[] parames)
		//
		/// <summary>
		/// 非查询语句的事务处理
		/// </summary>
		/// <param name="transaction">事务</param>
		/// <param name="sqlKey">为Resources file 中的ＳＱＬ语句的关键字</param>
		/// <param name="parames">SQL语句参数</param>
		/// <returns></returns>
		//
		private int  TransNonQueryDisposal(OracleTransaction transaction ,string sqlKey,string[] parames)
		{	
			string commandText = "";
			int rows = 0;					

			if (transaction.Connection == null) 
			{
				throw new ArgumentNullException("transaction.Connection");
			}
			IDbCommand cmd = CreateCommand();
			//获得commandText
			commandText = ResourceBundle.GetSQL(sqlKey);
			if (commandText == "") 
			{
				throw new ArgumentNullException("commandText");
			}
			try
			{ 	
				if (parames != null)
				{
					commandText = paraSQL(commandText, parames);	
				}

                //给cmd赋相应的值
				cmd.CommandText = commandText;			
				cmd.Connection = transaction.Connection;
				cmd.Transaction = transaction;
				rows = cmd.ExecuteNonQuery(); 	
			}	
			catch(Exception ex)
			{
				throw (ex);
			}			
			return rows;
		}

		#endregion 

		#region private object QueryDisposalCmdString(string commandText)
		//
		/// <summary>
		/// 查询语句的处理，直接的SQL语句　返回单个值 
		/// </summary>
		/// <param name="commandText">SQL语句串</param>
		/// <returns></returns>
		//
		private object QueryDisposalCmdString(string commandText)
		{
			DataSet myDataSetGet = new DataSet();
			DataTable MyTable = new DataTable();						
			object result = null;		
				
			OracleConnection conn = (OracleConnection)getConnection();			
			try
			{  		
				conn.Open();
				OracleCommand cmd = CreateCommand(commandText, conn);
				OracleDataAdapter myAdapter = CreateAdapter(cmd);
				//填充数据集			
				myAdapter.Fill(myDataSetGet, "myInvoke");
				MyTable = myDataSetGet.Tables[0];
				result = (object)MyTable.Rows[0][0];
			}	
			catch(Exception ex)
			{	
				//	ZeroWireLog.WriteLog(ex);
				//	throws new ZeroWireExcepton (ex);

				throw (ex);
			}
			finally
			{			
				conn.Close();
			}			
			return result;	
		}

		#endregion

		#region private object QueryDisposal(string sqlKey,string[] parames)
		//
		/// <summary>
		/// 查询处理
		/// </summary>
		/// <param name="sqlKey">为Resources file 中的ＳＱＬ语句的关键字</param>
		/// <param name="parames">SQL语句参数</param>
		/// <returns></returns>
		//
		private object QueryDisposal(string sqlKey, string[] parames)
		{
			DataSet myDataSetGet = new DataSet();
			DataTable MyTable = new DataTable();						
			object result = null;
			string commandText = "";
			//获得连接对象				
			OracleConnection conn = (OracleConnection)getConnection();			
			try
			{  		
				conn.Open();
				//获得commandText
				commandText = ResourceBundle.GetSQL(sqlKey);
				if (commandText == "") throw new ArgumentNullException("commandText");
				if (parames != null)
				{
					commandText = paraSQL(commandText, parames);	
				}
				OracleCommand cmd = CreateCommand(commandText, conn);

				OracleDataAdapter myAdapter = CreateAdapter(cmd); 							
				myAdapter.Fill(myDataSetGet, "myInvoke");
				MyTable = myDataSetGet.Tables[0];
				result = (object)MyTable.Rows[0][0];
			}	
			catch(Exception ex)
			{	
				//	ZeroWireLog.WriteLog(ex);
				//	throws new ZeroWireExcepton (ex);

				throw (ex);
			}
			finally
			{			
				conn.Close();
			}			
			return result;	
		}

		#endregion

		#region  private DataSet DataSetDisposalCmdString(string commandText)
		//
		/// <summary>
		/// 返回查询结果集的处理,直接的SQL语句
		/// </summary>
		/// <param name="sqlKey">为Resources file 中的ＳＱＬ语句的关键字</param>
		/// <param name="parames">SQL语句参数</param>
		/// <returns></returns>
		//
		private DataSet DataSetDisposalCmdString(string commandText)
		{
			DataSet myDataSetGet = new DataSet();
			
			OracleConnection conn = (OracleConnection)getConnection();			
			try
			{ 
				conn.Open();
				OracleCommand cmd = CreateCommand(commandText, conn);
				OracleDataAdapter myAdapter = CreateAdapter(cmd);
				//填充数据集			
				myAdapter.Fill(myDataSetGet, "myInvoke");
			}	
			catch (Exception ex)
			{	
				//	ZeroWireLog.WriteLog(ex);
				//	throws new ZeroWireExcepton (ex);				
				throw (ex);
			}
			finally
			{			
				conn.Close();
			}
			return myDataSetGet;
		}

		#endregion

		
		#region  private DataSet DataSetDisposal(string sqlKey, string[] parames)
		//
		/// <summary>
		/// 返回查询结果集的处理
		/// </summary>
		/// <param name="sqlKey">为Resources file 中的ＳＱＬ语句的关键字</param>
		/// <param name="parames">SQL语句参数</param>
		/// <returns></returns>
		//
		private DataSet DataSetDisposal(string sqlKey, string[] parames)
		{
			DataSet myDataSetGet = new DataSet();
			string commandText = "";
			OracleConnection conn = (OracleConnection)getConnection();			
			try
			{ 
				conn.Open();
				//获得commandText
				commandText = ResourceBundle.GetSQL(sqlKey);
				if (commandText == "") throw new ArgumentNullException("commandText");
				if (parames != null)
				{
					commandText = paraSQL(commandText, parames);	
				}
				OracleCommand cmd = CreateCommand(commandText, conn);

				OracleDataAdapter myAdapter = CreateAdapter(cmd);
				//填充数据集			
				myAdapter.Fill(myDataSetGet, "myInvoke");
			}	
			catch (Exception ex)
			{	
				//	ZeroWireLog.WriteLog(ex);
				//	throws new ZeroWireExcepton (ex);				
				throw (ex);
			}
			finally
			{			
				conn.Close();
			}
			return myDataSetGet;
		}

		#endregion

		#region  private DataSet DataSetDisposal(string sqlKey, string[] parames,DataSet myDataSetGet,string tableName)
		/// <summary>
		/// 返回查询结果集的处理
		/// </summary>
		/// <param name="sqlKey">为Resources file 中的ＳＱＬ语句的关键字</param>
		/// <param name="parames">SQL语句参数</param>
		/// <param name="myDataSetGet">DataSet参数</param>
		/// <param name="tableName">传入的DataSet表名</param>
		/// <returns></returns>
		private DataSet DataSetDisposal(string sqlKey, string[] parames, DataSet myDataSetGet, string tableName)
		{
			string commandText = "";
			OracleConnection conn = (OracleConnection)getConnection();			
			try
			{ 
				conn.Open();
				//获得commandText
				commandText = ResourceBundle.GetSQL(sqlKey);
				if (commandText == "") throw new ArgumentNullException("commandText");
				if (parames != null)
				{
					commandText = paraSQL(commandText, parames);	
				}
				OracleCommand cmd = CreateCommand(commandText, conn);

				OracleDataAdapter myAdapter = CreateAdapter(cmd);
				
				//填充数据集
				if (tableName == "") 
				{
					myAdapter.Fill(myDataSetGet, "myInvoke");
				}
				else
				{
					myAdapter.Fill(myDataSetGet, tableName);

				}
			}	
			catch (Exception ex)
			{	
				//	ZeroWireLog.WriteLog(ex);
				//	throws new ZeroWireExcepton (ex);				
				throw (ex);
			}
			finally
			{			
				conn.Close();
			}
			return myDataSetGet;
		}

		#endregion


		#region  private DataSet transDataSetDisposal(OracleTransaction transaction,string sqlKey, string[] parames)
		//
		/// <summary>
		/// 返回数据集查询的事务处理
		/// </summary>
		/// <param name="transaction">事务</param>
		/// <param name="sqlKey">为Resources file 中的ＳＱＬ语句的关键字</param>
		/// <param name="parames">SQL语句参数</param>
		/// <returns></returns>
		//
		private DataSet transDataSetDisposal(OracleTransaction transaction,string sqlKey, string[] parames)
		{
			string commandText = "";
			DataSet myDataSetGet = new DataSet();
			try
			{ 	
				IDbCommand cmd = CreateCommand();
				commandText = ResourceBundle.GetSQL(sqlKey);
				if (commandText == "") throw new ArgumentNullException("commandText");
				if (parames != null)
				{
					commandText = paraSQL(commandText, parames);	
				}			
				cmd.CommandText = commandText;
				if (transaction.Connection == null) 
				{
					throw new ArgumentNullException("transaction.Connection");
				}
				cmd.Connection = transaction.Connection;
				//获得cmd的Transaction
				cmd.Transaction = transaction;
				OracleDataAdapter myAdapter = CreateAdapter((OracleCommand)cmd); 
				//填充数据集			
				myAdapter.Fill(myDataSetGet, "myInvoke");
			}	
			catch(Exception ex)
			{	
				//	ZeroWireLog.WriteLog(ex);
				//	throws new ZeroWireExcepton (ex);				
				throw (ex);
			}			
			return myDataSetGet;
		}

		#endregion


		#endregion

	}
}
