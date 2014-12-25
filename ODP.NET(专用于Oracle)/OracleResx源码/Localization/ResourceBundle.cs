using System;
using System.Globalization;
using System.Resources;

namespace ZeroWire.Shared.Localization
{
	/// <summary>
	/// 多语言支持 Create:Jason.Wang 05/03/27 Update:05/04/27 
	/// v1.001 SP1
	/// 1. 修正对 SQL 资源读取支持
	/// 2. 对语言资源读取只允许进行一次，不允许重复进行，且错误抛出
	/// 3. 对 SQL 资源读取只允许进行一次，不允许重复进行，但暂不做错误抛出
	/// </summary>
	/// <remarks>
	/// 调用范例:
	/// 1.程式载入时的初始化设置
	///		ZeroWire.Shared.Localization.ResourceBundle.DefaultConfigCulture = new CultureInfo("en-US")
	///		ZeroWire.Shared.Localization.ResourceBundle.DefaultResourceManager = ?
	///		(CultureInfo)CultureInfo.CurrentUICulture.Clone()  // 与系统区域语言一致
	///		WebApplication 资源调用方式
	///		new System.Resources.ResourceManager("your.Resources.strings",System.Reflection.Assembly.GetExecutingAssembly())
	
	/// 2.方法调用
	///		ResourceBundle.CultureStringRes("APPLICATION_TITLE",null,null));
	/// </remarks>
	public class ResourceBundle
	{
		// internal fields setting language culture
		// 区域标识
		static CultureInfo DefaultCulture = (CultureInfo)CultureInfo.CurrentUICulture.Clone();
		
		// 语言资源集
		static System.Resources.ResourceManager resManager = null;
		
		// SQL 资源集
		static System.Resources.ResourceManager resSQLMgr = null;

		/// <summary>
		/// Culture displays.
		/// </summary>
		public enum CultureType
		{
			English = 0,
			Chinese = 1
		}

		/// <summary>
		/// 多语言支持
		/// </summary>
		public ResourceBundle()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		/// <summary>
		/// 程式载入时调用,设定语言,默认为系统语言 (CultureInfo)CultureInfo.CurrentUICulture.Clone()
		/// </summary>
		/// <param name="CurrentCulture">
		///  语言标识 范例:new System.Globalization.CultureInfo("en-US")
		///  唯一区域定义格式: 主语言标识(小写) en - 大写字母为亚文化代码(大写) US
		///  </param>
		static public void SetConfigCulture(CultureInfo CurrentCulture)
		{
			DefaultCulture = CurrentCulture;
		}

		/// <summary>
		///  程式载入时调用, 设置多语言的引用资源
		/// </summary>
		/// <param name="defaultResManager">范例:new System.Resources.ResourceManager("your.resources",this.GetType().Assembly)</param>
		static public void SetResourceManager(ResourceManager defaultResManager)
		{
			if (resManager != null) return; // throw new Exception("Resource Already Existent!");
			resManager = defaultResManager;
		}

		/// <summary>
		///  程式载入时调用, 设置多语言的 SQL 引用资源
		/// </summary>
		/// <param name="defaultResManager">范例:new System.Resources.ResourceManager("your.resources.OrclLiteSql",this.GetType().Assembly)</param>
		static public void SetSQLResourceManager(ResourceManager defaultResManager)
		{
			if (resSQLMgr != null) return;
			resSQLMgr = defaultResManager;
		}
		
		/// <summary>
		/// 返回当前区域语言标识
		/// </summary>
		/// <returns>当前区域语言标识</returns>
		static public CultureInfo GetCurrentCulture()
		{
			return DefaultCulture;
		}
		
		#region SQL 资源调用
		/// <summary>
		/// 取得 SQL 资源
		/// </summary>
		/// <param name="ResKey">资源标识 Ex. COMMON_OK</param>
		/// <returns>结果资源</returns>
		public static string GetSQL(string ResKey)
		{
			return GetSQLRes(ResKey,null);
		}

		public static string GetSQL(string ResKey,object ParaObj)
		{
			return GetSQLRes(ResKey,ParaObj);
		}

		private static string GetSQLRes(string ResKey,object ParaObj)
		{
			string retval = string.Empty;

			if (resSQLMgr == null) throw new NullReferenceException("Resources Not Exist");
			try
			{
				if ((object)(retval = resSQLMgr.GetString(ResKey)) == null)
					retval = "";
			}
			catch(Exception)
			{
				throw new Exception("Localization.TranslateRes");
			}

			if (ParaObj != null)
			{
				try
				{
					retval = string.Format(retval,(string[])ParaObj);
				}
				catch(Exception)
				{
					throw new ArgumentException("Paramter Absent");
				}
			}
			return retval;
		}

		#endregion

		#region 语言翻译重载调用
		/// <summary>
		/// 区域化字符串资源
		/// </summary>
		/// <param name="ResKey">资源标识 Ex. COMMON_OK</param>
		/// <returns>结果资源</returns>
		public static string CultureStringRes(string ResKey)
		{
			return CultureStringRes(ResKey,null,null);
		}
		
		/// <summary>
		/// 区域化字符串资源
		/// </summary>
		/// <param name="ResKey">资源标识 Ex. COMMON_OK</param>
		/// <param name="CurrentCulture">强制区域资源定义</param>
		/// <returns>结果资源</returns>
		public static string CultureStringRes(string ResKey,CultureInfo CurrentCulture)
		{
			return CultureStringRes(ResKey,null,CurrentCulture);
		}

		/// <summary>
		/// 区域化字符串资源
		/// </summary>
		/// <param name="ResKey">资源标识 Ex. "COMMON_OK"</param>
		/// <param name="ParaObj">参数 string[] = {?,?,...}</param>
		/// <returns>结果资源</returns>
		public static string CultureStringRes(string ResKey,object ParaObj)
		{
			return CultureStringRes(ResKey,ParaObj,null);
		}


		#endregion

		/// <summary>
		/// 区域化字符串资源
		/// </summary>
		/// <param name="ResKey">资源标识 Ex. COMMON_OK</param>
		/// <param name="ParaObj">参数 string[] = {?,?,...}</param>
		/// <param name="CurrentCulture">强制区域资源定义</param>
		/// <returns>结果资源</returns>
		public static string CultureStringRes(string ResKey,object ParaObj,CultureInfo CurrentCulture)
		{
			return TranslateRes(ResKey,ParaObj,CurrentCulture);
		}

		// 本单元需充分考虑多语言支持的灵活性
		// 1.支持使用系统的区域性设置
		// 2.支持强定义使用某区域语言
		private static string TranslateRes(string ResKey,object ParaObj, CultureInfo CurrentCulture)
		{
			string retval = string.Empty;
			CurrentCulture = CurrentCulture == null ? DefaultCulture : CurrentCulture;
			if (CurrentCulture == null) throw new NullReferenceException("CultureInfo Parameter Not Define");
			if (resManager == null) throw new NullReferenceException("Resources Not Exist");
			
			try
			{
				if ((object)(retval = resManager.GetString(ResKey, CurrentCulture)) == null)
					retval = "";
			}
			catch(Exception)
			{
				throw new Exception("Localization.TranslateRes");
			}

			if (ParaObj != null)
			{
				try
				{
					retval = string.Format(retval,(string[])ParaObj);
				}
				catch(Exception)
				{
					throw new ArgumentException("Paramter Absent");
				}
			}

			return retval;

		}
	}
	
}
