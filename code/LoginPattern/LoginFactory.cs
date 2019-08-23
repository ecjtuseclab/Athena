using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：登录模式工厂（周庆）
//最后修改时间:2017-12-18
//修改日志：

namespace LoginPattern
{
   public class LoginFactory
    {
        /// <summary>
        /// 获取登录类型
        /// </summary>
        /// <param name="logintype">登录类型</param>
        /// <returns></returns>
        public Login GetLoginType(string logintype)
        {
            if (logintype == null)
            {
                return null;
            }
            else if (logintype == "Cookie")
            {
                CookieLogin col = new CookieLogin();
                return col;
            }
            else if (logintype == "Session")
            {
                SessionLogin sel = new SessionLogin();
                return sel;
            }
            return null;
        }
    }
}
 