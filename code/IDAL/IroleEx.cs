using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athena.model;
using Athena.tool.ACEPagination;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：角色接口，供IOC（张梦丽）
//最后修改时间:2018-01-25
//修改日志：
//2017-04-10 新增获取所有对象条数方法接口（张婷婷）

namespace Athena.Idal
{
    #region 基本操作
    public interface IroleEx: IRepositoryBase<role>
    {
        /// <summary>
        /// 差异更新
        /// </summary>
        /// <param name="id">跟新数据主码</param>
        /// <param name="dictionary">需要更新的数据</param>
        /// <returns>返回更新结果（true/false）</returns>
        bool update(int id, Dictionary<string, object> dictionary);

        /// <summary>
        /// 根据角色名称获取角色数据
        /// </summary>
        /// <param name="rolename">角色名称</param>
        /// <returns>一条角色数据</returns>
        role getRole(string rolename);
        /// <summary>
        /// 通过角色id获取角色实体对象
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        role getRole(int roleid);
        List<role> getUserRoles(string userName);

        /// <summary>
        /// 根据角色名称获取角色数据id
        /// </summary>
        /// <param name="rolename">角色名称</param>
        /// <returns>角色数据id</returns>
        int getRoleId(string rolename);
        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="ro">角色实体</param>
        /// <returns></returns>
        int insert(role ro);
        /// <summary>
        /// 判断角色名称是否已经存在
        /// </summary>
        /// <param name="rolename">角色名称</param>
        /// <returns>bool值</returns>
        bool isLegalRoleName(string rolename,int rolecode);
        /// <summary>
       /// 修改角色信息时判断角色是否唯一
       /// </summary>
       /// <param name="roleentity">角色实体</param>
       /// <param name="resourcepermissionIds">角色拥有的资源列表</param>
       /// <param name="actionpermissionIds">角色拥有的动作</param>
       /// <returns></returns>
        bool isLegalEntity(role roleentity, string resourcepermissionIds, string actionpermissionIds);
        //  List<Imodel.Irole> IroleEx.getUserRoles(string userName);
        /// <summary>
        /// 提交表单
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <param name="keyValue"></param>
        bool SubmitForm(role roleEntity, string permissionIds, string actionpermissionIds, string keyValue);
        /// <summary>
        /// 通过角色名获取部分角色信息
        /// </summary>
        /// <param name="rolename">角色名称（需要判断该条件是否为空）</param>
        /// <returns></returns>
        List<role> getRoleList(string rolename);
       /// <summary>
        /// 给角色赋予指定资源权限
       /// </summary>
        /// <param name="resourcepermissionIds">资源id集合组成的字符串</param>
       /// <param name="keyValue">角色主键</param>
        void resourceAuthority(string resourcepermissionIds,int keyValue);
        /// <summary>
        /// 给角色赋予指定动作权限
        /// </summary>
        /// <param name="actionpermissionIds">动作id集合组成的字符串</param>
        /// <param name="keyValue">角色主键</param>
        void actionAuthority(string actionpermissionIds, int keyValue);
        /// <summary>
        /// 根据角色id删除角色及该角色对应的动作和资源
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        bool deleteroleAR(int id);

        #region ACE
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="page">分页信息</param>
        /// <param name="keyword">关键词</param>
        /// <returns>返回分页数据</returns>
        PagedData<role> GetPageData(ACEPagination page, string keyword);
        
        #endregion
    
    }
    #endregion

    
}
