using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Athena.common.Util.Web
{
    /// <summary>
    /// ///////////////////////////////////////////////////////
    ///项目名称:Athena开发框架
    ///开发者:夏萍萍
    ///模块名称和描述:高级查询信息
    ///最后更新时间:2017/5/8
    /// ////////////////////////////////////////////
    /// <summary>
    public class MutiplySearch
    {
        /// <summary>
        /// sql 规则
        /// </summary>
        public class rules
        {

            public string field { get; set; }//字段名

            public string op { get; set; }//操作符

            public string data { get; set; }//数据
         
        }

        public class Search
        {
            public string groupOp { get; set; }  //规则连接符

            public List<rules> rules { get; set; } //规则
        }
        public enum compare
        {
            eq = 0,
            gt = 1,
            lt = 2,
            cn = 3
            //[System.ComponentModel.Description("等于")]
            //eq,
            //[System.ComponentModel.Description("大于")]
            //gt,
            //[System.ComponentModel.Description("小于")]
            //lt,
            //[System.ComponentModel.Description("包含")]
            //cn,
            //[System.ComponentModel.Description("开头")]
            //bw
        }
        public static string getSqlOp(string op)
        {
            string result = string.Empty;
            switch(op)
            {
                case "eq": result = "="; break;
                case "gt": result = ">";  break;
                case "lt": result = "<";   break;
                case "cn": result = "like"; break;
                default: result = "="; break;
            }
            return result;
        }
   

    }
}
