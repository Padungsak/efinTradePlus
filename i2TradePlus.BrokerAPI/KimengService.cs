using i2TradePlus.WebProxy;
using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace i2TradePlus.BrokerAPI
{
	public class KimengService
	{
		private Https kimengAPI;

		public string _keServer = string.Empty;

		[MethodImpl(MethodImplOptions.NoInlining)]
		public KimengService()
		{
			try
			{
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public KimengWSResult GetCustomerGrade(string userID, string session, string productName)
		{
			KimengWSResult kimengWSResult = default(KimengWSResult);
			KimengWSResult result;
			try
			{
				this.kimengAPI = new Https();
				string kE_Server = ApplicationInfo.KE_Server;
				string xml = this.kimengAPI.Get(string.Concat(new string[]
				{
					kE_Server,
					"/keapi/get_cust_grade.asp?Userid=",
					userID,
					"&Sid=",
					session,
					"&Product=",
					productName
				}));
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(xml);
				kimengWSResult.Code = xmlDocument.GetElementsByTagName("res_code").Item(0).InnerText.ToString();
				if (kimengWSResult.Code == "0")
				{
					kimengWSResult.AccountNO = xmlDocument.GetElementsByTagName("Inv_no").Item(0).InnerText;
					kimengWSResult.CustomerGrade = xmlDocument.GetElementsByTagName("user_grade").Item(0).InnerText;
					kimengWSResult.Description = "Successed";
				}
				else
				{
					kimengWSResult.Description = xmlDocument.GetElementsByTagName("res_msg").Item(0).InnerText;
				}
				result = kimengWSResult;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
	}
}
