using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Athena.model;
using Athena.basedal;
using Athena.Idal;

namespace Athena.common.Util.WebControl
{

   public class AcTree
    {
        private action action { get; set; }
        public int id { get { return action.id; } }
        public string text { get { return action.actionname; } }
       // public int? type { get { return resource.actiontype; } }
       // public int? _parentId { get; set; }
        public bool @checked { get; set; }//加@是因为和关键字冲突
        private List<action> roleresource { get; set; }
        private List<AcTree> _children;
        public List<AcTree> children
        {
            get
            {
                if (_children == null)
                    _children = new List<AcTree>();
                return _children;
            }
        }
        //需要获取check值的初始化方法
        public AcTree(action action, List<action> roleresource)
        {
            this.action = action;
            this.roleresource = roleresource;

            if (action != null)
            {
                IactionEx IactionEx = IocModule.GetEntity<IactionEx>();
                List<action> rlist = IactionEx.getEntityList().Where(p => p.actionowner == action.id.ToString()).ToList();//获取属于这个节点的子节点
                foreach (action r in rlist)
                {
                    AcTree tn = new AcTree(r, roleresource);
                    if (roleresource.Where(a => a.id == r.id).Count() > 0)
                        tn.@checked = true;
                    this.children.Add(tn);
                }
            }
        }
    }
}