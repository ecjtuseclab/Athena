﻿using System;
using System.Linq;
using System.Text;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：角色动作类
//最后修改时间:2018-04-01
//修改日志：

namespace Athena.model
{
    public class role_action
    {
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int roleid { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int actionid { get; set; }
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string controlername { get; set; }
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int actionscode { get; set; } 
    }
}
