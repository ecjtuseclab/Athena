using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athena.Idal;
using Ninject;
using Ninject.Modules;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：IOC依赖注入
//最后修改时间:2018-01-25
//修改日志：

namespace Athena.mydal
{
    public class mydalModule:NinjectModule
    {
        public override void Load()
        {
            Bind<IactionEx>().To<actionEx>();
            Bind<IareaEx>().To<areaEx>();
            Bind<IgroupEx>().To<groupEx>();
            Bind<IpropertyMappingEx>().To<propertyMappingEx>();
            Bind<IresourceEx>().To<resourceEx>();
            Bind<IroleActionEx>().To<roleActionEx>();
            Bind<IroleEx>().To<roleEx>();
            Bind<IroleResourceEx>().To<roleResourceEx>();
            Bind<IuserEx>().To<userEx>();
            Bind<IuserGroupEx>().To<userGroupEx>();
            Bind<IuserRoleEx>().To<userRoleEx>();
            Bind<IworkflowEx>().To<workflowEx>();
            Bind<IworkflowHelper>().To<workflowHelper>();
            Bind<IworkflowinstancesEx>().To<workflowinstancesEx>();
            Bind<IworkflowinstancetracingsEx>().To<workflowinstancetracingsEx>();            
            Bind<IworkflownodeactionEx>().To<workflownodeactionEx>();
            Bind<IworkflownodeEx>().To<workflownodeEx>();
            Bind<IworkflownodeoperatorEx>().To<workflownodeoperatorEx>();            
            Bind<IbackupEx>().To<backupEx>();
        }
    }
}
