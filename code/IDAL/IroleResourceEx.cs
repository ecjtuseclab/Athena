using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athena.model;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：角色资源接口，供IOC（张梦丽）
//最后修改时间:2018-01-25
//修改日志：
//2017-04-10 动作接口，供IOC（张梦丽）
//2017-04-11 新增获取所有对象条数方法（张婷婷）
    
namespace Athena.Idal
{    
    public interface IroleResourceEx : IRepositoryBase<role_resource>
    {
        #region 1.基本操作
        /// <summary>
        ///判断新增的role_resource对象是否合法，即是否有同样的数据
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="resourceid">资源id</param>
        /// <returns></returns>
        bool isLegalRoleResource(int roleid, int resourceid);


        /// <summary>
        /// 删除role_resource对象
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="resourceid">资源id</param>
        /// <returns></returns>
        bool delete(int roleid, int resourceid);

        /// <summary>
        /// 差异更新
        /// </summary>
        /// <param name="id">跟新数据主码</param>
        /// <param name="dictionary">需要更新的数据</param>
        /// <returns>返回更新结果（true/false）</returns>
        bool update(int id, Dictionary<string, object> dictionary);

        /// <summary>
        /// 获取指定角色的特定类型和属主的资源列表
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="type">资源类型</param>
        /// <param name="owner">资源owner</param>
        /// <returns>指定角色的role_resource对象列表</returns>
        List<resource> getRoleResource(int roleid, int type, string owner = "");

        /// <summary>
        /// 获取指定角色属主的资源列表
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="owner">resourceowner</param>
        /// <returns>指定角色的role_resource对象列表</returns>
        List<resource> getRoleAllResource(int roleid, string owner = "");
        /// <summary>
        /// 角色资源实体
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="resourceid">资源id</param>
        /// <returns></returns>
        role_resource getRoleResourceById(int roleid, int resourceid);
        
        #endregion
    }
}
