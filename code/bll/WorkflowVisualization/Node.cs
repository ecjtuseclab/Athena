using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athena.model;
using Athena.basedal;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：节点类（陈祚松）
//最后修改时间:2018-01-11
//修改日志：2018-01-26 添加

namespace Athena.bll.WorkflowVisualization
{
    public class Node : State
    {
        public string wfnodename { get; set; }
        public string wfnodememo { get; set; }
        public int? wfnodelock { get; set; }
        public int wfid { get; set; }
        public int wfnodestatus { get; set; }
        public bool Save()
        {
            if (this.id==-1)
            {
                workflownode _wfnode = new workflownode();
                _wfnode.wfid = this.wfid;
                _wfnode.wfnodename = this.wfnodename;
                _wfnode.wfnodememo = this.wfnodememo;
                _wfnode.wfnodelock = this.wfnodelock;
                _wfnode.id = IdalCommon.IworkflownodeEx.insert(_wfnode);
                this.id = _wfnode.id;
            }
            else
            {
              return   this.update();
            }
            return true;

        }
        private bool update()
        {
            workflownode _wfnode = new workflownode();
            _wfnode.id =this.id;
            _wfnode.wfid = this.wfid;
            _wfnode.wfnodename = this.wfnodename;
            _wfnode.wfnodememo = this.wfnodememo;
            _wfnode.wfnodestatus = this.wfnodestatus;
            _wfnode.wfnodelock = this.wfnodelock;
            IdalCommon.IworkflownodeEx.update(_wfnode);
            return true;
        }
        public bool delete()
        {
            return IdalCommon.IworkflownodeEx.delete(Convert.ToInt32(this.id));
        }
    }

}
