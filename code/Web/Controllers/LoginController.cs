using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Athena.bll;
using System.Configuration;
using LoginPattern;
//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：登录控制器（周庆）
//最后修改时间:2018-01-26
//修改日志：
//2017-04-11 添加此类（周庆）
//2018-01-26 添加相关返回信息处理方法（周庆）
namespace Web.Controllers
{
    public class LoginController :baseController
    {
        //登入初始页面
        public override ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult OutLogin()
        {
            //new LogApp().WriteDbLog(new LogEntity
            //{
            //    F_ModuleName = "系统登录",
            //    F_Type = DbLogType.Exit.ToString(),
            //    F_Account = OperatorProvider.Provider.GetCurrent().UserCode,
            //    F_NickName = OperatorProvider.Provider.GetCurrent().UserName,
            //    F_Result = true,
            //    F_Description = "安全退出系统",
            //});
            Session.Abandon();
            Session.Clear();
            //OperatorProvider.Provider.RemoveCurrent();
            return RedirectToAction("Index", "Login");
        }

        //获取验证码
        [HttpGet]
        public ActionResult GetAuthCode()
        {
            return File(new VerifyCode().GetVerifyCode(), @"image/Gif");
        }

        //登入事件
        [HttpPost, ActionName("CheckLogin")]
        public void CheckLogin()
        {
            
            string msg = string.Empty;
            //检查记录登录次数
            if ((this.ctx.Session["PassErrorCount"] != null) && (this.ctx.Session["PassErrorCount"].ToString() != ""))
            {
                //获取错误登入次数
                int PassErroeCount = Convert.ToInt32(this.ctx.Session["PassErrorCount"]);
                if (PassErroeCount > 3)
                {
                    msg = "登录尝试错误次数超过3次";
                    this.Content(msg);
                    WriteBackHtml(msg, false);
                    return;
                }
            }
            /// 检查验证码
            if ((this.ctx.Request["code"] != null) && (this.ctx.Request["code"].ToString() != ""))
            {
                if (Md5.md5(ctx.Request["code"].ToString().ToLower(), 16).ToString().ToLower() != this.ctx.Session["Athena_session_verifycode"].ToString().ToLower())
                {
                    msg = "验证码填写错误 !";
                    WriteBackHtml(msg, false);
                    return;
                }
            }
            string userName = this.ctx.Request["username"];
            string passWord = this.ctx.Request["password"];
            AccountsPrincipal accctx = AccountsPrincipal.ValidateLogin(userName, passWord);
            string loginpattern = ConfigurationManager.AppSettings["LoginPattern"];
            LoginFactory loginfactory=new LoginFactory();
            Login lo=loginfactory.GetLoginType(loginpattern);
            if (lo.IsLoginPatternMessageExist())
            {
                this.ctx.User = accctx;
                this.ctx.Session["accctx"] = accctx;
                lo.ClearLoginPattern();
                msg = "登录成功，页面跳转中!";
                WriteBackHtml(msg, true);//在页面上可以以红字提醒，JS直接direct跳转到下一个链接
            }
            else
            {
                if (accctx == null)//登录信息不对
                {
                    msg = "登陆失败： " + userName;
                    if ((this.ctx.Session["PassErrorCount"] != null) && (this.ctx.Session["PassErrorCount"].ToString() != ""))
                    {
                        int PassErroeCount = Convert.ToInt32(this.ctx.Session["PassErrorCount"]);
                        this.ctx.Session["PassErrorCount"] = PassErroeCount + 1;
                    }
                    else
                    {
                        this.ctx.Session["PassErrorCount"] = 1;
                    }
                }
                else
                {
                    lo.SetLoginPatternMessage(userName, passWord);
                    //把当前用户对象实例赋给Context.User，这样做将会把完整的用户信息加载到ASP.NET提供的验证体系中
                    this.ctx.User = accctx;
                    this.ctx.Session["accctx"] = accctx;
                    msg = "登录成功，页面跳转中!";
                    WriteBackHtml(msg, true);//在页面上可以以红字提醒，JS直接direct跳转到下一个链接
                }
            }
        }   

        //登出事件
        public ActionResult Logout()
        {
            this.ctx.Session["PassErrorCount"] = null;
            this.ctx.Session["accctx"] = null;
            //清除session值
            //System.Web.HttpContext.Current.Session["username"] = null;
            //System.Web.HttpContext.Current.Session["password"] = null;
            return RedirectToAction("Index");
        }
    }
}