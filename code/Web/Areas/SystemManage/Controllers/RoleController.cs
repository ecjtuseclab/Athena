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
using Web.Controllers.JqGrid;
using Athena.common.Util.WebControl;
using Athena.bll;
using Web.Models;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：角色控制器（周庆）
//最后修改时间:2018-01-25
//修改日志：
//2017-12-19 新增前端为ACE相关的方法（王露）

namespace Web.Areas.SystemManage.Controllers
{
    public class RoleController : WebController
    {
        private Context db = new Context();
        public RoleController()
        {
            #region BootstrapTemplate
            this.noauth_actions.Add("RoleForm");
            this.noauth_actions.Add("RoleGetGridJson");
            this.noauth_actions.Add("RoleDetails");
            this.noauth_actions.Add("RoleGetFormJson");
            this.noauth_actions.Add("RoleSubmitForm");
            this.noauth_actions.Add("RoleDeleteForm");
            this.noauth_actions.Add("RolecopyAndPasteForm");
            this.noauth_actions.Add("RoleGetPermissionTree");
            this.noauth_actions.Add("RoleActionAuthorityTree");
            #endregion
            #region ACE
            this.noauth_actions.Add("RoleIndex");
            this.noauth_actions.Add("RoleGetModels");
            this.noauth_actions.Add("RoleSaveRoleResourceMap");
            this.noauth_actions.Add("RoleSaveRoleActionMap");
            this.noauth_actions.Add("RoleAdd");
            this.noauth_actions.Add("RoleUpdate");
            this.noauth_actions.Add("RoleDelete");
            this.noauth_actions.Add("RoleGetPermissionActionTree");
            this.noauth_actions.Add("RoleGetACEPermissionTree");
            #endregion
        }

        public override ActionResult Index()
        {
            if (this.ctx.Session["accctx"] != null)
            {
                this.ViewBag.actions = ((AccountsPrincipal)this.ctx.Session["accctx"]).Actions.Where(a => a.controlername == "User").Where(a => a.actiontype == 0).ToList<action>(); ;
                List<action> actionss = ((AccountsPrincipal)this.ctx.Session["accctx"]).Actions.Where(a => a.controlername == "User").Where(a => a.actiontype == 0).ToList<action>(); ;
            }
            else
            {
                Response.Redirect("~/Login/Index");
            }
            return View();
        }
        
        /// <summary>
        /// 获取显示数据的json格试
        /// </summary>
        /// <param name="pagination">页面信息</param>
        /// <param name="keyword">搜索关键字</param>
        /// <returns></returns>
        public ActionResult GetGridJson(Pagination pagination, string keyword, string _search, string filters)
        {
            try
            {
                string str = "";
                List<role> list = null;
                List<role> rolelist = null;
                //pagination.records = IdalCommon.IroleEx.getRoleList(keyword).Count;
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("rolename", keyword);
                if (_search == "true")
                {
                    str = JqGridHandler.getWhere(filters);
                    if (str != "")
                    {
                        str = "select * from role where" + "  " + str;
                        list = IdalCommon.IroleEx.getSearch(str);
                        pagination.records = list.Count();
                        rolelist = IdalCommon.IroleEx.getPaginationEntityList(pagination.page, pagination.rows, list);
                    }
                }
                else
                {
                    pagination.records = IdalCommon.IroleEx.getRoleList(keyword).Count();
                    rolelist = IdalCommon.IroleEx.getPaginationEntityList(pagination.page, pagination.rows, dictionary);
                }
                var data = new
                {
                    rows = rolelist,
                    total = pagination.total,
                    page = pagination.page,
                    records = pagination.records
                };
                return Content(data.ToJson());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
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
            var data = IdalCommon.IroleEx.getRole(id);
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
        public ActionResult SubmitForm(role role, string permissionIds, string actionpermissionIds, string keyValue)
        {
            bool flag = IdalCommon.IroleEx.SubmitForm(role, permissionIds, actionpermissionIds, keyValue);
            if (flag)
            return Success("操作成功");
            else
            return Error("角色、编码已存在或未修改任何信息");
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue">角色对象的主键</param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            int key = Convert.ToInt32(keyValue);
            IdalCommon.IroleEx.deleteroleAR(key);
            return Success("删除成功。");
        }
        /// <summary>
        /// 复制粘贴
        /// </summary>
        /// <param name="keyValue">角色对象的主键</param>
        /// <returns></returns>
        public ActionResult copyAndPasteForm(string keyValue)
        {
            IdalCommon.IroleEx.copyAndPaste(keyValue);
            return Success("粘贴成功。");
        }
        /// <summary>
        /// 获取资源权限树
        /// </summary>
        /// <param name="roleId">被选中角色的id</param>
        /// <returns></returns>
        public ActionResult GetPermissionTree(string roleId)
        {
            var allresource = IdalCommon.IresourceEx.getEntityList();
            var authorizedata = new List<resource>();
            if (!string.IsNullOrEmpty(roleId))
            {
                int id = Convert.ToInt32(roleId);
                authorizedata = IdalCommon.IroleResourceEx.getRoleAllResource(id, "");
            }
            var treeList = new List<TreeViewModel>();
            foreach (resource item in allresource)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = allresource.Count(t => t.resourceowner == item.id.ToString()) == 0 ? false : true;
                tree.id = item.id.ToString();
                tree.text = item.resourcename;
                tree.value = item.id.ToString();
                tree.parentId = item.resourceowner;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = authorizedata.Count(t => t.id == item.id);
                tree.hasChildren = true;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson());
        }
        /// <summary>
        /// 动作权限树
        /// </summary>
        /// <param name="roleId">被选中角色的id</param>
        /// <returns></returns>
        public ActionResult ActionAuthorityTree(string roleId)
        {
            var allaction = IdalCommon.IactionEx.getEntityList();
            var authorizedata = new List<action>();
            if (!string.IsNullOrEmpty(roleId))
            {
                int id = Convert.ToInt32(roleId);
                authorizedata = IdalCommon.IroleActionEx.getRoleAllAction(id, "");

            }
            var treeList = new List<TreeViewModel>();
            foreach (action item in allaction)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = allaction.Count(t => t.controlername == item.id.ToString()) == 0 ? false : true;
                tree.id = item.id.ToString();
                tree.text = item.actionname;
                tree.value = item.id.ToString();
                tree.parentId = null;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = authorizedata.Count(t => t.id == item.id);
                tree.hasChildren = true;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson());
        }
        #region ACE
        /// <summary>
        /// 返回role  model
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetModels(string keyword)
        {
            List<role> roleList = IdalCommon.IroleEx.getEntityList();
            return this.SuccessData(roleList);
        }
        /// <summary>
        ///保存角色的资源授权
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="permissionIds">资源授权</param>
        public void SaveRoleResourceMap(int roleid, string permissionIds)
        {
            string resIdLists = permissionIds;
            string[] strids = resIdLists.Split(',');//资源ID集合
            bool flag = false;
            int intflag = 0;
            string msg = string.Empty;
            try
            {
                //保存角色资源的权限关系
                List<resource> oldres = IdalCommon.IroleResourceEx.getRoleAllResource(roleid);//角色的所有资源对象
                role_resource newr_r = new role_resource();
                List<resource> oldbtnres = oldres.Where(p => p.resourcetype == 3).ToList();//获取角色的原来的所有btn资源对象
                List<resource> oldmenures = oldres.Where(p => p.resourcetype == 1).ToList();//获取角色的原来的所有菜单资源对象
                // string newBtnParentID;//添加btn的上级
                foreach (resource r in oldres)//删除-原来有现在没有权限的资源
                {
                    if (!resIdLists.Contains(r.id.ToString()))//如果新获取的id不包含原来roles集合的id，则删除
                    {
                        IdalCommon.IroleResourceEx.delete(roleid, r.id);
                    }
                }
                for (int i = 0; i < strids.Length; i++)//循环选中的资源
                {
                    if (int.Parse(strids[i]) != 0)
                    {
                        newr_r.roleid = roleid;
                        newr_r.resoureceid = int.Parse(strids[i]);
                        intflag = IdalCommon.IroleResourceEx.insert(newr_r);//在关联表中添加btn或者menu关系

                        if (-1 == intflag) flag = true;
                        else flag = false;
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                //  WriteBackHtml(msg, false);
            }
        }
        /// <summary>
        /// 保存角色的动作授权
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="permissionIds">动作授权</param>
        public void SaveRoleActionMap(int roleid, string permissionActionIds)
        {
            string resIdLists = permissionActionIds;
            string[] strids = resIdLists.Split(',');//资源ID集合
            bool flag = false;
            int intflag = 0;
            string msg = string.Empty;
            try
            {
                List<action> oldres = IdalCommon.IroleActionEx.getRoleAllAction(roleid);//角色的所有动作对象
                role_action newr_r = new role_action();
                List<action> oldbtnres = oldres.Where(p => p.actiontype == 3).ToList();//获取角色的原来的所有btn动作对象
                List<action> oldmenures = oldres.Where(p => p.actiontype == 1).ToList();//获取角色的原来的所有菜单动作对象
                foreach (action r in oldres)//删除-原来有现在没有权限的动作
                {
                    if (!resIdLists.Contains(r.id.ToString()))//如果新获取的id不包含原来roles集合的id，则删除
                    {
                        IdalCommon.IroleActionEx.delete(roleid, r.id);
                    }
                }
                for (int i = 0; i < strids.Length; i++)//循环选中的动作
                {
                    if (int.Parse(strids[i]) != 0)
                    {
                        newr_r.roleid = roleid;
                        newr_r.actionid = int.Parse(strids[i]);
                        intflag = IdalCommon.IroleActionEx.insert(newr_r);//在关联表中添加btn或者menu关系
                        if (-1 == intflag) flag = true;
                        else flag = false;
                    }

                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                //  WriteBackHtml(msg, false);
            }
        }
        /// <summary>
        /// 更新角色，包括对某个角色权限的授权
        /// </summary>
        /// <param name="role"></param>
        /// <param name="permissionIds"></param>
        /// <returns></returns> 
        [HttpPost]
        public ActionResult Update(role role, string permissionIds, string permissionActionIds)
        {
            string msg = string.Empty;
            try
            {
                IdalCommon.IroleEx.update(role);//更新修改的角色信息
                //保存角色资源的权限关系
                SaveRoleResourceMap(role.id, permissionIds);
                SaveRoleActionMap(role.id, permissionActionIds);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                //  WriteBackHtml(msg, false);
            }
            return this.UpdateSuccessMsg();
        }


        public ActionResult Delete(string id)
        {
            try
            {
                IdalCommon.IroleEx.deleteroleAR(int.Parse(id));
                //IdalCommon.IroleEx.delete(int.Parse(id));
            }
            catch (Exception ex)
            {

            }
            return this.DeleteSuccessMsg();
        }
        /// <summary>
        /// 新增，包括其权限的授权
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(role role, string permissionIds, string permissionActionIds)
        {
            string msg = string.Empty;
            try
            {
                bool flag = IdalCommon.IroleEx.isLegalRoleName(role.rolename,Convert.ToInt32( role.rolecode));
                if (!flag)
                {
                    msg = "y角色名或角色编码相同";
                }
                else
                {
                //插入的角色信息
                int roleid = IdalCommon.IroleEx.insert(role);
                //保存角色资源的权限关系
                SaveRoleResourceMap(roleid, permissionIds);
                //保存角色动作的权限关系
                SaveRoleActionMap(roleid, permissionActionIds);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                //  WriteBackHtml(msg, false);
            }
            return this.Success(msg);

        }
        /// <summary>
        /// 资源树的加载
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        public ActionResult GetACEPermissionTree(string roleId)
        {
            //所有的资源
            var resourceData = IdalCommon.IresourceEx.getEntityList();
            //此角色拥有的所有的资源
            var authorizeData = new List<resource>();
            if (!string.IsNullOrEmpty(roleId))
            {
                //获取这个角色所有的资源
                authorizeData = IdalCommon.IroleResourceEx.getRoleAllResource(int.Parse(roleId));//this.CreateService<IRoleAuthorizeAppService>().GetList(roleId);
            }
            var treeList = new List<ACETreeEntity>();
            foreach (resource r in resourceData)
            {
                ACETreeEntity tree = new ACETreeEntity();
                bool hasChildren = resourceData.Any(a => a.resourceowner == r.id.ToString());
                tree.id = r.id;
                tree.Text = r.resourcename;
                // tree.Value = r.EnCode;
                tree.ParentId = r.resourceowner;
                tree.Isexpand = true;
                tree.Complete = true;
                tree.Showcheck = true;
                tree.Checkstate = authorizeData.Count(t => t.id == r.id);
                tree.HasChildren = hasChildren;
                //tree.Img = r.Icon == "" ? "" : r.Icon;
                treeList.Add(tree);
            }
            return Content(TreeJson.ConvertToJson(treeList));
        }
        /// <summary>
        /// 动作树的加载
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        public ActionResult GetPermissionActionTree(string roleId)
        {
            //所有的动作
            var actionData = IdalCommon.IactionEx.getEntityList();
            //此角色拥有的所有的资源
            var authorizeData = new List<action>();
            if (!string.IsNullOrEmpty(roleId))
            {
                //获取这个角色所有的资源
                authorizeData = IdalCommon.IroleActionEx.getRoleAllAction(int.Parse(roleId));//this.CreateService<IRoleAuthorizeAppService>().GetList(roleId);
            }
            var treeList = new List<ACETreeEntity>();
            foreach (action r in actionData)
            {
                ACETreeEntity tree = new ACETreeEntity();
                bool hasChildren = actionData.Any(a => a.actionowner == r.id.ToString());
                tree.id = r.id;
                tree.Text = r.actionname;
                // tree.Value = r.EnCode;
                tree.ParentId = null;
                tree.Isexpand = true;
                tree.Complete = true;
                tree.Showcheck = true;
                tree.Checkstate = authorizeData.Count(t => t.id == r.id);
                tree.HasChildren = hasChildren;
                //tree.Img = r.Icon == "" ? "" : r.Icon;
                treeList.Add(tree);
            }
            return Content(TreeJson.ConvertToJson(treeList));
        }

        #endregion
    }
}
