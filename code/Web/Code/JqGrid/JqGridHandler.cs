using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using Athena.model;
using System.Text;

namespace Web.Controllers.JqGrid
{
    public class JqGridHandler
    {
        public static string getWhere(string filters)
        {
            var f = Athena.common.Util.Json.ToObject<jqgridFilter>(filters);
            string str = "(";
            if (f.groups != null && f.groups.Count() != 0)
                str = str + getGroup(f.groups);
            str = str + getString(f.groupOp, f.rules);
            str = str.Substring(0, str.Length - f.groupOp.Length - 1);
            str = str + ")";
            return str;
        }

        public static string getGroup(List<jqgridFilter> groups)
        {
            string str = "";
            foreach (jqgridFilter group in groups)
            {
                //if (group.groups.Count() != 0)
                str = "(";
                if (group.groups.Count() != 0)
                    str = str + getGroup(group.groups);
                str = str + getString(group.groupOp, group.rules);
                str = str.Substring(0, str.Length - group.groupOp.Length - 1);
                str = str + ")";
                str = str + group.groupOp;
            }
            return str;
        }

        public static string getString(string groupon, List<jqgridRule> rules)
        {
            string str = "";
            foreach (jqgridRule rule in rules)
            {
                Type type = typeof(Athena.model.role);
                var fieldname = type.GetProperty(rule.field);
                var propertytype = fieldname.PropertyType.Name;

                str = str + rule.field;
                switch (rule.op)
                {
                    case "eq":
                        str = str + " " + "=" + " ";
                        if (propertytype == "String")
                            str = str + "'" + rule.data + "'";
                        else
                            str = str + rule.data;
                        break;//等于
                    case "ne":
                        str = str + " " + "<>" + " ";
                        if (propertytype == "String")
                            str = str + "'" + rule.data + "'";
                        else
                            str = str + rule.data;
                        break;//不等于
                    case "lt":
                        str = str + " " + "=" + " ";
                        if (propertytype == "String")
                            str = str + "'" + rule.data + "'";
                        else
                            str = str + rule.data;
                        break;//小于
                    case "le":
                        str = str + " " + "<>" + " ";
                        if (propertytype == "String")
                            str = str + "'" + rule.data + "'";
                        else
                            str = str + rule.data;
                        break;//不于等于
                    case "gt":
                        str = str + " " + "=" + " ";
                        if (propertytype == "String")
                            str = str + "'" + rule.data + "'";
                        else
                            str = str + rule.data;
                        break;//大于
                    case "ge":
                        str = str + " " + "<>" + " ";
                        if (propertytype == "String")
                            str = str + "'" + rule.data + "'";
                        else
                            str = str + rule.data;
                        break;//大于等于
                    case "bw":
                        str = str + " " + "like" + " ";
                        if (propertytype == "String")
                            str = str + "'" + rule.data + "%" + "'";
                        else
                            str = str + rule.data;
                        break;//开始于
                    case "bn":
                        str = str + " " + "not like" + " ";
                        if (propertytype == "String")
                            str = str + "'" + rule.data + "%" + "'";
                        else
                            str = str + rule.data;
                        break;//不开始于
                    case "ew":
                        str = str + " " + "like" + " ";
                        if (propertytype == "String")
                            str = str + "'" + "%" + rule.data + "'";
                        else
                            str = str + rule.data;
                        break;//结束于
                    case "en":
                        str = str + " " + "not like " + " ";
                        if (propertytype == "String")
                            str = str + "'" + "%" + rule.data + "'";
                        else
                            str = str + rule.data;
                        break;//不结束于
                    case "cn":
                        str = str + " " + "like " + " ";
                        if (propertytype == "String")
                            str = str + "'" + "%" + rule.data + "%" +  "'";
                        else
                            str = str + rule.data;
                        break;//包含
                    case "nc":
                        str = str + " " + "not like " + " ";
                        if (propertytype == "String")
                            str = str + "'"+ "%" + rule.data + "%" +  "'";
                        else
                            str = str + rule.data;
                        break;//不包含
                    case "nu":
                        str = str + " " + "is null";
                        break;//空值
                    case "nn":
                        str = str + " " + "is not null";
                        break;//非空值
                    case "in":
                        str = str + " " + "in" + " " + "(" + "'" +rule.data + "'" +")";
                        break;//属于
                    case "ni":
                        str = str + " " + "not in" + " " + "(" + "'" + rule.data + "'" + ")";
                        break;//不属于
                    default:
                        str = str + " " + "=" + " ";
                        break;
                }

                str = str + " " + groupon + " ";
            }
            return str;
        }
    }
}