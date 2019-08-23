using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Athena.model;
using Athena.Idal;
using SqlSugar;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：对用户角色表数据处理（陈兰兰）
//最后修改时间:2017-04-06
//修改日志：

namespace Athena.dal
{
    public class userRoleEx : RepositoryBase<user_role>, IuserRoleEx
    {
        #region 对象静态实例
        private static userRoleEx _instance;
        public static userRoleEx Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new userRoleEx();
                }
                return _instance;
            }
        }
        #endregion

        #region 1.基本操作
        /// <summary>
        /// 新增记录
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public override int insert(user_role table)
        {
            try
            {
                if (!isExistURid(table.userid, table.roleid)) //判断数据是否存在，不存在
                {
                    return db.Insert<user_role>(table as user_role).ObjToInt();
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
        /// 差异更新
        /// </summary>
        /// <param name="id">跟新数据主码</param>
        /// <param name="dictionary">需要更新的数据</param>
        /// <returns>返回更新结果（true/false）</returns>
        public bool update(int id, Dictionary<string, object> dictionary)
        {
            try
            {
                db.Update<user_role>(dictionary, p => p.id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 根据用户名获取角色列表
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<role> getRole(string username)
        {
            List<role> rr = new List<role>();
            user uu = db.Queryable<user>().Where(d => d.username == username).FirstOrDefault();
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
        /// <summary>
        /// 根据用户id获取角色列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<role> getRoleList(int id)
        {
            List<role> rr = new List<role>();
            user uu = db.Queryable<user>().Where(d => d.id == id).FirstOrDefault();
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
        /// <summary>
        /// 通过用户id获取所有关联表信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<user_role> getRole(int userid)
        {
            List<user_role> urs = db.Queryable<user_role>().Where(d => d.userid == userid).ToList();
            List<user_role> Iurs=new List<user_role>();
            foreach(user_role u_r in urs)
            {
                Iurs.Add(u_r as user_role);
            }
            return Iurs;
        }

        /// <summary>
        /// 通过用户id和角色ID获取所有关联表信息
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public user_role getUserRole(int userid, int roleid)
        {
            user_role ur = db.Queryable<user_role>().Where(d => d.userid == userid && d.roleid == roleid).FirstOrDefault();
            return ur as user_role;
        }

        /// <summary>
        /// 通过用户名获取所有角色
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<string> getRoleName(string username)
        {
            List<role> rr = new List<role>();
            user uu = db.Queryable<user>().Where(d => d.username == username).FirstOrDefault();
            if (uu != null)
                rr = db.Queryable<role>()
                    .JoinTable<user_role>((s1, s2) => s1.id == s2.roleid)
                    .Where<user_role>((s1, s2) => s2.userid == uu.id)
                    .Select("s1.*")
                    .ToList();
            List<string> rname = new List<string>(); ;
            if (rr.Count > 0)
                foreach (var e in rr) rname.Add(e.rolename);
            return rname;
        }

        /// <summary>
        /// 通过用户id获取所有角色名称，多个角色用逗号隔开
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string getRoleName(int userid)
        {
            List<role> rr = new List<role>();
            user uu = db.Queryable<user>().Where(d => d.id == userid).FirstOrDefault();
            if (uu != null)
                rr = db.Queryable<role>()
                    .JoinTable<user_role>((s1, s2) => s1.id == s2.roleid)
                    .Where<user_role>((s1, s2) => s2.userid == uu.id)
                    .Select("s1.*")
                    .ToList();
            string rname = ""; ;
            if (rr.Count > 0)
                foreach (var e in rr)
                {
                    rname += e.rolename;
                    rname += ',';
                }
            return rname.Trim(',');
        }

        /// <summary>
        /// 通过用户id获取所有角色id，多个角色id用逗号隔开
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string getRoleId(int userid)
        {
            List<user_role> ur = db.Queryable<user_role>().Where(d => d.userid == userid).ToList();
            string roleids = ""; ;
            if (ur.Count > 0)
                foreach (var e in ur)
                {
                    roleids += e.roleid;
                    roleids += ',';
                }
            return roleids.Trim(',');

        }

        /// <summary>
        /// 是否是有效的用户名，存在返回true
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public bool isExistURid(int userid, int roleid)
        {
            bool flag = false;
            int count = db.Queryable<user_role>()
                .Where(d => d.userid == userid && d.roleid == roleid)
                .Count();
            if (0 == count)//不存在
                flag = false;
            else
                flag = true;
            return flag;
        }
        /// <summary>
        /// 获取指定角色的特定类型和属主的资源列表
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns>指定角色的user_role对象列表</returns>
        public List<role> getUserRole(int userid)
        {
            List<role> rr = new List<role>();

            rr = db.Queryable<role>()
                   .JoinTable<user_role>((s1, s2) => s1.id == s2.roleid)
                   .Where<user_role>((s1, s2) => s2.userid == userid)
                   .Select("s1.*")
                   .ToList();

            List<role> Irr = new List<role>();
            foreach (role r in rr)
            {
                Irr.Add(r as role);
            }
            return Irr;
        }
        /// <summary>
        /// 通过用户id和角色ID获取所有关联表信息
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public user_role getRole(int userid, int roleid)
        {
            user_role ur = db.Queryable<user_role>().Where(d => d.userid == userid && d.roleid == roleid).FirstOrDefault();
            return ur as user_role;
        }


        #endregion


        #region 扩展方法
        /// <summary>
        /// 新增用户角色
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="roleid">角色id</param>
        /// <returns></returns>
        public int insertUserRole(int userid,int roleid){
            try
            {
                user_role ur = db.Queryable<user_role>().Where(d => d.roleid == roleid && d.userid == userid).FirstOrDefault();
                if (ur == null)
                {
                    user_role ure = new user_role();
                    ure.roleid = roleid;
                    ure.userid = userid;
                    return db.Insert<user_role>(ure).ObjToInt();
                }
                else
                    return ur.id;
            }catch(Exception){
                return -1;
            }
        }
        /// <summary>
        /// 根据角色id删除用户角色
        /// </summary>
        /// <param name="roleid">角色id</param>
        public void deleteUerRole(int userid ,int roleid)
        {
            //List<user_role> urList = db.Queryable<user_role>().Where(d => d.roleid == roleid).ToList();
            //foreach (var item in urList)
            //{
            //    this.delete(item.id);
            //}
             user_role ur = db.Queryable<user_role>().Where(d => d.roleid == roleid && d.userid == userid).FirstOrDefault();
                if (ur != null)
                {
                    ur.roleid = roleid;
                    ur.userid = userid;
                    this.delete(ur);
                }
        }
       

        #endregion


    }
}
