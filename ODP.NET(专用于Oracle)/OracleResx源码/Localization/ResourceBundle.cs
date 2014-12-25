using System;
using System.Globalization;
using System.Resources;

namespace ZeroWire.Shared.Localization
{
	/// <summary>
	/// ������֧�� Create:Jason.Wang 05/03/27 Update:05/04/27 
	/// v1.001 SP1
	/// 1. ������ SQL ��Դ��ȡ֧��
	/// 2. ��������Դ��ȡֻ�������һ�Σ��������ظ����У��Ҵ����׳�
	/// 3. �� SQL ��Դ��ȡֻ�������һ�Σ��������ظ����У����ݲ��������׳�
	/// </summary>
	/// <remarks>
	/// ���÷���:
	/// 1.��ʽ����ʱ�ĳ�ʼ������
	///		ZeroWire.Shared.Localization.ResourceBundle.DefaultConfigCulture = new CultureInfo("en-US")
	///		ZeroWire.Shared.Localization.ResourceBundle.DefaultResourceManager = ?
	///		(CultureInfo)CultureInfo.CurrentUICulture.Clone()  // ��ϵͳ��������һ��
	///		WebApplication ��Դ���÷�ʽ
	///		new System.Resources.ResourceManager("your.Resources.strings",System.Reflection.Assembly.GetExecutingAssembly())
	
	/// 2.��������
	///		ResourceBundle.CultureStringRes("APPLICATION_TITLE",null,null));
	/// </remarks>
	public class ResourceBundle
	{
		// internal fields setting language culture
		// �����ʶ
		static CultureInfo DefaultCulture = (CultureInfo)CultureInfo.CurrentUICulture.Clone();
		
		// ������Դ��
		static System.Resources.ResourceManager resManager = null;
		
		// SQL ��Դ��
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
		/// ������֧��
		/// </summary>
		public ResourceBundle()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		/// <summary>
		/// ��ʽ����ʱ����,�趨����,Ĭ��Ϊϵͳ���� (CultureInfo)CultureInfo.CurrentUICulture.Clone()
		/// </summary>
		/// <param name="CurrentCulture">
		///  ���Ա�ʶ ����:new System.Globalization.CultureInfo("en-US")
		///  Ψһ�������ʽ: �����Ա�ʶ(Сд) en - ��д��ĸΪ���Ļ�����(��д) US
		///  </param>
		static public void SetConfigCulture(CultureInfo CurrentCulture)
		{
			DefaultCulture = CurrentCulture;
		}

		/// <summary>
		///  ��ʽ����ʱ����, ���ö����Ե�������Դ
		/// </summary>
		/// <param name="defaultResManager">����:new System.Resources.ResourceManager("your.resources",this.GetType().Assembly)</param>
		static public void SetResourceManager(ResourceManager defaultResManager)
		{
			if (resManager != null) return; // throw new Exception("Resource Already Existent!");
			resManager = defaultResManager;
		}

		/// <summary>
		///  ��ʽ����ʱ����, ���ö����Ե� SQL ������Դ
		/// </summary>
		/// <param name="defaultResManager">����:new System.Resources.ResourceManager("your.resources.OrclLiteSql",this.GetType().Assembly)</param>
		static public void SetSQLResourceManager(ResourceManager defaultResManager)
		{
			if (resSQLMgr != null) return;
			resSQLMgr = defaultResManager;
		}
		
		/// <summary>
		/// ���ص�ǰ�������Ա�ʶ
		/// </summary>
		/// <returns>��ǰ�������Ա�ʶ</returns>
		static public CultureInfo GetCurrentCulture()
		{
			return DefaultCulture;
		}
		
		#region SQL ��Դ����
		/// <summary>
		/// ȡ�� SQL ��Դ
		/// </summary>
		/// <param name="ResKey">��Դ��ʶ Ex. COMMON_OK</param>
		/// <returns>�����Դ</returns>
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

		#region ���Է������ص���
		/// <summary>
		/// �����ַ�����Դ
		/// </summary>
		/// <param name="ResKey">��Դ��ʶ Ex. COMMON_OK</param>
		/// <returns>�����Դ</returns>
		public static string CultureStringRes(string ResKey)
		{
			return CultureStringRes(ResKey,null,null);
		}
		
		/// <summary>
		/// �����ַ�����Դ
		/// </summary>
		/// <param name="ResKey">��Դ��ʶ Ex. COMMON_OK</param>
		/// <param name="CurrentCulture">ǿ��������Դ����</param>
		/// <returns>�����Դ</returns>
		public static string CultureStringRes(string ResKey,CultureInfo CurrentCulture)
		{
			return CultureStringRes(ResKey,null,CurrentCulture);
		}

		/// <summary>
		/// �����ַ�����Դ
		/// </summary>
		/// <param name="ResKey">��Դ��ʶ Ex. "COMMON_OK"</param>
		/// <param name="ParaObj">���� string[] = {?,?,...}</param>
		/// <returns>�����Դ</returns>
		public static string CultureStringRes(string ResKey,object ParaObj)
		{
			return CultureStringRes(ResKey,ParaObj,null);
		}


		#endregion

		/// <summary>
		/// �����ַ�����Դ
		/// </summary>
		/// <param name="ResKey">��Դ��ʶ Ex. COMMON_OK</param>
		/// <param name="ParaObj">���� string[] = {?,?,...}</param>
		/// <param name="CurrentCulture">ǿ��������Դ����</param>
		/// <returns>�����Դ</returns>
		public static string CultureStringRes(string ResKey,object ParaObj,CultureInfo CurrentCulture)
		{
			return TranslateRes(ResKey,ParaObj,CurrentCulture);
		}

		// ����Ԫ���ֿ��Ƕ�����֧�ֵ������
		// 1.֧��ʹ��ϵͳ������������
		// 2.֧��ǿ����ʹ��ĳ��������
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
