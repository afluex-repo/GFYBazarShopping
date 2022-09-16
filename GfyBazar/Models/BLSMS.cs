using System;
using System.Configuration;
using System.IO;
using System.Net;

namespace GfyBazar
{
    static public class BLSMS
    {
        static public void SendSms(string mobile, string Message, string TempId)
        {
            try
            {
                string SMSAPI = ConfigurationManager.AppSettings["SMSAPI"].ToString();
                SMSAPI = SMSAPI.Replace("[AND]", "&");
                SMSAPI = SMSAPI.Replace("[Mobile]", mobile);
                SMSAPI = SMSAPI.Replace("[SMS]", Message);
                SMSAPI = SMSAPI.Replace("[TempId]", TempId);
                SMSAPI = SMSAPI.Replace("[Date]", DateTime.Now.ToString());
                HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(new Uri(SMSAPI, false));
                HttpWebResponse httpresponse = (HttpWebResponse)(httpreq.GetResponse());
            }
            catch (Exception)
            {

                throw;
            }
        }
        static public string OrderPlace(string Name,string OrderNo,string Amount)
        {
            string Message = ConfigurationSettings.AppSettings["OrderPlaced"].ToString();
            Message = Message.Replace("[Name]", Name);
            Message = Message.Replace("[OrderNo]",OrderNo);
            Message = Message.Replace("[Amount]", Amount);
            return Message;
        }
        public static string SendSMSNew(string mobile, string msg)
        {
            string strUrl = "http://smsw.co.in/API/WebSMS/Http/v1.0a/index.php?username=dwpe&password=dw@2020pe&sender=DWPEFU&to=" + mobile + "&message=" + msg + "& reqid = 1 & format ={ json}&route_id = 39 & callback =#&unique=0&sendondate='" + DateTime.Now.ToString() + " '";

            WebRequest request = HttpWebRequest.Create(strUrl);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = (Stream)response.GetResponseStream();
            StreamReader readStream = new StreamReader(s);
            string dataString = readStream.ReadToEnd();
            response.Close();
            s.Close();
            readStream.Close();
            return dataString;
        }

        static public string Registration(string MemberName, string LoginId, string Password)
        {
            string Message = ConfigurationSettings.AppSettings["REGISTRATION"].ToString();
            Message = Message.Replace("[Member-Name]", MemberName);
            Message = Message.Replace("[LoginId]", LoginId);
            Message = Message.Replace("[Password]", Password);

            return Message;
        }

        static public string OTP(string MemberName, string OTP)
        {
            string Message = ConfigurationSettings.AppSettings["OTP"].ToString();
            Message = Message.Replace("[Member-Name]", MemberName);
            Message = Message.Replace("[OTP]", OTP);

            return Message;
        }

    }
}
