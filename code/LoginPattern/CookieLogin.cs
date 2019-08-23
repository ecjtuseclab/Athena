using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Web;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：Cookie登录模式（周庆）
//最后修改时间:2017-01-26
//修改日志：2017-01-26 添加此类（周庆）

namespace LoginPattern
{
    public class CookieLogin : Login
    {
        /// <summary>
        /// 设置指定登录模式的username与password的cookie值
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">用户密码</param>
        public void SetLoginPatternMessage(string username, string password)
        {
            System.Web.HttpCookie unamecookie = new HttpCookie("username");
            unamecookie.Value = username;
            unamecookie.Expires = DateTime.Now.AddDays(1);
            System.Web.HttpContext.Current.Response.Cookies.Add(unamecookie);
            System.Web.HttpCookie psdcookie = new HttpCookie("password");
            psdcookie.Value = password;
            psdcookie.Expires = DateTime.Now.AddDays(1);
            System.Web.HttpContext.Current.Response.Cookies.Add(psdcookie);
        }
        /// <summary>
        /// 清楚指定登录模式的username与password的cookie值
        /// </summary>
        public void ClearLoginPattern()
        {
            System.Web.HttpCookie usercookie = HttpContext.Current.Request.Cookies["username"];
            //usercookie.Expires = DateTime.Now.AddDays(-1);
            System.Web.HttpContext.Current.Response.Cookies.Add(usercookie);
            System.Web.HttpCookie psdcookie = HttpContext.Current.Request.Cookies["password"];
            //psdcookie.Expires = DateTime.Now.AddDays(-1);
            System.Web.HttpContext.Current.Response.Cookies.Add(psdcookie);
        }
        /// <summary>
        /// 判断指定登录模式的username与password的信息是否存在
        /// </summary>
        /// <returns></returns>
        public bool IsLoginPatternMessageExist()
        {
            bool flag = false;
            if (HttpContext.Current.Request.Cookies["username"] != null && HttpContext.Current.Request.Cookies["password"] != null)
            {
                
                flag = true;
            }
            return flag;
        }
    }
}
