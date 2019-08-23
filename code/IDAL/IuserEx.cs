using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athena.Idal;
using Athena.model;
using System.Data;
using Athena.tool.ACEPagination;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：用户接口，供IOC（张梦丽）
//最后修改时间:2018-01-25
//修改日志：
//2017-04-10 接口方法添加（张婷婷）
//2017-12-17 新增前端为ACE相关的方法、同时删除用户及对应分组、角色（王露）

namespace Athena.Idal
{
   public interface IuserEx : IRepositoryBase<user>
    {
        #region 1.基本操作
       /// <summary>
       /// 添加用户并对密码加密
       /// </summary>
       /// <param name="u"></param>
       /// <returns></returns>
       int insertuser(user u);
        /// <summary>
        /// 根据用户名和密码新用户
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        int  insert(string username, string password);

        /// <summary>
        /// 根据用户名删除用户
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        bool delete(string username);

        /// <summary>
        /// 根据用户id和用户名更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        bool update(int id, string username);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool updatePsw(string username, string password);

        /// <summary>
        /// 差异更新
        /// </summary>
        /// <param name="id">跟新数据主码</param>
        /// <param name="dictionary">需要更新的数据</param>
        /// <returns>返回更新结果（true/false）</returns>
        bool update(int id, Dictionary<string, object> dictionary);

        /// <summary>
        /// 根据用户名获得某用户数据
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        user getUser(string username);
        /// <summary>
        /// 根据用户ID获取用户实例
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        user getUser(int id);
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        int getUserId(string username);

        /// <summary>
        /// 根据用户名获取公钥
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        string getPubkey(string username);

        /// <summary>
        /// 获取私钥
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        string getPrvkey(string username);

        /// <summary>
        /// 根据用户名获取ID
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        int getId(string username);

        /// <summary>
        /// 获取口令摘要，SM3散列
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        string getPasswordCipher(string username);

        /// <summary>
        /// 检查用户口令
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool checkPassword(string username, string password);

        /// <summary>
        /// 是否是有效的用户名，如果已存在，则返回false
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        bool isLegalUsername(string username);
        /// <summary>
        /// 更新用户，并对密码加密2017-12-28
        /// </summary>
        /// <param name="us">用户对象</param>
        /// <returns></returns>
        bool updateandencrypt(user us);
        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="username"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        string sign(string username, string msg);

        /// <summary>
        /// 签名验证
        /// </summary>
        /// <param name="username"></param>
        /// <param name="msg"></param>
        /// <param name="sig"></param>
        /// <returns></returns>
        bool verify(string username, string msg, string sig);

        /// <summary>
        /// 设置用户口令，并初始化用户秘钥对,如果用户已存在，则直接更新
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool setPassword(string username, string password);

        /// <summary>
        /// 设置用户公钥私钥
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        user SetUserKey(string username, string password);
        /// <summary>
       /// 根据用户id删除对应的用户及该用户对应的分组和角色
       /// </summary>
       /// <param name="id">用户id</param>
       /// <returns></returns>
        bool deleteuserRG(int id);
        #endregion

        #region BootstrapTemplate
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        List<user> getUserList(string username);
        void roleAuthority(string rolelistIds, int keyValue);
        void groupAuthority(string grouplistIds, int keyValue);
        bool SubmitForm(user u, string rolelistIds, string grouplistIds, string keyValue);
        DataTable getUserRoleGroup(string keyValue);
        List<user> getPaginationUserList(int pageIndex, int pageSize, Dictionary<string, object> searchStr); 
       /// <summary>
       /// 删除列表中指定用户,并判断除本身外用户是否存在
       /// </summary>
       /// <param name="userlist">用户列表</param>
        /// <param name="olduser">修改前用户信息</param>
        /// <param name="newuser">修改后用户信息</param>
       /// <returns></returns>
        bool isdeleterepeatuser(List<user> userlist, user olduser, user newuser);
        #endregion
        #region ACE
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="page">分页信息</param>
        /// <param name="keyword">关键词</param>
        /// <returns>返回分页数据</returns>
        PagedData<user> GetPageData(ACEPagination page, string keyword);
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="newPassword">新密码</param>
        /// <returns></returns>
        bool RevisePassword(int userId, string newPassword);
        #endregion
    }
}
