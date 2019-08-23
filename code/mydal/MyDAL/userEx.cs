using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Athena.model;
using Athena.Idal;
using MySqlSugar;
using Smatrix.Crypto;
using Athena.tool.ACEPagination;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：对用户表的数据处理
//最后修改时间:2018-01-25
//修改日志：
//2017-12-17 新增前端为ACE相关的方法、同时删除用户及对应分组、角色（王露）

namespace Athena.mydal
{
    public class userEx : RepositoryBase<user>, IuserEx
    {
        #region 对象静态实例
        private static userEx _instance;
        public static userEx Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new userEx();
                }
                return _instance;
            }
        }
        #endregion

        #region 1.基本操作
        /// <summary>
        /// 新增用户并对密码进行加密
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public int insertuser(user u)
        {
            try
            {
                if (isLegalUsername(u.username))
                {
                    TanSM3Ex sm3 = new TanSM3Ex();
                    string digest = sm3.TanGetDigest(u.password);
                    u.password = digest;
                    return db.Insert<user>(u).ObjToInt();
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }
        /// <summary>
        /// 修改用户时根据用户名和密码添加新用户
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int insert(string username, string password)
        {
            try
            {
                if (isLegalUsername(username))
                {
                    user u = SetUserKey(username, password);
                    //u.rolelist=
                    return db.Insert<user>(u).ObjToInt();
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }
        /// <summary>
        /// 更新用户信息，并对用户密码加密
        /// </summary>
        /// <param name="us"></param>
        /// <returns></returns>
        public bool updateandencrypt(user us)
        {

            bool flag = false;
            user olduser = getEntityById(us.id);
            List<user> userlist = getEntityList();
            flag = isdeleterepeatuser(userlist, olduser, us);
            TanSM3Ex sm3 = new TanSM3Ex();
            user uuser = getEntityById(us.id);

            try
            {
                if (uuser.password == us.password)
                {
                    us.password = uuser.password;
                }
                else
                {
                    us.password = sm3.TanGetDigest(us.password);
                }
                if (flag)
                    this.update(us);
                //flag = true;
            }
            catch (Exception)
            {
                return false;
            }
            return flag;
        }
        /// <summary>
        /// 根据用户名删除用户
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool delete(string username)
        {
            try
            {
                user u = getUser(username);
                return delete(u as user);

            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 根据用户id和用户名更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool update(int id, string username)
        {
            try
            {
                db.Update<user>(new { username = username }, u => u.id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
          /// <summary>
        /// 根据用户id获取用户实例
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public user getUser(int id)
        {
            user table = db.Queryable<user>().Where(d => d.id == id).FirstOrDefault();
            return table;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool updatePsw(string username, string password)
        {
            try
            {
                user un = SetUserKey(username, password);
                db.Update<user>(un);
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
                db.Update<user>(dictionary, p => p.id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }
        
        /// <summary>
        /// 根据用户名获得某用户数据
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public user getUser(string username)
        {
            user table = db.Queryable<user>().Where(d => d.username == username).FirstOrDefault();
            return table;
        }
        /// <summary>
        /// 根据用户id删除对应的用户及该用户对应的分组和角色
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        public bool deleteuserRG(int id)
        {
            userRoleEx ure = new userRoleEx();
            List<role> curroleList = ure.getRoleList(id);
            userGroupEx uge = new userGroupEx();
            List<group> curgroupList = uge.getGroupList(id);
            user u = getUser(id);
            bool uflag = db.Delete<user>(u);
            if (curroleList.Count == 0 && curgroupList.Count == 0)
                return uflag;
            else
            {
                bool urflag = false;
                bool ugflag = false;
                foreach (var item in curroleList)
                {
                    user_role ur = ure.getUserRole(id, item.id);
                    urflag = db.Delete<user_role>(ur);
                }
                foreach (var item in curgroupList)
                {
                    user_group ug = uge.getUserGroup(id, item.id);
                    ugflag = db.Delete<user_group>(ug);
                }
                return uflag & urflag & ugflag;
            }
        }
        /// <summary>
        /// 删除列表中指定用户,并判断除本身外用户是否存在
        /// </summary>
        /// <param name="userlist"></param>
        /// <param name="u"></param>
        /// <returns></returns>
        public bool isdeleterepeatuser(List<user> userlist, user olduser, user newuser)
        {
            bool flag = true;
            List<user> ulist = new List<user>();
            foreach (user uu in userlist)
            {
                if (uu.username != olduser.username)
                    ulist.Add(uu);
            }
            foreach (user uu in ulist)
            {
                if (newuser != null && uu.username == newuser.username)
                    flag = false;
            }
            return flag;
        }
        /// <summary>
        /// 提交表单
        /// </summary>
        /// <param name="u">用户对象</param>
        /// <param name="rolelistIds">角色id</param>
        /// <param name="grouplistIds">分组id</param>
        /// <param name="keyValue"></param>
        public bool SubmitForm(user u, string rolelistIds, string grouplistIds, string keyValue)
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(keyValue))
            {
                int id = Convert.ToInt32(keyValue);
                u.id = id;
                user uu = getUser(id);
                if (uu != null)
                {

                    user us = getUser(u.username);
                    List<user> userlist = getEntityList();
                    bool flg = isdeleterepeatuser(userlist, uu, us);
                    if (flg)
                    {
                        db.Update<user>(u);
                        roleAuthority(rolelistIds, id);
                        groupAuthority(grouplistIds, id);
                        flag = true;
                    }
                }

            }
            else if (getUser(u.username) == null)
            {

                int newid = insertuser(u);
                if (newid > 0)
                {
                    // List<user> userlist = this.getEntityList();
                    roleAuthority(rolelistIds, newid);
                    groupAuthority(grouplistIds, newid);
                    flag = true;
                }
            }
            return flag;
        }
        /// <summary>
        /// 给用户赋予角色
        /// </summary>
        /// <param name="rolelistIds">多个选中的角色id</param>
        /// <param name="keyValue">用户id</param>
        public void roleAuthority(string rolelistIds, int keyValue)
        {
            user u = this.getEntityById(keyValue);
            roleEx re = new roleEx();
            IuserRoleEx userRoleEx = new userRoleEx();
            List<role> currentUserAllRole = re.getUserRoles(u.username);
            List<string> currentUserRoleIds = new List<string>();
            foreach (var item in currentUserAllRole)
            {
                currentUserRoleIds.Add(item.id.ToString());
            }
            if (!string.IsNullOrEmpty(rolelistIds))
            {
                string[] ids = rolelistIds.Split(',');
                List<string> roleIdsList = ids.ToList();
                var interselectlist = roleIdsList.Intersect(currentUserRoleIds).ToList();//对数据库中角色id和当前用户的角色id做交集
                var exceptList = currentUserRoleIds.Except(roleIdsList).ToList();//做差集，选出当前用户数据库中有但没有传入的id。
                if (exceptList != null)
                {
                    foreach (var item in exceptList)
                    {
                        //userRoleEx.delete(Convert.ToInt32(item));
                        // userRoleEx.deleteEntity(Convert.ToInt32(item));
                        userRoleEx.deleteUerRole(keyValue, Convert.ToInt32(item));
                    }
                }
                var exceptList1 = roleIdsList.Except(currentUserRoleIds).ToList();//做差集，选出传入的角色id有但数据库中该用户没有的角色id
                if (exceptList1 != null)
                {
                    foreach (var item in exceptList1)
                    {
                        userRoleEx.insertUserRole(keyValue, Convert.ToInt32(item));
                    }
                }
            }
        }
        /// <summary>
        /// 给用户分配分组
        /// </summary>
        /// <param name="grouplistIds">分组id</param>
        /// <param name="keyValue">用户id</param>
        public void groupAuthority(string grouplistIds, int keyValue)
        {
            user u = this.getEntityById(keyValue);
            IuserGroupEx userGroupEx = new userGroupEx();
            userGroupEx uge = new mydal.userGroupEx();
            List<group> currentUserGroup = uge.getGroup(u.username);
            List<string> currentUserGroupIds = new List<string>();
            foreach (var item in currentUserGroup)
            {
                currentUserGroupIds.Add(item.id.ToString());
            }
            if (!string.IsNullOrEmpty(grouplistIds))
            {
                string[] ids = grouplistIds.Split(',');
                List<string> groupIds = ids.ToList();
                var interselectList = groupIds.Intersect(currentUserGroupIds).ToList();//对数据库中分组id和当前用户的分组id做交集
                var exceptList = currentUserGroupIds.Except(groupIds).ToList();////做差集，选出当前用户数据库中有但没有传入的分组id。
                if (exceptList != null)
                {
                    foreach (var item in exceptList)
                    {
                        userGroupEx.deleteUserGroup(keyValue, Convert.ToInt32(item));
                    }
                }
                var exceptList1 = groupIds.Except(currentUserGroupIds).ToList();//做差集，选出传入的分组id中有但数据库中该用户没有的分组id
                if (exceptList1 != null)
                {
                    foreach (var item in exceptList1)
                    {
                        userGroupEx.insertUserGroup(keyValue, Convert.ToInt32(item));
                    }
                }
            }
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int getUserId(string username)
        {
            int id = 0;
            user table = db.Queryable<user>().Where(d => d.username == username).FirstOrDefault();
            if (table != null) id = table.id;
            return id;
        }

        /// <summary>
        /// 根据用户名获取公钥
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public string getPubkey(string username)
        {
            string pkey = "";
            user u = db.Queryable<user>().Where(d => d.username == username).FirstOrDefault();
            if (u != null) pkey = u.pubkey;
            return pkey;
        }

        /// <summary>
        /// 获取私钥
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public string getPrvkey(string username)
        {
            string prvkey = "";
            user u = db.Queryable<user>()
                .Where(d => d.username == username)
                .FirstOrDefault();
            if (u != null) prvkey = u.prikey;
            return prvkey;
        }

        /// <summary>
        /// 根据用户名获取ID
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int getId(string username)
        {
            int id = 0;
            user u = db.Queryable<user>()
                .Where(d => d.username == username)
                .FirstOrDefault();
            if (u != null) id = u.id;
            return id;
        }

        /// <summary>
        /// 获取口令摘要，SM3散列
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public string getPasswordCipher(string username)
        {
            string pass = "";
            user u = db.Queryable<user>()
                       .Where(d => d.username == username)
                       .FirstOrDefault();
            if (u != null) pass = u.password;
            return pass;
        }

        /// <summary>
        /// 检查用户口令
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool checkPassword(string username, string password)
        {
            bool flag = false;
            TanSM3Ex sm3 = new TanSM3Ex();
            string digest = sm3.TanGetDigest(password);
            string cipher = getPasswordCipher(username);
            if (digest == cipher)
                flag = true;
            else
                flag = false;
            return flag;
        }

        /// <summary>
        /// 是否是有效的用户名，如果已存在，则返回false
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool isLegalUsername(string username)
        {
            bool flag = false;
            int count = db.Queryable<user>()
                .Where(d => d.username == username)
                .Count();
            if (0 == count)
                flag = true;
            else
                flag = false;
            return flag;
        }

        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="username"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string sign(string username, string msg)
        {
            //  string result = "";
            try
            {
                user u = db.Queryable<user>()
                    .Where(d => d.username == username)
                    .FirstOrDefault();
                if (u == null) return "";
                TanSM2Ex sm2 = new TanSM2Ex();
                string sig = "";
                sm2.TanSM2Sign(msg, u.prikey, out sig);
                return sig;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// 签名验证
        /// </summary>
        /// <param name="username"></param>
        /// <param name="msg"></param>
        /// <param name="sig"></param>
        /// <returns></returns>
        public bool verify(string username, string msg, string sig)
        {
            bool flag = false;
            try
            {
                user u = db.Queryable<user>()
                           .Where(d => d.username == username)
                           .FirstOrDefault();
                if (u == null) return false;
                TanSM2Ex sm2 = new TanSM2Ex();
                flag = sm2.TanSM2Verify(msg, u.pubkey, sig);
                return flag;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 设置用户口令，并初始化用户秘钥对,如果用户已存在，则直接更新
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool setPassword(string username, string password)
        {
            bool flag = true;
            try
            {
                user u = SetUserKey(username, password);
                if (isLegalUsername(username))
                    db.Insert<user>(u);
                else
                    db.Update<user>(u);
            }
            catch (Exception)
            {
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// 设置用户公钥私钥
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public user SetUserKey(string username, string password)
        {
            TanSM3Ex sm3 = new TanSM3Ex();
            string digest = sm3.TanGetDigest(password);
            TanSM2Ex sm2 = new TanSM2Ex();
            sm2.TanGenSM2KeyPair();
            string pubkey = sm2.publicKey;
            string prvkey = sm2.privateKey;
            user u = getUser(username);
            if (u == null) u = new user() as user;
            u.username = username;
            u.password = digest;
            u.pubkey = pubkey;
            u.prikey = prvkey;

            return u as user;
        }
        #endregion
        #region ACE
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
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="page">分页信息</param>
        /// <param name="keyword">关键词</param>
        /// <returns>返回分页数据</returns>
        public PagedData<user> GetPageData(ACEPagination page, string keyword)
        {
            //查询相关的字段，根据页面的指定的字段
            PagedData<user> pageData = new PagedData<user>();
            pageData = TakePageData(page);
            try
            {
                if (string.IsNullOrEmpty(keyword))
                    pageData = TakePageData(page);
                else if (changeParse(keyword))
                    pageData.DataList = pageData.DataList.Where(a => a.id == Convert.ToInt32(keyword)).ToList<user>();
                else
                    pageData.DataList = pageData.DataList.Where(a => a.username.Contains(keyword)).ToList<user>();
                return pageData;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="newPassword">新密码</param>
        /// <returns></returns>
        public bool RevisePassword(int userId, string newPassword)
        {
            try
            {
                user u = getEntityById(userId);
                u.password = newPassword;
                return update(u);
            }
            catch (Exception)
            {
                return false;
            }

        }
        #endregion
        #region BootstrapTemplate扩展方法
        /// <summary>
        /// 根据用户名获取用户列表
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<user> getUserList(string username)
        {
            List<user> userList = new List<user>();
            if (!string.IsNullOrEmpty(username))
            {
                userList = getEntityList().Where(d => d.username == username).ToList();
            }
            else
            {
                userList = getEntityList();
            }
            return userList;
        }
        /// <summary>
        /// 获取用户表的分页并显示用户角色及分组
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize">页面的数据总数</param>
        /// <param name="searchStr">搜索的字段</param>
        /// <returns></returns>
        public List<user> getPaginationUserList(int pageIndex, int pageSize, Dictionary<string, object> searchStr)
        {
            userRoleEx ure = new userRoleEx();
            userGroupEx uge = new userGroupEx();
            List<user> entitylist = new List<user>();
            var entitydata = getEntityList(searchStr);
            var pagerfirst = (pageIndex - 1) * pageSize;
            entitylist = entitydata.Skip(pagerfirst).Take(pageSize).ToList();
            foreach (user us in entitylist)
            {
                us.rolelist = ure.getRoleName(us.id);
                us.grouplist = uge.getGroupName(us.id);
            }
            return entitylist;
        }
        /// <summary>
        /// 获取用户及角色和所属分组
        /// </summary>
        /// <returns></returns>
        public DataTable getUserRoleGroup(string keyValue)
        {
            var r = db.Queryable<user>().ToList();
            DataTable dt = db.Queryable<user>().ToDataTable();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                userRoleEx ure = new userRoleEx();
                userGroupEx uge = new userGroupEx();
                dt.Rows[i]["rolelist"] = ure.getRoleName(Convert.ToInt32(dt.Rows[i]["id"]));
                dt.Rows[i]["grouplist"] = uge.getGroupName(Convert.ToInt32(dt.Rows[i]["id"]));
            }
            return dt;
        }

        #endregion
    }
}
