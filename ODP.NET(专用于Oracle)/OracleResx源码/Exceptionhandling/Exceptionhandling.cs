using System;
using System.IO;
using System.Windows.Forms;

namespace ZeroWire.Shared.Exceptionhandling
{
	/// <summary>
	/// 异常模块处理 Create:Jason.Wang 05/03/24 Update:05/04/16
	/// v1.001 SP1
	/// 1. 修正对文本内容的写入和对控制台的显示
	/// </summary>
	public class ZwException : System.Exception
	{

		// 定义 LOG 存储路径及文件名
		private static string ConfigTextLogName = "ZeroWire.Log.txt";

		// 将错误讯息存储到数据库存
		private object ExceptionLogToDB = null;

		public void SetExceptionLogToDB(object LogToDB)
		{
			// 将错误日志写入资料库
			ExceptionLogToDB = LogToDB;
		}

		public ZwException(string ThrowMessage)
		{
			new ZwException(null,null,ThrowMessage,true);
		}

		public ZwException(Exception ex)
		{
			new ZwException(ex,null,null,false);
		}

		/// <summary>
		/// 抛出错误
		/// </summary>
		/// <param name="ex">错误Exception</param>
		/// <param name="caller">参数请使用 this</param>
		public ZwException(Exception ex, object caller) : base()
		{
			new ZwException(ex,caller,null,false);
		}

		public ZwException(Exception ex, object caller, string Message, bool True)
		{
			string ConfigTextLogPath;

			ConfigTextLogPath = AppPath() + @"\" + ConfigTextLogName;
 
			string ErrorMsg = string.Empty;
			ErrorMsg = ex == null ? Message : ex.ToString();

			Console.WriteLine("ZeroWire " + System.DateTime.Now);
			Console.WriteLine(ex);
			Console.WriteLine("");
			
			string ExceptionMemo = System.DateTime.Now + " " + "Path:" + ConfigTextLogPath + "\r\n " 
						+ "Caller:"+ caller.GetType().FullName.ToString() + " \r\n " 
						+ ex.ToString() + "\r\n\r\n";

			try
			{
				StreamWriter WriteFile = null;

				if (File.Exists(ConfigTextLogPath))
				{
					 WriteFile = File.AppendText(ConfigTextLogPath);	
				}
				else
				{
					WriteFile = File.CreateText(ConfigTextLogPath);
				}

				WriteFile.Write(ExceptionMemo);
				WriteFile.Close();
				
			}
			catch (Exception)
			{
				MessageBox.Show("Write Log Error!");
				// throw new Exception("Write Log Error");
			}

			MessageBox.Show("Handle:" + ConfigTextLogPath + "\r\n Message: " + ErrorMsg);
			// throw new Exception("Handle:" + ConfigTextLogPath + "\r\n Message: " + ErrorMsg);
			
		}

		#region 判断 framework 类型
		private static bool FULLFRAMEWORK 
		{ 
			get
			{
				return System.Environment.OSVersion.Platform != PlatformID.WinCE;
			} 
		} 
		#endregion 

		#region 取得程序路径
		private static string AppPath()
		{
			string AppPath = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.
				Substring(0, System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.LastIndexOf(@"\")); 
			return AppPath;
		}
		#endregion
	}
}
