using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athena.basedal;
using Athena.model;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：节点动作类（陈祚松）
//最后修改时间:2018-01-11
//修改日志：

namespace Athena.bll.WorkflowVisualization
{
    public class NodeAction : State
    {
        public int actionid { get; set; }
        public int? nodetype { get; set; }
        public int? operatortype { get; set; }
        public string operatorid { get; set; }
        public int? operatorlock { get; set; }
        public int wfid { get; set; }
        public int opid { get; set; }
        public int operatorstatus { get; set; }
        public string nodeoperator { get; set; }
        public int currentnodeid
        {
            get;
            set;
        }
        public int nextnodeid
        {
            get;
            set;
        }

        //工作流节点保存
        public bool Save()
        {
            workflownodeaction wfna = new workflownodeaction();
            if (this.id==-1)
            {
                wfna.wfid = this.wfid;
                wfna.actionid = this.actionid;
                wfna.nodetype = this.nodetype;
                wfna.currentnodeid = this.currentnodeid;
                wfna.nextnodeid = this.nextnodeid; 
                wfna.id = IdalCommon.IworkflownodeactionEx.insert(wfna);
                this.id = wfna.id;
                this.saveoperator(wfna.id);
            }
            else
            {
                this.update();
            }
            return true;
        }
        private bool update()
        {
            workflownodeaction wfna = new workflownodeaction();
            wfna.id = Convert.ToInt32(this.id);
            wfna.wfid = this.wfid;
            wfna.actionid = this.actionid;
            wfna.currentnodeid = this.currentnodeid;
            wfna.nextnodeid = this.nextnodeid;
            wfna.nodetype = this.nodetype;
            updateoperator();
            return IdalCommon.IworkflownodeactionEx.update(wfna);
             
        }
        private bool updateoperator()
        {
            workflownodeoperator wfnoperator = new workflownodeoperator();
            wfnoperator.id = this.opid;
            wfnoperator.nodeactionid =this.id;
            wfnoperator.operatortype = this.operatortype == null ? 0 : (int)this.operatortype;
            wfnoperator.operatorid = Convert.ToInt32(this.operatorid);
            wfnoperator.nodeoperator = this.operatorid;
            wfnoperator.operatorlock = this.operatorlock;
            wfnoperator.operatorstatus = this.operatorstatus;
            wfnoperator.nodeoperator = this.nodeoperator;
            IdalCommon.IworkflownodeoperatorEx.update(wfnoperator);
            return true;
        }
        //跃迁执行规则 保存
        private workflownodeoperator saveoperator(int nodeactionid)
        {
            workflownodeoperator wfnoperator = new workflownodeoperator();
            wfnoperator.nodeactionid = nodeactionid;
            wfnoperator.operatortype = this.operatortype == null ? 0 : (int)this.operatortype;
            wfnoperator.nodeoperator = this.operatorid;
            wfnoperator.operatorlock = this.operatorlock;
            wfnoperator.operatorstatus = this.operatorstatus;
            wfnoperator.nodeoperator = this.nodeoperator;
            wfnoperator.id = IdalCommon.IworkflownodeoperatorEx.insert(wfnoperator);
            this.opid = wfnoperator.id;
            return wfnoperator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool delete()
        {
            if (IdalCommon.IworkflownodeoperatorEx.deletebynodeactionid(Convert.ToInt32(this.id)))
            {
                return IdalCommon.IworkflownodeactionEx.delete(Convert.ToInt32(this.id));
            }
            return false;
        }
    }
}
