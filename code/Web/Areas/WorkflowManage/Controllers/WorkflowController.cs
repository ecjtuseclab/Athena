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
using Athena.tool.ACEPagination;
using Athena.bll.WorkflowVisualization;
using Athena.common.Util;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 张婷婷 王露 陈祚松 艾美珍 张梦丽 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：工作流控制器
//最后修改时间：2018-01-26
//修改日志：

namespace Web.Areas.WorkflowManage.Controllers
{
    public class WorkflowController : WebController
    {
        public WorkflowController()
        {
            noauth_actions.Add("WorkflowIndex");
            noauth_actions.Add("WorkflowForm");
            noauth_actions.Add("WorkflowGetGridJson");
            noauth_actions.Add("WorkflowDetails");
            noauth_actions.Add("WorkflowGetFormJson");
            noauth_actions.Add("WorkflowSubmitForm");
            noauth_actions.Add("WorkflowDeleteForm");
            noauth_actions.Add("WorkflowWorkflowVisualization");
            noauth_actions.Add("WorkflowProcessDesign");
            #region ACE
            noauth_actions.Add("WorkflowGetModels");
            noauth_actions.Add("WorkflowAdd");
            noauth_actions.Add("WorkflowUpdate");
            noauth_actions.Add("WorkflowDelete");
            #endregion

        }
        public override ActionResult Index()
        {

            return View();
        }



        #region BootstrapTemplate
        /// <summary>
        /// 获取表格的json数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetGridJson(Pagination pagination, string keyword, string _search, string filters)
        {
            pagination.records = IdalCommon.IworkflowEx.getWorkflowList(keyword).Count;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("wfname", keyword);
            List<workflow> workflowlist = IdalCommon.IworkflowEx.getPaginationEntityList(pagination.page, pagination.rows, dictionary);
            var data = new
            {
                rows = workflowlist,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
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
            var data = IdalCommon.IworkflowEx.getWorkflow(id);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 提交表单数据
        /// </summary>
        /// <param name="workflow">分组对象</param>
        /// <param name="keyValue">分组对象的主键</param>
        /// <returns></returns>
        public ActionResult SubmitForm(workflow workflow, string keyValue)
        {
            IdalCommon.IworkflowEx.SubmitForm(workflow, keyValue);
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
            IdalCommon.IworkflowEx.delete(key);
            return Success("删除成功。");
        }
        /// <summary>
        /// 复制粘贴
        /// </summary>
        /// <param name="keyValue">分组对象的主键</param>
        /// <returns></returns>
        public ActionResult copyAndPasteForm(string keyValue)
        {
            IdalCommon.IworkflowEx.copyAndPaste(keyValue);
            return Success("粘贴成功。");
        }
        #endregion
        #region ACE
        public ActionResult GetModels(ACEPagination pagination, string keyword)
        {
            var data = IdalCommon.IworkflowEx.getEntityList();
            PagedData<workflow> pagedData = IdalCommon.IworkflowEx.GetPageData(pagination, keyword);
            return this.SuccessData(pagedData);
        }
        public ActionResult Add(workflow input)
        {
            IdalCommon.IworkflowEx.insert(input);
            return this.AddSuccessMsg();
        }
        public ActionResult Update(workflow input)
        {
            IdalCommon.IworkflowEx.update(input);
            return this.UpdateSuccessMsg();
        }
        public ActionResult Delete(string id)
        {
            int delid = Convert.ToInt32(id);
            //IdalCommon.IworkflowEx.delete(delid);
            return this.DeleteSuccessMsg();
        }


        #endregion

        public ActionResult WorkflowVisualization()
        {
            object wfid = this.ControllerContext.RequestContext.HttpContext.Request["wfid"];
            if (wfid != null)
            {
                workflow wf = IdalCommon.IworkflowEx.getEntityById(Convert.ToInt32(wfid));
                this.ViewBag.workflow = wf;

                string resutlt = "{}";
                if (!string.IsNullOrEmpty(wf.wfjsonstr))
                {
                    resutlt = wf.wfjsonstr.TrimStart('"').TrimEnd('"');
                }
                this.ViewBag.workflowjsonstr = resutlt;
                this.ViewBag.actionlist = wfselectoption.CreateList(IdalCommon.IactionEx.getActionListbywfid(Convert.ToInt32(wfid)), "id", "actiondescription");
                this.ViewBag.rolelist = wfselectoption.CreateList(IdalCommon.IroleEx.getEntityList(), "id", "rolename");
            }
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ProcessDesign()
        {
            try
            {
                string workflowJson = this.Request["workflowJson"];
                WorkflowPanel wp = Athena.common.Util.Json.ToObject<WorkflowPanel>(workflowJson);
                string primitiveJSonStr = this.Request["primitiveJSonStr"];
                wp.workflowjson = primitiveJSonStr;
                bool result = wp.Save();
                return Json(new { result = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return this.SuccessMsg();
            }
        }
    }

}
