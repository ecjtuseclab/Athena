using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：登录接口（周庆）
//最后修改时间:2018-01-25
//修改日志：


namespace LoginPattern
{
    public interface Login
    {
        //判断登录信息是否存在
        bool IsLoginPatternMessageExist();
        //清除指定登录模式的信息
        void ClearLoginPattern();
        //
        void SetLoginPatternMessage(string username,string password);
    }
}
