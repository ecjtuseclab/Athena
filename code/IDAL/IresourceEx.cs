using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athena.model;
using Athena.tool.ACEPagination;
using Athena.tool;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：资源接口，供IOC（张梦丽）
//最后修改时间:2018-01-25
//修改日志：

namespace Athena.Idal
{
    public interface IresourceEx : IRepositoryBase<resource>
    {
        #region 1.基本操作
        /// <summary>
        /// 判断新增的资源是否合法，即资源名称不能重复
        /// </summary>
        /// <param name="resourcename">资源名称</param>
        /// <returns>合法：true;不合法：false</returns>
        bool isLeagalResourcename(string resourcename);
        /// <summary>
        /// 判断修改的数据字典是否合法，即字典名称不能重复
        /// </summary>
        /// <param name="resname">名称</param>
        /// <param name="value">值</param>
        /// <param name="reurl">含义</param>
        /// <param name="resscript">备注</param>
        /// <param name="parentID">上级</param>
        /// <returns></returns>
        bool isLegalEntity(string resname, int restype, string reurl, string resscript, string resowner,
            int resleval, string resleftico, string resrightico, string resclass, int resnum, int order, string des);
        /// <summary>
        /// 根据资源名称删除资源
        /// </summary>
        /// <param name="resourcename"></param>
        /// <returns>成功：true;失败:false</returns>
        bool delete(string resourcename);
        

        /// <summary>
        /// 差异更新
        /// </summary>
        /// <param name="id">更新数据主码</param>
        /// <param name="dictionary">需要更新的数据</param>
        /// <returns>返回更新结果（true/false）</returns>
        bool update(int id, Dictionary<string, object> dictionary);
        
        /// <summary>
        /// 根据资源id获取角色数据
        /// </summary>
        /// <param name="id">资源id</param>
        /// <returns>一条角色数据</returns>
        resource getResource(int id);
        
        /// <summary>
        /// 根据资源的名称获得某资源
        /// </summary>
        /// <param name="resourcename">资源名称</param>
        /// <returns>一个资源对象</returns>
        resource getResource(string resourcename);
        
        /// <summary>
        /// 根据资源名称获得此资源对象的id
        /// </summary>
        /// <param name="resourcename">资源名称</param>
        /// <returns>资源id</returns>
        int getResourceId(string resourcename);
        ///<summary>
        /// 表单提交
        /// </summary>
        /// <param name="resourceEntity">表单提交的一条资源数据</param>
        /// <param name="keyValue">资源主键</param>
        void SubmitForm(resource resourceEntity, string keyValue);
        
        /// <summary>
        /// 通过资源名称获取若干条资源数据
        /// </summary>
        /// <param name="resourcename">资源名称</param>
        /// <returns>若干条资源数据</returns>
        List<resource> getResourceList(string resourcename);
        
        /// <summary>
        /// 获取对应资源名的数据以及它的父数据与相关数据
        /// </summary>
        /// <param name="resourcename">资源名</param>
        /// <param name="resourcelist">资源列表</param>
        /// <returns></returns>
        List<resource> getResourceListByresourcename(string resourcename, List<resource> resourcelist);
        

        /// <summary>
        /// 获取对应资源名的数据以及它的子数据与相关数据
        /// </summary>
        /// <param name="resourcename">资源名</param>
        /// <returns></returns>
        List<resource> getResourceListByresourceowner(int id);
        

        /// <summary>
        /// 通过资源名获取资源的上级节点数据
        /// </summary>
        /// <param name="resourcename">资源名</param>
        /// <returns>资源的上级节点数据</returns>
        resource getSuperiorNode(string resourcename);
        
        #endregion

        #region 重载的方法
        /// <summary>
        /// 新增资源
        /// </summary>
        /// <param name="table"></param>
        /// <returns>成功：新增资源数据的主码；失败：-1</returns>
        int insert(resource table);
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
        /// <param name="table">传入的参数为List<resource></param>
        /// <returns></returns>
        void copyAndPasteInert(List<resource> table);
        

        List<resource> getResourceList(int id);
        
        #endregion

        #region ACE
        #region  分页+查询
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="page">分页信息</param>
        /// <param name="keyword">关键词</param>
        /// <returns>返回分页数据</returns>
        PagedData<resource> GetPageData(ACEPagination page, string keyword);
        
        //获取树形分页数据
        PagedData<DataTableTree> GetTreePageData(ACEPagination page, string keyword);
       
        /// <summary>
        /// 类型转换成功与否判断
        /// </summary>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        bool changeParse(string keyword);
        
        #endregion
        /// <summary>
        /// 树形表格删除
        /// </summary>
        /// <param name="id"></param>
        void deleteById(string id);
        
        #endregion
    }
}
