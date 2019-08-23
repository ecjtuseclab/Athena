using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Athena.model;
using Athena.Idal;
using Athena.basedal;
//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：客户端用户控制器（周庆）
//最后修改时间:2018-01-26
//修改日志：
//2017-04-11 添加此类（周庆）
//2018-01-26 添加相关返回信息处理方法（周庆）
namespace Web.Controllers
{
    public class ClientsDataController : Controller
    {
        //
        // GET: /ClientsData/
        [HttpGet]
        public ActionResult GetClientsDataJson()
        {
            var data = new
            {
                dataItems = "",//this.GetDataItemList(),
                organize = "",//this.GetOrganizeList(),
                role = "",//this.GetRoleList(),
                duty = "",//this.GetDutyList(),
                user = "",
                authorizeMenu = this.GetMenuList(),
                authorizeButton = "",//this.GetMenuButtonList(),
            };
            return Content(data.ToJson());
        }

        private object GetMenuList()
        {
            List<resource> list = IocModule.GetEntity<IresourceEx>().getEntityList();
            //string str = ToMenuJson(list, "0");
            return ToMenuJson(list, "0");
        }

        private string ToMenuJson(List<resource> data, string parentId)
        {
            StringBuilder sbJson = new StringBuilder();
            sbJson.Append("[");
            List<resource> entitys = data.FindAll(t => t.resourceowner == parentId);
            if (entitys.Count > 0)
            {
                foreach (var item in entitys)
                {
                    string strJson = item.ToJson();
                    strJson = strJson.Insert(strJson.Length - 1, ",\"ChildNodes\":" + ToMenuJson(data, item.id.ToString()) + "");
                    sbJson.Append(strJson + ",");
                }
                sbJson = sbJson.Remove(sbJson.Length - 1, 1);
            }
            sbJson.Append("]");
            return sbJson.ToString();
        }

        //private object GetMenuButtonList()
        //{
        //    var roleId = OperatorProvider.Provider.GetCurrent().RoleId;
        //    var data = new RoleAuthorizeApp().GetButtonList(roleId);
        //    var dataModuleId = data.Distinct(new ExtList<ModuleButtonEntity>("F_ModuleId"));
        //    Dictionary<string, object> dictionary = new Dictionary<string, object>();
        //    foreach (ModuleButtonEntity item in dataModuleId)
        //    {
        //        var buttonList = data.Where(t => t.F_ModuleId.Equals(item.F_ModuleId));
        //        dictionary.Add(item.F_ModuleId, buttonList);
        //    }
        //    return dictionary;
        //}
    }
}
