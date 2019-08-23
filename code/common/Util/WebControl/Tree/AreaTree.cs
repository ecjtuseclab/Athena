using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athena.model;
using Athena.Idal;
using Athena.basedal;

namespace Athena.common.Util.WebControl
{
    /// <summary>
    /// 项目名称：腾杰超轻量级开发框架
    /// 开发者：陈兰兰
    /// 模块名称和描述：区域树
    /// 最后更新时间：2017/4/9
    /// 日志说明：
    /// </summary>
    public class AreaTree
    {
        private area area { get; set; }
        public int id { get { return area.id; } }
        public string text { get { return area.fullname; } }
        public bool @checked { get; set; }//加@是因为和关键字冲突
        private List<area> arealist { get; set; }//已有的地区
        private List<AreaTree> _children;
        public List<AreaTree> children
        {
            get
            {
                if (_children == null)
                    _children = new List<AreaTree>();
                return _children;
            }
        }
        public IareaEx IareaEx = IocModule.GetEntity<IareaEx>();
        /// <summary>
        /// 构造函数，返回树形区域信息并且勾选已有区域
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="arealist">已拥有区域</param>
        public AreaTree(area currentNode, List<area> arealist)
        {
            this.area = currentNode;
            this.arealist = arealist;
            if (currentNode != null)
            {
                List<area> allNodes = IareaEx.getEntityList().Where(p => Convert.ToInt32(p.parentid) == currentNode.id).ToList();//获取属于这个节点的子节点
                foreach (area an in allNodes)//遍历当前节点下的所有子节点
                {
                    AreaTree at = new AreaTree(an, arealist);
                    at.@checked = false;
                    if (arealist.Where(p => p.id == an.id).Count() > 0)
                        at.@checked = true;
                    this.children.Add(at);//递归创建子节点
                }
            }
        }
        /// <summary>
        /// 构造函数，单纯显示树形
        /// </summary>
        /// <param name="currentNode">当前区域</param>
        public AreaTree(area currentNode)//单纯显示树形
        {
            this.area = currentNode;

            List<area> allNodes = IareaEx.getEntityList().Where(p => Convert.ToInt32(p.parentid) == currentNode.id).ToList();//获取属于这个节点的子节点
            foreach (area a in allNodes)//遍历当前节点下的所有子节点
            {
                AreaTree tn = new AreaTree(a);
                this.children.Add(tn);//递归创建子节点
            }
        }
    }
}
