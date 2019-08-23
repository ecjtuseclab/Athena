using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athena.model;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：工作流影子表接口
//最后修改时间:2017-04-09
//修改日志：

namespace Athena.Idal
{
   public interface IworkflowinstancesEx : IRepositoryBase<workflowinstances>
    {
      
        /// <summary>
        /// 获取业务数据状态
        /// </summary>
        /// <param name="workflowid">工作流id</param>
        /// <param name="dataid">业务数据主码</param>
        /// <returns></returns>
        workflowinstances getworkflowinstances(int workflowid, int dataid);

        /// <summary>
        /// 节点跃迁方法
        /// </summary>
        /// <param name="workflowName">工作流名称</param>
        /// <param name="dataid">业务数据主码</param>
        /// <param name="actionName">动作名称</param>
        /// <param name="operatorid">操作者ID</param>
        /// <param name="executer">执行者</param>
        /// <param name="remark">工作流执行备注</param>
        /// <param name="isRecordTrace">是否记录工作流操作日志（默认记录）</param>
        /// <returns>返回是否执行成功</returns>
      bool trace(string workflowName, int dataid, int actionid, int operatorid, string executer, string remark, bool isRecordTrace = true);
       /// <summary>
       /// 新增数据
       /// </summary>
       /// <param name="wf">工作流对象</param>
       /// <param name="instancesid">工作流状态id</param>
       /// <param name="wfna">节点动作对象</param>
      /// <param name="dataid">业务数据主码</param>
       /// <returns></returns>
      int insert(workflow wf, int instancesid, workflownodeaction wfna, int dataid);
        
    }
}
