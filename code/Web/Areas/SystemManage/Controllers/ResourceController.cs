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
using Web.Controllers.JqGrid;
using Athena.tool.Code;
using Athena.tool.ACEPagination;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 张婷婷 王露 陈祚松 艾美珍 张梦丽 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：资源控制器，添加ACE相关方法
//最后修改时间：2018-01-26
//修改日志：
//2018-01-26 新增ACE相关的方法（张婷婷）

namespace Web.Areas.SystemManage.Controllers
{
   
    public class ResourceController : WebController
    {
        public ResourceController()
        {
            #region  ACE
            this.noauth_actions.Add("ResourceIndex");
            this.noauth_actions.Add("ResourceGetModels");
            this.noauth_actions.Add("ResourceAdd");
            this.noauth_actions.Add("ResourceUpdate");
            this.noauth_actions.Add("ResourceDelete");
            #endregion
            #region  BootstrapTemplate
            this.noauth_actions.Add("ResourceGetTreeSelectJson");
            this.noauth_actions.Add("ResourceGetTreeGridJson");
            this.noauth_actions.Add("ResourceForm");
            this.noauth_actions.Add("ResourceGetFormJson");
            this.noauth_actions.Add("ResourcecopyAndPasteForm");
            this.noauth_actions.Add("ResourceDetails");
            this.noauth_actions.Add("ResourceSubmitForm");
            this.noauth_actions.Add("ResourcecopyAndPasteForm");
            this.noauth_actions.Add("ResourceDeleteForm");
            #endregion
        }
        /// <summary>
        /// 显示页面BootstrapTemplate
        /// </summary>
        /// <returns></returns>
        //public override ActionResult Index()
        //{
        //    return View();
        //}
        /// <summary>
        /// 显示页面ACE(页面视图入口)
        /// </summary>
        /// <returns></returns>
        public override ActionResult Index()
        {
            var data = IdalCommon.IresourceEx.getEntityList();
            var treeList = new List<TreeSelectModel>();

            //菜单下拉集合
            List<SelectOption> resourceList = SelectOption.CreateList(IdalCommon.IresourceEx.getEntityList(), "id", "resourcename");
            this.ViewBag.MenusList = resourceList;
            this.ViewBag.MenusListString = Athena.common.Util.Json.ToJson(resourceList);
            foreach (resource item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.id.ToString();
                treeModel.text = item.resourcename;
                treeModel.parentId = item.resourceowner;
                treeList.Add(treeModel);
            }
            this.ViewBag.Menus = TreeJson.ConvertToJson(treeList);//数据json格式数据
            // this.ViewBag.padata = TreeJson.PageConvertToJson(this.SuccessDatajson(ret));//分页格式数据
            return View();
        }

        /// <summary>
        /// 获得一个tree型数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetTreeSelectJson()
        {
            var data = IdalCommon.IresourceEx.getEntityList();
            var treeList = new List<TreeSelectModel>();
            foreach (resource item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.id.ToString();
                treeModel.text = item.resourcename;
                if (item.resourceowner == null)
                {
                    treeModel.parentId = "0";
                }
                else
                {
                    treeModel.parentId = item.resourceowner;
                }
                
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }

        /// <summary>
        /// 获得一个tree型网格数据
        /// </summary>
        /// <param name="pagination">分页栏信息</param>
        /// <param name="keyword">名称（筛选条件）</param>
        /// <returns></returns>
        public ActionResult GetTreeGridJson(Pagination pagination, string keyword, string _search, string filters)
        {
            try
            {
                string strValue = "";
                List<resource> data = IdalCommon.IresourceEx.getEntityList();
                List<resource> resourcelist = null;
                pagination.records = data.Count();
                var treeList = new List<TreeGridModel>();
                var p = (pagination.page - 1) * pagination.rows;
                List<resource> dlist = new List<resource>();
                if (_search == "true")
                {
                    strValue = JqGridHandler.getWhere(filters);
                    if (strValue != "")
                    {
                        strValue = "select * from role where" + "  " + strValue;
                        data = IdalCommon.IresourceEx.getSearch(strValue);
                        pagination.records = data.Count();
                        resourcelist = IdalCommon.IresourceEx.getPaginationEntityList(pagination.page, pagination.rows, data);
                    }
                }
                else
                {
                    data=IdalCommon.IresourceEx.getResourceList(keyword);
                    pagination.records = data.Count();
                    resourcelist = data.Skip(p).Take(pagination.rows).ToList();
                }
                /**************判断当前页的数据的父节点都包含在当前页中，不是则把父节点存入当前页*******************/
                
                foreach (resource re in resourcelist)
                {
                    if (!string.IsNullOrEmpty(re.resourceowner) && re.resourceowner != "0")
                    {
                        int count = resourcelist.Where(d => d.id.ToString() == re.resourceowner).Count();
                        if (count == 0)
                        {
                            resource rr = data.Where(d => d.id.ToString() == re.resourceowner).FirstOrDefault();
                            if (rr != null)
                                dlist.Add(rr);
                        }
                    }
                }
                if (dlist.Count() > 0 && dlist != null)
                {
                    dlist = dlist.Where((x, i) => dlist.FindIndex(z => z.id == x.id) == i).ToList();
                    resourcelist.AddRange(dlist);
                }
                /*************获取当前页需要显示的数据********************/
                foreach (resource item in resourcelist)
                {
                    TreeGridModel treeModel = new TreeGridModel();
                    bool hasChildren = data.Count(t => t.resourceowner == item.id.ToString()) == 0 ? false : true;
                    treeModel.id = item.id.ToString();
                    treeModel.isLeaf = hasChildren;
                    if (item.resourceowner == null)
                    {
                        treeModel.parentId = "0";
                    }
                    else
                    {
                        treeModel.parentId = item.resourceowner;
                    }
                    treeModel.expanded = hasChildren;
                    treeModel.entityJson = item.ToJson();
                    treeList.Add(treeModel);
                }
                string str = treeList.TreeGridJson().Substring(0, treeList.TreeGridJson().Length - 1) + ",\"total\":" + pagination.total + ",\"page\":" + pagination.page + ",\"records\":" + pagination.records + "}";
                return Content(str);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        /// <summary>
        /// 获得表单
        /// </summary>
        /// <returns></returns>
        public override ActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// 查看资源详情
        /// </summary>
        /// <returns></returns>
        public override ActionResult Details()
        {
            return View();
        }

        /// <summary>
        /// 通过id获得数据
        /// </summary>
        /// <param name="keyValue">id的string型</param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            try
            {
                int id = Convert.ToInt32(keyValue);
                var data = IdalCommon.IresourceEx.getResource(id);
                return Content(data.ToJson());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 数据提交
        /// </summary>
        /// <param name="resource">资源对象</param>
        /// <param name="keyValue">对象id</param>
        /// <returns></returns>
        public ActionResult SubmitForm(resource resource, string keyValue)
        {
            IdalCommon.IresourceEx.SubmitForm(resource, keyValue);
            return Success("操作成功。");
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="keyValue">对象id</param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            //int key = Convert.ToInt32(keyValue);
            IdalCommon.IresourceEx.deleteById(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 复制粘贴
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult copyAndPasteForm(string keyValue)
        {
            IdalCommon.IresourceEx.copyAndPaste(keyValue);
            return Success("粘贴成功。");
        }

        #region ACE相关方法
        /// <summary>
        /// 查看数据
        /// </summary>
        /// <param name="keyword">搜索框关键词</param>
        /// <returns></returns>
        public ActionResult GetModels(ACEPagination pagination, string keyword)
        {
            try
            {
                //resourceEx reex = new resourceEx();
                //分页代码
                //return this.SuccessData(IdalCommon.IresourceEx.GetTreePageData(pagination, keyword));
                #region 
                var pagedData = IdalCommon.IresourceEx.getEntityList();
                if (!string.IsNullOrEmpty(keyword))
                {
                    pagedData = TreeJson.TreeWhere(pagedData, a => a.resourcename.Contains(keyword), a => a.id.ToString(), a => a.resourceowner);
                }
                List<DataTableTree> ret = new List<DataTableTree>();
                DataTableTree.AppendChildren(pagedData, ref ret, null, 0, a => a.id.ToString(), a => a.resourceowner);

                return this.SuccessData(ret);
                #endregion
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
        public ActionResult Add(resource input)
        {
            try
            {
                int rr = IdalCommon.IresourceEx.insert(input);
                if (rr != -1)
                    return this.AddSuccessMsg();                   
                else
                    return this.FailedMsg("当前资源名称已存在，请重新填写！");
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
        public ActionResult Update(resource input)
        {
           try
            {
                if (IdalCommon.IresourceEx.isLegalEntity(input.resourcename,input.resourcetype,input.resourceurl,
                    input.resourcescript,input.resourceowner,(int)input.resourceleval,input.resourceleftico,
                    input.resourcerightico,input.resourceclass,(int)input.resourcenumber,(int)input.order,input.description
                    ))
               IdalCommon.IresourceEx.update(input);
                return this.UpdateSuccessMsg();
            }
            catch (Exception e)
            {
                return this.FailedMsg(e.Message);
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
                if (string.IsNullOrEmpty(id))
                {
                    return this.FailedMsg("请选择要删除的数据！");
                }
                IdalCommon.IresourceEx.deleteById(id);
                return this.DeleteSuccessMsg();
            }
            catch (Exception e)
            {
                return this.FailedMsg(e.Message);
            }
            
        }   

        #endregion
    }
}
