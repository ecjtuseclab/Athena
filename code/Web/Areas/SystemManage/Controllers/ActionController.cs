using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Athena.model;
using Athena.basedal;
using Athena.Idal;
using Athena.dal;
using Athena.common.Util.WebControl;
using Athena.tool.Code;
using Athena.tool.ACEPagination;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 张婷婷 王露 陈祚松 艾美珍 张梦丽 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：动作控制器，添加ACE相关方法
//最后修改时间：2018-01-25
//修改日志：
//2018-01-25 新增前端为ACE相关的方法（周庆）

namespace Web.Areas.SystemManage.Controllers
{
    public class ActionController : WebController
    {
        public ActionController()
        {
            #region ACE
            this.noauth_actions.Add("ActionIndex");
            //this.noauth_actions.Add("ActionAction");
            this.noauth_actions.Add("ActionGetModels");
            this.noauth_actions.Add("ActionGetDocument");
            this.noauth_actions.Add("ActionAdd");
            this.noauth_actions.Add("ActionUpdate");
            this.noauth_actions.Add("ActionDelete");
            #endregion
            #region  BootstrapTemplate
            this.noauth_actions.Add("ActionGetFormJson");
            this.noauth_actions.Add("ActionGetTreeSelectJson");
            this.noauth_actions.Add("ActionGetGridJson");
            this.noauth_actions.Add("ActionForm");
            this.noauth_actions.Add("ActionDetails");
            this.noauth_actions.Add("ActionGetFormJson");
            this.noauth_actions.Add("ActioncopyAndPasteForm");
            this.noauth_actions.Add("ActionSubmitForm");
            this.noauth_actions.Add("ActioncopyAndPasteForm");
            this.noauth_actions.Add("ActionDeleteForm");
            #endregion
        }
        //public override ActionResult Index()
        //{
        //    return View();
        //}

        public override ActionResult Index()
        {
            try
            {
                List<action> aa = IdalCommon.IactionEx.getEntityList();
                List<SelectOption> actionList = SelectOption.CreateList(IdalCommon.IactionEx.getEntityList(), "id", "actionname");
                this.ViewBag.ActionList = actionList;

                this.ViewBag.ActionListString = actionList.ToJson();
                //string str = actionList.ToJson();                
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        IactionEx actionex = new actionEx();

        [HttpGet]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            try
            {
                //int pageSize = Convert.ToInt32(ctx.Request["rows"]);
                //int pageIndex = Convert.ToInt32(ctx.Request["page"]);
                pagination.records = actionex.getActionList(keyword).Count;
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("actionname", keyword);
                List<action> actionlist = actionex.getPaginationEntityList(pagination.page, pagination.rows, dictionary);
                var data = new
                {
                    rows = actionlist,
                    total = pagination.total,
                    page = pagination.page,
                    records = pagination.records
                };
                return Content(data.ToJson());
            }
            catch (Exception ex)
            {
                return Content(ex.ToJson());
            }
        }


        public ActionResult GetFormJson(string keyValue)
        {
            var data = IdalCommon.IactionEx.getEntityById(Convert.ToInt32(keyValue));
            return Content(data.ToJson());
        }


        public ActionResult SubmitForm(action ac, string keyValue)
        {
            actionex.SubmitForm(ac, keyValue);
            return Success("操作成功。");
        }

        [HttpGet]
        public ActionResult GetTreeSelectJson()
        {
            var data = IdalCommon.IresourceEx.getEntityList();
            var treeList = new List<TreeSelectModel>();
            foreach (resource item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.id.ToString();
                treeModel.text = item.resourcename;
                treeModel.parentId = item.resourceowner;
                treeModel.data = item;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }


        public ActionResult DeleteForm(string keyValue)
        {
            int key = Convert.ToInt32(keyValue);
            actionex.delete(key);
            return Success("删除成功。");
        }


        public ActionResult copyAndPasteForm(string keyValue)
        {
            IdalCommon.IactionEx.copyAndPaste(keyValue);
            return Success("粘贴成功。");
        }

        #region ACE
        public ActionResult Action(string id)
        {
            ActionModel act = new ActionModel();

            List<SelectOption> actionList = SelectOption.CreateList(IdalCommon.IactionEx.getEntityList(), "id", "actionname");
            this.ViewBag.ActionList = actionList;

            this.ViewBag.ActionListString = actionList.ToJson();
            if (id != null)
            {
                var aa = IdalCommon.IactionEx.getEntityById(int.Parse(id));
                act.id = aa.id.ToString();
                act.actionname = aa.actionname;
                act.actioncode = aa.actioncode;
                act.controlername = aa.controlername;
                act.actionurl = aa.actionurl;
                act.actionparam = aa.actionparam;
                act.actionowner = aa.actionowner;
                act.MarkdownCode = null;
            }
            this.ViewBag.Act = act;

            return View();
        }
        //获取action表的所有数据
        public ActionResult GetModels(ACEPagination pagination, string keyword)//根据搜索框的keyword值获取数据
        {
            //var data = IdalCommon.IactionEx.getEntityList();
            //return this.SuccessData(data);
            PagedData<action> pageData = IdalCommon.IactionEx.GetPageData(pagination, keyword);
            return this.SuccessData(pageData);
        }
        public ActionResult GetDocument(string id)
        {
            action aa = IdalCommon.IactionEx.getEntityById(int.Parse(id));
            return this.SuccessData(aa);
        }

        //新增数据
        public ActionResult Add(action input)
        {
            IdalCommon.IactionEx.insert(input);
            return this.AddSuccessMsg();
        }
        //修改数据
        public ActionResult Update(action input)
        {
            IdalCommon.IactionEx.update(input);
            return this.UpdateSuccessMsg();
        }
        //删除数据
        public ActionResult Delete(string id)
        {
            int delid = Convert.ToInt32(id);
            IdalCommon.IactionEx.delete(delid);
            return this.DeleteSuccessMsg();
        }

        public class ActionModel
        {
            public string id { get; set; }/* 由于 */
            public string actionname { get; set; }
            public string actionowner { get; set; }
            public int actioncode { get; set; }
            public string actionurl { get; set; }
            public string actionparam { get; set; }
            public string controlername { get; set; }
            public string MarkdownCode { get; set; }
        }
        #endregion
    }
}
