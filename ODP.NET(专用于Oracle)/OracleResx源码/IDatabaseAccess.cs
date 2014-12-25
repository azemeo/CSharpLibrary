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
		/// 获得SQL语句
		/// </summary>
		/// <param name="sqlKey">为Resources file 中的ＳＱＬ语句的关键字</param>
		/// <returns></returns>
		string getCommandText(string sqlKey);

		/// <summary>
		/// 获得SQL语句
		/// </summary>
		/// <param name="sqlKey">为Resources file 中的ＳＱＬ语句的关键字</param>
		/// <param name="parames">当有参数时传入的参数数组</param>
		/// <returns></returns>
		string getCommandText(string sqlKey,string[] parames);

		//
		/// <summary>
		///获得数据库的连接串
		/// </summary>
		IDbConnection getConnection();
		IDbConnection getConnection(string ConnectionString);

		/// <summary>
		/// 的到conn的事务
		/// </summary>
		/// <param name="conn"></param>
		/// <returns></returns>
		IDbTransaction getTransaction(IDbConnection connString);

		//
		/// <summary>
		///执行查询操作 返回单个值，建议此方法不使用，只有在使用复杂的ＳＱＬ语句的时候使用此方法
		/// </summary>
		/// <param name="commandText">传入的ＳＱＬ命令语句</param>
		/// <returns></returns>
		//
		int executeNoQueryCmdString(string commandText);

		//exexcute no query SQL
		/// <summary>
		/// 执行非查询操作
		/// </summary>
		/// <param name="sqlKey">为Resources file 中的ＳＱＬ语句的关键字</param>
		/// <returns></returns>
		/// 
		int executeNonQuery(string sqlKey);

		//
		/// <summary>
		/// 执行非查询操作
		/// </summary>
		/// <param name="sqlKey">为Resources file 中的ＳＱＬ语句的关键字</param>
		/// <param name="parames">当有参数时传入的参数数组</param>
		/// <returns></returns>
		//
		int executeNonQuery(string sqlKey, string[] parames);

		//execute no query SQL with Transaction 

		//
		/// <summary>
		/// 执行事务非查询操作
		/// </summary>
		/// <param name="transaction">传入的事务</param>
		/// <param name="sqlKey">为Resources file 中的ＳＱＬ语句的关键字</param>
		/// <returns></returns>
		//
		int  executeNonQuery(IDbTransaction transaction, string sqlKey);

		/// <summary>
		/// 执行事务非查询操作
		/// </summary>
		/// <param name="transaction">传入的事务</param>
		/// <param name="sqlKey">为Resources file 中的ＳＱＬ语句的关键字</param>
		/// <param name="parames">当有参数时传入的参数数组</param>
		/// <returns></returns>
		int  executeNonQuery(IDbTransaction transaction ,string sqlKey,string[] parames);

		//execute query.return single object like string ,date, integer.

		//
		/// <summary>
		///执行查询操作 返回单个值，建议此方法不使用，只有在使用复杂的ＳＱＬ语句的时候使用此方法
		/// </summary>
		/// <param name="commandText">传入的ＳＱＬ命令语句</param>
		/// <returns></returns>
		//
		object executeQueryCmdString(string commandText);

		//
		/// <summary>
		/// 执行查询操作 返回单个值
		/// </summary>
		/// <param name="sqlKey">为Resources file 中的ＳＱＬ语句的关键字</param>
		/// <returns></returns>
		//
		object executeQuery(string sqlKey);

		//
		/// <summary>
		/// 执行查询操作 返回单个值
		/// </summary>
		/// <param name="sqlKey">为Resources file 中的ＳＱＬ语句的关键字</param>
		/// <param name="parames">传入的语句参数</param>
		/// <returns></returns>
		//
		object executeQuery(string sqlKey,string[] parames);

        

		//execute query. return DataSet

		/// <summary>
		/// 执行查询操作 返回数据集，建议此方法不使用，只有在使用复杂的ＳＱＬ语句的时候使用此方法
		/// </summary>
		/// <param name="commandText">ＳＱＬ命令语句</param>
		/// <returns></returns>
		DataSet executeDataSetCmdString(string commandText);
	
		//
		/// <summary>
		/// 执行查询操作，返回数据集
		/// </summary>
		/// <param name="sqlKey">为Resources file 中的ＳＱＬ语句的关键字</param>
		/// <returns></returns>
		//
		DataSet executeDataSet(string sqlKey);
	
		//
		/// <summary>
		/// 执行查询操作，返回数据集
		/// </summary>
		/// <param name="sqlKey">为Resources file 中的ＳＱＬ语句的关键字</param>
		/// <param name="parames">为传入的ＳＱＬ参数</param>
		/// <returns></returns>
		//
		DataSet executeDataSet(string sqlKey, string[] parames);

		/// <summary>
		/// 执行查询操作，返回数据集
		/// </summary>
		/// <param name="sqlKey">为Resources file 中的ＳＱＬ语句的关键字</param>
		/// <param name="myDataSetGet">传入的DataSet参数</param>
		/// <param name="tableName">表名称</param>
		/// <returns></returns>
		DataSet executeDataSet(string sqlKey, DataSet myDataSetGet, string tableName);

		/// <summary>
		/// 执行查询操作，返回数据集
		/// </summary>
		/// <param name="sqlKey">为Resources file 中的ＳＱＬ语句的关键字</param>
		/// <param name="parames">传入的ＳＱＬ参数</param>
		/// <param name="myDataSetGet">传入的DataSet参数</param>
		/// <param name="tableName">表名称</param>
		/// <returns></returns>
		DataSet executeDataSet(string sqlKey, string[] parames, DataSet myDataSetGet, string tableName);

		//
		/// <summary>
		/// 执行查询事务操作，返回数据集
		/// </summary>
		/// <param name="transaction">传入的事务参数</param>
		/// <param name="sqlKey">为Resources file 中的ＳＱＬ语句的关键字</param>
		/// <returns></returns>
		//
		DataSet executeDataSet(IDbTransaction transaction, string sqlKey);

		//
		/// <summary>
		/// 执行查询事务操作，返回数据集
		/// </summary>
		/// <param name="transaction">传入的事务参数</param>
		/// <param name="sqlKey">为Resources file 中的ＳＱＬ语句的关键字</param>
		/// <param name="parames">传入的ＳＱＬ参数</param>
		/// <returns></returns>
		//
		DataSet executeDataSet(IDbTransaction transaction, string sqlKey, string[] parames);
		
		//Update DataSet 
		//
		/// <summary>
		/// 更新数据集
		/// </summary>
		/// <param name="dataAdapter">传入的DataAdapter参数</param>
		/// <param name="ds"></param>
		//
		void updateDataSet(IDbDataAdapter dataAdapter, DataSet ds);		
	}

}
