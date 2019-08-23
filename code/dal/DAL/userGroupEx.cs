using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Athena.model;
using SqlSugar;
using Athena.Idal;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：用户与组之间的数据处理（夏萍萍）
//最后修改时间:2017-03-05
//修改日志：

namespace Athena.dal
{
   public class userGroupEx : RepositoryBase<user_group>, IuserGroupEx
    {
        #region 对象静态实例
        private static userGroupEx _instance;
        public static userGroupEx Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new userGroupEx();
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
                db.Update<user_group>(dictionary, p => p.id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
         }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string getAllUserGroupView()
        {
            var ugv =
                db.Queryable<user_group>()
                .JoinTable<user>((ug, u) => ug.userid == u.id) //默认left join
                .ToJson();
            return ugv;
        }
        /// <summary>
        /// 根据用户名获取分组列表
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<group> getGroup(string username)
        {
            List<group> g = new List<group>();
            user uu = db.Queryable<user>().Where(d =>d.username ==username).FirstOrDefault();
            if(uu !=null){
                g = db.Queryable<group>()
                    .JoinTable<user_group>((s1, s2) => s1.id == s2.groupid)
                    .Where<user_group>((s1, s2) => s2.userid == uu.id)
                    .Select("s1.*")
                    .ToList();
            }
            return g;
        }
        /// <summary>
        /// 根据用户名id获取分组列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<group> getGroupList(int id)
        {
            List<group> g = new List<group>();
            user uu = db.Queryable<user>().Where(d => d.id == id).FirstOrDefault();
            if (uu != null)
            {
                g = db.Queryable<group>()
                    .JoinTable<user_group>((s1, s2) => s1.id == s2.groupid)
                    .Where<user_group>((s1, s2) => s2.userid == uu.id)
                    .Select("s1.*")
                    .ToList();
            }
            return g;
        }
        /// <summary>
        /// 根据用户id获取分组名称
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns>分组名称</returns>
        public string getGroupName(int userid)
        {
            List<group> g = new List<group>();
            user uu = db.Queryable<user>().Where(d => d.id == userid).FirstOrDefault();
            if(uu !=null){
                g = db.Queryable<group>()
                    .JoinTable<user_group>((s1, s2) => s1.id == s2.groupid)
                    .Where<user_group>((s1, s2) => s2.userid == userid)
                    .Select("s1.*")
                    .ToList();
            }
            string gname = "";
            if(g.Count>0)
                foreach(var e in g){
                    gname += e.groupname;
                    gname +=',';
                }
            return gname;
        }
        /// <summary>
        /// 新增用户分组
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="groupid">分组id</param>
        /// <returns></returns>
        public int insertUserGroup(int userid,int groupid)
        {
            try
            {
                user_group ug = db.Queryable<user_group>().Where(d =>d.groupid ==groupid && d.userid ==userid).FirstOrDefault();
                if (ug == null)
                {
                    user_group ugp = new user_group();
                    ugp.userid = userid;
                    ugp.groupid = groupid;
                    return db.Insert<user_group>(ugp).ObjToInt();
                }
                else
                    return ug.id;

            }catch(Exception){
                return -1;
            }
        }
        /// <summary>
        /// 根据用户id和分组id删除用户分组
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="groupid">分组id</param>
        public void deleteUserGroup(int userid,int groupid)
        {
            user_group ug = db.Queryable<user_group>().Where(d => d.userid ==userid && d.groupid ==groupid).FirstOrDefault();
            if( ug !=null){
                this.delete(ug);
            }
        }
       /// <summary>
       /// 用户分组实体
       /// </summary>
       /// <param name="userid">用户id</param>
       /// <param name="groupid">分组id</param>
       /// <returns></returns>
        public user_group getUserGroup(int userid, int groupid)
        {
            user_group ug = db.Queryable<user_group>().Where(d => d.userid == userid && d.groupid == groupid).FirstOrDefault();
            return ug as user_group;
        }

    }
}
