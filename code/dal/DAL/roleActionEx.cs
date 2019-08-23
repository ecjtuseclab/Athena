using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Athena.Idal;
using Athena.model;
using SqlSugar;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员： 周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：对角色动作表进行数据处理
//最后修改时间:2018-01-26
//修改日志：
//2017-04-11 新增获取所有对象条数方法(张婷婷)
//2017-12-19 新增ACE相关的方法（王露）
    
namespace Athena.dal
{    
    public class roleActionEx : RepositoryBase<role_action>, IroleActionEx
    {
        #region 对象静态实例
        private static roleActionEx _instance;
        public static roleActionEx Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new roleActionEx();
                }
                return _instance;
            }
        }
        #endregion
        /// <summary>
        ///判断新增的role_action对象是否合法，即是否有同样的数据
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="actionid">资源id</param>
        /// <returns></returns>
        public bool isLegalRoleAction(int roleid, int actionid)
        {
            bool flag = false;
            int count = 0;
            try
            {
                count = db.Queryable<role_action>().Where(d => d.roleid == roleid && d.actionid == actionid).Count();
                if (0 == count)
                    flag = true;
                else
                    flag = false;
                return flag;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 插入角色动作数据
        /// </summary>
        /// <param name="actionid">动作数据id</param>
        /// <param name="roleid">角色数据id</param>
        /// <returns>角色动作数据id</returns>
        public int addRoleAction(int actionid, int roleid)
        {
            try
            {
                role_action r = db.Queryable<role_action>().Where(d => d.actionid == actionid && d.roleid == roleid).FirstOrDefault();
                if (r == null)
                {
                    role_action ra = new role_action();
                    ra.roleid = roleid;
                    ra.actionid = actionid;
                    ra.controlername = "Role";
                    ra.actionscode = 0;
                    return db.Insert<role_action>(ra).ObjToInt();
                }
                else
                    return r.id;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        /// <summary>
        /// 新增role_action对象
        /// </summary>
        /// <param name="table">role_action对象</param>
        /// <returns>成功：新增数据的主码id;失败：-1</returns>
        public override int insert(role_action table)
        {
            try
            {
                if (isLegalRoleAction(table.roleid, table.actionid))
                {
                    return db.Insert<role_action>(table).ObjToInt();
                }
                else
                    return -1;

            }
            catch (Exception)
            {
                return -1;
            }
        }
        /// <summary>
        /// 删除角色动作数据
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="actionid">动作id</param>
        /// <returns></returns>
        public bool delete(int roleid, int actionid)
        {
            try
            {
                db.Delete<role_action>(d => d.actionid == actionid && d.roleid == roleid);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        ///// <summary>
        ///// 删除指定角色的所有角色动作数据
        ///// </summary>
        ///// <param name="roleid">角色数据id</param>
        ///// <returns>bool值</returns>
        //public override bool delete(int roleid)
        //{
        //    try
        //    {
        //        db.Delete<role_action>(d => d.roleid == roleid);
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

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
                db.Update<role_action>(dictionary, p => p.id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 根据角色动作id获取角色动作数据
        /// </summary>
        /// <param name="roleactionid">角色数据id</param>
        /// <returns>一条角色动作数据</returns>
        public role_action getRoleAction(int roleactionid)
        {
            try
            {
                role_action table = db.Queryable<role_action>().Where(d => d.id == roleactionid).FirstOrDefault();
                return table as role_action;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获得指定角色的动作数据
        /// </summary>
        /// <param name="roleid">角色数据id</param>
        /// <param name="owner">动作数据控制器</param>
        /// <returns>动作数据list</returns>
        public List<action> getRoleAllAction(int roleid, string owner = "")
        {
            List<action> acs = new List<action>();
            if (owner == "")//不指定属主则获取全部，否则获取指定属主的
            {
                acs = db.Queryable<action>()
                    .JoinTable<role_action>((s1, s2) => s1.id == s2.actionid)
                    .Where<role_action>((s1, s2) => s2.roleid == roleid)
                    .Select("s1.*")
                    .ToList();
            }
            else
            {

                acs = db.Queryable<action>()
                    .JoinTable<role_action>((s1, s2) => s1.id == s2.actionid)
                    .Where<role_action>((s1, s2) => s2.roleid == roleid)
                    .Where<action>((s1, s2) => s1.actionowner == owner)
                    .Select("s1.*")
                    .ToList();


            }
            List<action> Iacs = new List<action>();
            foreach (action a in acs)
            {
                Iacs.Add(a as action);
            }
            return Iacs;
        }
        /// <summary>
        /// 角色动作实体
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="actionid">动作id</param>
        /// <returns></returns>
        public role_action getRoleAction(int roleid, int actionid)
        {
            role_action ra = db.Queryable<role_action>().Where(d => d.roleid == roleid && d.actionid == actionid).FirstOrDefault();
            return ra as role_action;
        }
        /// <summary>
        /// 获得指定角色的特定类型的动作数据
        /// </summary>
        /// <param name="roleid">角色数据id</param>
        /// <param name="type">角色类型</param>
        /// <param name="owner">动作数据控制器</param>
        /// <returns>动作数据list</returns>
        public List<action> getRoleAction(int roleid, int type, string owner = "")//获取指定角色的特定类型和属主的动作列表
        {
            List<action> acs = new List<action>();
            if (owner == "")//不指定属主则获取全部，否则获取指定属主的
            {

                acs = db.Queryable<action>()
                       .JoinTable<role_action>((s1, s2) => s1.id == s2.actionid)
                       .Where<role_action>((s1, s2) => s2.roleid == roleid)
                       .Where<action>((s1, s2) => s1.actiontype == type)
                       .Select("s1.*")
                       .ToList();

            }
            else
            {

                acs = db.Queryable<action>()
                       .JoinTable<role_action>((s1, s2) => s1.id == s2.actionid)
                       .Where<role_action>((s1, s2) => s2.roleid == roleid)
                       .Where<action>((s1, s2) => s1.actiontype == type)
                       .Where<action>((s1, s2) => s1.actionowner == owner)
                       .Select("s1.*")
                       .ToList();
            }
            List<action> Iacs = new List<action>();
            foreach (action a in acs)
            {
                Iacs.Add(a as action);
            }
            return Iacs;
        }

    }
}
