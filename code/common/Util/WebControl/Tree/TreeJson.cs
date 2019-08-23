using Athena.common.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Athena.tool.Code;
namespace Athena.common.Util.WebControl
{
    /// <summary>
    /// 构造树形Json
    /// </summary>
    public static class TreeJson
    {
        /// <summary>
        /// 转换树Json
        /// </summary>
        /// <param name="list">数据源</param>
        /// <param name="ParentId">父节点</param>
        /// <returns></returns>
        public static string TreeToJson(this List<TreeEntity> list, string ParentId = "0")
        {
            StringBuilder strJson = new StringBuilder();
            List<TreeEntity> item = list.FindAll(t => t.parentId == ParentId);
            strJson.Append("[");
            if (item.Count > 0)
            {
                foreach (TreeEntity entity in item)
                {
                    strJson.Append("{");
                    strJson.Append("\"id\":\"" + entity.id + "\",");
                    strJson.Append("\"text\":\"" + entity.text.Replace("&nbsp;", "") + "\",");
                    strJson.Append("\"value\":\"" + entity.value + "\",");
                    if (entity.Attribute != null && !string.IsNullOrEmpty(entity.Attribute))
                    {
                        strJson.Append("\"" + entity.Attribute + "\":\"" + entity.AttributeValue + "\",");
                    }
                    if (entity.AttributeA != null && !string.IsNullOrEmpty(entity.AttributeA))
                    {
                        strJson.Append("\"" + entity.AttributeA + "\":\"" + entity.AttributeValueA + "\",");
                    }
                    if (entity.title != null && !string.IsNullOrEmpty(entity.title.Replace("&nbsp;", "")))
                    {
                        strJson.Append("\"title\":\"" + entity.title.Replace("&nbsp;", "") + "\",");
                    }
                    if (entity.img != null && !string.IsNullOrEmpty(entity.img.Replace("&nbsp;", "")))
                    {
                        strJson.Append("\"img\":\"" + entity.img.Replace("&nbsp;", "") + "\",");
                    }
                    if (entity.checkstate != null)
                    {
                        strJson.Append("\"checkstate\":" + entity.checkstate + ",");
                    }
                    if (entity.parentId != null)
                    {
                        strJson.Append("\"parentnodes\":\"" + entity.parentId + "\",");
                    }
                    if (entity.Level != null)
                    {
                        strJson.Append("\"Level\":" + entity.Level + ",");
                    }
                    strJson.Append("\"showcheck\":" + entity.showcheck.ToString().ToLower() + ",");
                    strJson.Append("\"isexpand\":" + entity.isexpand.ToString().ToLower() + ",");
                    if (entity.complete == true)
                    {
                        strJson.Append("\"complete\":" + entity.complete.ToString().ToLower() + ",");
                    }
                    strJson.Append("\"hasChildren\":" + entity.hasChildren.ToString().ToLower() + ",");
                    strJson.Append("\"ChildNodes\":" + TreeToJson(list, entity.id) + "");
                    strJson.Append("},");
                }
                strJson = strJson.Remove(strJson.Length - 1, 1);
            }
            strJson.Append("]");
            return strJson.ToString();
        }
        #region ACE树结构构建使用方法
        public static string ConvertToJson(List<TreeSelectModel> data)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            sb.Append(ConvertToJson(data, null, ""));
            sb.Append("]");
            return sb.ToString();
        }
        //对返回页面的数据进行处理
        public static string PageConvertToJson(string pagemsg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            sb.Append(pagemsg);
            sb.Append("]");
            return sb.ToString();
        }
        public static string ConvertToJson(this List<ACETreeEntity> data, string parentid = null)
        {
            StringBuilder strJson = new StringBuilder();
            List<ACETreeEntity> item = data.FindAll(t => t.ParentId == parentid);
            strJson.Append("[");
            if (item.Count > 0)
            {
                foreach (ACETreeEntity entity in item)
                {
                    strJson.Append("{");
                    strJson.Append("\"id\":\"" + entity.id + "\",");
                    strJson.Append("\"text\":\"" + entity.Text.Replace("&nbsp;", "") + "\",");
                    strJson.Append("\"value\":\"" + entity.Value + "\",");
                    if (entity.Title != null && !string.IsNullOrEmpty(entity.Title.Replace("&nbsp;", "")))
                    {
                        strJson.Append("\"title\":\"" + entity.Title.Replace("&nbsp;", "") + "\",");
                    }
                    if (entity.Img != null && !string.IsNullOrEmpty(entity.Img.Replace("&nbsp;", "")))
                    {
                        strJson.Append("\"img\":\"" + entity.Img.Replace("&nbsp;", "") + "\",");
                    }
                    if (entity.Checkstate != null)
                    {
                        strJson.Append("\"checkstate\":" + entity.Checkstate + ",");
                    }
                    if (entity.ParentId != null)
                    {
                        strJson.Append("\"parentnodes\":\"" + entity.ParentId + "\",");
                    }
                    strJson.Append("\"showcheck\":" + entity.Showcheck.ToString().ToLower() + ",");
                    strJson.Append("\"isexpand\":" + entity.Isexpand.ToString().ToLower() + ",");
                    if (entity.Complete == true)
                    {
                        strJson.Append("\"complete\":" + entity.Complete.ToString().ToLower() + ",");
                    }
                    strJson.Append("\"hasChildren\":" + entity.HasChildren.ToString().ToLower() + ",");
                    strJson.Append("\"ChildNodes\":" + ConvertToJson(data, entity.id.ToString()) + "");
                    strJson.Append("},");
                }
                strJson = strJson.Remove(strJson.Length - 1, 1);
            }
            strJson.Append("]");
            string ss = strJson.ToString();
            return strJson.ToString();
        }

        public static List<TreeModel> TreeWhere<TreeModel>(List<TreeModel> entityList, Predicate<TreeModel> condition, Func<TreeModel, bool> isRoot, Func<TreeModel, TreeModel, bool> isParentNodeOf)
        {
            List<TreeModel> locateList = entityList.FindAll(condition);
            List<TreeModel> treeList = new List<TreeModel>();
            foreach (TreeModel entity in locateList)
            {
                treeList.Add(entity);
                TreeModel currentNode = entity;
                while (true)
                {
                    if (isRoot(currentNode)) /* currentNode.ParentId == null */
                        break;

                    TreeModel upRecord = entityList.Find(a => isParentNodeOf(a, currentNode));
                    if (upRecord != null)
                    {
                        treeList.Add(upRecord);
                        currentNode = upRecord;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return treeList.Distinct().ToList();
        }
        public static List<TreeModel> TreeWhere<TreeModel>(List<TreeModel> entityList, Predicate<TreeModel> condition, Func<TreeModel, string> idSelector, Func<TreeModel, string> parentIdSelector)
        {
            List<TreeModel> locateList = entityList.FindAll(condition);
            List<TreeModel> treeList = new List<TreeModel>();
            foreach (TreeModel entity in locateList)
            {
                treeList.Add(entity);
                TreeModel currentNode = entity;
                while (true)
                {
                    string parentId = parentIdSelector(currentNode);
                    if (parentId == null) /* currentNode.ParentId == null */
                        break;

                    TreeModel upRecord = entityList.Find(a => idSelector(a) == parentId);
                    if (upRecord != null)
                    {
                        treeList.Add(upRecord);
                        currentNode = upRecord;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return treeList.Distinct().ToList();
        }

        /// <summary>
        /// 2017-12-25:21:30修改
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parentId"></param>
        /// <param name="blank"></param>
        /// <returns></returns>
        static string ConvertToJson(List<TreeSelectModel> data, string parentId, string blank)
        {
            StringBuilder sb = new StringBuilder();
            var ChildNodeList = data.FindAll(t => t.parentId==parentId);
            var tabline = "";
            if (parentId != null)
            {
                tabline = "　　";
            }
            if (ChildNodeList.Count > 0)
            {
                tabline = tabline + blank;
            }
            foreach (TreeSelectModel entity in ChildNodeList)
            {
                entity.text = tabline + entity.text;
                string strJson = Json.ToJson(entity);

                sb.Append(strJson);
                sb.Append(ConvertToJson(data, entity.id, tabline));
            }
            return sb.ToString().Replace("}{", "},{");
        } 
        #endregion
    }
}
