using i2TradePlus.Properties;
using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace i2TradePlus.WebProxy
{
	public class Https
	{
		public delegate void OnErrorHandler(Exception ex);

		public delegate void OnTransferHandler(string text);

		public delegate void OnEndTransferHandler(int direction);

		public delegate void OnStartTransferHandler(int direction);

		public delegate void OnSSLServerAuthenticationHandler(HttpsSSLServerAuthenticationEventArgs e);

		private int readBufferSize = 1024;

		private int timeOut = 15000;

		private WebProxy proxy = null;

		[CompilerGenerated]
		private static RemoteCertificateValidationCallback <>9__CachedAnonymousMethodDelegate1;

		public event Https.OnErrorHandler OnError
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this.OnError = (Https.OnErrorHandler)Delegate.Combine(this.OnError, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this.OnError = (Https.OnErrorHandler)Delegate.Remove(this.OnError, value);
			}
		}

		public event Https.OnTransferHandler OnTransfer
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this.OnTransfer = (Https.OnTransferHandler)Delegate.Combine(this.OnTransfer, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this.OnTransfer = (Https.OnTransferHandler)Delegate.Remove(this.OnTransfer, value);
			}
		}

		public event Https.OnEndTransferHandler OnEndTransfer
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this.OnEndTransfer = (Https.OnEndTransferHandler)Delegate.Combine(this.OnEndTransfer, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this.OnEndTransfer = (Https.OnEndTransferHandler)Delegate.Remove(this.OnEndTransfer, value);
			}
		}

		public event Https.OnStartTransferHandler OnStartTransfer
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this.OnStartTransfer = (Https.OnStartTransferHandler)Delegate.Combine(this.OnStartTransfer, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this.OnStartTransfer = (Https.OnStartTransferHandler)Delegate.Remove(this.OnStartTransfer, value);
			}
		}

		public event Https.OnSSLServerAuthenticationHandler OnSSLServerAuthentication
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this.OnSSLServerAuthentication = (Https.OnSSLServerAuthenticationHandler)Delegate.Combine(this.OnSSLServerAuthentication, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this.OnSSLServerAuthentication = (Https.OnSSLServerAuthenticationHandler)Delegate.Remove(this.OnSSLServerAuthentication, value);
			}
		}

		public int ReadBufferSize
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.readBufferSize;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.readBufferSize = value;
			}
		}

		public int TimeOut
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.timeOut;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.timeOut = value;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Get(string URL)
		{
			CookieContainer cookies = new CookieContainer();
			HttpWebRequest webRequest = this.GetWebRequest(new Uri(URL), cookies);
			ServicePointManager.ServerCertificateValidationCallback = ((object obj, X509Certificate x509Certificate, X509Chain x509Chain, SslPolicyErrors sslPolicyErrors) => true);
			string result;
			try
			{
				if (this.OnStartTransfer != null)
				{
					this.OnStartTransfer(1);
				}
				HttpWebResponse httpWebResponse = (HttpWebResponse)webRequest.GetResponse();
				Stream responseStream = httpWebResponse.GetResponseStream();
				StreamReader streamReader = new StreamReader(responseStream);
				char[] array = new char[this.ReadBufferSize];
				int i = streamReader.Read(array, 0, this.ReadBufferSize);
				string text = string.Empty;
				while (i > 0)
				{
					string text2 = new string(array, 0, i);
					text += text2;
					i = streamReader.Read(array, 0, this.ReadBufferSize);
					if (this.OnTransfer != null)
					{
						this.OnTransfer(text2);
					}
				}
				streamReader.Close();
				responseStream.Close();
				httpWebResponse.Close();
				if (this.OnEndTransfer != null)
				{
					this.OnEndTransfer(1);
				}
				result = text;
			}
			catch (Exception ex)
			{
				if (this.OnError != null)
				{
					this.OnError(ex);
				}
				throw ex;
			}
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private HttpWebRequest GetWebRequest(Uri uri, CookieContainer cookies)
		{
			HttpWebRequest result;
			try
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
				httpWebRequest.CookieContainer = cookies;
				httpWebRequest.Timeout = this.TimeOut;
				httpWebRequest.ReadWriteTimeout = 10000;
				httpWebRequest.KeepAlive = true;
				httpWebRequest.ProtocolVersion = HttpVersion.Version11;
				if (ApplicationInfo.IsUseProxy)
				{
					if (this.proxy == null)
					{
						this.proxy = new WebProxy(Settings.Default.ProxyHost, Settings.Default.ProxyPort);
						this.proxy.Credentials = new NetworkCredential(Settings.Default.ProxyUsername, ApplicationInfo.ProxyPassword);
					}
					httpWebRequest.Proxy = this.proxy;
				}
				result = httpWebRequest;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool CertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			HttpsSSLServerAuthenticationEventArgs httpsSSLServerAuthenticationEventArgs = new HttpsSSLServerAuthenticationEventArgs();
			if (this.OnSSLServerAuthentication != null)
			{
				this.OnSSLServerAuthentication(httpsSSLServerAuthenticationEventArgs);
			}
			return httpsSSLServerAuthenticationEventArgs.Accept;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public string GetCookies(string URL)
		{
			string path = Environment.GetFolderPath(Environment.SpecialFolder.Cookies) + "\\";
			Uri uri = new Uri(URL);
			string dnsSafeHost = uri.DnsSafeHost;
			string searchPattern = string.Empty;
			string text = Environment.UserName.ToLower();
			IPAddress iPAddress;
			if (IPAddress.TryParse(dnsSafeHost, out iPAddress))
			{
				string[] array = dnsSafeHost.Split(new char[]
				{
					'.'
				});
				searchPattern = string.Concat(new object[]
				{
					text,
					'@',
					array[0],
					'.',
					array[1],
					'.',
					array[2],
					'*'
				});
			}
			else
			{
				string[] array2 = dnsSafeHost.Split(new char[]
				{
					'.'
				});
				searchPattern = string.Concat(new object[]
				{
					text,
					'@',
					'*',
					array2[1],
					'*'
				});
			}
			string[] files = Directory.GetFiles(path, searchPattern);
			string result;
			if (files != null && files.Length > 0)
			{
				string text2 = File.ReadAllText(files[0]);
				string[] array3 = text2.Split(new char[]
				{
					'\n'
				});
				if (array3 != null && array3.Length > 2)
				{
					result = array3[1];
				}
				else
				{
					result = string.Empty;
				}
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public Https()
		{
		}
	}
}
