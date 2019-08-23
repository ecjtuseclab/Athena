using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Athena.bll;
using System.Text;
using System.Web.Mvc;
using Athena.common.Util;
//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：web控制器基类（周庆）
//最后修改时间:2018-01-26
//修改日志：
//2017-04-11 添加此类（周庆）
//2018-01-26 添加相关返回信息处理方法（周庆）
namespace Web
{
    public class WebController: baseController 
    {
        protected AccountsPrincipal accctx = null;//当前用户上下文对象
        protected List<string> noauth_actions = new List<string>();//不需要验证的方法列表,默认情况下方法都是要验证的，不需要验证的方法这里添加
        protected override IAsyncResult BeginExecute(System.Web.Routing.RequestContext requestContext, AsyncCallback callback, object state)
        {
            if (requestContext.HttpContext.Session["accctx"] != null)//判断用户上下文对象是否不为空
            {
                accctx = (AccountsPrincipal)requestContext.HttpContext.Session["accctx"];
               //   Response.Redirect ("/mymaimai.aspx", false);
            }
            else//用户上下文对象为空时跳转到登入页面
            {
                requestContext.HttpContext.Response.Redirect("/Login/Index"); 
            }
            return base.BeginExecute(requestContext, callback, state);
        }
        //动作执行的前置判断（判断当前用户是否有操作权限）
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string actionName = filterContext.ActionDescriptor.ActionName;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string controllerActionName = controllerName + actionName;
            //判断是否有权执行控制器的方法
            if ((noauth_actions.Contains(controllerActionName)) || ((accctx != null) && (accctx.HasActionPermission(actionName,controllerName))))
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                Response.Redirect("~/Login/Index");//无权访问控制器方法时跳转默认页面（可跳转指定错误页）
            }
        }
        //[HttpGet]
        //public virtual ActionResult Index()
        //{
        //    return View();
        //}
        //[HttpGet]
        //public virtual ActionResult Form()
        //{
        //    return View();
        //}
        //[HttpGet]
        //public virtual ActionResult Details()
        //{
        //    return View();
        //}

    }
}