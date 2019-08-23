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
using Athena.common.Util.WebControl;
using Athena.tool.ACEPagination;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 张婷婷 王露 陈祚松 艾美珍 张梦丽 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：工作流节点动作控制器
//最后修改时间：2018-01-26
//修改日志：

namespace Web.Areas.WorkflowManage.Controllers
{
    public class WorkflownodeactionController : WebController
    {
        public WorkflownodeactionController()
        {
            noauth_actions.Add("WorkflownodeactionIndex");
            noauth_actions.Add("WorkflownodeactionGetGridJson");
            noauth_actions.Add("WorkflownodeactionDetails");
            noauth_actions.Add("WorkflownodeactionGetFormJson");
            noauth_actions.Add("WorkflownodeactionSubmitForm");
            noauth_actions.Add("WorkflownodeactionDeleteForm");
            noauth_actions.Add("WorkflownodeactionForm");
            noauth_actions.Add("WorkflownodeactioncopyAndPasteForm");
            noauth_actions.Add("WorkflownodeactionGetSelectJson");
            noauth_actions.Add("WorkflownodeactionGetSelectNodeJson");
            #region ACE
            noauth_actions.Add("WorkflownodeactionGetModels");
            noauth_actions.Add("WorkflownodeactionAdd");
            noauth_actions.Add("WorkflownodeactionUpdate");
            noauth_actions.Add("WorkflownodeactionDelete"); 
            #endregion

        }
        public override  ActionResult Index()
        {
            List<workflownode> workflownodeList = IdalCommon.IworkflownodeEx.getEntityList();
            this.ViewBag.WorkflownodeList = workflownodeList;
            //工作流下拉集合
            List<SelectOption> workflowList = SelectOption.CreateList(IdalCommon.IworkflowEx.getEntityList(), "id", "wfmemo");
            this.ViewBag.WorkflowList = workflowList;
            this.ViewBag.WorkflowListString = Athena.common.Util.Json.ToJson(workflowList);
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
            pagination.records = IdalCommon.IworkflownodeactionEx.getWorkflowNodeactionList(keyword).Count;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("nodeactionname", keyword);
            List<workflownodeaction> workflownodeactionlist = IdalCommon.IworkflownodeactionEx.getPaginationEntityList(pagination.page, pagination.rows, dictionary);
            var data = new
            {
                rows = workflownodeactionlist,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            string str = data.ToJson();
            return Content(data.ToJson());
        } 
        /// <summary>
        /// 获取工作流
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetSelectJson(string keyValue)
        {
            List<object> list = new List<object>();
            if (!string.IsNullOrEmpty(keyValue))
            {
                int wfid = int.Parse(keyValue);
                var data = IdalCommon.IworkflowEx.getEntityById(wfid);
                list.Add(new { id = data.id, text = data.wfname });
            }
            else
            {
                var data = IdalCommon.IworkflowEx.getEntityList();
                foreach (workflow item in data)
                {
                    list.Add(new { id = item.id, text = item.wfname });
                }
            }
            string str = list.ToJson();
            return Content(list.ToJson());
        }
        /// <summary>
        /// 获取工作流使用节点
        /// </summary>
        /// <returns></returns>
        public ActionResult GetActionName(string keyValue)
         {
            //IdalCommon.IworkflownodeactionEx.getEntityList().Where(a => a == "User").Where(a => a.actiontype == 1).ToList<action>(); ;
            List<action> actions = new List<action>();
            List<object> listaction = new List<object>();
            List<int> actionids = new List<int >();
            if (!string.IsNullOrEmpty(keyValue))
            {
                int id = int.Parse(keyValue);
                var data = IdalCommon.IactionEx.getEntityById(id);
                listaction.Add(new { id = data.id, text = data.actionname });
            }
            else
            {
                actions = IdalCommon.IactionEx.getEntityList();
                List<workflownodeaction> wonodeaction = IdalCommon.IworkflownodeactionEx.getEntityList();
                foreach (workflownodeaction wo in wonodeaction)
                {
                    if (wo.actionid > 0)
                        actionids.Add(Convert.ToInt32(wo.actionid));
                }
                foreach (action ac in actions)
                {
                    bool flag = actionids.Contains(ac.id);
                    if ((ac.actiontype == 2 && !actionids.Contains(ac.id)) || ac.actiontype == 4)
                        listaction.Add(new { id = ac.id, text = ac.actionname });
                    //newactions.Add(ac);
                }
            }
            


            string str = listaction.ToJson();
            return Content(listaction.ToJson());
        }
        /// <summary>
        /// 获取节点
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetSelectNodeJson(string keyValue)
        {
            List<object> list = new List<object>();
            if (!string.IsNullOrEmpty(keyValue))
            {
                int id = int.Parse(keyValue);
                var data = IdalCommon.IworkflownodeEx.getEntityById(id);
                list.Add(new { id = data.id, text = data.wfnodename });
            }
            else
            {
                var data = IdalCommon.IworkflownodeEx.getEntityList();
                foreach (workflownode item in data)
                {
                    list.Add(new { id = item.id, text = item.wfnodename });
                }
            }
            return Content(list.ToJson());
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
            var data = IdalCommon.IworkflownodeactionEx.getWorkflowNodeaction(id);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 提交表单数据
        /// </summary>
        /// <param name="workflownodeaction">分组对象</param>
        /// <param name="keyValue">分组对象的主键</param>
        /// <returns></returns>
        public ActionResult SubmitForm(workflownodeaction workflownodeaction, string keyValue)
        {
            IdalCommon.IworkflownodeactionEx.SubmitForm(workflownodeaction, keyValue);
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
            IdalCommon.IworkflownodeoperatorEx.deleteWfNodeoperatorListByNodeactionid(key);
            IdalCommon.IworkflownodeactionEx.delete(key); 
            return Success("删除成功。");
        }
        /// <summary>
        /// 复制粘贴
        /// </summary>
        /// <param name="keyValue">分组对象的主键</param>
        /// <returns></returns>
        public ActionResult copyAndPasteForm(string keyValue)
        {
            IdalCommon.IworkflownodeactionEx.copyAndPaste(keyValue);
            return Success("粘贴成功。");
        }
        #region ACE
        public ActionResult GetModels(ACEPagination pagination, string keyword)
        {
            var data = IdalCommon.IworkflowEx.getEntityList();
            PagedData<workflownodeaction> pagedData = IdalCommon.IworkflownodeactionEx.GetPageData(pagination, keyword);
            return this.SuccessData(pagedData);
            //var data = IdalCommon.IworkflownodeactionEx.getEntityList();
            //return this.SuccessData(data);
        }
        public ActionResult Add(workflownodeaction input)
        {
            try
            {
                IdalCommon.IworkflownodeactionEx.insert(input);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return this.AddSuccessMsg();
        }
        public ActionResult Update(workflownodeaction input)
        {
            IdalCommon.IworkflownodeactionEx.update(input);
            return this.UpdateSuccessMsg();
        }
        public ActionResult Delete(string id)
        {
            int delid = Convert.ToInt32(id);
            IdalCommon.IworkflownodeactionEx.delete(delid);
            return this.DeleteSuccessMsg();
        } 
        #endregion
    }
}
