using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athena.model;
using Athena.tool.ACEPagination;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 张婷婷 王露 陈祚松 艾美珍 张梦丽 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：提供文章接口，供IOC
//最后修改时间：2018/1/26
//修改日志：
//2018-01-24 提供文章接口，供IOC（张婷婷）
    
namespace Athena.Idal
{   
    public interface IarticleEx : IRepositoryBase<article>
    {        
        #region 1.基础操作
        /// <summary>
        ///  判断新增的区域是否合法，即区域名称不能重复
        /// </summary>
        /// <param name="title">区域名称</param>
        /// <param name="encode">区域编码</param>
        /// <returns>合法：true;不合法：false</returns>    
        bool isLeagalArticlename(string title, string content);
        
        /// <summary>
        /// 新增区域
        /// </summary>
        /// <param name="table"></param>
        /// <returns>成功：新增区域数据的主码；失败：-1</returns>
        int insert(article entity);
        

        /// <summary>
        /// 更新区域
        /// </summary>
        /// <param name="table"></param>
        /// <returns>成功：更新区域数据的主码；失败：-1</returns>
        bool update(article entity);
        
        /// <summary>
        /// 差异更新
        /// </summary>
        /// <param name="id">跟新数据主码</param>
        /// <param name="dictionary">需要更新的数据</param>
        /// <returns>返回更新结果（true/false）</returns>
        bool update(int id, Dictionary<string, object> dictionary);
        
        /// <summary>
        /// 根据标题获得数据
        /// </summary>
        /// <param name="title">标题</param>
        /// <returns>一条数据</returns>
        article getArticle(string title);
        

        /// <summary>
        /// 根据数据id获得数据
        /// </summary>
        /// <param name="articleid">数据id</param>
        /// <returns>一条数据</returns>
        article getArticle(int articleid);
        
        /// <summary>
        /// 获得所有数据
        /// </summary>
        /// <returns>数据List</returns>
        List<article> getAllArticle();
        
        /// <summary>
        /// 根据标题获得数据id
        /// </summary>
        /// <param name="title">标题</param>
        /// <returns>数据id</returns>
        int getArticleId(string title);
        

        #endregion

        #region 基本操作
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="title">标题</param>
        /// <returns></returns>
        List<article> getArticleList(string title);
        
        /// <summary>
        /// 通过标题及文章类型获取数据列表
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="sortID">文章类型</param>
        /// <returns></returns>
        List<article> getArticleList(string title, int sortID);
        /// <summary>
        /// 通过文章类型获取数据
        /// </summary>
        /// <param name="sortID"></param>
        /// <returns></returns>
        List<article> getArticleList(int sortID);

        /// <summary>
        /// 表单提交
        /// </summary>
        /// <param name="roleEntity">表单提交的一条角色数据</param>
        /// <param name="keyValue">主键</param>
        bool SubmitForm(article articleEntity, string keyValue);

        /// <summary>
        /// 动态根据字段名，字段值查询指定数据
        /// </summary>
        /// <param name="key">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns>List<group></returns>
        List<article> getDynamicArticle(string key, string value);
        
        /// <summary>
        /// article表分页
        /// </summary>
        /// <param name="pageIndex">当前页的索引</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns></returns>
        List<article> getPageGroup(int pageIndex, int pageSize);
        
        #endregion

        #region ACE
        PagedData<article> GetPageData(ACEPagination page, string keyword);
        
        /// <summary>
        /// 类型转换成功与否判断
        /// </summary>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        bool changeParse(string keyword);
        #endregion

    }
}
