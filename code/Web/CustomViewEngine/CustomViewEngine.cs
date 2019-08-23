using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员： 周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：自定义视图引擎，用于自定义主体的切换
//最后修改时间:2018-01-25
//修改日志：
//2018-01-26 新增此类 （周庆）

namespace Web.CustomView
{     
    public class CustomViewEngine : RazorViewEngine
    {
       
        public CustomViewEngine(string theme)
        {
            FormatLocations(base.AreaViewLocationFormats, theme);
            FormatLocations(base.AreaMasterLocationFormats, theme);
            FormatLocations(base.AreaPartialViewLocationFormats, theme);
            FormatLocations(base.ViewLocationFormats, theme);
            FormatLocations(base.MasterLocationFormats, theme);
            FormatLocations(base.PartialViewLocationFormats, theme);
            FormatLocations(base.MasterLocationFormats, theme);
        }

        private void FormatLocations(string[] locationFormats, string theme)
        {
            for (int i = 0; i < locationFormats.Length; i++)
            {
                locationFormats[i] = locationFormats[i].Replace("/Views/", string.Format("/Views/{0}/", theme));
            }
        }
        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            return base.FindView(controllerContext, viewName, masterName, useCache);
        }
    }
}