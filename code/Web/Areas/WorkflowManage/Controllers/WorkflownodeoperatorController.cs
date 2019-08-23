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
//开发团队成员：周庆 张婷婷 王露 陈祚松 艾美珍 张梦丽 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：操作者控制器
//最后修改时间：2018-01-26
//修改日志：

namespace Web.Areas.WorkflowManage.Controllers
{
    public class WorkflownodeoperatorController : WebController 
    {
        public WorkflownodeoperatorController()
        {
            noauth_actions.Add("WorkflownodeoperatorIndex");
            noauth_actions.Add("WorkflownodeoperatorForm");
            noauth_actions.Add("WorkflownodeoperatorDetails");
            noauth_actions.Add("WorkflownodeoperatorGetGridJson");
            noauth_actions.Add("WorkflownodeoperatorGetFormJson");
            noauth_actions.Add("WorkflownodeoperatorSubmitForm");
            noauth_actions.Add("WorkflownodeoperatorDeleteForm");
            noauth_actions.Add("WorkflownodeoperatorcopyAndPasteForm");
            noauth_actions.Add("WorkflownodeoperatorGetSelectNaJson");
            noauth_actions.Add("WorkflownodeoperatorGetSelectUserJson");
            #region ACE
            noauth_actions.Add("WorkflownodeoperatorGetModels");
            noauth_actions.Add("WorkflownodeoperatorAdd");
            noauth_actions.Add("WorkflownodeoperatorUpdate");
            noauth_actions.Add("WorkflownodeoperatorDelete"); 
            #endregion
        }
        public override ActionResult Index()
        {
            //工作流下拉集合
            List<SelectOption> workflowList = SelectOption.CreateList(IdalCommon.IworkflowEx.getEntityList(), "id", "wfmemo");
            this.ViewBag.WorkflowList = workflowList;
            this.ViewBag.WorkflowListString = Athena.common.Util.Json.ToJson(workflowList);

            //工作流节点动作下来项值
            List<workflownodeaction> wfnodeactionList = IdalCommon.IworkflownodeactionEx.getEntityList();
            this.ViewBag.WfnodeactionList = wfnodeactionList;
            //this.ViewBag.WfnodeactionListString = Athena.common.Util.Json.ToJson(wfnodeactionList);
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
            pagination.records = IdalCommon.IworkflownodeoperatorEx.getWorkflowNodeoperatorList(keyword).Count;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("operatortype", keyword);
            List<workflownodeoperator> workflownodeoperatorlist = IdalCommon.IworkflownodeoperatorEx.getPaginationEntityList(pagination.page, pagination.rows, dictionary);
            var data = new
            {
                rows = workflownodeoperatorlist,
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

        public ActionResult GetSelectNaJson(string keyValue)
        {
            List<object> list = new List<object>();
            if (!string.IsNullOrEmpty(keyValue))
            {
                int wfid = int.Parse(keyValue);
                var data = IdalCommon.IworkflownodeactionEx.getEntityById(wfid);
                list.Add(new { id = data.id, text = data.nodeactionname });
            }
            else
            {
                var data = IdalCommon.IworkflownodeactionEx.getEntityList();
                foreach (workflownodeaction item in data)
                {
                    list.Add(new { id = item.id, text = item.nodeactionname });
                }
            }
            return Content(list.ToJson());
        }

        public ActionResult GetSelectUserJson(string keyValue)
        {
            List<object> list = new List<object>();
            if (!string.IsNullOrEmpty(keyValue))
            {
                int wfid = int.Parse(keyValue);
                var data = IdalCommon.IuserEx.getEntityById(wfid);
                list.Add(new { id = data.id, text = data.username });
            }
            else
            {
                var data = IdalCommon.IuserEx.getEntityList();
                foreach (user item in data)
                {
                    list.Add(new { id = item.id, text = item.username });
                }
            }
            return Content(list.ToJson());
        }

        /// <summary>
        /// 获取表单提交的数据的json格式
        /// </summary>
        /// <param name="keyValue">分组对象的主键</param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            int id = Convert.ToInt32(keyValue);
            var data = IdalCommon.IworkflownodeoperatorEx.getWorkflowNodeoperatorEntity(id);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 提交表单数据
        /// </summary>
        /// <param name="workflownodeaction">分组对象</param>
        /// <param name="keyValue">分组对象的主键</param>
        /// <returns></returns>
        public ActionResult SubmitForm(workflownodeoperator workflownodeoperator, string keyValue)
        {
            IdalCommon.IworkflownodeoperatorEx.SubmitForm(workflownodeoperator, keyValue);
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
            IdalCommon.IworkflownodeoperatorEx.delete(key);
            return Success("删除成功。");
        }
        /// <summary>
        /// 复制粘贴
        /// </summary>
        /// <param name="keyValue">分组对象的主键</param>
        /// <returns></returns>sss
        public ActionResult copyAndPasteForm(string keyValue)
        {
            IdalCommon.IworkflownodeoperatorEx.copyAndPaste(keyValue);
            return Success("粘贴成功。");
        }
        #region ACE
         public ActionResult GetModels(ACEPagination pagination, string keyword)
        {
            var data = IdalCommon.IworkflowEx.getEntityList();
            PagedData<workflownodeoperator> pagedData = IdalCommon.IworkflownodeoperatorEx.GetPageData(pagination, keyword);
            return this.SuccessData(pagedData);
            //var data = IdalCommon.IworkflownodeoperatorEx.getEntityList();
            //return this.SuccessData(data);
        }
        public ActionResult Add(workflownodeoperator input)
        {
            IdalCommon.IworkflownodeoperatorEx.insert(input);
            return this.AddSuccessMsg();
        }
        public ActionResult Update(workflownodeoperator input)
        {
            IdalCommon.IworkflownodeoperatorEx.update(input);
            return this.UpdateSuccessMsg();
        }
        public ActionResult Delete(string id)
        {
            int delid = Convert.ToInt32(id);
            IdalCommon.IworkflownodeoperatorEx.delete(delid);
            return this.DeleteSuccessMsg();
        }
        #endregion
    }
}
