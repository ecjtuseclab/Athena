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
//模块及代码页功能描述：分组接口，供IOC（张梦丽）
//最后修改时间:2017-12-17
//修改日志：
//2017-12-17 新增前端为ACE相关的方法接口（王露）

namespace Athena.Idal
{
    public interface IareaEx : IRepositoryBase<area>
    {
        #region 1.基本操作
        /// <summary>
        ///  判断新增的区域是否合法，即区域名称不能重复
        /// </summary>
        /// <param name="fullname">区域名称</param>
        /// <param name="encode">区域编码</param>
        /// <returns>合法：true;不合法：false</returns>        
        bool isLeagalAreaname(string fullname,string encode);
        /// <summary>
        ///判断是否为合法的区域实体
        /// </summary>
        /// <param name="parentid">上级id</param>
        /// <param name="layers">级别</param>
        /// <param name="encode">编码</param>
        /// <param name="fullname">区域名</param>
        /// <returns></returns>
        bool isLegalEntity(string parentid, int layers, string encode, string fullname);
        /// <summary>
        /// 新增区域
        /// </summary>
        /// <param name="table"></param>
        /// <returns>成功：新增区域数据的主码；失败：-1</returns>
        int insert(area entity);
        /// <summary>
        /// 根据区域名称删除区域
        /// </summary>
        /// <param name="fullname"></param>
        /// <returns>成功：true;失败:false</returns>
        bool delete(string fullname);

        /// <summary>
        /// 差异更新
        /// </summary>
        /// <param name="id">更新数据主码</param>
        /// <param name="dictionary">需要更新的数据</param>
        /// <returns>返回更新结果（true/false）</returns>
        bool update(int id, Dictionary<string, object> dictionary);

        ///// <summary>
        ///// 更新区域
        ///// </summary>
        ///// <param name="table"></param>
        ///// <returns>成功：更新区域数据的主码；失败：-1</returns>
        //bool update(area entity);
        /// <summary>
        /// 根据区域的名称获得某区域
        /// </summary>
        /// <param name="fullname">区域名称</param>
        /// <returns>一个区域对象</returns>
        area getArea(string fullname);

        /// <summary>
        /// 根据区域的id获得区域对象
        /// </summary>
        /// <param name="areaid">区域id</param>
        /// <returns>一个区域对象</returns>
        List<area> getParentArea();


        /// <summary>
        /// 根据区域名称获得此区域对象的id
        /// </summary>
        /// <param name="fullname">区域名称</param>
        /// <returns>区域id</returns>
        int getAreaId(string fullname);

        /// <summary>
        /// 根据区域名称获得此区域对象
        /// </summary>
        /// <param name="fullname">区域名称</param>
        List<area> getAreaList(string fullname);
        /// <summary>
        /// 获取对应资源名的数据以及它的子数据与相关数据
        /// </summary>
        /// <param name="Areaname">资源名</param>
        /// <returns></returns>
        List<area> getAreaListByAreaowner(int id);
        /// <summary>
        /// 复制粘贴  （PS：可做优化，使用批量删除方法）
        /// 用字符分割符根据‘，’分割出字符串id值，再根据动作数据id获取动作数据，再插入一样的数据
        /// </summary>
        /// <param name="ids">传入的参数由动作数据id+','组成的字符串</param>
        /// <returns>bool值</returns>
        bool copyAndPaste(string ids);
        /// <summary>
        /// 复制粘贴  （PS：可做优化，使用批量删除方法）
        /// 用字符分割符根据‘，’分割出字符串id值，再根据动作数据id获取动作数据，再插入一样的数据
        /// </summary>
        /// <param name="table">传入的参数为List<Area></param>
        /// <returns></returns>
        void copyAndPasteInert(List<area> table);
        #endregion
        #region ACE
        #region 分页+查询
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="page">分页信息</param>
        /// <param name="keyword">关键词</param>
        /// <returns>返回分页数据</returns>
        PagedData<area> GetPageData(ACEPagination page, string keyword);
        /// <summary>
        /// 类型转换成功与否判断
        /// </summary>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        bool changeParse(string keyword);
        #endregion
        /// <summary>
        /// 动态根据字段名，字段值查询指定数据
        /// </summary>
        /// <param name="key">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns>List<area></returns>
        List<area> getDynamicArea(string key, string value);
        /// <summary>
        /// area表分页
        /// </summary>
        /// <param name="pageIndex">当前页的索引</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns></returns>
        List<area> getPageArea(int pageIndex, int pageSize);
        /// <summary>
        /// 树形表格删除
        /// </summary>
        /// <param name="id"></param>
        void deleteById(string id);
        #endregion
    }
}
