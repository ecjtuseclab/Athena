using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;

namespace Athena.common.Util.Web
{
    public class WebHelper
    {
        static string[] BeReplacedStrs = new string[] { ".com", ".cn", ".com.cn", ".edu.cn", ".net.cn", ".org.cn", ".co.jp", ".gov.cn", ".co.uk", "ac.cn", ".edu", ".tv", ".info", ".ac", ".ag", ".am", ".at", ".be", ".biz", ".bz", ".cc", ".de", ".es", ".eu", ".fm", ".gs", ".hk", ".in", ".info", ".io", ".it", ".jp", ".la", ".md", ".ms", ".name", ".net", ".nl", ".nu", ".org", ".pl", ".ru", ".sc", ".se", ".sg", ".sh", ".tc", ".tk", ".tv", ".tw", ".us", ".co", ".uk", ".vc", ".vg", ".ws", ".il", ".li", ".nz" };

        /// <summary>
        /// 从 cookieName 中获取有效的 FormsAuthenticationTicket
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="checkExpiration">是否验证票据的有效期</param>
        /// <returns></returns>
        public static FormsAuthenticationTicket GetTicketByCookieName(string cookieName, bool checkExpiration = false)
        {
            FormsAuthenticationTicket authTicket = null;

            HttpRequest request = HttpContext.Current.Request;
            HttpCookie authCookie = request.Cookies[cookieName];
            if (authCookie != null && !string.IsNullOrEmpty(authCookie.Value))
            {
                try
                {
                    authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                }
                catch (CryptographicException)
                {
                    return null;
                }
                if (checkExpiration && authTicket.Expired)
                {
                    return authTicket = null;
                }
            }

            return authTicket;
        }
        public static string CreateEncryptedTicket(string name, DateTime expiration, string userData, bool isPersistent = false)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, name, DateTime.Now, expiration, false, userData);
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);

            return encryptedTicket;
        }

        /// <summary>
        /// 从 cookieName 中获取有效 FormsAuthenticationTicket 的 Name，如果获取 Ticket 失败，则返回 null
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="checkExpiration">是否验证票据的有效期</param>
        /// <returns></returns>
        public static string GetTicketNameByCookieName(string cookieName, bool checkExpiration = false)
        {
            FormsAuthenticationTicket authTicket = GetTicketByCookieName(cookieName, checkExpiration);

            if (authTicket != null)
            {
                return authTicket.Name;
            }

            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="allowCrossAccess">是否允许跨域访问</param>
        /// <returns></returns>
        public static HttpCookie CreateCookie(string name, string value = null, bool allowCrossAccess = false)
        {
            HttpCookie cookie = new HttpCookie(name, value);

            if (allowCrossAccess == true)
            {
                HttpRequest request = HttpContext.Current.Request;
                string cookieDomain = WebHelper.GetDomain(request.Url.Host);
                if (!string.IsNullOrEmpty(cookieDomain))
                {
                    cookie.Domain = cookieDomain;
                }
            }

            return cookie;
        }
        public static void SetCookie(HttpCookie cookie)
        {
            HttpContext.Current.Response.AppendCookie(cookie);
        }
        public static void SetCookie(string name, string value, DateTime? expires = null, bool httpOnly = true)
        {
            HttpCookie cookie = CreateCookie(name, value);

            if (expires != null)
                cookie.Expires = expires.Value;

            cookie.HttpOnly = httpOnly;

            SetCookie(cookie);
        }
        public static HttpCookie GetCookie(string name)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[name];
            return cookie;
        }


        public static string GetSessionId()
        {
            return HttpContext.Current.Session.SessionID;
        }
        public static void SetSession(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }
        public static object GetSession(string key)
        {
            return HttpContext.Current.Session[key];
        }
        public static T GetSession<T>(string key)
        {
            object val = HttpContext.Current.Session[key];
            T ret = (T)val;
            return ret;
        }
        public static void RemoveSession(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }

        public static string GetDomain(string url)
        {
            string host;
            Uri uri;
            try
            {
                uri = new Uri(url);

                if (uri.HostNameType != UriHostNameType.Dns)
                    return string.Empty;

                host = uri.Host + " ";
            }
            catch
            {
                return string.Empty;
            }

            foreach (string oneBeReplacedStr in BeReplacedStrs)
            {
                string BeReplacedStr = oneBeReplacedStr + " ";
                if (host.IndexOf(BeReplacedStr) != -1)
                {
                    host = host.Replace(BeReplacedStr, string.Empty);
                    break;
                }
            }

            int dotIndex = host.LastIndexOf(".");
            host = uri.Host.Substring(dotIndex + 1);
            return host;
        }
        /// <summary>
        /// 是否是Ajax请求
        /// </summary>
        /// <returns></returns>
        public static bool IsAjax()
        {
            return HttpContext.Current.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
        /// <summary>
        /// 获取当前请求url中的虚拟目录部分，如设 url=http://192.168.1.105:81/Chloe/wiki/TT1，如果应用程序部署的虚拟目录是 "/WebApp",则返回  http://192.168.1.105:81/Chloe ，如果应用程序部署的虚拟目录是 "/"，则返回  http://192.168.1.105:81
        /// </summary>
        /// <returns></returns>
        public static string GetRequestUrlApplicationPath()
        {
            HttpRequest request = HttpContext.Current.Request;
            Uri uri = request.Url;

            string applicationPath = request.ApplicationPath == "/" ? "" : request.ApplicationPath;

            //http://192.168.1.105/WebApp
            string u = string.Format("{0}://{1}{2}", uri.Scheme, uri.Authority, applicationPath);

            return u;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>http://192.168.1.105:81</returns>
        static string GetRequestUrlAuthority()
        {
            HttpRequest request = HttpContext.Current.Request;
            Uri uri = request.Url;

            //return http://192.168.1.105:81
            string u = string.Format("{0}://{1}", uri.Scheme, uri.Authority);

            return u;
        }
        /// <summary>
        /// 传入一个以"~/"开头的虚拟路径，构建一个完整的 url
        /// </summary>
        /// <param name="virtualPath">以"~/"开头的虚拟路径</param>
        /// <returns>virtualPath 为空或不以"~/"开头返回 virtualPath，否则返回一个完整的 url</returns>
        public static string ConstructFullUrl(string virtualPath)
        {
            if (string.IsNullOrEmpty(virtualPath) || !virtualPath.StartsWith("~/"))
            {
                return virtualPath;
            }

            return GetRequestUrlApplicationPath() + virtualPath.Substring(1);
        }

        public static string GetUserIP()
        {
            /* http://www.blogjava.net/Todd/archive/2009/10/09/297590.html 有关 HTTP_X_FORWARDED_FOR、HTTP_VIA、REMOTE_ADDR的介绍 */

            string customerIP = null;

            //customerIP = System.Web.HttpContext.Current.Request.Headers["Cdn-Src-Ip"]; //Cdn-Src-Ip 不知道是什么，先不管

            customerIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];//REMOTE_ADDR 如果获得真实IP，但如果经过代理，则会获得代理Ip

            if (string.IsNullOrEmpty(customerIP) || string.Equals(customerIP, "unknown", StringComparison.InvariantCultureIgnoreCase))
            {
                customerIP = System.Web.HttpContext.Current.Request.UserHostAddress;
            }

            /* HTTP_X_FORWARDED_FOR 可以伪造，极其不可靠：http://www.jb51.net/article/21161.htm */
            //if (string.IsNullOrEmpty(customerIP))
            //{
            //    customerIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            //}

            if (customerIP == "::1")
                customerIP = "127.0.0.1";

            return customerIP;
        }

    }
}