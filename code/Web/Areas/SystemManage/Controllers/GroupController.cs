using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Athena.model;
using Athena.basedal;
using Athena.Idal;
using Athena.dal;
using Athena.tool.Code;
using Athena.tool.ACEPagination;
using Athena.common.Util.WebControl;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：分组控制器（周庆）
//最后修改时间:2018-01-25
//修改日志：
//2017-12-19 新增前端为ACE相关的方法（王露）

namespace Web.Areas.SystemManage.Controllers
{
    public class GroupController : WebController
    {
        public GroupController()
        {
            this.noauth_actions.Add("GroupGetGridJson");
            this.noauth_actions.Add("GroupDetails");
            this.noauth_actions.Add("GroupGetFormJson");
            this.noauth_actions.Add("GroupSubmitForm");
            this.noauth_actions.Add("GroupDeleteForm");
            this.noauth_actions.Add("GroupcopyAndPasteForm");
            this.noauth_actions.Add("GroupForm");
            #region ACE
            this.noauth_actions.Add("GroupIndex");
            this.noauth_actions.Add("GroupGetModels");
            this.noauth_actions.Add("GroupAdd");
            this.noauth_actions.Add("GroupUpdate");
            this.noauth_actions.Add("GroupDelete");
            #endregion
        }
        
        //ACE Index
        public override ActionResult Index()
        {
            List<SelectOption> departmentList = SelectOption.CreateList(IdalCommon.IgroupEx.getEntityList());

            this.ViewBag.DepartmentList = departmentList;

            this.ViewBag.DepartmentListString = departmentList.ToJson();
            //this.ViewBag.DepartmentListString = JsonHelper.Serialize(departmentList);
            string str = departmentList.ToJson();
            return View();
        }

        /// <summary>
        /// 获取表格的json数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            pagination.records = IdalCommon.IgroupEx.getGroupList(keyword).Count;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("groupname", keyword);
            List<group> grouplist = IdalCommon.IgroupEx.getPaginationEntityList(pagination.page, pagination.rows, dictionary);
            var data = new
            {
                rows = grouplist,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            string str = data.ToJson();
            return Content(data.ToJson());
        }
        /// <summary>
        /// 查看角色详情
        /// </summary>
        /// <returns></returns>
        public override ActionResult Details()
        {
            return View();
        }
        /// <summary>
        /// 获取表单提交的数据的json格式
        /// </summary>
        /// <param name="keyValue">分组对象的主键</param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            int id = Convert.ToInt32(keyValue);
            var data = IdalCommon.IgroupEx.getGroup(id);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 提交表单数据
        /// </summary>
        /// <param name="group">分组对象</param>
        /// <param name="keyValue">分组对象的主键</param>
        /// <returns></returns>
        public ActionResult SubmitForm(group group, string keyValue)
        {
            IdalCommon.IgroupEx.SubmitForm(group, keyValue);
            return Success("操作成功。");
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue">分组对象的主键</param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            int key = Convert.ToInt32(keyValue);
            IdalCommon.IgroupEx.delete(key);
            return Success("删除成功。");
        }
        /// <summary>
        /// 复制粘贴
        /// </summary>
        /// <param name="keyValue">分组对象的主键</param>
        /// <returns></returns>
        public ActionResult copyAndPasteForm(string keyValue)
        {
            IdalCommon.IgroupEx.copyAndPaste(keyValue);
            return Success("粘贴成功。");
        }
        #region ACE
        [HttpGet]
        public ActionResult GetModels(ACEPagination pagination, string keyword, string filters)
        {
            PagedData<group> pagedData = new PagedData<group>();
            if (!string.IsNullOrEmpty(filters))
            {
                filters = SqlFilter.SqlFilters(filters);   //过滤特殊字符
                pagedData = IdalCommon.IgroupEx.mutiplySearch(filters, pagination, "group");
            }
            else
            {
                pagedData = IdalCommon.IgroupEx.GetPageData(pagination, keyword);
            }
            return this.SuccessData(pagedData);
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="page">分页信息</param>
        /// <param name="keyword">关键词</param>
        /// <returns>返回分页数据</returns>
        //public PagedData<group> GetPageData(Pagination page, string keyword)
        //{
        //    var pagedData = IdalCommon.IgroupEx.TakePageData(page);//查询相关的字段，根据页面的指定的字段
        //    return pagedData;
        //}
       // [HttpPost]
        public ActionResult Add(group g)
        {
            if(IdalCommon.IgroupEx.isLeagalGroupname(g.groupname,(int)g.groupcode))
            IdalCommon.IgroupEx.insert(g);
            return this.AddSuccessMsg();
        }
        //[HttpPost]
        public ActionResult Update(group g)
        {
            if (IdalCommon.IgroupEx.isLeagalGroupname(g.groupname, (int)g.groupcode))
            IdalCommon.IgroupEx.update(g);
            return this.UpdateSuccessMsg();
        }

        //[HttpPost]
        public ActionResult Delete(group g)
        {

            IdalCommon.IgroupEx.delete(g);
            return this.DeleteSuccessMsg();
        }
        #endregion
    }
}
