using System;
using System.IO;
using System.Windows.Forms;

namespace ZeroWire.Shared.Exceptionhandling
{
	/// <summary>
	/// �쳣ģ�鴦�� Create:Jason.Wang 05/03/24 Update:05/04/16
	/// v1.001 SP1
	/// 1. �������ı����ݵ�д��ͶԿ���̨����ʾ
	/// </summary>
	public class ZwException : System.Exception
	{

		// ���� LOG �洢·�����ļ���
		private static string ConfigTextLogName = "ZeroWire.Log.txt";

		// ������ѶϢ�洢�����ݿ��
		private object ExceptionLogToDB = null;

		public void SetExceptionLogToDB(object LogToDB)
		{
			// ��������־д�����Ͽ�
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
		/// �׳�����
		/// </summary>
		/// <param name="ex">����Exception</param>
		/// <param name="caller">������ʹ�� this</param>
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

		#region �ж� framework ����
		private static bool FULLFRAMEWORK 
		{ 
			get
			{
				return System.Environment.OSVersion.Platform != PlatformID.WinCE;
			} 
		} 
		#endregion 

		#region ȡ�ó���·��
		private static string AppPath()
		{
			string AppPath = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.
				Substring(0, System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.LastIndexOf(@"\")); 
			return AppPath;
		}
		#endregion
	}
}
