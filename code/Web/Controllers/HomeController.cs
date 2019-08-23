using Athena.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.CustomView;
using Athena.basedal;
using System.Text;
using Web;
using Athena.bll;
//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：主页控制器（周庆）
//最后修改时间:2018-01-26
//修改日志：
//2017-04-11 添加此类（周庆）
//2018-01-26 添加相关返回信息处理方法（周庆）
namespace Web.Controllers
{
    public class HomeController : baseController
    {
        /// <summary>
        /// ztt
        /// 默认视图
        /// </summary>
        public override void view()
        {
            tp.Put("currole", acctx.CurrentRole.rolename);
            tp.Put("rolelist", acctx.Roles);
            tp.Put("curuser", acctx.currentuser.username);
            tp.Display(hctx, "Index.cshtml");
        }
        [HttpGet]
        public override ActionResult Index()
         {
            
             if (this.ctx.Session["accctx"] != null)
             {
                 //当前用户拥有的角色
                 this.ViewBag.roles = ((AccountsPrincipal)this.ctx.Session["accctx"]).Roles;
                 //当前用户拥有的动作
                 this.ViewBag.actions = ((AccountsPrincipal)this.ctx.Session["accctx"]).Actions;
                 //当前用户拥有的系统管理菜单
                 this.ViewBag.systemmenus = ((AccountsPrincipal)this.ctx.Session["accctx"]).Menus.Where(a => a.resourceowner == "1").ToList<resource>(); ;
                 this.ViewBag.workflowmenus = ((AccountsPrincipal)this.ctx.Session["accctx"]).Menus.Where(a => a.resourceowner == "3").ToList<resource>(); ;
             }
             else
             {
                 Response.Redirect("~/Login/Index"); 
             }
            return View();
        }

        [HttpGet]
        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Default()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SpareIndex()
        {
            return View();
        }
        public ActionResult TestCustomViewPath(string theme)
        {
            string themeName = string.IsNullOrEmpty(theme) ? "Default" : theme;
            return View(themeName + "/Test"); //注意这里指定了一相对路径
        }
        //ztt
        [HttpPost]
        public ActionResult selectRole(string roid)//切换角色
        {

            int roleid = int.Parse(roid);
            acctx.SetCurrentRole(roleid);
            acctx.Reload();
            hctx.Response.Write(acctx.CurrentRole.rolename);
            return View();
        }
    }
}
