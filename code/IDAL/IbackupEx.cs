using Athena.model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：备份接口，供IOC（胡凯雨）
//最后修改时间:2017-04-10
//修改日志：

namespace Athena.Idal
{
    public interface IbackupEx : IRepositoryBase<backup>
    {
        #region 1.基本操作
        /// <summary>
        /// 判断新增的备份文件是否合法，即备份文件名称不能重复
        /// </summary>
        /// <param name="backupname">备份文件名称</param>
        /// <returns>合法：true;不合法：false</returns>
        bool isLeagalBackupname(string backupname);
        /// <summary>
        /// 新增备份文件
        /// </summary>
        /// <param name="table"></param>
        /// <returns>成功：新增备份文件数据的主码；失败：-1</returns>
       

        /// <summary>
        /// 根据备份文件名称删除备份文件
        /// </summary>
        /// <param name="backupname"></param>
        /// <returns>成功：true;失败:false</returns>
        bool delete(string backupname);

        /// <summary>
        /// 根据备份文件的名称获得某备份文件
        /// </summary>
        /// <param name="backupname">备份文件名称</param>
        /// <returns>一个备份文件对象</returns>
        backup getBackup(string backupname);


        /// <summary>
        /// 根据备份文件名称获得此备份文件对象的id
        /// </summary>
        /// <param name="backupname">备份文件名称</param>
        /// <returns>备份文件id</returns>
        int getBackupId(string backupname);

        #endregion

        ///〈summary〉 
        ///直接删除目录下的所有文件及文件夹(保留目录)  
        ///〈/summary〉 
        ///〈param name="strDir"〉目录地址〈/param〉 
        void deleteFiles(string strDir);

    }
}
