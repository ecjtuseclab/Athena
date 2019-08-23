using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.tool
{
    public class DataTableTree
    {
        public string id { get; set; }
        public string ParentId { get; set; }
        public bool HasChildren { get; set; }
        public int Level { get; set; }
        public object Data { get; set; }


        public static void AppendChildren<TData>(List<TData> source, ref List<DataTableTree> ret, string parentId, int nodeLevel, Func<TData, string> idSelector, Func<TData, string> parentIdSelector, Func<TData, int?> sortCodeSelector = null)
        {
            var entities = source.Where(a => parentIdSelector(a) == parentId);

            if (sortCodeSelector != null)
            {
                entities = entities.OrderBy(a => sortCodeSelector(a)).ToList();
            }
            try
            {
                foreach (var entity in entities)
                {
                    DataTableTree tree = new DataTableTree();
                    tree.id = idSelector(entity);
                    tree.ParentId = parentId;
                    tree.HasChildren = source.Any(a => parentIdSelector(a) == idSelector(entity));
                    tree.Level = nodeLevel;
                    tree.Data = entity;
                    ret.Add(tree);
                    AppendChildren(source, ref ret, idSelector(entity), nodeLevel + 1, idSelector, parentIdSelector, sortCodeSelector);
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
    }
}
