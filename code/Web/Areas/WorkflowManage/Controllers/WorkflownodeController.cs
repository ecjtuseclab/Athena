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
//模块及代码页功能描述：工作流节点控制器
//最后修改时间：2018-01-26
//修改日志：

namespace Web.Areas.WorkflowManage.Controllers
{
    public class WorkflownodeController : WebController
    {
        public WorkflownodeController()
        {
            noauth_actions.Add("WorkflownodeIndex");
            noauth_actions.Add("WorkflownodeForm");
            noauth_actions.Add("WorkflownodeGetGridJson");
            noauth_actions.Add("WorkflownodeDetails");
            noauth_actions.Add("WorkflownodeGetFormJson");
            noauth_actions.Add("WorkflownodeSubmitForm");
            noauth_actions.Add("WorkflownodeDeleteForm");
            noauth_actions.Add("WorkflownodeGetSelectJson");
            noauth_actions.Add("WorkflownodecopyAndPasteForm");

            #region ACE
            this.noauth_actions.Add("WorkflownodeIndex");
            this.noauth_actions.Add("WorkflownodeGetModels");
            this.noauth_actions.Add("WorkflownodeAdd");
            this.noauth_actions.Add("WorkflownodeUpdate");
            this.noauth_actions.Add("WorkflownodeDelete"); 
            #endregion

        }
        public override ActionResult Index()
        {
            List<SelectOption> workflowList = SelectOption.CreateList(IdalCommon.IworkflowEx.getEntityList(), "id", "wfmemo");
            this.ViewBag.WorkflowList = workflowList;
            this.ViewBag.WorkflowListString = Athena.common.Util.Json.ToJson(workflowList);
            return View();
        }

        /// <summary>
        /// 获取显示数据的json格式
        /// </summary>
        /// <param name="pagination">页面信息</param>
        /// <param name="keyword">搜索关键字</param>
        /// <returns></returns>
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            pagination.records = IdalCommon.IworkflownodeEx.getWfnodeList(keyword).Count;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("wfnodename", keyword);
            List<workflownode> wfnodelist = IdalCommon.IworkflownodeEx.getPaginationEntityList(pagination.page, pagination.rows, dictionary);
            var data = new
            {
                rows = wfnodelist,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        //public ActionResult GetTreeSelectJson()
        //{
        //    var data = IdalCommon.IworkflowEx.getEntityList();
        //    var treeList = new List<TreeSelectModel>();
        //    foreach (workflow item in data)
        //    {
        //        TreeSelectModel treeModel = new TreeSelectModel();
        //        treeModel.id = item.id.ToString();
        //        treeModel.text = item.wfname;
        //        treeModel.parentId = "0";
        //        treeList.Add(treeModel);
        //    }
        //    return Content(treeList.TreeSelectJson());
        //}

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
            var data = IdalCommon.IworkflownodeEx.getEntityById(id);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 表单提交
        /// </summary>
        /// <param name="role">角色对象</param>
        /// <param name="permissionIds">资源id集合的字符串</param>
        /// <param name="actionpermissionIds">动作id集合的字符串</param>
        /// <param name="keyValue">角色主键</param>
        /// <returns></returns>
        public ActionResult SubmitForm(workflownode wfnodeEntity, string keyValue)
        {
            //IdalCommon.IroleEx.SubmitForm(role, permissionIds, actionpermissionIds, keyValue);
            //return Success("操作成功。");
            if (string.IsNullOrEmpty(keyValue))
            {
                IdalCommon.IworkflownodeEx.insert(wfnodeEntity);
                return Success("操作成功。");
            }
            else
            {
                wfnodeEntity.id = int.Parse(keyValue);
                IdalCommon.IworkflownodeEx.update(wfnodeEntity);
                return Success("操作成功。");
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue">角色对象的主键</param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            int key = Convert.ToInt32(keyValue);
            IdalCommon.IworkflownodeEx.delete(key);
            return Success("删除成功。");
        }
        /// <summary>
        /// 复制粘贴
        /// </summary>
        /// <param name="keyValue">角色对象的主键</param>
        /// <returns></returns>
        public ActionResult copyAndPasteForm(string keyValue)
        {
            IdalCommon.IworkflownodeEx.copyAndPaste(keyValue);
            return Success("粘贴成功。");
        }

        public override ActionResult Details()
        {
            return View();
        }

        public override ActionResult Form()
        {
            return View();
        }
        #region ACE
          public ActionResult GetModels(ACEPagination pagination, string keyword)
         {
             var data = IdalCommon.IworkflowEx.getEntityList();
             PagedData<workflownode> pagedData = IdalCommon.IworkflownodeEx.GetPageData(pagination, keyword);
             return this.SuccessData(pagedData);
             //var data = IdalCommon.IworkflownodeEx.getEntityList();
             //return this.SuccessData(data);
         }
         public ActionResult Add(workflownode input)
         {
             IdalCommon.IworkflownodeEx.insert(input);
             return this.AddSuccessMsg();
         }
         public ActionResult Update(workflownode input)
         {
             IdalCommon.IworkflownodeEx.update(input);
             return this.UpdateSuccessMsg();
         }
         public ActionResult Delete(string id)
         {
             int delid = Convert.ToInt32(id);
             IdalCommon.IworkflownodeEx.delete(delid);
             return this.DeleteSuccessMsg();
         }
        #endregion
    }
}
