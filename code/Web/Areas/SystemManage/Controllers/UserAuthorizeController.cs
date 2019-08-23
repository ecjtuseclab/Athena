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

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 张婷婷 王露 陈祚松 艾美珍 张梦丽 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：用户授权控制器，添加ACE相关方法
//最后修改时间：2018/1/26
//修改日志：2018/1/26 新增ACE相关的方法（周庆）

namespace Web.Areas.SystemManage.Controllers
{
    //ACE框架使用的用户的角色授权管理
    public class UserAuthorizeController:WebController
    {
        public UserAuthorizeController()
        {
            this.noauth_actions.Add("UserAuthorizeIndex");
            this.noauth_actions.Add("UserAuthorizeGetPermissionTree");
            this.noauth_actions.Add("UserAuthorizeGetPermissionGroupTree");
        }
        // GET: SystemManage/RoleAuthorize
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 角色树的加载
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public ActionResult GetPermissionTree(string userId)
        {
            //所有的用户
            var userData = IdalCommon.IroleEx.getEntityList();
            //此角色拥有的所有的资源
            var authorizeData = new List<role>();
            if (!string.IsNullOrEmpty(userId))
            {
                //获取这个用户所有的角色
                authorizeData = IdalCommon.IuserRoleEx.getRoleList(int.Parse(userId));//this.CreateService<IRoleAuthorizeAppService>().GetList(roleId);
            }
            var treeList = new List<ACETreeEntity>();
            foreach (role r in userData)
            {
                ACETreeEntity tree = new ACETreeEntity();
                bool hasChildren = userData.Any(a => a.id == r.id);
                tree.id = r.id;
                tree.Text = r.rolename;
                // tree.Value = r.EnCode;
                tree.ParentId = null;
                tree.Isexpand = false;
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
        /// 组树的加载
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public ActionResult GetPermissionGroupTree(string userId)
        {
            //所有的组
            var groupData = IdalCommon.IgroupEx.getEntityList();
            //此用户拥有的所有的组
            var authorizeData = new List<group>();
            if (!string.IsNullOrEmpty(userId))
            {
                //获取这个用户所有的组
                authorizeData = IdalCommon.IuserGroupEx.getGroupList(int.Parse(userId));//this.CreateService<IRoleAuthorizeAppService>().GetList(roleId);
            }
            var treeList = new List<ACETreeEntity>();
            foreach (group r in groupData)
            {
                ACETreeEntity tree = new ACETreeEntity();
                bool hasChildren = groupData.Any(a => a.id == r.id);
                tree.id = r.id;
                tree.Text = r.groupname;
                // tree.Value = r.EnCode;
                tree.ParentId = null;
                tree.Isexpand = false;
                tree.Complete = true;
                tree.Showcheck = true;
                tree.Checkstate = authorizeData.Count(t => t.id == r.id);
                tree.HasChildren = hasChildren;
                //tree.Img = r.Icon == "" ? "" : r.Icon;
                treeList.Add(tree);
            }
            return Content(TreeJson.ConvertToJson(treeList));
        }

    }
}
