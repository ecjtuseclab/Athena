using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athena.model;
using Athena.Idal;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：对工作流帮助表数据处理
//最后修改时间:2018-01-25
//修改日志：

namespace Athena.dal
{
   public  class workflowHelper
   {
       public static bool checkNodeAction(string workflowName, int dataid, int actionid, int operatorid)
       {
           workflow wf = workflowEx.Instance.getworkflow(workflowName);
           if (wf.wfstatus == 2 || wf.wflock == 2)//工作流启用且未锁定
           {
               return false;
           }
           workflowinstances wfinstances = workflowinstancesEx.Instance.getworkflowinstances(wf.id, dataid);
           workflownodeaction wfna = workflownodeactionEx.Instance.getworkflownodeaction(wf.id, actionid);
           workflownodeoperator wfno = workflownodeoperatorEx.Instance.getworkflownodeoperator(wfna.id, operatorid);
           if (wfinstances != null)
           {
               if (wfna.nastatus == 2 || wfna.nalock == 2)//跃迁锁定
                   return false;
               if (wfna.currentnodeid != wfinstances.currentnodeid)//不存在以当前状态为起点的跃迁
               {
                   return false;
               }
               if (wfno == null)//不存在指定执行者的 执行条件
               {
                   return false;
               }
               if (wfno.operatorlock == 2)//操作锁定
                   return false;
               return true;
           }
           else//新增动作只判断当前操作者是否有权限执行这个动作
           {
               if (wfno == null)
               {
                   return false;
               }
               return true;
           }
       }

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
       public static bool trace(string workflowName, int dataid, int actionid, int operatorid, string executer, string remark, bool isRecordTrace = true)
        {
          
            workflowinstancesEx.Instance.trace(workflowName,dataid,actionid,operatorid,executer,remark);
            return true;
        }

    }
  
}
