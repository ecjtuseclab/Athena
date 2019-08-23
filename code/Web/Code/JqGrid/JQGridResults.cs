using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Controllers.JqGrid
{
    public class JQGridResults
    {
        public int page;
        public int total;
        public int records;
        public JQGridRow[] rows;
    }

    public struct JQGridRow
    {
        public int id;
        public string[] cell;
    }
    public class jqgridFilter
    {
        public string groupOp;
        public List<jqgridRule> rules;
        public List<jqgridFilter> groups;
    }
    public class jqgridRule
    {
        public string field;
        public string op;
        public string data;
    }
}