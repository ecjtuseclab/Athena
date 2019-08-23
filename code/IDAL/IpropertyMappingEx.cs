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
//模块及代码页功能描述：数据字典接口，供IOC（张梦丽）
//最后修改时间:2018-01-25
//修改日志：
//2017-04-11 新增获取所有对象条数方法接口（张婷婷）
//2017-12-19 新增前端为ACE相关的方法（王露）

namespace Athena.Idal
{
   public interface IpropertyMappingEx : IRepositoryBase<propertymapping>
    {
        /// <summary>
        ///  判断新增的数据字典是否合法，即字典名称不能重复
        /// </summary>
        /// <param name="fullname">数据字典名称</param>
        /// <returns>合法：true;不合法：false</returns>    
        bool isLeagalEntityname(string fullname);
        /// <summary>
        /// 判断修改的数据字典是否合法，即字典名称不能重复
        /// </summary>
        /// <param name="fullname">名称</param>
        /// <param name="value">值</param>
        /// <param name="meaning">含义</param>
        /// <param name="remark">备注</param>
        /// <param name="parentID">上级</param>
        /// <returns></returns>
        bool isLegalEntity(string fullname, string value, string meaning, string remark);
        /// <summary>
        /// 差异更新
        /// </summary>
        /// <param name="id">跟新数据主码</param>
        /// <param name="dictionary">需要更新的数据</param>
        /// <returns>返回更新结果（true/false）</returns>
        bool update(int id, Dictionary<string, object> dictionary);

        /// <summary>
        /// 根据propertyName获得数据
        /// </summary>
        /// <param name="propertyName">propertyName</param>
        /// <returns>一条数据</returns>
        propertymapping getPropertyMapping(string propertyName);

        /// <summary>
        /// 根据propertyMeaning获得数据id
        /// </summary>
        /// <param name="actionname">propertyMeaning</param>
        /// <returns>数据id</returns>
        int getPropertyMappingId(string propertyMeaning);
        /// <summary>
        /// 根据属性名获取数据字典列表
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <returns></returns>
        List<propertymapping> getPropertyMappingList(string propertyName);
        /// <summary>
        /// 根据属性名和父节点获取数据字典列表
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <returns></returns>
        List<propertymapping> getPropertyMappingList(int parentid,string propertyName);
        /// <summary>
        /// 根据id获取数据字典一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        propertymapping getPropertyMapping(int id);
        /// <summary>
        /// 根据parentid获取数据
        /// </summary>
        /// <param name="parentid"></param>
        /// <returns></returns>
        List<propertymapping> getPropertyMappingByParentid(int parentid);
        /// <summary>
        /// 提交表单
        /// </summary>
        /// <param name="propertymappingEntity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(propertymapping propertymappingEntity, string keyValue);

        #region ACE
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="page">分页信息</param>
        /// <param name="keyword">关键词</param>
        /// <returns>返回分页数据</returns>
        PagedData<propertymapping> GetPageData(ACEPagination page, string keyword);
         /// <summary>
        /// 类型转换成功与否判断
        /// </summary>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        bool changeParse(string keyword);
        #endregion
    }
}
