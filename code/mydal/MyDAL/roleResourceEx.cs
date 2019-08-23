using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athena.Idal;
using Athena.model;
using MySqlSugar;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员： 周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：对角色资源表进行数据处理
//最后修改时间:2018-01-26
//修改日志：
//2017-04-11 新增获取所有对象条数方法(张婷婷)
    
namespace Athena.mydal
{    
    public class roleResourceEx : RepositoryBase<role_resource>, IroleResourceEx
    {
        #region 对象静态实例
        private static roleResourceEx _instance;
        public static roleResourceEx Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new roleResourceEx();
                }
                return _instance;
            }
        }
        #endregion

        #region 1.基本操作
        /// <summary>
        ///判断新增的role_resource对象是否合法，即是否有同样的数据
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="resourceid">资源id</param>
        /// <returns></returns>
        public bool isLegalRoleResource(int roleid, int resourceid)
        {
            bool flag = false;
            int count = 0;
            try
            {
                count = db.Queryable<role_resource>().Where(d => d.roleid == roleid && d.resoureceid == resourceid).Count();
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
        /// 新增role_resource对象
        /// </summary>
        /// <param name="table">role_resource对象</param>
        /// <returns>成功：新增数据的主码id;失败：-1</returns>
        public override int insert(role_resource table)
        {
            try
            {
                if (isLegalRoleResource(table.roleid, table.resoureceid))
                {
                    return db.Insert<role_resource>(table).ObjToInt();
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
        /// 删除role_resource对象
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="resourceid">资源id</param>
        /// <returns></returns>
        public bool delete(int roleid, int resourceid)
        {
            try
            {
                db.Delete<role_resource>(d => d.resoureceid == resourceid && d.roleid == roleid);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

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
                db.Update<role_resource>(dictionary, p => p.id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 角色资源实体
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="resourceid">资源id</param>
        /// <returns></returns>
        public role_resource getRoleResourceById(int roleid, int resourceid)
        {
            role_resource rr = db.Queryable<role_resource>().Where(d => d.roleid == roleid && d.resoureceid == resourceid).FirstOrDefault();
            return rr as role_resource;
        }
        /// <summary>
        /// 获取指定角色的特定类型和属主的资源列表
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="type">资源类型</param>
        /// <param name="owner">资源owner</param>
        /// <returns>指定角色的role_resource对象列表</returns>
        public List<resource> getRoleResource(int roleid, int type, string owner = "")
        {
            List<resource> rr = new List<resource>();
            if (owner == "")//不指定属主则获取全部，否则获取指定属主的
            {
                rr = db.Queryable<resource>()
                       .JoinTable<role_resource>((s1, s2) => s1.id == s2.resoureceid)
                       .Where<role_resource>((s1, s2) => s2.roleid == roleid)
                       .Where<resource>((s1, s2) => s1.resourcetype == type)
                       .Select("s1.*")
                       .ToList();
            }
            else
            {
                rr = db.Queryable<resource>()
                       .JoinTable<role_resource>((s1, s2) => s1.id == s2.resoureceid)
                       .Where<role_resource>((s1, s2) => s2.roleid == roleid)
                       .Where<resource>((s1, s2) => s1.resourcetype == type)
                       .Where<resource>((s1, s2) => s1.resourceowner == owner)
                       .Select("s1.*")
                       .ToList();

            }
            List<resource> Irr = new List<resource>();
            foreach (resource r in rr)
            {
                Irr.Add(r as resource);
            }
            return Irr;
        }

        /// <summary>
        /// 获取指定角色属主的资源列表
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="owner">resourceowner</param>
        /// <returns>指定角色的role_resource对象列表</returns>
        public List<resource> getRoleAllResource(int roleid, string owner = "")
        {
            List<resource> rr = new List<resource>();
            if (owner == "")//不指定属主则获取全部，否则获取指定属主的
            {
                rr = db.Queryable<resource>()
                       .JoinTable<role_resource>((s1, s2) => s1.id == s2.resoureceid)
                       .Where<role_resource>((s1, s2) => s2.roleid == roleid)
                    // .Where<resource>((s1, s2) => s1.resourcetype == type)
                       .Select("s1.*")
                       .ToList();
            }
            else
            {
                rr = db.Queryable<resource>()
                       .JoinTable<role_resource>((s1, s2) => s1.id == s2.resoureceid)
                       .Where<role_resource>((s1, s2) => s2.roleid == roleid)                    
                       .Where<resource>((s1, s2) => s1.resourceowner == owner)
                       .Select("s1.*")
                       .ToList();

            }
            List<resource> Irr = new List<resource>();
            foreach (resource r in rr)
            {
                Irr.Add(r as resource);
            }
            return Irr;
        }
        #endregion
    }
}
