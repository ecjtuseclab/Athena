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
//模块及代码页功能描述：动作接口，供IOC（张梦丽）
//最后修改时间:2018-01-25
//修改日志：
//2017-04-09 新增获取所有对象条数方法接口（张婷婷）
//2017-12-17 新增前端为ACE相关的方法接口（王露）

namespace Athena.Idal
{
    public interface IactionEx : IRepositoryBase<action>
    {
        /// <summary>
        /// 差异更新
        /// </summary>
        /// <param name="id">跟新数据主码</param>
        /// <param name="dictionary">需要更新的数据</param>
        /// <returns>返回更新结果（true/false）</returns>
        bool update(int id, Dictionary<string, object> dictionary);

        /// <summary>
        /// 根据动作名称获得动作数据
        /// </summary>
        /// <param name="actionname">动作名称</param>
        /// <returns>一条动作数据</returns>
        action getAction(string actionname);

        /// <summary>
        /// 根据动作名称获得动作数据id
        /// </summary>
        /// <param name="actionname">动作名称</param>
        /// <returns>动作数据id</returns>
        int getActionId(string actionname);

        /// <summary>
        /// 根据组名获取分组数据列表
        /// </summary>
        /// <param name="groupname"></param>
        /// <returns></returns>
        List<action> getActionList(string actionname);

        /// <summary>
        /// 表单提交
        /// </summary>
        /// <param name="roleEntity">表单提交的一条角色数据</param>
        /// <param name="keyValue">主键</param>
        void SubmitForm(action ac, string keyValue);

        #region ACE
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="page">分页信息</param>
        /// <param name="keyword">关键词</param>
        /// <returns>返回分页数据</returns>
        PagedData<action> GetPageData(ACEPagination page, string keyword);
        /// <summary>
        /// 类型转换成功与否判断
        /// </summary>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        bool changeParse(string keyword);
        /// <summary>
        /// 动态根据字段名，字段值查询指定数据
        /// </summary>
        /// <param name="key">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns>List<action></returns>
        List<action> getDynamicAction(string key, string value);
        /// <summary>
        /// group表分页
        /// </summary>
        /// <param name="pageIndex">当前页的索引</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns></returns>
        List<action> getPageAction(int pageIndex, int pageSize);
        #endregion


        List<action> getActionListbywfid(int wfid);
    }
}
