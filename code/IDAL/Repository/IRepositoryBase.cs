using Athena.tool.ACEPagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：公共接口
//最后修改时间:2018-01-25
//修改日志：

namespace Athena.Idal
{
    public interface IRepositoryBase<TEntity> where TEntity : class,new()
    {

        #region 共用方法
        /// <summary>
        /// 插入某条数据的方法
        /// </summary>
        /// <param name="entity">某条要插入的数据</param>
        /// <returns>插入完成返回的主键值</returns>
         int insert(TEntity entity);

        /// <summary>
        /// 批量插入数据的方法
        /// </summary>
        /// <param name="tableList">插入List类型的group数据</param>
        /// <returns></returns>
        int insertList(List<TEntity> tableList);

        /// <summary>
        /// 更新某条数据的方法
        /// </summary>
        /// <param name="table">某条要更新的数据</param>
        /// <returns>bool值</returns>
         bool update(TEntity entity);

        ///// <summary>
        ///// 差异更新某条数据的方法
        ///// </summary>
        ///// <param name="id">更新的数据主码</param>
        ///// <param name="dictionary">需要更新的数据</param>
        ///// <returns>返回更新结果（true/false）</returns>
        // bool update(int id, Dictionary<string, object> dictionary);

        /// <summary>
        /// 删除某条数据的方法
        /// </summary>
        /// <param name="entity">某条要删除的数据</param>
        /// <returns>bool值</returns>
        bool delete(TEntity entity);

        /// <summary>
        /// 根据ID删除某条数据的方法
        /// </summary>
        /// <param name="id">某条要删除的数据id</param>
        // <returns>bool值</returns>
        bool delete(int id);

        /// <summary>
        /// 根据List id 批量删除
        /// </summary>
        /// <param name="ids">List<ids></param>
        /// <returns>bool</returns>
        bool deleteList(List<int> ids);

        /// <summary>
        /// 删除所有数据的方法
        /// </summary>
        /// <returns>bool值</returns>
        bool deleteAll();

        /// <summary>
        /// 复制粘贴  （PS：可做优化，使用批量删除方法）
        /// 用字符分割符根据‘，’分割出字符串id值，再根据动作数据id获取动作数据，再插入一样的数据
        /// </summary>
        /// <param name="ids">传入的参数由动作数据id+','组成的字符串</param>
        /// <returns>bool值</returns>
        bool copyAndPaste(string ids);

        /// <summary>
        /// 根据动作数据id获得动作数据
        /// </summary>
        /// <param name="actionid">动作数据id</param>
        /// <returns>一条动作数据</returns>
        TEntity getEntityById(int id);

        /// <summary>
        /// 获得所有动作数据
        /// </summary>
        /// <returns>动作数据List</returns>
        List<TEntity> getEntityList();
        /// <summary>
        /// 获取指定页的数据列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前页数据量</param>
        /// <param name="searchStr">查询条件</param>
        /// <returns></returns>
        List<TEntity> getPaginationEntityList(int pageIndex, int pageSize, Dictionary<string, object> searchStr);

        /// <summary>
        /// 获取指定页的数据列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前页数据量</param>
        /// <param name="searchStr">查询条件</param>
        /// <returns></returns>
        List<TEntity> getPaginationEntityList(int pageIndex, int pageSize, List<TEntity> searchStr);
        /// <summary>
        /// 通过查询条件获取数据列表
        /// </summary>
        /// <param name="searchStr">查询条件</param>
        /// <returns></returns>
        List<TEntity> getEntityList(Dictionary<string, object> searchStr);
        #endregion

        //#region 前台通过指定多项查询条件查询、同时分页查询
        ///// <summary>
        ///// 获取表中数据的总数
        ///// </summary>
        ///// <returns>总数</returns>
        //int pageCount();

        ///// <summary>
        ///// 多条件+分页查询
        ///// </summary>
        ///// <param name="pageIndex">当前页为第pageIndex</param>
        ///// <param name="pageSize">每页数据条数</param>
        ///// <param name="searchStr">接收前台传过来的参数，
        ///// 按格式拼接转换成字典类型，查询条件，
        ///// 拼接格式为("key=groupname|string，value=总经理")</param>
        ///// <returns>List<group></returns>
        //List<TEntity> getPage(int pageIndex, int pageSize, Dictionary<string, object> searchStr);
        //#endregion

        #region
        /// <summary>
        /// 总条数
        /// </summary>
        /// <param name="pageIndex">当前页为第pageIndex</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="searchStr">接收前台传过来的参数，
        /// 按格式拼接转换成字典类型，查询条件，
        /// 拼接格式为("key=groupname|string，value=总经理")</param>
        /// <returns>int<group></returns>
        int pageCount<TEntity>(Dictionary<string, object> searchStr) where TEntity : class,new();
        /// <summary>
        /// 多条件+分页查询
        /// </summary>
        /// <param name="pageIndex">当前页为第pageIndex</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="searchStr">接收前台传过来的参数，
        /// 按格式拼接转换成字典类型，查询条件，
        /// 拼接格式为("key=groupname|string，value=总经理")</param>
        /// <returns>List<group></returns>
        List<TEntity> getPage<TEntity>(int pageIndex, int pageSize, Dictionary<string, object> searchStr) where TEntity : class,new();
        /// <summary>
        /// 返回where 条件
        /// </summary>
        /// <param name="searchStr">查询条件，拼接格式为("key=groupname|string，value=总经理")</param>
        /// <returns>string ：拼接后的where条件</returns>
        string getWhere(Dictionary<string, object> searchStr);
        #endregion

        List<TEntity> getPage(int pageIndex, int pageSize, Dictionary<string, object> searchStr);

        int pageCount();
        List<TEntity> getSearch(string strwhere);
        
        /// <summary>
        /// 高级查询+分页
        /// </summary>
        /// <param name="searchStr">查询条件，拼接格式为("key=groupname，value=总经理")</param>
        /// <returns>string ：拼接后的where条件</returns>
        /// 前台的数据格式："{\"groupOp\":\"AND\",\"rules\":[{\"field\":\"id\",\"op\":\"eq\",\"data\":\"54\"},{\"field\":\"actioncode\",\"op\":\"lt\",\"data\":\"454\"}]}"
        PagedData<TEntity> mutiplySearch(string filters, ACEPagination pagination, string tableName);
    }
}
