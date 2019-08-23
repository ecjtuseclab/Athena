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
//模块及代码页功能描述：区域控制器，添加ACE相关方法
//最后修改时间：2018-01-26
//修改日志：

namespace Web.Areas.SystemManage.Controllers
{
    
    public class AreaController : WebController
    {
        public AreaController()
        {
            #region template1
            this.noauth_actions.Add("AreaIndex");
            this.noauth_actions.Add("AreaGetTreeGridJson");
            this.noauth_actions.Add("AreaGetTreeSelectJson");
            this.noauth_actions.Add("AreaGetFormJson");
            this.noauth_actions.Add("AreaSubmitForm");
            this.noauth_actions.Add("AreaDeleteForm");
            this.noauth_actions.Add("AreacopyAndPasteForm");
            this.noauth_actions.Add("AreaForm");
            this.noauth_actions.Add("AreaDetails");
            #endregion

            #region template2
            this.noauth_actions.Add("AreaGetModels");
            this.noauth_actions.Add("AreaAdd");
            this.noauth_actions.Add("AreaUpdate");
            this.noauth_actions.Add("AreaDelete");
            #endregion
        }


        /// <summary>
        /// 获得一个tree型网格数据
        /// </summary>
        /// <param name="pagination">分页栏信息</param>
        /// <param name="keyword">名称（筛选条件）</param>
        /// <returns></returns>
        public ActionResult GetTreeGridJson(Pagination pagination, string keyword)
        {
            try
            {
                /**************判断当前页的数据的父节点都包含在当前页中，不是则把父节点存入当前页*******************/
                var data = IdalCommon.IareaEx.getAreaList(keyword);
                pagination.records = data.Count();
                var treeList = new List<TreeGridModel>();
                var p = (pagination.page - 1) * pagination.rows;
                //Dictionary<string, object> dictionary = new Dictionary<string, object>();
                //dictionary.Add("rolename", keyword);
                //var dd = IdalCommon.IresourceEx.getPaginationEntityList(pagination.page, pagination.rows, dictionary);
                List<area> dlist = new List<area>();
                var dd = data.Skip(p).Take(pagination.rows).ToList();
                //for (var i = p; i < pagertotal; i++)
                foreach (area re in dd)
                {
                    //resource re = data[i];
                    if (re.parentid != null)
                    {
                        int count = dd.Where(d => d.id == Convert.ToInt32(re.parentid)).Count();
                        if (count == 0)
                        {
                            area rr = data.Where(d => d.id == Convert.ToInt32(re.parentid)).FirstOrDefault();
                            if (rr != null)
                                dlist.Add(rr);
                        }
                    }
                }
                if (dlist.Count() > 0 && dlist != null)
                {
                    dlist = dlist.Where((x, i) => dlist.FindIndex(z => z.id == x.id) == i).ToList();
                    dd.AddRange(dlist);
                }
                foreach (area item in dd)
                {
                    TreeGridModel treeModel = new TreeGridModel();
                    bool hasChildren = data.Count(t => Convert.ToInt32(t.parentid) == item.id) == 0 ? false : true;
                    treeModel.id = item.id.ToString();
                    treeModel.text = item.fullname;
                    treeModel.isLeaf = hasChildren;
                    if (item.parentid == null)
                    {
                        treeModel.parentId = "0";
                    }
                    else
                    {
                        treeModel.parentId = item.parentid;
                    }
                    treeModel.expanded = true;
                    treeModel.entityJson = item.ToJson();
                    treeList.Add(treeModel);
                }
                //if (!string.IsNullOrEmpty(keyword))
                //{
                //    treeList = treeList.TreeWhere(t => t.text.Contains(keyword), "id", "parentid");
                //}
                string str = treeList.TreeGridJson().Substring(0, treeList.TreeGridJson().Length - 1) + ",\"total\":" + pagination.total + ",\"page\":" + pagination.page + ",\"records\":" + pagination.records + "}";
                return Content(str);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 获得一个tree型数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetTreeSelectJson()
        {
            List<area> data = IdalCommon.IareaEx.getEntityList();
            var treeList = new List<TreeSelectModel>();
            foreach (area item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.id.ToString();
                treeModel.text = item.fullname;
                if (item.parentid == null)
                {
                    treeModel.parentId = "0";
                }
                else
                {
                    treeModel.parentId = item.parentid;
                }
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }

        /// <summary>
        /// 通过id获得数据
        /// </summary>
        /// <param name="keyValue">id的string型</param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            var data = IdalCommon.IareaEx.getEntityById(int.Parse(keyValue));
            return Content(data.ToJson());
        }

        /// <summary>
        /// 数据提交
        /// </summary>
        /// <param name="area">区域对象</param>
        /// <param name="keyValue">对象id</param>
        /// <returns></returns>
        public ActionResult SubmitForm(area areaEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (IdalCommon.IareaEx.isLegalEntity(areaEntity.parentid,(int)areaEntity.layers, areaEntity.encode,areaEntity.fullname))
                areaEntity.id = int.Parse(keyValue);
                IdalCommon.IareaEx.update(areaEntity);
            }
            else
            {
                if(IdalCommon.IareaEx.isLeagalAreaname(areaEntity.fullname,areaEntity.encode))
                IdalCommon.IareaEx.insert(areaEntity);
            }
            return Success("操作成功。");
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="keyValue">对象id</param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            //IdalCommon.IareaEx.delete(int.Parse(keyValue));
            IdalCommon.IareaEx.deleteById(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 复制粘贴
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult copyAndPasteForm(string keyValue)
        {
            IdalCommon.IareaEx.copyAndPaste(keyValue);
            return Success("粘贴成功。");
        }

        /// <summary>
        /// 显示页面
        /// </summary>
        /// <returns></returns>
        //public override ActionResult Index()
        //{
        //    return View();
        //}
        public override ActionResult Index()
        {
            var data = IdalCommon.IareaEx.getEntityList();
            var treeList = new List<TreeSelectModel>();
            foreach (area item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.id.ToString();
                treeModel.text = item.fullname;
                treeModel.parentId = item.parentid;
                treeList.Add(treeModel);
            }
            this.ViewBag.AreaString = TreeJson.ConvertToJson(treeList);
            string strjson = TreeJson.ConvertToJson(treeList);
            return View();
        }

        /// <summary>
        /// 查看详情
        /// </summary>
        /// <returns></returns>
        public override ActionResult Details()
        {
            return View();
        }

        #region ACE
        //显示数据，树形
        public ActionResult GetModels(ACEPagination pagination, string keyword)
        {
            try
            {
                var pagedData = IdalCommon.IareaEx.getEntityList();
                //PagedData<area> pagedData = IdalCommon.IareaEx.GetPageData(pagination, keyword);
               // var data = pageData.DataList;
                if (!string.IsNullOrEmpty(keyword))
                {
                    if (IdalCommon.IareaEx.changeParse(keyword))
                    {
                        pagedData = TreeJson.TreeWhere(pagedData, a => a.encode.Contains(keyword), a => a.id.ToString(), a => a.parentid.ToString());
                    }
                    else
                        pagedData = TreeJson.TreeWhere(pagedData, a => a.fullname.Contains(keyword), a => a.id.ToString(), a => a.parentid.ToString());
                }
                List<DataTableTree> ret = new List<DataTableTree>();//构建区域树
                DataTableTree.AppendChildren(pagedData, ref ret, null, 0, a => a.id.ToString(), a => a.parentid);//将id、parentid需转化成string型
                return this.SuccessData(ret);
            }
            catch (Exception)
            {
                return this.FailedMsg("显示出错");
            }
        }
        //新增数据
        public ActionResult Add(area input)
        {
            try
            {
                int aa= IdalCommon.IareaEx.insert(input);
                if (aa != -1)
                    return this.AddSuccessMsg();                    
                else
                    return this.FailedMsg("当前区域名称或编号已存在,请重新填写！");
            }
            catch (Exception e)
            {
                return this.FailedMsg(e.Message);
            }
        }
        //修改数据
        public ActionResult Update(area input)
        {
            try
            {
               if (IdalCommon.IareaEx.isLegalEntity(input.parentid, (int)input.layers, input.encode, input.fullname))
                 IdalCommon.IareaEx.update(input);
                 return this.UpdateSuccessMsg();
            }
            catch (Exception e)
            {
                return this.FailedMsg(e.Message);
            }            
        }
        //根据节点id删除数据
        public ActionResult Delete(string id)
        {
            //try
            //{
            //    if (string.IsNullOrEmpty(id))
            //    {
            //        return this.FailedMsg("id不能为空");
            //    }
            //    int delid = Convert.ToInt32(id);
            //    IdalCommon.IareaEx.delete(delid);
            //    return this.DeleteSuccessMsg();
            //}
            //catch (Exception e)
            //{
            //    return this.FailedMsg(e.Message);
            //}
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return this.FailedMsg("请选择要删除的数据！");
                }               
                IdalCommon.IareaEx.deleteById(id);
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
