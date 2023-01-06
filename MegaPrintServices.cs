using System;
using System.IO;
using System.Net;
using System.Text;
using MegaPrintProxy.Models;

namespace MegaPrintProxy.Services
{
    public class WebClient
    {
		private String str2base64(String str64)
		{
			System.Byte[] bytes = System.Convert.FromBase64String(str64);

			return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
		}
		
        public EstadoServicio Post(ServiceParams serviceParams)
        {
			var request = (HttpWebRequest)WebRequest.Create(serviceParams.url);		
			var postdata = str2base64(serviceParams.body);
			Exception ex;

			var data = Encoding.UTF8.GetBytes(postdata);

			request.Method = "POST";
			request.ContentType = "application/xml";
			request.ContentLength = data.Length;

			if (!String.IsNullOrWhiteSpace(serviceParams.token))
			{
				request.Headers.Add(HttpRequestHeader.Authorization,"Bearer " + serviceParams.token);
			}
			try
			{
				using (var stream = request.GetRequestStream())
				{
					stream.Write(data, 0, data.Length);
				}

				var response = (HttpWebResponse)request.GetResponse();
				String responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
				return new EstadoServicio() { statusOk = true, response = responseString, msgerror = "" };
			}
			catch (Exception ex2)
			{
				ex = ex2;
				return new EstadoServicio() { statusOk = false, response = "", msgerror = ex.ToString() };
			}
        }
	}
}
