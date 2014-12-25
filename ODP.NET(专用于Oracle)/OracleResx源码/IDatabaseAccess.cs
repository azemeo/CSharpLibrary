/********************************************************************
** "This program code contains trade secrets of Zero Global that   ** 
** (i) derive independent economic value, actual or potential,     ** 
** from not being generally known to, and not being readily        ** 
** ascertainable by proper means by other persons who can obtain   ** 
** economic value from their disclosure or use and; (ii) are the   ** 
** subject of efforts by Zero Global that are reasonable under the ** 
** circumstances to maintain their secrecy. You may not use this   ** 
** program code without prior written authorization from           ** 
** Zero Global."                                                   ** 
********************************************************************/
using System;
using System.Data;


namespace ZeroWire.Shared.Database
{
	/// <summary>
	/// Summary description for IDatabaseAccess.
	/// </summary>
	/// 
	public interface IDatabaseAccess
	{
		
		/// <summary>
		/// ���SQL���
		/// </summary>
		/// <param name="sqlKey">ΪResources file �еģӣѣ����Ĺؼ���</param>
		/// <returns></returns>
		string getCommandText(string sqlKey);

		/// <summary>
		/// ���SQL���
		/// </summary>
		/// <param name="sqlKey">ΪResources file �еģӣѣ����Ĺؼ���</param>
		/// <param name="parames">���в���ʱ����Ĳ�������</param>
		/// <returns></returns>
		string getCommandText(string sqlKey,string[] parames);

		//
		/// <summary>
		///������ݿ�����Ӵ�
		/// </summary>
		IDbConnection getConnection();
		IDbConnection getConnection(string ConnectionString);

		/// <summary>
		/// �ĵ�conn������
		/// </summary>
		/// <param name="conn"></param>
		/// <returns></returns>
		IDbTransaction getTransaction(IDbConnection connString);

		//
		/// <summary>
		///ִ�в�ѯ���� ���ص���ֵ������˷�����ʹ�ã�ֻ����ʹ�ø��ӵģӣѣ�����ʱ��ʹ�ô˷���
		/// </summary>
		/// <param name="commandText">����ģӣѣ��������</param>
		/// <returns></returns>
		//
		int executeNoQueryCmdString(string commandText);

		//exexcute no query SQL
		/// <summary>
		/// ִ�зǲ�ѯ����
		/// </summary>
		/// <param name="sqlKey">ΪResources file �еģӣѣ����Ĺؼ���</param>
		/// <returns></returns>
		/// 
		int executeNonQuery(string sqlKey);

		//
		/// <summary>
		/// ִ�зǲ�ѯ����
		/// </summary>
		/// <param name="sqlKey">ΪResources file �еģӣѣ����Ĺؼ���</param>
		/// <param name="parames">���в���ʱ����Ĳ�������</param>
		/// <returns></returns>
		//
		int executeNonQuery(string sqlKey, string[] parames);

		//execute no query SQL with Transaction 

		//
		/// <summary>
		/// ִ������ǲ�ѯ����
		/// </summary>
		/// <param name="transaction">���������</param>
		/// <param name="sqlKey">ΪResources file �еģӣѣ����Ĺؼ���</param>
		/// <returns></returns>
		//
		int  executeNonQuery(IDbTransaction transaction, string sqlKey);

		/// <summary>
		/// ִ������ǲ�ѯ����
		/// </summary>
		/// <param name="transaction">���������</param>
		/// <param name="sqlKey">ΪResources file �еģӣѣ����Ĺؼ���</param>
		/// <param name="parames">���в���ʱ����Ĳ�������</param>
		/// <returns></returns>
		int  executeNonQuery(IDbTransaction transaction ,string sqlKey,string[] parames);

		//execute query.return single object like string ,date, integer.

		//
		/// <summary>
		///ִ�в�ѯ���� ���ص���ֵ������˷�����ʹ�ã�ֻ����ʹ�ø��ӵģӣѣ�����ʱ��ʹ�ô˷���
		/// </summary>
		/// <param name="commandText">����ģӣѣ��������</param>
		/// <returns></returns>
		//
		object executeQueryCmdString(string commandText);

		//
		/// <summary>
		/// ִ�в�ѯ���� ���ص���ֵ
		/// </summary>
		/// <param name="sqlKey">ΪResources file �еģӣѣ����Ĺؼ���</param>
		/// <returns></returns>
		//
		object executeQuery(string sqlKey);

		//
		/// <summary>
		/// ִ�в�ѯ���� ���ص���ֵ
		/// </summary>
		/// <param name="sqlKey">ΪResources file �еģӣѣ����Ĺؼ���</param>
		/// <param name="parames">�����������</param>
		/// <returns></returns>
		//
		object executeQuery(string sqlKey,string[] parames);

        

		//execute query. return DataSet

		/// <summary>
		/// ִ�в�ѯ���� �������ݼ�������˷�����ʹ�ã�ֻ����ʹ�ø��ӵģӣѣ�����ʱ��ʹ�ô˷���
		/// </summary>
		/// <param name="commandText">�ӣѣ��������</param>
		/// <returns></returns>
		DataSet executeDataSetCmdString(string commandText);
	
		//
		/// <summary>
		/// ִ�в�ѯ�������������ݼ�
		/// </summary>
		/// <param name="sqlKey">ΪResources file �еģӣѣ����Ĺؼ���</param>
		/// <returns></returns>
		//
		DataSet executeDataSet(string sqlKey);
	
		//
		/// <summary>
		/// ִ�в�ѯ�������������ݼ�
		/// </summary>
		/// <param name="sqlKey">ΪResources file �еģӣѣ����Ĺؼ���</param>
		/// <param name="parames">Ϊ����ģӣѣ̲���</param>
		/// <returns></returns>
		//
		DataSet executeDataSet(string sqlKey, string[] parames);

		/// <summary>
		/// ִ�в�ѯ�������������ݼ�
		/// </summary>
		/// <param name="sqlKey">ΪResources file �еģӣѣ����Ĺؼ���</param>
		/// <param name="myDataSetGet">�����DataSet����</param>
		/// <param name="tableName">������</param>
		/// <returns></returns>
		DataSet executeDataSet(string sqlKey, DataSet myDataSetGet, string tableName);

		/// <summary>
		/// ִ�в�ѯ�������������ݼ�
		/// </summary>
		/// <param name="sqlKey">ΪResources file �еģӣѣ����Ĺؼ���</param>
		/// <param name="parames">����ģӣѣ̲���</param>
		/// <param name="myDataSetGet">�����DataSet����</param>
		/// <param name="tableName">������</param>
		/// <returns></returns>
		DataSet executeDataSet(string sqlKey, string[] parames, DataSet myDataSetGet, string tableName);

		//
		/// <summary>
		/// ִ�в�ѯ����������������ݼ�
		/// </summary>
		/// <param name="transaction">������������</param>
		/// <param name="sqlKey">ΪResources file �еģӣѣ����Ĺؼ���</param>
		/// <returns></returns>
		//
		DataSet executeDataSet(IDbTransaction transaction, string sqlKey);

		//
		/// <summary>
		/// ִ�в�ѯ����������������ݼ�
		/// </summary>
		/// <param name="transaction">������������</param>
		/// <param name="sqlKey">ΪResources file �еģӣѣ����Ĺؼ���</param>
		/// <param name="parames">����ģӣѣ̲���</param>
		/// <returns></returns>
		//
		DataSet executeDataSet(IDbTransaction transaction, string sqlKey, string[] parames);
		
		//Update DataSet 
		//
		/// <summary>
		/// �������ݼ�
		/// </summary>
		/// <param name="dataAdapter">�����DataAdapter����</param>
		/// <param name="ds"></param>
		//
		void updateDataSet(IDbDataAdapter dataAdapter, DataSet ds);		
	}

}
