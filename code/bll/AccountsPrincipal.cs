using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using Athena.model;
using Athena.Idal;
using Athena.basedal;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：当前用户相关信息列表
//最后修改时间:2018-01-11
//修改日志：

namespace Athena.bll
{
   public class AccountsPrincipal:IPrincipal
    {
        public user currentuser = new user();
        public role currentrole = new role();
        #region 属性
        protected IIdentity identity;
        protected List<action> actionList;//当前用户拥有的权限列表
        protected List<role> roleList;//当前用户的所有角色列表
        protected List<resource> menuList;//当前用户的所有菜单资源列表,type=1;
        protected List<resource> toolbarList;//当前用户的所有工具条资源列表，type=2;
        protected List<resource> buttonList;//当前用户的所有按钮资源列表,type=3;
        protected List<resource> scriptList;//当前用户的所有脚本资源列表,type=4;
        protected List<resource> fileList;//当前用户的所有文件资源列表,type=5;
        /////////////////////////////////////////////工作流相关////////////////////////////////////
        protected List<action> wfCtrlActionList;//当前用户拥有的工作流控制器方法权限列表
        protected List<workflownodeaction> wfActionList;//当前用户的所有拥有的工作流节点方法权限列表
        ///////////////////////////工作流相关//////////////////////////////////////////////////
        /// <summary>
        /// 当前用户的所有的工作流控制器方法权限
        /// </summary>
        public List<action> WorkflowCtrlActions
        {
            get
            {
                return wfCtrlActionList;
            }
        }
        /// <summary>
        /// 当前用户拥有的工作流节点方法权限列表
        /// </summary>
        public List<workflownodeaction> WorkflowActions
        {
            get
            {
                return wfActionList;
            }
        }
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// 当前用户的所有角色
        /// </summary>
        public List<role> Roles
        {
            get
            {
                return roleList;
            }
        }
        /// <summary>
        /// 当前用户拥有的权限列表
        /// </summary>
        public List<action> Actions
        {
            get
            {
                return actionList;
            }
        }
        /// <summary>
        /// 当前用户拥有的菜单资源列表
        /// </summary>
        public List<resource> Menus
        {
            get
            {
                return menuList;
            }
        }
        /// <summary>
        /// 当前用户拥有的工具条资源列表
        /// </summary>
        public List<resource> Toolbars
        {
            get
            {
                return toolbarList;
            }
        }
        /// <summary>
        /// 当前用户拥有的按钮资源列表
        /// </summary>
        public List<resource> Buttons
        {
            get
            {
                return buttonList;
            }
        }
        /// <summary>
        /// 当前用户拥有的脚本资源列表
        /// </summary>
        public List<resource> Scripts
        {
            get
            {
                return scriptList;
            }
        }
        /// <summary>
        /// 当前用户拥有的文件资源列表
        /// </summary>
        public List<resource> Files
        {
            get
            {
                return fileList;
            }
        }
        // IPrincipal Interface Requirements:
        /// <summary>
        /// 当前用户的标识对象
        /// </summary>
        public IIdentity Identity
        {
            get
            {
                return identity;
            }
            set
            {
                identity = value;
            }
        }
       /// <summary>
       /// 当前角色
       /// </summary>
        protected role _currentrole;
        public role CurrentRole
        {
            get
            {
                return _currentrole;
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 根据用户编号构造
        /// </summary>
        public AccountsPrincipal(int userID)
        {
            identity = new SiteIdentity(userID);
            user u = IocModule.GetEntity<IuserEx>().getEntityById(userID) as user;
            if (u != null)
            {
                currentuser = u;
                string username = u.username;
                SetList(username);
            }
        }

       /// <summary>
       /// 重载
       /// </summary>
        public void Reload()
        {
            SetList(this.currentuser.username, this.CurrentRole.id);
        }

       /// <summary>
       /// 设置当前角色
       /// </summary>
       /// <param name="roleid"></param>
       /// <returns></returns>
        public bool SetCurrentRole(int roleid)
        {
            role roleTemp = this.roleList.Where(p => p.id == roleid).FirstOrDefault();
            if (roleTemp != null)
            {
                this._currentrole = roleTemp;
                return true;
            }
            else
            {
                return false;
            }
        }
       /// <summary>
       /// 设置当前角色数据列表
       /// </summary>
       /// <param name="username"></param>
       /// <param name="roleID"></param>
        public void SetList(string username, int roleID)
        {
          
            actionList =IocModule.GetEntity<IroleActionEx>().getRoleAllAction(roleID);
            menuList = IocModule.GetEntity<IroleResourceEx>().getRoleResource(roleID, 1);
            toolbarList = IocModule.GetEntity<IroleResourceEx>().getRoleResource(roleID, 2);
            buttonList = IocModule.GetEntity<IroleResourceEx>().getRoleResource(roleID, 3);
            scriptList = IocModule.GetEntity<IroleResourceEx>().getRoleResource(roleID, 4);
            fileList = IocModule.GetEntity<IroleResourceEx>().getRoleResource(roleID, 5);
        }
        /// <summary>
        /// 设置当前用户数据列表
        /// </summary>
        /// <param name="username"></param>
        private void SetList(string username)
        {
            try
            {
                actionList = UserInfo.getUserAllAction(username);
                roleList = IocModule.GetEntity<IuserRoleEx>().getRole(username);
                menuList = UserInfo.getUserResource(username, 1);
                toolbarList = UserInfo.getUserResource(username, 2);
                buttonList = UserInfo.getUserResource(username, 3);
                scriptList = UserInfo.getUserResource(username, 4);
                fileList = UserInfo.getUserResource(username, 5);
            }
            catch (Exception ex)
            {
                string m = ex.Message;
            }
        }
        /// <summary>
        /// 根据用户名构造
        /// </summary>
        public AccountsPrincipal(string userName)
        {
            identity = new SiteIdentity(userName);
            user u = IocModule.GetEntity<IuserEx>().getUser(userName) as user;
            if (u != null)
            {
                SetList(userName);
                currentuser = u;
            }
        }
        
        #endregion
        #region 权限判断
        /// <summary>
        /// 当前用户是否属于指定名称的角色
        /// </summary>
        public bool IsInRole(string role)
        {
            List<string> rolenames = roleList.Select(t => t.rolename).ToList<string>();//抽取rolename列到新List
            if (rolenames == null)
                return false;
            else
                return rolenames.Contains(role);
        }
       //ztt
        public bool IsInRole(int roleid)
        {
            List<int> roleids = roleList.Select(t => t.id).ToList<int>();//抽取rolename列到新List
            if (roleids == null)
                return false;
            else
                return roleids.Contains(roleid);
        }
        /// <summary>
        /// ztt
        /// 当前用户是否拥有指定动作名称的权限
        /// </summary>
        /// <param name="actionname"></param>
        /// <returns></returns>
        public bool HasActionPermission(string actionname)
        {
            List<string> actionnames = actionList.Select(t => t.actionname).ToList<string>();
            if (actionname == null)
                return false;
            else
                return actionnames.Contains(actionname);
        }
        /// <summary>
        /// 当前用户是否拥有指定动作名称的权限
        /// </summary>
        public bool HasActionPermission(string actionName, string controlerName = "")//???????????????????????
        {
            List<string> actionnames = new List<string>();
            if (controlerName == "")
                actionnames = actionList.Select(t => t.actionname).ToList<string>();
            else
                actionnames = actionList.Where(t => t.controlername == controlerName).Select(t => t.actionname).ToList<string>();

            if (actionnames == null)
                return false;
            else
                return actionnames.Contains(actionName);
        }


        /// <summary>
        /// 当前用户是否拥有指定动作ID的权限
        /// </summary>
        public bool HasActionPermission(int actionId)
        {
            List<int> actionids = actionList.Select(t => t.id).ToList<int>();
            if (actionids == null)
                return false;
            else
                return actionids.Contains(actionId);
        }
        /// <summary>
        /// 当前用户是否拥有指定菜单权限
        /// </summary>
        /// <param name="menuname"></param>
        /// <returns></returns>
        public bool HasMenu(string menuname)
        {
            List<string> menus = menuList.Select(t => t.resourcename).ToList<string>();
            if (menus == null)
                return false;
            else
                return menus.Contains(menuname);
        }
        /// <summary>
        /// 当前用户是否拥有指定菜单权限
        /// </summary>
        /// <param name="menuname"></param>
        /// <returns></returns>
        public bool HasMenu(int menuid)
        {
            List<int> menus = menuList.Select(t => t.id).ToList<int>();
            if (menus == null)
                return false;
            else
                return menus.Contains(menuid);
        }
        /// <summary>
        /// 当前用户是否拥有指定工具条权限
        /// </summary>
        /// <param name="toolbarname"></param>
        /// <returns></returns>
        public bool HasToolbar(string toolbarname)
        {
            List<string> toolbars = toolbarList.Select(t => t.resourcename).ToList<string>();
            if (toolbars == null)
                return false;
            else
                return toolbars.Contains(toolbarname);
        }
        /// <summary>
        /// 当前用户是否拥有指定工具条权限
        /// </summary>
        /// <param name="toolbarname"></param>
        /// <returns></returns>
        public bool HasToolbar(int toolbarid)
        {
            List<int> toolbars = toolbarList.Select(t => t.id).ToList<int>();
            if (toolbars == null)
                return false;
            else
                return toolbars.Contains(toolbarid);
        }
        /// <summary>
        /// 当前用户是否拥有指定按钮权限
        /// </summary>
        /// <param name="buttonname"></param>
        /// <returns></returns>
        public bool HasButton(string buttonname)
        {
            List<string> buttons = buttonList.Select(t => t.resourcename).ToList<string>();
            if (buttons == null)
                return false;
            else
                return buttons.Contains(buttonname);
        }
        /// <summary>
        /// 当前用户是否拥有指定按钮权限
        /// </summary>
        /// <param name="buttonname"></param>
        /// <returns></returns>
        public bool HasButton(int buttonid)
        {
            List<int> buttons = buttonList.Select(t => t.id).ToList<int>();
            if (buttons == null)
                return false;
            else
                return buttons.Contains(buttonid);
        }
        /// <summary>
        /// 当前用户是否拥有指定脚本权限
        /// </summary>
        /// <param name="scriptname"></param>
        /// <returns></returns>
        public bool HasScrpit(string scriptname)
        {
            List<string> scripts = scriptList.Select(t => t.resourcename).ToList<string>();
            if (scripts == null)
                return false;
            else
                return scripts.Contains(scriptname);
        }
        /// <summary>
        /// 当前用户是否拥有指定脚本权限
        /// </summary>
        /// <param name="scriptname"></param>
        /// <returns></returns>
        public bool HasScrpit(int scriptid)
        {
            List<int> scripts = scriptList.Select(t => t.id).ToList<int>();
            if (scripts == null)
                return false;
            else
                return scripts.Contains(scriptid);
        }
        /// <summary>
        /// 当前用户是否拥有指定文件权限
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool HasFile(string filename)
        {
            List<string> files = fileList.Select(t => t.resourcename).ToList<string>();
            if (files == null)
                return false;
            else
                return files.Contains(filename);
        }
        /// <summary>
        /// 当前用户是否拥有指定文件权限
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool HasFile(int fileid)
        {
            List<int> files = fileList.Select(t => t.id).ToList<int>();
            if (files == null)
                return false;
            else
                return files.Contains(fileid);
        }
        #endregion
        #region 登陆验证
        /// <summary>
        /// 验证方式1，传统的基于口令的验证登录信息
        /// </summary>
        public static AccountsPrincipal ValidateLogin(string username, string password)
        {
            if (IocModule.GetEntity<IuserEx>().checkPassword(username, password))
            {
                return new AccountsPrincipal(username);
            }
            else
                return null;
        }
        /// <summary>
        /// 验证方式2，通过数字签名验证登录信息
        /// </summary>
        public static AccountsPrincipal ValidateLoginBySignature(string username, string msg, string sig)
        {
            if (IocModule.GetEntity<IuserEx>().verify(username, msg, sig))
            {
                return new AccountsPrincipal(username);
            }
            else
                return null;
        }
       #endregion
    }
}
