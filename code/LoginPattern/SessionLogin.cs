using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：session登录模式（周庆）
//最后修改时间:2017-12-18
//修改日志：

namespace LoginPattern
{
    public class SessionLogin:Login
    {
        static string unamesession = null;
       
        static string psdsession = null;
        /// <summary>
        /// 设置指定登录模式的username与password的Session值
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">用户密码</param>
        public void SetLoginPatternMessage(string username, string password)
        {
            System.Web.HttpContext.Current.Session["username"] = username;
            System.Web.HttpContext.Current.Session["password"] = password;
            unamesession = System.Web.HttpContext.Current.Session["username"].ToString();
            psdsession = System.Web.HttpContext.Current.Session["password"].ToString();
            
        }
        /// <summary>
        /// 清楚指定登录模式的username与password的Session值
        /// </summary>
        public void ClearLoginPattern()
        {
            System.Web.HttpContext.Current.Session["username"] = null;
            System.Web.HttpContext.Current.Session["password"] = null;
            unamesession = null;
            psdsession = null;
        }
        /// <summary>
        /// 判断指定登录模式的username与password的信息是否存在
        /// </summary>
        /// <returns></returns>
        public bool IsLoginPatternMessageExist()
        {
            bool flag = false;
            if (unamesession!=null && psdsession!= null)
                flag = true;
            return flag;
        }
    }
}
