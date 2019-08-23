using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athena.model;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：角色动作接口，供IOC（张梦丽）
//最后修改时间:2018-01-25
//修改日志：

namespace Athena.Idal
{   
    public interface IroleActionEx //: IRepositoryBase<Irole_action>
    {
        /// <summary>
        /// 插入角色动作数据
        /// </summary>
        /// <param name="actionid">动作数据id</param>
        /// <param name="roleid">角色数据id</param>
        /// <returns>角色动作数据id</returns>
        int addRoleAction(int actionid, int roleid);

        /// <summary>
        /// 删除角色动作数据
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="actionid">动作id</param>
        /// <returns></returns>
        bool delete(int roleid, int actionid);

        ///// <summary>
        ///// 删除指定角色的所有角色动作数据
        ///// </summary>
        ///// <param name="roleid">角色数据id</param>
        ///// <returns>bool值</returns>
        //override bool delete(int roleid)
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
        bool update(int id, Dictionary<string, object> dictionary);

        /// <summary>
        /// 根据角色动作id获取角色动作数据
        /// </summary>
        /// <param name="roleactionid">角色数据id</param>
        /// <returns>一条角色动作数据</returns>
        role_action getRoleAction(int roleactionid);

        /// <summary>
        /// 获得指定角色的动作数据
        /// </summary>
        /// <param name="roleid">角色数据id</param>
        /// <param name="owner">动作数据控制器</param>
        /// <returns>动作数据list</returns>
        List<action> getRoleAllAction(int roleid, string owner = "");

        /// <summary>
        /// 获得指定角色的特定类型的动作数据
        /// </summary>
        /// <param name="roleid">角色数据id</param>
        /// <param name="type">角色类型</param>
        /// <param name="owner">动作数据控制器</param>
        /// <returns>动作数据list</returns>
        List<action> getRoleAction(int roleid, int type, string owner = "");//获取指定角色的特定类型和属主的动作列表
        /// <summary>
        /// 判断role_action是否合法
        /// </summary>
        /// <param name="roleid"></param>
        /// <param name="actionid"></param>
        /// <returns></returns>
        bool isLegalRoleAction(int roleid, int actionid);
         /// <summary>
        /// 新增role_action对象
        /// </summary>
        /// <param name="table">role_action对象</param>
        /// <returns>成功：新增数据的主码id;失败：-1</returns>
        int insert(role_action table);
        /// <summary>
        /// 角色动作实体
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="actionid">动作id</param>
        /// <returns></returns>
        role_action getRoleAction(int roleid, int actionid);
       
    }
}
