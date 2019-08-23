using System;
using System.Web;
using System.IO;
using NVelocity;
using NVelocity.App;
using NVelocity.Context;
using NVelocity.Runtime;
using Commons.Collections;
using System.Text;
///////////////////////////////////////////////////
//项目名称：腾杰超轻量级开发框架
//开发者：limingzuo
//模块名称和描述：
//最后更新时间：2017/3/5
//日志说明：
//////////////////////////////////////////////
namespace Athena.common.Util
{


    /// <summary>
    /// NVelocity模板工具类 VelocityHelper
    /// </summary>
    public class VelocityHelper
    {
        private VelocityEngine velocity = null;
        private IContext context = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="templatDir">模板文件夹路径</param>
        public VelocityHelper(string templatDir)
        {
            Init(templatDir);
        }

        /// <summary>
        /// 无参数构造函数
        /// </summary>
        public VelocityHelper() { }

        /// <summary>
        /// 初始话NVelocity模块
        /// </summary>
        public void Init(string templatDir)
        {
            //创建VelocityEngine实例对象
            velocity = new VelocityEngine();

            //使用设置初始化VelocityEngine
            ExtendedProperties props = new ExtendedProperties();
            props.AddProperty(RuntimeConstants.RESOURCE_LOADER, "file");
            props.AddProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH, HttpContext.Current.Server.MapPath(templatDir));
            //props.AddProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH, Path.GetDirectoryName(HttpContext.Current.Request.PhysicalPath));
            props.AddProperty(RuntimeConstants.INPUT_ENCODING, "utf-8");
            props.AddProperty(RuntimeConstants.OUTPUT_ENCODING, "utf-8");

            //模板的缓存设置
            props.AddProperty(RuntimeConstants.FILE_RESOURCE_LOADER_CACHE, true);              //是否缓存
            props.AddProperty("file.resource.loader.modificationCheckInterval", (Int64)30);    //缓存时间(秒)

            velocity.Init(props);

            //为模板变量赋值
            context = new VelocityContext();
        }

        /// <summary>
        /// 给模板变量赋值
        /// </summary>
        /// <param name="key">模板变量</param>
        /// <param name="value">模板变量值</param>
        public void Put(string key, object value)
        {
            if (context == null)
                context = new VelocityContext();
            context.Put(key, value);
        }

        /// <summary>
        /// 显示模板
        /// </summary>
        /// <param name="templatFileName">模板文件名</param>
        public void Display(HttpContext httpcontext,string templatFileName)
        {
            //从文件中读取模板
            Template template = velocity.GetTemplate(templatFileName);
            //合并模板
            StringWriter writer = new StringWriter();
            template.Merge(context, writer);
            //输出
            httpcontext.Response.Clear();
            httpcontext.Response.Write(writer.ToString());
            httpcontext.Response.Flush();
            httpcontext.ApplicationInstance.CompleteRequest();
            //httpcontext.Response.End();
        }

        /// <summary>
        /// 根据模板生成静态页面
        /// </summary>
        /// <param name="templatFileName"></param>
        /// <param name="htmlpath"></param>
        public void CreateHtml(string templatFileName, string htmlpath)
        {
            //从文件中读取模板
            Template template = velocity.GetTemplate(templatFileName);
            //合并模板
            StringWriter writer = new StringWriter();
            template.Merge(context, writer);
            //using (StreamWriter write2 = new StreamWriter(HttpContext.Current.Server.MapPath(htmlpath), false, Encoding.UTF8, 200))
            using (StreamWriter write2 = new StreamWriter(htmlpath, false, Encoding.UTF8, 200))
            {
                write2.Write(writer);
                write2.Flush();
                write2.Close();
            }
        }
        /// <summary>
        /// 向前端UI回写返回信息
        /// </summary>
        /// <param name="context">http上下文</param>
        /// <param name="strJson">json语句</param>
        public void writeJson(HttpContext context, string strJson)
        {
            context.Response.Clear();
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.ContentType = "application/json";
            context.Response.Write(strJson);
            context.Response.Flush();
            context.ApplicationInstance.CompleteRequest();
            //context.Response.End();
        }
        /// <summary>
        /// 向前端UI回写返回信息,按照内容类型排列的 Mime 类型列表中的写法来进行
        ///参考格式：http://www.cnblogs.com/huanhuan86/archive/2012/06/12/2546362.html
        /// </summary>
        /// <param name="context">http上下文</param>
        /// <param name="strHtml">html语句</param>
        public void writeHtml(HttpContext context, string strHtml)
        {
            context.Response.Clear();
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.ContentType = "text/html";
            context.Response.Write(strHtml);
            context.Response.Flush();
            context.ApplicationInstance.CompleteRequest();
            //context.Response.End();
        }

    }

}

