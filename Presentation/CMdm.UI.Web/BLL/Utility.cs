using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CMdm.UI.Web.BLL
{
      
        public class UtilityClass
        {

            public UtilityClass()
            { }
            public string GetUserIP()
            {

                string ipList = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (!string.IsNullOrEmpty(ipList))
                {
                    return ipList.Split(',')[0];
                }

                return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            public string ConnectionString { get { return ConfigurationManager.ConnectionStrings["ConnectionStringCDMA"].ConnectionString; } set { ConnectionString = value; } }

            //public static string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionStringCDMA"].ConnectionString;
            //"User Id=cdms;Password=cdms;Data Source=127.0.0.1:1521/cdmsdb";
            public string TitleCase(string x)
            {
                return new string(CharsToTitleCase(x).ToArray());
            }
            IEnumerable<char> CharsToTitleCase(string s)
            {
                bool newWord = true;
                foreach (char c in s)
                {
                    if (newWord) { yield return Char.ToUpper(c); newWord = false; }
                    else yield return Char.ToLower(c);
                    if (c == ' ') newWord = true;
                }
            }
        }
   }