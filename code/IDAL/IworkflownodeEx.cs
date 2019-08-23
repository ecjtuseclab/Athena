using System;
using System.Collections.Generic;
using Athena.tool.ACEPagination;
using Athena.model;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：工作流节点表接口
//最后修改时间:2017-04-09
//修改日志：

namespace Athena.Idal
{
    public interface IworkflownodeEx:IRepositoryBase<workflownode>
    {
         /// <summary>
         /// 差异更新
         /// </summary>
         /// <param name="id">跟新数据主码</param>
         /// <param name="dictionary">需要更新的数据</param>
         /// <returns>返回更新结果（true/false）</returns>
         bool update(int id, Dictionary<string, object> dictionary);

        /// <summary>
        /// 根据名称获得节点数据
        /// </summary>
        /// <param name="wfnodename">节点名称</param>
        /// <returns></returns>
        List<workflownode> getWfnodeList(string wfnodename);
        /// <summary>
        /// ACE分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        PagedData<workflownode> GetPageData(ACEPagination page, string keyword);


        List<workflownode> getEntityList(int wfid);

    }
}
