using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Athena.model;
using SqlSugar;
using Athena.Idal;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：对工作流影子跟踪表的数据处理
//最后修改时间:2018-01-25
//修改日志：

namespace Athena.dal
{
    public class workflowinstancetracingsEx : RepositoryBase<workflowinstancetracings>, IworkflowinstancetracingsEx
    {
       
        private static workflowinstancetracingsEx _instance;
        public static workflowinstancetracingsEx Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new workflowinstancetracingsEx();
                }
                return _instance;
            }
        }
        public int insert(string executer, int instancesid, workflownodeaction wfna)
        {
            workflownode startnode = workflownodeEx.Instance.getEntityById(wfna.currentnodeid);
            workflownode endnode = workflownodeEx.Instance.getEntityById(wfna.nextnodeid);
            workflowinstancetracings wfiting = new workflowinstancetracings();
            wfiting.instanceid = instancesid;
            wfiting.startnode = startnode.wfnodememo;
            wfiting.endnode = endnode.wfnodememo;
            wfiting.executer = executer;
            wfiting.executeaction = wfna.nodeactionmemo;
            wfiting.executetime = DateTime.Now;
            return workflowinstancetracingsEx.Instance.insert(wfiting).ObjToInt();
        }

    
    }
}
