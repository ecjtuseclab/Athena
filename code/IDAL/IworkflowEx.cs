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
//模块及代码页功能描述：工作流接口，供IOC（张梦丽）
//最后修改时间:2018-01-25
//修改日志：
//2017-4-11 接口方法添加（张婷婷）

namespace Athena.Idal
{
    public interface IworkflowEx : IRepositoryBase<workflow>
    {
        /// 张婷婷接口方法添加
        ///最后更新时间：2017/4/10
        
        /// <summary>
        /// 差异更新
        /// </summary>
        /// <param name="id">跟新数据主码</param>
        /// <param name="dictionary">需要更新的数据</param>
        /// <returns>返回更新结果（true/false）</returns>
        bool update(int id, Dictionary<string, object> dictionary);
        /// <summary>
        /// 获取工作流
        /// </summary>
        /// <param name="wfname">工作流名称</param>
        /// <returns>工作流对象</returns>
        workflow getworkflow(string wfname);
        /// <summary>
        /// 获取工作流id
        /// </summary>
        /// <param name="wfname">工作流名称</param>
        /// <returns>工作流id</returns>
        int getworkflowId(string wfname);
        /// <summary>
        /// 通过工作流名称查询工作流数据列表
        /// </summary>
        /// <param name="wfname">工作流名称</param>
        /// <returns></returns>
        List<workflow> getWorkflowList(string wfname);
        /// <summary>
        /// 通过id获取工作流数据
        /// </summary>
        /// <param name="groupid"></param>
        /// <returns></returns>
        workflow getWorkflow(int groupid);
        /// <summary>
        /// 提交表单
        /// </summary>
        /// <param name="workflowEntity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(workflow workflowEntity, string keyValue);
        /// <summary>
        /// ACE分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        PagedData<workflow> GetPageData(ACEPagination page, string keyword);
    }
}
