using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Athena.basedal;
using Athena.model;
using Athena.dal;
using Athena.common.Util.WebControl;
using Athena.common.Util.Web;
using Athena.tool.Code;
using System.Data;
using Athena.Idal;
using System.Web.WebPages;
using Athena.tool.ACEPagination;
using Athena.common.Util.WebControl;
using Athena.bll;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 张婷婷 王露 陈祚松 艾美珍 张梦丽 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：用户控制器，添加ACE相关方法
//最后修改时间：2018/1/26
//修改日志：2018/1/26 新增ACE相关的方法（周庆）

namespace Web.Areas.SystemManage.Controllers
{
    public class UserController : WebController
    {
        public UserController()
        {
            #region BootstrapTemplate
            this.noauth_actions.Add("UserForm");
            this.noauth_actions.Add("UserIndex");
            this.noauth_actions.Add("UserGetGridJson");
            this.noauth_actions.Add("UserDetails");
            this.noauth_actions.Add("UserRevisePassword");
            this.noauth_actions.Add("UserSubmitForm");
            this.noauth_actions.Add("UsergetRolelist");
            this.noauth_actions.Add("UsergetGrouplist");
            this.noauth_actions.Add("UserGetFormJson");
            this.noauth_actions.Add("UserDeleteForm");
            this.noauth_actions.Add("UserdeleteAll");
            this.noauth_actions.Add("UserSubmitRevisePassword");
            this.noauth_actions.Add("UsercopyAndPasteForm");
            #endregion
            #region ACE
            this.noauth_actions.Add("UserGetModels");
            this.noauth_actions.Add("UserSaveUserRoleMap");
            this.noauth_actions.Add("UserSaveUserGroupMap");
            this.noauth_actions.Add("UserUpdate");
            this.noauth_actions.Add("UserAdd");
            this.noauth_actions.Add("UserDelete");
            this.noauth_actions.Add("UserRevisePassword");
            #endregion
        }

        [HttpGet]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            try
            {
                pagination.records = IdalCommon.IuserEx.getUserList(keyword).Count;
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("username", keyword);
                List<user> userlist = IdalCommon.IuserEx.getPaginationUserList(pagination.page, pagination.rows, dictionary);
                var data = new
                {
                    rows = userlist,
                    total = pagination.total,
                    page = pagination.page,
                    records = pagination.records
                };
                //string str = data.ToJson();
                return Content(data.ToJson());
            }
            catch (Exception ex)
            {
                return Content(ex.ToJson());
            }
        }

        public override ActionResult Index()
        {
            if (this.ctx.Session["accctx"] != null)
            {
                this.ViewBag.actions = ((AccountsPrincipal)this.ctx.Session["accctx"]).Actions.Where(a => a.controlername == "User").Where(a => a.actiontype == 1).ToList<action>(); ;
                List<action> actionss = ((AccountsPrincipal)this.ctx.Session["accctx"]).Actions.Where(a => a.controlername == "User").Where(a => a.actiontype == 1).ToList<action>(); ;
            }
            else
            {
                Response.Redirect("~/Login/Index");
            }
            ViewBag.Controller = "User";
            ViewBag.Action = "Index";
            return View("Index");
        }
        public override ActionResult Details()
        {
            ViewBag.Controller = "User";
            ViewBag.Action = "Details";
            return View("Details");
        }
        public ActionResult RevisePassword()
        {
            ViewBag.Controller = "User";
            ViewBag.Action = "RevisePassword";
            return View("RevisePassword");
        }
        /// <summary>
        /// 提交表单
        /// </summary>
        /// <param name="u">用户对象</param>
        /// <param name="keyValue">id</param>
        /// <returns></returns>
        public ActionResult SubmitForm(user u, string rolelistIds, string grouplistIds, string keyValue)
        {
           bool msg= IdalCommon.IuserEx.SubmitForm(u, rolelistIds, grouplistIds, keyValue);
           if (msg)
               return Success("操作成功");
           else
               return Error("存在相同用户名");
        }
        /// <summary>
        /// 根据用户id获取用户角色列表
        /// </summary>
        /// <returns></returns>
        public ActionResult getRolelist(string userId)
        {

            var rolelist = IdalCommon.IroleEx.getEntityList();
            var authorityRole = new List<role>();
            if (!string.IsNullOrEmpty(userId))
            {
                try
                {
                    int id = Convert.ToInt32(userId);
                    //user u = IdalCommon.IuserEx.getEntityById(id);
                    //authorityRole = IdalCommon.IuserRoleEx.getRole(u.username);
                    authorityRole = IocModule.GetEntity<IuserRoleEx>().getRoleList(id);
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                }
            }
          
            var treeList = new List<TreeViewModel>();
            foreach (role item in rolelist)
            {
                TreeViewModel tree = new TreeViewModel();
                tree.id = item.id.ToString();
                tree.text = item.rolename;
                tree.value = item.id.ToString();
                tree.parentId = null;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = authorityRole.Count(t => t.id == item.id);
                tree.hasChildren = false;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson());
        }
        /// <summary>
        /// 根据用户id获取用户分组列表
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public ActionResult getGrouplist(string userid)
        {
            var grouplist = IdalCommon.IgroupEx.getEntityList();
            var authorityGroup = new List<group>();
            if (!string.IsNullOrEmpty(userid))
            {
                int id = Convert.ToInt32(userid);
                //user u = IdalCommon.IuserEx.getEntityById(id);
                authorityGroup = IdalCommon.IuserGroupEx.getGroupList(id);
            }
            var treeList = new List<TreeViewModel>();
            foreach (group item in grouplist)
            {
                TreeViewModel tree = new TreeViewModel();
                tree.id = item.id.ToString();
                tree.text = item.groupname;
                tree.value = item.id.ToString();
                tree.parentId = null;
                tree.isexpand = false;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = authorityGroup.Count(t => t.id == item.id);
                tree.hasChildren = false;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson());
        }
        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <returns></returns>
        //public ActionResult getUserRole()
        //{
        //   List<role> roleList= IdalCommon.IroleEx.getEntityList();
        //   return Content(roleList.ToJson());
        //}
        /// <summary>
        /// 获取表单数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            int id = Convert.ToInt32(keyValue);
            var data = IdalCommon.IuserEx.getEntityById(id);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 删除表单
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            int id = Convert.ToInt32(keyValue);
            IdalCommon.IuserEx.deleteuserRG(id);
            return Success("删除成功");
        }
        /// <summary>
        /// 清除所有数据
        /// </summary>
        /// <returns></returns>
        public ActionResult deleteAll()
        {
            try
            {
                bool flag = IdalCommon.IuserEx.deleteAll();
                if (flag)
                    return Success("清空成功");
                else
                    return Success("清空失败");
            }
            catch (Exception)
            {
                return Success("清空失败");
            }

        }
        /// <summary>
        /// 提交重置密码表单
        /// </summary>
        /// <param name="userPassword"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitRevisePassword(string userPassword, string keyValue)
        {
            //int id = Convert.ToInt32(keyValue);
            //user u = IdalCommon.IuserEx.getEntityById(id);
            IdalCommon.IuserEx.updatePsw(userPassword,keyValue);
            return Success("重置成功");
        }
        /// <summary>
        /// 复制粘贴
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult copyAndPasteForm(string keyValue)
        {
            IdalCommon.IuserEx.copyAndPaste(keyValue);
            return Success("复制粘贴成功");
        }
        #region ACE
        /// <summary>
        /// 获取模型Model，返回前端页面
        /// </summary>
        /// <param name="pagination">分页对象</param>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetModels(Athena.tool.ACEPagination.ACEPagination pagination, string keyword)
        {
            PagedData<user> pagedData = IdalCommon.IuserEx.GetPageData(pagination, keyword);
            return this.SuccessData(pagedData);
        }
        /// <summary>
        ///保存用户的角色
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="permissionIds">角色授权</param>
        public void SaveUserRoleMap(int userid, string permissionIds)
        {
            string roleIdLists = permissionIds;
            string[] strids = roleIdLists.Split(',');//角色ID集合
            bool flag = false;
            int intflag = 0;
            string msg = string.Empty;
            try
            {
                //保存用户角色的权限关系
                List<role> oldroles = IdalCommon.IuserRoleEx.getUserRole(userid);//用户的所有角色对象
                user_role newu_r = new user_role();
                string roleListStr = " ";
                foreach (role r in oldroles)
                {
                    if (!roleIdLists.Contains(r.id.ToString()))
                    {

                        IdalCommon.IuserRoleEx.deleteUerRole(userid, r.id);
                    }
                }
                for (int i = 0; i < strids.Length; i++)//循环选中的角色
                {
                    if (int.Parse(strids[i]) != 0)
                    {
                        newu_r.userid = userid;
                        newu_r.roleid = int.Parse(strids[i]);
                        intflag = IdalCommon.IuserRoleEx.insert(newu_r);//在关联表中添加btn或者menu关系
                        if (-1 == intflag) flag = true;
                        else flag = false;
                        roleListStr += IdalCommon.IroleEx.getEntityById(int.Parse(strids[i])).rolename + ',';
                        Dictionary<string, object> UserList = new Dictionary<string, object>();
                        UserList["rolelist"] = roleListStr.Trim(',');
                        IdalCommon.IuserEx.update(userid, UserList);
                    }
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }
        /// <summary>
        ///保存用户的分组
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="permissionGroupIds">分组授权</param>
        public void SaveUserGroupMap(int userid, string permissionGroupIds)
        {
            string groupIdLists = permissionGroupIds;
            string[] strids = groupIdLists.Split(',');//角色ID集合
            bool flag = false;
            int intflag = 0;
            string msg = string.Empty;
            try
            {
                //保存用户组的权限关系
                List<group> oldgro = IdalCommon.IuserGroupEx.getGroupList(userid);//用户的所有组对象
                user_group newu_g = new user_group();
                string groupListStr = " ";
                foreach (group g in oldgro)
                {
                    if (!groupIdLists.Contains(g.id.ToString()))
                    {
                        IdalCommon.IuserGroupEx.deleteUserGroup(userid, g.id);
                    }
                }
                for (int i = 0; i < strids.Length; i++)//循环选中的分组
                {
                    if (int.Parse(strids[i]) != 0)
                    {
                        newu_g.userid = userid;
                        newu_g.groupid = int.Parse(strids[i]);
                        intflag = IdalCommon.IuserGroupEx.insert(newu_g);//在关联表中添加btn或者menu关系
                        if (-1 == intflag) flag = true;
                        else flag = false;
                        groupListStr += IdalCommon.IgroupEx.getEntityById(int.Parse(strids[i])).groupname + ',';
                    }
                    Dictionary<string, object> UserList = new Dictionary<string, object>();
                    UserList["grouplist"] = groupListStr.Trim(',');
                    IdalCommon.IuserEx.update(userid, UserList);
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }
        /// <summary>
        /// 更新角色，包括对某个角色权限的授权
        /// </summary>
        /// <param name="role"></param>
        /// <param name="permissionIds"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(user user, string permissionIds, string permissionGroupIds)
        {
            string msg = string.Empty;
            bool flag = false;
            try
            {
               flag= IdalCommon.IuserEx.updateandencrypt(user);//更新修改的用户信息,并对用户密码加密
               if (flag)
               {
                   //保存用户角色的权限关系
                   SaveUserRoleMap(user.id, permissionIds);
                   SaveUserGroupMap(user.id, permissionGroupIds);
                   msg="修改成功";
               }
               else
               {
                   msg="用户名已存在，修改失败";
               }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                //  WriteBackHtml(msg, false);
            }
            return this.UpdateSuccessMsg(msg);
        }
        /// <summary>
        /// 新增，包括其权限的授权
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(user user, string permissionIds, string permissionGroupIds)
        {
            string msg = string.Empty;
            try
            {
               // user u = IdalCommon.IuserEx.SetUserKey(user.username,user.password);
                //插入的角色信息
                
                int userid = IdalCommon.IuserEx.insertuser(user);
                //int userid = IdalCommon.IuserEx.insert(user.username,user.password);
                if (userid > 0)
                {
                    //保存角色资源的权限关系
                    SaveUserRoleMap(userid, permissionIds);
                    //保存用户分组的权限关系
                    SaveUserGroupMap(userid, permissionGroupIds);
                    msg = "添加成功";
                }
                else
                {
                    msg = "用户名相同，添加失败";
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
           //return this.SuccessData(pagedData);
           return this.AddSuccessMsg(msg);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(string id)
        {
            try
            {
                IdalCommon.IuserEx.deleteuserRG(int.Parse(id));
            }
            catch (Exception ex)
            {

            }
            return this.DeleteSuccessMsg();
        }

        [HttpPost]
        public ActionResult OpenRevisePasswordDialog(string userId, string newPassword)
        {
            if (userId == null)
                return this.FailedMsg("userId 不能为空");
            IdalCommon.IuserEx.RevisePassword(int.Parse(userId), newPassword);
            return this.SuccessMsg("重置密码成功");
        }
        #endregion


    }
}