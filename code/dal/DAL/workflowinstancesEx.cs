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
//模块及代码页功能描述：对工作流影子表表的数据处理
//最后修改时间:2018-01-25
//修改日志：

namespace Athena.dal
{
    public class workflowinstancesEx : RepositoryBase<workflowinstances>, IworkflowinstancesEx
    {
        private static workflowinstancesEx _instance;
        public static workflowinstancesEx Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new workflowinstancesEx();
                }
                return _instance;
                
            }
        }

        public workflowinstances getworkflowinstances(int workflowid, int dataid)
        {
            workflowinstances wfinstances = db.Queryable<workflowinstances>().Where(d => d.wfid == workflowid && d.ownertabledataid == dataid).FirstOrDefault();
            return wfinstances as workflowinstances;
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
        public bool trace(string workflowName, int dataid, int actionid, int operatorid, string executer, string remark, bool isRecordTrace = true)
        {
           
            workflow wf = workflowEx.Instance.getworkflow(workflowName);
            int instancesid = 0;
            workflowinstances wfinstances = workflowinstancesEx.Instance.getworkflowinstances(wf.id, dataid);
            workflownodeaction wfna = workflownodeactionEx.Instance.getworkflownodeaction(wf.id, actionid);
            workflownodeEx wfnEx = new workflownodeEx();
            string sql = string.Empty;
            if (wfinstances != null)
            {
                int currentnodeid = wfna.nextnodeid;
                int? nodcodevalue = 0;
                if (wfna.nodetype == 2)//会签节点跳跃
                {
                    List<workflownodeaction> wfnas = workflownodeactionEx.Instance.getcountersignnodeaction(wfna);//会签动作集合
                    nodcodevalue = (wfinstances.nodcode | wfna.nodeactioncode) == wfna.nodeactioncode ? wfinstances.nodcode : (wfinstances.nodcode + wfna.nodeactioncode);
                    if (nodcodevalue != wfnas.Select(p => p.nodeactioncode).Sum())
                    {
                        currentnodeid = wfna.currentnodeid;
                    }
                }
                //更新Trace表节点信息
                sql = string.Format(@"update {0} 
                                       set currentnodeid={1},
                                                  nodcode={2}
                                                 where id={3}",
                                          wf.wfinstancestable, currentnodeid, nodcodevalue, wfinstances.id);
                instancesid = wfinstances.id;
                db.SqlQueryDynamic(sql);
                //更新业务表工作流字段值

                workflownode wfn = workflownodeEx.Instance.getEntityById(wfna.nextnodeid);
                string sqlstr = string.Format("update {0} set {1}={2} where id={3}", wf.wfownertable, wf.wffieldname, wfn.wfnodememo);
                db.SqlQueryDynamic(sqlstr);
            }
            else
            {
                instancesid = insert(wf, instancesid, wfna, dataid);
                //更新业务表工作流字段值
                workflownode wfn = workflownodeEx.Instance.getEntityById(wfna.nextnodeid);
                string sqlstr = string.Format("update {0} set {1}='{2}' where id={3}", wf.wfownertable, wf.wffieldname, wfn.wfnodememo, dataid);
                db.SqlQueryDynamic(sqlstr);
            }
            if (isRecordTrace)
            {
               workflowinstancetracingsEx.Instance.insert(executer, instancesid, wfna);
            }

            return true;
        }

        public int insert(workflow wf, int instancesid, workflownodeaction wfna, int dataid)
        {
            workflowinstances wfinstacnes = new workflowinstances();
            wfinstacnes.ownertabledataid = dataid;
            wfinstacnes.nodcode = wfna.nodeactioncode;
            wfinstacnes.wfid = wf.id;
            wfinstacnes.currentnodeid = wfna.nextnodeid;
            instancesid = workflowinstancesEx.Instance.insert(wfinstacnes);
            return instancesid;
        }

    }
}
