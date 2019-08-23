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
//开发团队成员： 周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：数据字典控制器
//最后修改时间:2018-01-25
//修改日志：
//2018-01-25 新增前端为ACE相关的方法（王露）

namespace Web.Areas.SystemManage.Controllers
{
   
    public class PropertymappingController : WebController
    {
       
        public PropertymappingController()
        {
          
            #region ACE
            this.noauth_actions.Add("PropertymappingIndex");
            this.noauth_actions.Add("PropertymappingGetModels");
            this.noauth_actions.Add("PropertymappingAdd");
            this.noauth_actions.Add("PropertymappingUpdate");
            this.noauth_actions.Add("PropertymappingDelete");
            #endregion
            #region BootstrapTemplate
            this.noauth_actions.Add("PropertymappingGetGridJson");
            this.noauth_actions.Add("PropertymappingGetFormJson");
            this.noauth_actions.Add("PropertymappingGetTreeJson");
            this.noauth_actions.Add("PropertymappingSubmitForm");
            this.noauth_actions.Add("PropertymappingForm");
            this.noauth_actions.Add("PropertymappingDeleteForm");
            this.noauth_actions.Add("PropertymappingcopyAndPasteForm");
            this.noauth_actions.Add("PropertymappingDetails");
            #endregion
        }

        public override ActionResult Index()
        {
            ViewBag.Controller = "PropertyMapping";
            ViewBag.Action = "Index";
            return View("Index");
        }
        /// <summary>
        /// 获取显示数据的json格式
        /// </summary>
        /// <param name="pagination">页面信息</param>
        /// <param name="keyword">搜索关键字</param>
        /// <returns></returns>
        public ActionResult GetGridJson(Pagination pagination, string keyword)
         {
             pagination.records = IdalCommon.IpropertyMappingEx.getPropertyMappingList(keyword).Count;
             Dictionary<string, object> dictionary = new Dictionary<string, object>();
             dictionary.Add("propertyName", keyword);
             List<propertymapping> propertyMappingList = IdalCommon.IpropertyMappingEx.getPaginationEntityList(pagination.page, pagination.rows, dictionary);
             var data = new
             {
                 rows = propertyMappingList,
                 total = pagination.total,
                 page = pagination.page,
                 records = pagination.records
             };
             return Content(data.ToJson());

             //var propertyMappingList = IdalCommon.IpropertyMappingEx.getPropertyMappingList(int.Parse(itemId), keyword);
             //return Content(propertyMappingList.ToJson());
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult getParentList()
        {
            propertymapping pm = new propertymapping();
            List<propertymapping> parentList = IdalCommon.IpropertyMappingEx.getEntityList().Where<propertymapping>(d => d.parentId == 0).ToList();
            return Content(parentList.ToJson());
        }

        /// <summary>
        /// 获取显示数据的节点
        /// </summary>
        /// <param name="pagination">页面信息</param>
        /// <param name="keyword">搜索关键字</param>
        /// <returns></returns>
        public ActionResult GetTreeJson()
        {
            var data = IdalCommon.IpropertyMappingEx.getPropertyMappingByParentid(0);

            var treeList = new List<TreeViewModel>();
            foreach (propertymapping item in data)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = data.Count(t => t.parentId == item.id) == 0 ? false : true;
                tree.id = item.id.ToString();
                tree.text = item.propertyMeaning;
                tree.value = item.propertyName;
                tree.parentId = item.parentId.ToString();
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson());
        }
        /// <summary>
        /// 获取表单提交的数据的json格式
        /// </summary>
        /// <param name="keyValue">数据字典对象的主键</param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            int id = Convert.ToInt32(keyValue);
            var data = IdalCommon.IpropertyMappingEx.getPropertyMapping(id);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 提交表单数据
        /// </summary>
        /// <param name="group">数据字典对象</param>
        /// <param name="keyValue">数据字典对象的主键</param>
        /// <returns></returns>
        public ActionResult SubmitForm(propertymapping propertymapping,string keyValue)
        {
            IdalCommon.IpropertyMappingEx.SubmitForm(propertymapping, keyValue);
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
            IdalCommon.IpropertyMappingEx.delete(key);
            return Success("删除成功。");
        }
        /// <summary>
        /// 复制粘贴
        /// </summary>
        /// <param name="keyValue">数据字典对象的主键</param>
        /// <returns></returns>
        public ActionResult copyAndPasteForm(string keyValue)
        {
            IdalCommon.IpropertyMappingEx.copyAndPaste(keyValue);
            return Success("删除成功。");
        }

        #region ACE
        /// <summary>
        /// 查看数据
        /// </summary>
        /// <param name="keyword">搜索框关键词</param>
        /// <returns></returns>
        public ActionResult GetModels(ACEPagination pagination, string keyword)
        {
            try
            {
                //List<propertymapping> pagedData = IdalCommon.IpropertyMappingEx.getEntityList();
                PagedData<propertymapping> pagedData = IdalCommon.IpropertyMappingEx.GetPageData(pagination, keyword);
                return this.SuccessData(pagedData);
            }
            catch (Exception)
            {
                return this.FailedMsg("出错！");
            }
        }
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="input">前台填写表单数据</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(propertymapping input)
        {
            try
            {
                if(IdalCommon.IpropertyMappingEx.isLeagalEntityname(input.propertyName))
                IdalCommon.IpropertyMappingEx.insert(input);
                return this.AddSuccessMsg();
            }
            catch (Exception)
            {
                return this.FailedMsg("出错！");
            }
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="input">前台填写表单数据</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Update(propertymapping input)
        {
            try
            {
                if (IdalCommon.IpropertyMappingEx.isLegalEntity(input.propertyName, input.propertyValue, input.propertyMeaning, input.remark))
                IdalCommon.IpropertyMappingEx.update(input);
                return this.UpdateSuccessMsg();
            }
            catch (Exception)
            {
                return this.FailedMsg("出错！");
            }
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(string id)
        {
            try
            {
                int intid = int.Parse(id);
                IdalCommon.IpropertyMappingEx.delete(intid);
                return this.DeleteSuccessMsg();
            }
            catch (Exception)
            {
                return this.FailedMsg("出错！");
            }
        }
        

        #endregion
    }
}
