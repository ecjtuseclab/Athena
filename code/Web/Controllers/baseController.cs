using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Athena.bll;
using Athena.common.Util;
using System.Text;
using Athena.common.Util.Web;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：控制器基类（周庆）
//最后修改时间:2018-01-26
//修改日志：
//2017-04-11 添加此类（周庆）
//2018-01-26 添加相关返回信息处理方法（周庆）

namespace Web
{
    public class baseController:Controller 
    {
        public virtual void view()
        {
            
        }
        protected HttpContextBase ctx = null;
        protected HttpContext hctx = null;//http上下文对象
        protected AccountsPrincipal acctx = null;//当前用户上下文对象
        protected static VelocityHelper tp = new VelocityHelper(@"~/Views");//模板静态对象
        protected override IAsyncResult BeginExecute(System.Web.Routing.RequestContext requestContext, AsyncCallback callback, object state)
        {
            if (requestContext.HttpContext != null)
            {
                this.ctx = requestContext.HttpContext;//获取http内容上下文}
            }
            return base.BeginExecute(requestContext, callback, state);
        } 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="msg"></param>
        /// <param name="flag"></param>
        protected static void WriteBackHtml(HttpContextBase context, string msg, bool flag)
        {
            string success = "true";
            if (!flag)
            {
                success = "false";
            }
            Dictionary<string, string> result = new Dictionary<string, string>();
            result.Add("success", success);
            result.Add("msg", msg);
            string strJson = Athena.common.Util.Json.ToJson(result);
            writeJsonBack(context, strJson);

        }

        #region ace中使用
        protected ContentResult JsonContent(object obj)
        {
            string json = Athena.common.Util.Json.ToJson(obj);
            return base.Content(json);
        }
        protected ContentResult SuccessData(object data = null)
        {
            Result<object> result = Result.CreateResult<object>(ResultStatus.OK, data);
            return this.JsonContent(result);
        }
        //获取json格式的分页形式数据
        protected string SuccessDatajson(object data = null)
        {
            Result<object> result = Result.CreateResult<object>(ResultStatus.OK, data);
            string json = Athena.common.Util.Json.ToJson(result);
            //return this.JsonContent(result);
            return json;
        } 
        #endregion
        protected virtual void WriteBackHtml(string msg, bool flag)
        {
            WriteBackHtml(ctx, msg, flag);
        }
        /// <summary>
        /// 向前端UI回写返回信息
        /// </summary>
        /// <param name="context">http上下文</param>
        /// <param name="strJson">json语句</param>
        protected static void writeJsonBack(HttpContextBase context, string strJson)
        {
            context.Response.Clear();
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.ContentType = "application/json";
            context.Response.Write(strJson);
            context.Response.Flush();
            context.ApplicationInstance.CompleteRequest();
            //context.Response.End();
        }
        protected virtual void writeJsonBack(string strJson)
        {
            writeJsonBack(ctx, strJson);
        }
        /// <summary>
        /// 向前端UI回写返回信息,按照内容类型排列的 Mime 类型列表中的写法来进行
        ///参考格式：http://www.cnblogs.com/huanhuan86/archive/2012/06/12/2546362.html
        /// </summary>
        /// <param name="context">http上下文</param>
        /// <param name="strHtml">html语句</param>
        protected static void writeHtmlBack(HttpContextBase context, string strHtml)
        {
            context.Response.Clear();
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.ContentType = "text/html";
            context.Response.Write(strHtml);
            context.Response.Flush();
            context.ApplicationInstance.CompleteRequest();
            //context.Response.End();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strHtml"></param>
        protected virtual void writeHtmlBack(string strHtml)
        {
            writeHtmlBack(ctx, strHtml);
        }

        
        #region 后来添加
        /// <summary>
        /// 查看详情
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Details()
        {
            return View();
        }

        /// <summary>
        /// 查看数据
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获得表单
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Form()
        {
            return View();
        }
        protected virtual ActionResult Success(string message)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = message }.ToJson());
        }
        protected virtual ActionResult Success(string message, object data)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = message, data = data }.ToJson());
        }
        protected virtual ActionResult Error(string message)
        {
            return Content(new AjaxResult { state = ResultType.error.ToString(), message = message }.ToJson());
        }
        #endregion
        #region ACE
        protected ContentResult SuccessMsg(string msg = null)
        {
            Result result = new Result(ResultStatus.OK, msg);
            return this.JsonContent(result);
        }
        protected ContentResult AddSuccessMsg(string msg = "添加成功")
        {
            return this.SuccessMsg(msg);
        }
        protected ContentResult UpdateSuccessMsg(string msg = "更新成功")
        {
            return this.SuccessMsg(msg);
        }
        protected ContentResult DeleteSuccessMsg(string msg = "删除成功")
        {
            return this.SuccessMsg(msg);
        }
        protected ContentResult FailedMsg(string msg = null)
        {
            Result retResult = new Result(ResultStatus.Failed, msg);
            return this.JsonContent(retResult);
        }
        #endregion
    }
}