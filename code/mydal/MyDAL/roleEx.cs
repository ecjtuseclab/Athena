using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athena.model;
using Athena.Idal;
using MySqlSugar;
using Athena.tool.ACEPagination;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员： 周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：对角色表的数据处理
//最后修改时间:2018-01-25
//修改日志：

namespace Athena.mydal
{
    public class roleEx : RepositoryBase<role>, IroleEx
    {
        #region 对象静态实例
        private static roleEx _instance;
        public static roleEx Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new roleEx();
                }
                return _instance;
            }
        }
        #endregion

        /// <summary>
        /// 差异更新
        /// </summary>
        /// <param name="id">跟新数据主码</param>
        /// <param name="dictionary">需要更新的数据</param>
        /// <returns>返回更新结果（true/false）</returns>
        public bool update(int id, Dictionary<string, object> dictionary)
        {
            try
            {
                db.Update<role>(dictionary, p => p.id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 根据角色名称获取角色数据
        /// </summary>
        /// <param name="rolename">角色名称</param>
        /// <returns>一条角色数据</returns>
        public role getRole(string rolename)
        {
            try
            {
                role table = db.Queryable<role>().Where(d => d.rolename == rolename).FirstOrDefault();
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 根据角色id获取角色数据
        /// </summary>
        /// <param name="rolename">角色id</param>
        /// <returns>一条角色数据</returns>
        public role getRole(int roleid)
        {
            try
            {
                role table = db.Queryable<role>().Where(d => d.id == roleid).FirstOrDefault();
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 根据用户名获取用户拥有角色列表
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public List<role> getUserRoles(string userName)
        {
            try
            {
                List<role> rr = new List<role>();
                user uu = db.Queryable<user>().Where(d => d.username == userName).FirstOrDefault();
                if (uu != null)
                    rr = db.Queryable<role>()
                        .JoinTable<user_role>((s1, s2) => s1.id == s2.roleid)
                        .Where<user_role>((s1, s2) => s2.userid == uu.id)
                        .Select("s1.*")
                        .ToList();
                List<role> Irr = new List<role>();
                foreach (role r in rr)
                {
                    Irr.Add(r as role);
                }
                return Irr;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return null;
            }

        }

        /// <summary>
        /// 根据角色名称获取角色数据id
        /// </summary>
        /// <param name="rolename">角色名称</param>
        /// <returns>角色数据id</returns>
        public int getRoleId(string rolename)
        {
            try
            {
                int id = 0;
                role table = db.Queryable<role>().Where(d => d.rolename == rolename).FirstOrDefault();
                if (table != null) id = table.id;
                return id;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// 判断角色名称是否已经存在
        /// </summary>
        /// <param name="rolename">角色名称</param>
        /// <returns>bool值</returns>
        public bool isLegalRoleName(string rolename, int rolecode)
        {
            int count = db.Queryable<role>().Where(d => d.rolename == rolename && d.rolecode == rolecode).Count();
            if (count == 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 动作授权
        /// </summary>
        /// <param name="actionpermissionIds"></param>
        /// <param name="keyValue"></param>
        public void actionAuthority(string actionpermissionIds, int keyValue)
        {
            if (!string.IsNullOrEmpty(actionpermissionIds))
            {
                string[] ids = actionpermissionIds.Split(',');
                IroleActionEx roleactionex = new roleActionEx();
                IactionEx actionex = new actionEx();
                for (int i = 0; i < ids.Length; i++)
                {
                    if (actionex.getEntityById(Convert.ToInt32(ids[i])).controlername != "0")
                    {
                        roleactionex.addRoleAction(Convert.ToInt32(ids[i]), keyValue);
                    }
                }
            }
        }
        /// <summary>
        /// 资源授权
        /// </summary>
        /// <param name="resourcepermissionIds">资源id集合的字符串</param>
        /// <param name="keyValue"></param>
        public void resourceAuthority(string resourcepermissionIds, int keyValue)
        {
            if (!string.IsNullOrEmpty(resourcepermissionIds))
            {
                string[] ids = resourcepermissionIds.Split(',');
                IroleResourceEx roleresourceex = new roleResourceEx();
                for (int i = 0; i < ids.Length; i++)
                {
                    role_resource ro = new role_resource();
                    ro.roleid = keyValue;
                    ro.resoureceid = Convert.ToInt32(ids[i]);
                    roleresourceex.insert(ro);
                }
            }
        }
        /// <summary>
        ///  表单提交
        /// </summary>
        /// <param name="roleEntity">表单提交的一条角色数据并给角色添加资源权限</param>
        /// <param name="resourcepermissionIds">资源id集合</param>
        /// <param name="keyValue">角色主键</param>
        public bool SubmitForm(role roleEntity, string resourcepermissionIds, string actionpermissionIds, string keyValue)
        {
            bool flag = false;

            if (!string.IsNullOrEmpty(keyValue))
            {
                //if (isLegalEntity(roleEntity, resourcepermissionIds, actionpermissionIds))
                //{
                    int id = Convert.ToInt32(keyValue);
                    roleEntity.id = id;
                    db.Update<role>(roleEntity);
                    actionAuthority(actionpermissionIds, id);
                    resourceAuthority(resourcepermissionIds, id);
                    flag = true;
                //}
            }
            else
            {
                if (isLegalRoleName(roleEntity.rolename, (int)roleEntity.rolecode))
                {
                    int newid = insert(roleEntity);
                    List<role> rolelist = this.getEntityList();
                    actionAuthority(actionpermissionIds, newid);
                    resourceAuthority(resourcepermissionIds, newid);
                    flag = true;
                }
            }


            return flag;
        }
        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="ro">角色实体</param>
        /// <returns></returns>
        public int insert(role ro)
        {
            try
            {
                return db.Insert<role>(ro).ObjToInt();
            }
            catch (Exception)
            {
                return -1;
            }
        }
        /// <summary>
        ///  判断修改的角色是否合法
        /// </summary>
        /// <param name="roleentity">角色实体</param>
        /// <returns></returns>
        public bool isLegalEntity(role roleentity, string resourcepermissionIds, string actionpermissionIds)
        {
            bool flag = false;

            int count = db.Queryable<role>().Where(d => d.rolename == roleentity.rolename && d.roleexpiretime == roleentity.roleexpiretime && d.rolecode == roleentity.rolecode).Count();
            if (0 == count)
                flag = true;
            else
                flag = false;
            return flag;
        }
        /// <summary>
        /// 通过角色名获取部分角色信息
        /// </summary>
        /// <param name="rolename">角色名</param>
        /// <returns>部分角色信息</returns>
        public List<role> getRoleList(string rolename)
        {
            List<role> rolelist = new List<role>();
            if (!string.IsNullOrEmpty(rolename))
            {
                rolelist = getEntityList().Where(r => r.rolename == rolename).ToList();
            }
            else
            {
                rolelist = getEntityList();
            }
            return rolelist;
        }

        /// <summary>
        /// 根据角色id删除角色及该角色对应的动作和资源
        /// </summary>
        /// <param name="id">角色id</param>
        /// <returns></returns>
        public bool deleteroleAR(int id)
        {
            roleActionEx rae = new roleActionEx();
            roleResourceEx rre = new roleResourceEx();
            List<action> curactionList = rae.getRoleAllAction(id,"");
            List<resource> curresourceList = rre.getRoleAllResource(id,"");
            role r = getRole(id);
            bool rflag = db.Delete<role>(r);
            if (curactionList.Count == 0 && curresourceList.Count == 0)
                return rflag;
            else
            {
                bool raflag = false;
                bool rrflag = false;
                foreach (var item in curactionList)
                {
                    role_action ra = rae.getRoleAction(id, item.id);
                    raflag = db.Delete<role_action>(ra);
                }
                foreach (var item in curresourceList)
                {
                    role_resource rr = rre.getRoleResourceById(id, item.id);
                    rrflag = db.Delete<role_resource>(rr);
                }
                return rflag & raflag & rrflag;
            }
        }    

        #region ACE
        #region 分页+查询
        //var q = this.DbContext.Query<WikiDocument>().FilterDeleted().WhereIfNotNullOrEmpty(keyword, a => a.Title.Contains(keyword) || a.Tag.Contains(keyword));
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="page">分页信息</param>
        /// <param name="keyword">关键词</param>
        /// <returns>返回分页数据</returns>
        public PagedData<role> GetPageData(ACEPagination page, string keyword)
        {
            //查询相关的字段，根据页面的指定的字段
            PagedData<role> pageData = new PagedData<role>();
            pageData = TakePageData(page);
            try
            {
                if (string.IsNullOrEmpty(keyword))
                    pageData = TakePageData(page);
                else
                    pageData.DataList = pageData.DataList.Where(a => a.rolename.Contains(keyword)).ToList<role>();
                return pageData;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 类型转换成功与否判断
        /// </summary>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        public bool changeParse(string keyword)
        {
            try
            {
                Convert.ToInt32(keyword);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
        #endregion

    }
}
