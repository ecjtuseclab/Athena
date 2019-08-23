using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athena.tool.ACEPagination;
using Athena.model;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：工作流节点操作者表接口
//最后修改时间:2017-04-09
//修改日志：

namespace Athena.Idal
{
    public interface IworkflownodeoperatorEx : IRepositoryBase<workflownodeoperator>
    {

        bool deletebynodeactionid(int nodeactionid);
        bool deletebynodeactionids(List<int> nodeactionids);
        /// <summary>
        /// 差异更新
        /// </summary>
        /// <param name="id">跟新数据主码</param>
        /// <param name="dictionary">需要更新的数据</param>
        /// <returns>返回更新结果（true/false）</returns>
        bool update(int id, Dictionary<string, object> dictionary);
        /// <summary>
        /// 获取节点操作者对象
        /// </summary>
        /// <param name="wfnodeactionid">节点id</param>
        /// <param name="operatorid">操作者id</param>
        /// <returns>节点操作者对象</returns>
        workflownodeoperator getworkflownodeoperator(int wfnodeactionid, int operatorid);
        /// <summary>
        /// 通过操作者类型获取节点操作者数据列表
        /// </summary>
        /// <param name="operatortype"></param>
        /// <returns></returns>
        List<workflownodeoperator> getWorkflowNodeoperatorList(string operatortype);
        /// <summary>
        /// 通过id获取节点操作者对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        workflownodeoperator getWorkflowNodeoperatorEntity(int id);
        /// <summary>
        /// 提交表单
        /// </summary>
        /// <param name="workflownodeoperatorEntity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(workflownodeoperator workflownodeoperatorEntity, string keyValue);
        /// <summary>
        /// 通过节点动作id获取节点操作者数据列表后删除该数据列表
        /// </summary>
        /// <param name="nodeactionid">节点动作id</param>
        /// <returns></returns>
        void deleteWfNodeoperatorListByNodeactionid(int nodeactionid);
        /// <summary>
        /// ACE分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        PagedData<workflownodeoperator> GetPageData(ACEPagination page, string keyword);
        List<workflownodeoperator> getEntityList(int wfid);
    }
}
