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


	   //��ó�ʼ����ֵ

		#region Component Designer generated code

		//
		/// <summary>
		///������ݿ�����Ӵ�
		/// </summary>

		private void getConnectionString() 
		{
			//��������ַ���(��Ҫ*****)
			ConnectionString="User Id=sfa;Password=zwsfa;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=172.16.1.6)(PORT=1521)))(CONNECT_DATA=(SID=zwora)(SERVER=DEDICATED)))";
		}

		/// <summary>
		/// ���Resources file 
		/// </summary>
		private void getResourcesFileSQL()
		{
			//�õ���Դ�ļ�(��Ҫ*****)��Ŀ¼�����·��
			ResourceBundle.SetSQLResourceManager(new System.Resources.ResourceManager("TestOracle.Resources.OrclSql",System.Reflection.Assembly.GetExecutingAssembly()));
		}

		#endregion	

        //��ʼ����
		#region Database getConnection
		//
		/// <summary>
		/// �õ�IDbConnection����
		/// </summary>
		/// <returns></returns>
		//
		public IDbConnection getConnection()
		{			
			//��������
			IDbConnection conn = null;
			try
			{
				//���Oracle����
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
		/// ���ⲿ������Ӵ�
		/// </summary>
		/// <param name="ConnectionString">�����ַ�</param>
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
		/// �ĵ�conn������
		/// </summary>
		/// <param name="conn">���Ӷ���</param>
		/// <returns></returns>
		public IDbTransaction getTransaction(IDbConnection conn)
		{	
			//����һ������
			return conn.BeginTransaction();
		}

		#endregion

		#region Database CreateCommand
		/// <summary>
		/// �õ�OracleCommand����
		/// </summary>
		/// <returns></returns>
		private static OracleCommand CreateCommand()
		{
			OracleCommand cmd = null;
			try 
			{
				//���һ��oracle�������
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
		/// �õ�OracleCommand����
		/// </summary>
		/// <param name="CommandText">SQL�ַ���</param>
		/// <param name="cnn">���Ӷ���</param>
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
		/// �õ�OracleDataAdapter����
		/// </summary>
		/// <param name="cmd">����OracleCommand����</param>
		/// <returns></returns>
		public static OracleDataAdapter CreateAdapter(OracleCommand cmd)
		{
			OracleDataAdapter da;
			try
			{
				//���һ��OracleDataAdapter
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
		/// ���SQL��䣬���ڲ�����
		/// </summary>
		/// <param name="sqlKey">ΪResources file �еģӣѣ����Ĺؼ���</param>
		/// <returns></returns>

		public string getCommandText(string sqlKey)
		{
			return getCommandText(sqlKey,(string[])null);

		}

		/// <summary>
		/// ���SQL��䣬���ڲ�����
		/// </summary>
		/// <param name="sqlKey">ΪResources file �еģӣѣ����Ĺؼ���</param>
		/// <param name="parames">�����sql���Ĳ���</param>
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
		/// ִ�зǲ�ѯ����
		/// </summary>
		/// <param name="sqlKey">ΪResources file �еģӣѣ����Ĺؼ���</param>
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
		/// ִ�зǲ�ѯ����
		/// </summary>
		/// <param name="sqlKey">ΪResources file �еģӣѣ����Ĺؼ���</param>
		/// <param name="parames">���в���ʱ����Ĳ�������</param>
		/// <returns></returns>
		//
		public int  executeNonQuery(string sqlKey,string[] parames)
		{	
			//���ص�����
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
		/// ִ������ǲ�ѯ����
		/// </summary>
		/// <param name="transaction">���������</param>
		/// <param name="sqlKey">ΪResources file �еģӣѣ����Ĺؼ���</param>
		/// <returns></returns>
		//
		public int  executeNonQuery(IDbTransaction transaction ,string sqlKey)
		{
			return executeNonQuery(transaction, sqlKey,(string[])null);
		}

		#endregion


		#region int  executeNonQuery(IDbTransaction transaction ,string sqlKey,string[] parames)
		/// <summary>
		/// ִ������ǲ�ѯ����
		/// </summary>
		/// <param name="transaction">���������</param>
		/// <param name="sqlKey">ΪResources file �еģӣѣ����Ĺؼ���</param>
		/// <param name="parames">���в���ʱ����Ĳ�������</param>
		/// <returns></returns>
		public int  executeNonQuery(IDbTransaction transaction ,string sqlKey,string[] parames)
		{
			//���ص�����
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
		///ִ�в�ѯ���� ���ص���ֵ������˷�����ʹ�ã�ֻ����ʹ�ø��ӵģӣѣ�����ʱ��ʹ�ô˷���
		/// </summary>
		/// <param name="commandText">����ģӣѣ��������</param>
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
		/// ִ�в�ѯ���� ���ص���ֵ
		/// </summary>
		/// <param name="sqlKey">ΪResources file �еģӣѣ����Ĺؼ���</param>
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
		/// ִ�в�ѯ���� ���ص���ֵ
		/// </summary>
		/// <param name="sqlKey">ΪResources file �еģӣѣ����Ĺؼ���</param>
		/// <param name="parames">�����������</param>
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
		/// ִ�в�ѯ���� �������ݼ�������˷�����ʹ�ã�ֻ����ʹ�ø��ӵģӣѣ�����ʱ��ʹ�ô˷���
		/// </summary>
		/// <param name="commandText">�ӣѣ��������</param>
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
		/// ִ�в�ѯ�������������ݼ�
		/// </summary>
		/// <param name="sqlKey">ΪResources file �еģӣѣ����Ĺؼ���</param>
		/// <returns></returns>
		//
		public DataSet executeDataSet(string sqlKey)
		{			
			
			return executeDataSet(sqlKey, (string[])null);			
		}

		#endregion

		#region DataSet executeDataSet(string sqlKey, string[] parames)
		
		/// <summary>
		/// ִ�в�ѯ�������������ݼ�
		/// </summary>
		/// <param name="sqlKey">ΪResources file �еģӣѣ����Ĺؼ���</param>
		/// <param name="parames">����ģӣѣ̲���</param>
		/// <returns></returns>
		public DataSet executeDataSet(string sqlKey, string[] parames)
		{
			DataSet myDataSetGet = new DataSet();

			return executeDataSet(sqlKey, parames, myDataSetGet,"");
		}

		#endregion

		
		#region DataSet executeDataSet(string sqlKey,DataSet myDataSetGet,string tableName)
		/// <summary>
		/// ִ�в�ѯ�������������ݼ�
		/// </summary>
		/// <param name="sqlKey">ΪResources file �еģӣѣ����Ĺؼ���</param>
		/// <param name="myDataSetGet">�����DataSet����</param>
		/// <param name="tableName">������</param>
		/// <returns></returns>

		public DataSet executeDataSet(string sqlKey, DataSet myDataSetGet, string tableName)
		{
			return executeDataSet(sqlKey,(string[])null, myDataSetGet, tableName);
		}

		#endregion


		#region DataSet executeDataSet(string sqlKey, string[] parames,DataSet myDataSetGet,string tableName)
		/// <summary>
		/// ִ�в�ѯ�������������ݼ�
		/// </summary>
		/// <param name="sqlKey">ΪResources file �еģӣѣ����Ĺؼ���</param>
		/// <param name="parames">����ģӣѣ̲���</param>
		/// <param name="myDataSetGet">�����DataSet����</param>
		/// <param name="tableName">������</param>
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
		/// ִ�в�ѯ����������������ݼ�
		/// </summary>
		/// <param name="transaction">������������</param>
		/// <param name="sqlKey">ΪResources file �еģӣѣ����Ĺؼ���</param>
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
		/// ִ�в�ѯ����������������ݼ�
		/// </summary>
		/// <param name="transaction">������������</param>
		/// <param name="sqlKey">ΪResources file �еģӣѣ����Ĺؼ���</param>
		/// <param name="parames">����ģӣѣ̲���</param>
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
		/// �������ݼ�
		/// </summary>
		/// <param name="dataAdapter">�����DataAdapter����</param>
		/// <param name="ds">����� DataSet</param>
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
				//��ñ��޸ĵ���
				MyDataSet = ds.GetChanges(DataRowState.Modified);
				//��������
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



		//���ܴ�����
		#region disposal
		
		

		#region string paraSQL(string commandText,string[] para)
		/// <summary>
		/// ����SQL���Ĳ���
		/// </summary>
		/// <param name="commandText">SQL���</param>
		/// <param name="para">����Ĳ���</param>
		/// <returns></returns>
		private  string paraSQL(string commandText,string[] para)
		{	
			//Para�ĳ���
			int length = para.Length;
			//����SQL�����"{i}"������
			int count = 0;
			try
			{
				for (int j = 0; j< 20; j++)
				{
					//�ж�sql������Ƿ���{i}
					if (commandText.IndexOf("{" + j.ToString() + "}", 1) == -1)
					{
						j = 20;                   
					}
					count ++;
				}

				//�滻����
				for  (int i= 0;i< length;i++)
				{
					commandText = commandText.Replace("{" + i.ToString() + "}"," " + para[i]).ToString();								
				}
            
				//�滻û�д���ֵ�Ĳ���
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
			//��¼����
			int rows = 0;
			try
			{
				//�õ��˽�
				conn = (OracleConnection)getConnection();
				//������
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
		/// �ǲ�ѯ���Ĵ���
		/// </summary>
		/// <param name="sqlKey">ΪResources file �еģӣѣ����Ĺؼ���</param>
		/// <param name="parames">�����SQL������</param>
		/// <returns></returns>
		//
		private int nonQueryDisposal(string sqlKey, string[] parames)
		{
			string commandText = "";
			OracleConnection conn = null;
			OracleCommand cmd = null;
			//��¼����
			int rows = 0;
			//�õ�sql���
			commandText = ResourceBundle.GetSQL(sqlKey);
			if (commandText == "") throw new ArgumentNullException("commandText");

			try
			{
				//�õ��˽�
				conn = (OracleConnection)getConnection();
				//������
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
		/// �ǲ�ѯ����������
		/// </summary>
		/// <param name="transaction">����</param>
		/// <param name="sqlKey">ΪResources file �еģӣѣ����Ĺؼ���</param>
		/// <param name="parames">SQL������</param>
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
			//���commandText
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

                //��cmd����Ӧ��ֵ
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
		/// ��ѯ���Ĵ���ֱ�ӵ�SQL��䡡���ص���ֵ 
		/// </summary>
		/// <param name="commandText">SQL��䴮</param>
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
				//������ݼ�			
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
		/// ��ѯ����
		/// </summary>
		/// <param name="sqlKey">ΪResources file �еģӣѣ����Ĺؼ���</param>
		/// <param name="parames">SQL������</param>
		/// <returns></returns>
		//
		private object QueryDisposal(string sqlKey, string[] parames)
		{
			DataSet myDataSetGet = new DataSet();
			DataTable MyTable = new DataTable();						
			object result = null;
			string commandText = "";
			//������Ӷ���				
			OracleConnection conn = (OracleConnection)getConnection();			
			try
			{  		
				conn.Open();
				//���commandText
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
		/// ���ز�ѯ������Ĵ���,ֱ�ӵ�SQL���
		/// </summary>
		/// <param name="sqlKey">ΪResources file �еģӣѣ����Ĺؼ���</param>
		/// <param name="parames">SQL������</param>
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
				//������ݼ�			
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
		/// ���ز�ѯ������Ĵ���
		/// </summary>
		/// <param name="sqlKey">ΪResources file �еģӣѣ����Ĺؼ���</param>
		/// <param name="parames">SQL������</param>
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
				//���commandText
				commandText = ResourceBundle.GetSQL(sqlKey);
				if (commandText == "") throw new ArgumentNullException("commandText");
				if (parames != null)
				{
					commandText = paraSQL(commandText, parames);	
				}
				OracleCommand cmd = CreateCommand(commandText, conn);

				OracleDataAdapter myAdapter = CreateAdapter(cmd);
				//������ݼ�			
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
		/// ���ز�ѯ������Ĵ���
		/// </summary>
		/// <param name="sqlKey">ΪResources file �еģӣѣ����Ĺؼ���</param>
		/// <param name="parames">SQL������</param>
		/// <param name="myDataSetGet">DataSet����</param>
		/// <param name="tableName">�����DataSet����</param>
		/// <returns></returns>
		private DataSet DataSetDisposal(string sqlKey, string[] parames, DataSet myDataSetGet, string tableName)
		{
			string commandText = "";
			OracleConnection conn = (OracleConnection)getConnection();			
			try
			{ 
				conn.Open();
				//���commandText
				commandText = ResourceBundle.GetSQL(sqlKey);
				if (commandText == "") throw new ArgumentNullException("commandText");
				if (parames != null)
				{
					commandText = paraSQL(commandText, parames);	
				}
				OracleCommand cmd = CreateCommand(commandText, conn);

				OracleDataAdapter myAdapter = CreateAdapter(cmd);
				
				//������ݼ�
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
		/// �������ݼ���ѯ��������
		/// </summary>
		/// <param name="transaction">����</param>
		/// <param name="sqlKey">ΪResources file �еģӣѣ����Ĺؼ���</param>
		/// <param name="parames">SQL������</param>
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
				//���cmd��Transaction
				cmd.Transaction = transaction;
				OracleDataAdapter myAdapter = CreateAdapter((OracleCommand)cmd); 
				//������ݼ�			
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
