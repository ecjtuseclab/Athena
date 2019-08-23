using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Athena.model;
using Athena.basedal;
using Athena.Idal;
namespace Athena.common.Util.WebControl
{

    public class RescTree
    {
        private resource resource { get; set; }

        public int id { get { return resource.id; } }
        public string text { get { return resource.resourcename; } }
        public int type { get { return resource.resourcetype; } }
        public string url { get { return resource.resourceurl; } }
        public string owner { get { return resource.resourceowner; } }

        public string leftico { get { return resource.resourceleftico; } }
        public string rightico { get { return resource.resourcerightico; } }
        public int? number { get { return resource.resourcenumber; } }

        public string description { get { return resource.resourcename; } }

        public bool @checked { get; set; }//加@是因为和关键字冲突
        private List<resource> roleresource { get; set; }

        private List<RescTree> _children;
        public List<RescTree> children
        {
            get
            {
                if (_children == null)
                    _children = new List<RescTree>();
                return _children;
            }
        }

        private IresourceEx _iresourceEx;
        public IresourceEx IresourceEx
        {
            get
            {
                if (_iresourceEx == null)
                {
                    _iresourceEx = IocModule.GetEntity<IresourceEx>();
                }
                return _iresourceEx;
            }
        }

        //需要获取check值的初始化方法
        public RescTree(resource resource)
        {
            this.resource = resource;

            if (resource != null)
            {
                List<resource> rlist = IresourceEx.getEntityList().Where(p => p.resourceowner == resource.id.ToString()).ToList();//获取属于这个节点的子节点
                foreach (resource r in rlist)
                {
                    RescTree tn = new RescTree(r);
                    this.children.Add(tn);
                }
            }
        }

        //需要获取check值的初始化方法
        public RescTree(resource resource, List<resource> roleresource)
        {
            this.resource = resource;
            this.roleresource = roleresource;

            if (resource != null)
            {
                List<resource> rlist = IresourceEx.getEntityList().Where(p => p.resourceowner == resource.id.ToString()).ToList();//获取属于这个节点的子节点
                foreach (resource r in rlist)
                {

                    RescTree tn = new RescTree(r, roleresource);
                    tn.@checked = false;
                    if (roleresource.Where(a => a.id == r.id).Count() > 0)
                        tn.@checked = true;
                    this.children.Add(tn);
                }
            }
        }
    }
}