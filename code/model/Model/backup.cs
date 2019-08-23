using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：备份类
//最后修改时间:2018-04-01
//修改日志：

namespace Athena.model.Model
{
   public class backup
    {
        public int id
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string databasename
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string backupname
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string backupsize
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? backuptime
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string backuppersonnel
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string instructions
        {
            set;
            get;
        }

        public string absolutepath
        {
            get;
            set;
        }

        public string relativepath
        {
            get;
            set;
        }
    }
}
