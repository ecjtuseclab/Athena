using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athena.model;
using Athena.Idal;
using Athena.basedal;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：工作流设计面板类（陈祚松）
//最后修改时间:2018-01-11
//修改日志：

namespace Athena.bll.WorkflowVisualization
{
   public class WorkflowPanel
   {

       #region 原工作流 属性
       public int id { get; set; }
       public string wfname { get; set; }
       public string wfmemo { get; set; }
       public string wfownertable { get; set; }
       public string wffieldname { get; set; }
       public DateTime wfbegintime { get; set; }
       public DateTime wfstoptime { get; set; }
       public int? wflock { get; set; }
       private string wfjsonstr { get { return this.WorkflowJsonProcess(); } }
       #endregion
       public bool Save()
       {
           try
           {
               if (this.CheckPath() && this.CheckNode() && this.CheckNodeaction())
               {
                   //保存节点信息
                   foreach (Node Item in this.Nodes)
                   {
                       Item.wfid = this.id;
                       Item.Save();
                   }
                   //修正跃迁顺序
                   foreach (NodeAction Item in this.NodeActions)
                   {
                       Path startPath = this.Paths.Where(p => p.to == Item.StateID).FirstOrDefault();
                       Node startNode = this.Nodes.Where(p => p.StateID == startPath.from).FirstOrDefault();
                       Path endPath = this.Paths.Where(p => p.from == Item.StateID).FirstOrDefault();
                       Node endNode = this.Nodes.Where(p => p.StateID == endPath.to).FirstOrDefault();
                       Item.currentnodeid = Convert.ToInt32(startNode.id);
                       Item.nextnodeid = Convert.ToInt32(endNode.id);
                       Item.wfid = this.id;
                       Item.Save();
                   }
                   this.UpdateJsonStr();
                   this.WorkflowClear();
                   return true;
               }
               return false;
           }
           catch(Exception ex)
           {

               return false;
           }
          
       }
       private bool WorkflowClear()
       {
           List<int> nodeids = this.Nodes.Select(p => p.id).ToList().Union(this.workflowNodes.Select(e => e.id).ToList()).ToList();
           foreach (int id in nodeids)
           {
               Node temp = this.Nodes.Where(p => p.id == id).FirstOrDefault();
               if (temp == null)
               {
                   IdalCommon.IworkflownodeEx.delete(id);
               }
           }
           List<int> nodeactionids = this.NodeActions.Select(p => p.id).ToList().Union(this.workflowNodeActions.Select(e => e.id).ToList()).ToList();
           foreach (int id in nodeactionids)
           {
               NodeAction temp = this.NodeActions.Where(p => p.id == id).FirstOrDefault();
               if (temp == null)
               {
                   IdalCommon.IworkflownodeoperatorEx.deletebynodeactionid(id);
                   IdalCommon.IworkflownodeactionEx.delete(id);
               }
           }
           return true;
       }
       private List<workflownode> _workflowNodes;
       public  List<workflownode> workflowNodes
       {
           get
           {
               if (_workflowNodes == null)
               {
                   _workflowNodes = IdalCommon.IworkflownodeEx.getEntityList(this.id);
               }
               return _workflowNodes;
           }
       }

       private List<workflownodeaction> _workflowNodeActions;
       public List<workflownodeaction> workflowNodeActions
       {
           get 
           {
               if (_workflowNodeActions == null)
               {
                   _workflowNodeActions = IdalCommon.IworkflownodeactionEx.getEntityList(this.id);
               }
               return _workflowNodeActions ;
           }
       }
       private bool UpdateJsonStr()
       {
           try
           {
               Dictionary<string, object> wfdic = new Dictionary<string, object>();
               wfdic["wfjsonstr"] = this.wfjsonstr;
               IdalCommon.IworkflowEx.update(this.id, wfdic);
               return true;
           }
           catch (Exception ex)
           {
               return false;
           }

       }
       public bool delete()
       {
           try
           {
               List<int> naids = this.NodeActions.Select(p => p.id).ToList();
               IdalCommon.IworkflownodeoperatorEx.deletebynodeactionids(naids);
               IdalCommon.IworkflownodeEx.deleteList(naids);
               List<int> nids = this.Nodes.Select(p => p.id).ToList();
               IdalCommon.IworkflownodeEx.deleteList(nids);
               IdalCommon.IworkflowEx.delete(Convert.ToInt32(this.id));
               return true;
           }
           catch
           {
               return false;
           }
       }
       #region 可视化节点、跃迁信息、路径集合
       private List<Node> _nodes;
       //工作流节点
       public List<Node> Nodes
       {
           get { if (_nodes == null) { _nodes = new List<Node>(); } return _nodes; }
           set { _nodes = value; }
       }
       private List<NodeAction> _nodeActions;
       //工作流节点动作
       public List<NodeAction> NodeActions
       {
         get{
             if (_nodeActions == null)
             {
                 _nodeActions = new List<NodeAction>();
             }
             return _nodeActions;
         }
           set { _nodeActions = value; }
       }
       //可视化矩形
       private List<State> _states;
       public List<State> States
       {
           get 
           {
               if (_states == null)
               {
                   _states = new List<State>();
                   foreach (Node Item in this.Nodes)
                   {
                       this._states.Add(Item);
                   }
                   foreach (NodeAction Item in this.NodeActions)
                   {
                       this._states.Add(Item);
                   }
               }
              
               return _states;
           }
       }
 
       private List<Path> _paths;
       //可视化路径
       public List<Path> Paths
       {
           get 
           {
               if (_paths == null)
               {
                   _paths = new List<Path>();
               }
               return _paths;
           }
           set { _paths = value; }
       }
       #endregion

       private  bool CheckPath()
       {
           foreach (Path Item in this.Paths)
           {
               State start = this.States.Where(p => p.StateID == Item.from).FirstOrDefault();
               State end = this.States.Where(p => p.StateID == Item.to).FirstOrDefault();
               if (start.typeName == end.typeName)
               {
                   return false;
               }
           }

           return true;
       }

       //检测有没有相同节点名称
       private bool CheckNode()
       {
        return !(this.Nodes.GroupBy(p => p.wfnodename).Where(g => g.Count() > 1).Count()>1);
       }
       //检测有没有相同动作名称
       private bool CheckNodeaction()
       {    
           return true; 
       }
       public string workflowjson { get; set; }
       public string WorkflowJsonProcess()
       {
            
           foreach (State Item in this.States)
           {
               workflowjson=workflowjson.Replace("<"+Item.StateID+">",Item.id.ToString());
           }
           foreach (NodeAction Item in this.NodeActions)
           {
               workflowjson = workflowjson.Replace("<op" + Item.StateID + ">", Item.opid.ToString());
           }
           return workflowjson;
       }
   }
   
}
