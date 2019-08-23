using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athena.model;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：用户角色接口，供IOC
//最后修改时间:2018-01-25
//修改日志：

namespace Athena.Idal
{
    public interface IuserRoleEx : IRepositoryBase<user_role>
    {
        #region 1.基本操作
        /// <summary>
        /// 新增记录
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
         int insert(user_role table);

        /// <summary>
        /// 差异更新
        /// </summary>
        /// <param name="id">跟新数据主码</param>
        /// <param name="dictionary">需要更新的数据</param>
        /// <returns>返回更新结果（true/false）</returns>
        bool update(int id, Dictionary<string, object> dictionary);

        /// <summary>
        /// 根据用户名获取角色列表
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        List<role> getRole(string username);
        /// <summary>
        /// 根据用户id获取角色列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<role> getRoleList(int id);
        /// <summary>
        /// 通过用户id获取所有关联表信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        List<user_role> getRole(int userid);

        /// <summary>
        /// 通过用户id和角色ID获取所有关联表信息
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="roleid"></param>
        /// <returns></returns>
        user_role getUserRole(int userid, int roleid);

        /// <summary>
        /// 通过用户名获取所有角色
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        List<string> getRoleName(string username);

        /// <summary>
        /// 通过用户id获取所有角色名称，多个角色用逗号隔开
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        string getRoleName(int userid);

        /// <summary>
        /// 通过用户id获取所有角色id，多个角色id用逗号隔开
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        string getRoleId(int userid);

        /// <summary>
        /// 是否是有效的用户名，存在返回true
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="roleid"></param>
        /// <returns></returns>
        bool isExistURid(int userid, int roleid);
        /// <summary>
        /// 获取指定角色的特定类型和属主的资源列表
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns>指定角色的user_role对象列表</returns>
        List<role> getUserRole(int userid);
         /// <summary>
        /// 通过用户id和角色ID获取所有关联表信息
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="roleid"></param>
        /// <returns></returns>
        user_role getRole(int userid, int roleid);

        #endregion
        #region 扩展方法
        /// <summary>
        /// 新增用户角色
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="roleid">角色id</param>
        /// <returns>用户角色id</returns>
        int insertUserRole(int userid, int roleid);
        /// <summary>
        /// 根据角色id删除用户角色
        /// </summary>
        /// <param name="roleid">角色id</param>
        //void deleteEntity(int roleid);
        void deleteUerRole(int userid, int roleid);
       #endregion

    }
}
