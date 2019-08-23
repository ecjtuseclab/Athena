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
//最后修改时间:2018-01-25
//修改日志：
//2017-04-11 新增获取所有对象条数方法接口（张婷婷）
//2017-12-19 新增前端为ACE相关的方法接口（王露）

namespace Athena.Idal
{
    public interface IgroupEx : IRepositoryBase<group>
    {
        /// <summary>
        ///  判断新增的分组是否合法，即分组名称不能重复
        /// </summary>
        /// <param name="fullname">分组名称</param>
        /// <param name="encode">分组编码</param>
        /// <returns>合法：true;不合法：false</returns>      
        bool isLeagalGroupname(string fullname, int encode);
        /// <summary>
        /// 根据分组id获取一个分组实体
        /// </summary>
        /// <param name="groupid"></param>
        /// <returns></returns>
        group getGroup(int groupid);
        /// <summary>
        /// 根据组名获取分组数据列表
        /// </summary>
        /// <param name="groupname"></param>
        /// <returns></returns>
        List<group> getGroupList(string groupname);
        /// <summary>
        /// 根据组名删除
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <returns>bool</returns>
        bool deleteByName(string groupName);

        /// <summary>
        /// 差异更新
        /// </summary>
        /// <param name="id">跟新数据主码</param>
        /// <param name="dictionary">需要更新的数据</param>
        /// <returns>返回更新结果（true/false）</returns>
        bool update(int id, Dictionary<string, object> dictionary);

        /// <summary>
        /// 动态根据字段名，字段值查询指定数据
        /// </summary>
        /// <param name="key">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns>List<group></returns>
        List<group> getDynamicGroup(string key, string value);

        /// <summary>
        /// 根据用户名获取的组
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <returns></returns>
        group getGroup(string groupName);
        /// <summary>
        /// 提交表单
        /// </summary>
        /// <param name="groupEntity"></param>
        /// <param name="keyValue"></param>
        bool SubmitForm(group groupEntity, string keyValue);
        /// <summary>
        /// group表分页
        /// </summary>
        /// <param name="pageIndex">当前页的索引</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns></returns>
        List<group> getPageGroup(int pageIndex, int pageSize);

        /// <summary>
        /// 根据组名获取主键id
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        int getGroupId(string groupName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupIds">字符串拼接的组ids，拼接格式（1,2,3）</param>
        /// <param name="cellHeaders">字符串拼接的字段名(id,groupName)</param>
        /// <param name="cellHeaderNames">字符串拼接的表单别名(ID,组名)</param>
        /// <returns>bool</returns>
        bool excelExport(string groupIds, string cellHeaders, string cellHeaderNames);

        /// <summary>
        /// excel导入
        /// </summary>
        Dictionary<string, string> getCellHeaderName(string[] cellHeaderNames, string[] cellHeaderNameArr);

        /// <summary>
        /// 空方法：后期完善
        /// </summary>
        /// <returns></returns>
        bool excelImport();

        /// <summary>
        /// 空方法：后期完善
        /// </summary>
        /// <returns></returns>
        bool pdfExport();
        /// <summary>
        /// 空方法：后期完善
        /// </summary>
        /// <returns></returns>
        bool wordImport();
        /// <summary>
        /// 空方法：后期完善
        /// </summary>
        /// <returns></returns>
        bool wordExport();

        #region ACE
        #region ACE分页
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="page">分页信息</param>
        /// <param name="keyword">关键词</param>
        /// <returns>返回分页数据</returns>
        PagedData<group> GetPageData(ACEPagination page, string keyword); 
        /// <summary>
        /// 类型转换成功与否判断
        /// </summary>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        bool changeParse(string keyword);
        #endregion
        
        #endregion
    }
    /// <summary>
    /// excel导入验证视图类
    /// </summary>
    public interface groupExcelValidateView
    {
        //bool _isExcelVaildateOK = true;
        /// <summary>
        /// Excel验证是否通过，默认为true
        /// <para>true：通过；false：不通过</para>
        /// </summary>
        //bool IsExcelVaildateOK;
    }

   
}
