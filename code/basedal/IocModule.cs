using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using Ninject;
using System.Configuration;
using System.Reflection;
//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员： 周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：实现调用不同数据库（SQLServer，MySQL）的dal切换
//最后修改时间:2018-01-26
//修改日志：2017-01-26    添加此类 （周庆）
namespace Athena.basedal
{
    public class IocModule
    {

        private static string[] _dalInfo;
        private  static string[] dalInfo
        {
            get
            {
                if (_dalInfo == null)
                {
                    _dalInfo = ConfigurationManager.AppSettings["baseDal"].Split('_');
                }
                return _dalInfo;
            }
        }
        private static NinjectModule _module;
        private static  NinjectModule module
        {
            get
            {
                if (_module == null)
                {
                    _module = getModule();
                }
                return _module;
            }
        }
        private static NinjectModule getModule()
        {
            string path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"bin\";
             Assembly assembly = Assembly.LoadFrom(path + dalInfo[0]);
             NinjectModule nModule = (NinjectModule)assembly.CreateInstance(dalInfo[1]);
             return nModule;
        }
        private static IKernel _kernels;
        private static IKernel kernels
        {
            get
            {
                if (_kernels == null)
                {
                    _kernels = new StandardKernel(module);
                }
                return _kernels;
            }
        }
        public static T GetEntity<T>()
        {
            return  kernels.Get<T>();
        }

        public static string dllPath
        {
            get 
            {
                return AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            }
        }
    }
}
