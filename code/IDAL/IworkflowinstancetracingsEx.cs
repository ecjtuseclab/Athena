using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athena.model;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：工作流影子跟踪表接口，供IOC
//最后修改时间:2017-04-09
//修改日志：

namespace Athena.Idal
{
   public interface IworkflowinstancetracingsEx :IRepositoryBase<workflowinstancetracings>
    {
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="executer">执行者</param>
        /// <param name="instancesid">工作流状态id</param>
        /// <param name="wfna">节点动作对象</param>
        /// <returns>新增数据的id</returns>
       int insert(string executer, int instancesid, workflownodeaction wfna);
        
    }
}
