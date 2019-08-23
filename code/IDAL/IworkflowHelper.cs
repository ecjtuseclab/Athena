using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athena.Idal;
using Athena.model;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：工作流帮助接口（张梦丽）
//最后修改时间:2018-01-25
//修改日志：

namespace Athena.Idal
{
    public interface IworkflowHelper
    {
        /// <summary>
        /// 检查节点动作
        /// </summary>
        /// <param name="workflowName">工作流名称</param>
        /// <param name="dataid">业务数据id</param>
        /// <param name="actionName">动作名称</param>
        /// <param name="operatorid">操作者id</param>
        /// <returns></returns>
        bool checkNodeAction(string workflowName, int dataid, int actionid, int operatorid);
       
       
    }
}
