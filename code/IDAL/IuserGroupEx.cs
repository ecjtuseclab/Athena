using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athena.model;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：动作接口，供IOC（张梦丽）
//最后修改时间:2018-01-25
//修改日志：
//2017-04-10 接口方法添加（张婷婷）

namespace Athena.Idal
{
    public interface IuserGroupEx : IRepositoryBase<user_group>
    {
        /// <summary>
        /// 差异更新
        /// </summary>
        /// <param name="id">跟新数据主码</param>
        /// <param name="dictionary">需要更新的数据</param>
        /// <returns>返回更新结果（true/false）</returns>
        bool update(int id, Dictionary<string, object> dictionary);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string getAllUserGroupView();
          /// <summary>
        /// 根据用户名id获取分组列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<group> getGroupList(int id);
        /// <summary>
        /// 根据用户名获取分组
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        List<group> getGroup(string username);
        /// <summary>
        /// 根据用户id获取分组名称
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        string getGroupName(int userid);
        /// <summary>
        /// 新增用户分组
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="groupid">分组id</param>
        /// <returns></returns>
        int insertUserGroup(int userid, int groupid);
        /// <summary>
        /// 根据用户id和分组id删除用户分组
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="groupid">分组id</param>
        void deleteUserGroup(int userid, int groupid);
        /// <summary>
       /// 用户分组实体
       /// </summary>
       /// <param name="userid">用户id</param>
       /// <param name="groupid">分组id</param>
       /// <returns></returns>
        user_group getUserGroup(int userid, int groupid);


    }
}
