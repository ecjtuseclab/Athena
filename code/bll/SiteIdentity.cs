using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using Athena.Idal;
using Athena.model;
using Athena.basedal;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：登录用户身份信息
//最后修改时间:2017-04-01
//修改日志：

namespace Athena.bll
{
    public class SiteIdentity:IIdentity
    {
        user webuser = new user();
        private IuserEx _IuserEx;
        public IuserEx IuserEx
        {
            get
            {
                if (_IuserEx == null)
                {
                    _IuserEx = IocModule.GetEntity<IuserEx>();
                }
                return _IuserEx;
            }
        }
        #region  用户属性
        private int userid;       //用户id
        private string username;   //用户名
        private string password;  //用户口令
        private string pubkey;    //用户公钥
        private string prvkey;    //用户私钥

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get
            {
                return username;
            }
        }
        /// <summary>
        /// 用户公钥
        /// </summary>
        public string PubKey
        {
            get
            {
                return pubkey;
            }
        }
        /// <summary>
        /// 用户私钥
        /// </summary>
        public string PrvKey
        {
            get
            {
                return prvkey;
            }
        }
        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserID
        {
            get
            {
                return userid;
            }
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get
            {
                return password;
            }
        }

        #endregion

        #region IIdentity 接口中要求实现的:

        /// <summary>
        /// 当前用户的名称
        /// </summary>
        public string Name
        {
            get
            {
                return username;
            }
        }

        /// <summary>
        /// 获取所使用的身份验证的类型。
        /// </summary>
        public string AuthenticationType
        {
            get
            {
                return "Custom Authentication";
            }
            set
            {
                // do nothing
            }
        }
        /// <summary>
        /// 是否验证了用户
        /// </summary>
        public bool IsAuthenticated
        {
            get
            {
                return true;
            }
        }
        #endregion

        /// <summary>
        /// 根据用户名构造
        /// </summary>
        public SiteIdentity(string currentUserName)
        {
            user u = IuserEx.getUser(currentUserName) as user;
            if (u != null)
            {
                username = currentUserName;
                userid = u.id;
                pubkey = u.pubkey;
                prvkey = u.prikey;
                password = u.password;
                webuser = u;
            }

        }
        /// <summary>
        /// 根据用户ID构造
        /// </summary>
        public SiteIdentity(int currentUserID)
        {
            user u = IuserEx.getEntityById(currentUserID) as user;
            if (u != null)
            {
                username = u.username;
                userid = currentUserID;
                pubkey = u.pubkey;
                prvkey = u.prikey;
                password = u.password;
                webuser = u;
            }

        }
        /// <summary>
        /// 检查当前用户对象密码
        /// </summary>
        public bool CheckPassword(string password)
        {
            return IuserEx.checkPassword(username, password);
        }
    }
}
