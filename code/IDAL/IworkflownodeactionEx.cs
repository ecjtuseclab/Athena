using System;
using System.Collections.Generic;
using Athena.tool.ACEPagination;
using Athena.model;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：工作流节点动作表接口
//最后修改时间:2017-04-09
//修改日志：

namespace Athena.Idal
{
    public interface IworkflownodeactionEx : IRepositoryBase<workflownodeaction>
    {

        /// <summary>
        /// 差异更新
        /// </summary>
        /// <param name="id">跟新数据主码</param>
        /// <param name="dictionary">需要更新的数据</param>
        /// <returns>返回更新结果（true/false）</returns>
        bool update(int id, Dictionary<string, object> dictionary);
        /// <summary>
        /// 获取初始化的节点动作
        /// </summary>
        /// <returns>节点动作list数据</returns>
        List<workflownodeaction> getInitworkflownodeaction();
        /// <summary>
        /// 获取节点动作对象
        /// </summary>
        /// <param name="workflowid">工作流id</param>
        /// <param name="actionname">动作名称</param>
        /// <returns></returns>
        workflownodeaction getworkflownodeaction(int workflowid, string actionname);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="workflowid"></param>
        /// <param name="actionname"></param>
        /// <returns></returns>
        workflownodeaction getworkflownodeaction(int workflowid, int actionid);
        /// <summary>
        /// 获取符合节点动作条件的数据
        /// </summary>
        /// <param name="nodeaction">节点对象</param>
        /// <returns>节点动作list数据</returns>
        List<workflownodeaction> getcountersignnodeaction(workflownodeaction nodeaction);
        /// <summary>
        /// 通过节点动作名获取工作流节点动作列表上
        /// </summary>
        /// <param name="nodeactionname">节点动作名称</param>
        /// <returns></returns>
        List<workflownodeaction> getWorkflowNodeactionList(string nodeactionname);
        /// <summary>
        /// 通过id获取节点动作对象
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        workflownodeaction getWorkflowNodeaction(int id);
        /// <summary>
        /// 提交表单
        /// </summary>
        /// <param name="workflownodeactionEntity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(workflownodeaction workflownodeactionEntity, string keyValue);
        /// <summary>
        /// 通过节点动作名称获取节点动作对象
        /// </summary>
        /// <param name="nodeactionname">节点动作名称</param>
        /// <returns></returns>
        workflownodeaction getWorkflowNodeactionEntity(string nodeactionname);
        /// <summary>
        /// ACE分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        PagedData<workflownodeaction> GetPageData(ACEPagination page, string keyword);

        List<workflownodeaction> getEntityList(int wfid);
    }
}
