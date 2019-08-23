using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athena.Idal;
using Athena.model.Model;
using MySqlSugar;
using System.IO;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员： 周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：数据备份
//最后修改时间:2018-01-26
//修改日志：
//2017-04-11 新增获取所有对象条数方法(张婷婷)
//2018-01-26 新增获取指定ID实例的方法（周庆）

namespace Athena.mydal
{
    public class backupEx:RepositoryBase<backup>,IbackupEx
    {
        #region 对象静态实例
        private static backupEx _instance;
        public static backupEx Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new backupEx();
                }
                return _instance;
            }
        }
        #endregion
        #region 1.基本操作
        /// <summary>
        /// 判断新增的备份文件是否合法，即备份文件名称不能重复
        /// </summary>
        /// <param name="backupname">备份文件名称</param>
        /// <returns>合法：true;不合法：false</returns>
        public bool isLeagalBackupname(string backupname)
        {
            bool flag = false;
            int count = db.Queryable<backup>().Where(d => d.backupname == backupname).Count();
            if (0 == count)
                flag = true;
            else
                flag = false;
            return flag;

        }
        /// <summary>
        /// 新增备份文件
        /// </summary>
        /// <param name="table"></param>
        /// <returns>成功：新增备份文件数据的主码；失败：-1</returns>
        public override int insert(backup table)
        {
            try
            {
                if (isLeagalBackupname(table.backupname))
                {
                    return db.Insert<backup>(table).ObjToInt();
                }
                else
                    return -1;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// 根据备份文件名称删除备份文件
        /// </summary>
        /// <param name="backupname"></param>
        /// <returns>成功：true;失败:false</returns>
        public bool delete(string backupname)
        {
            try
            {
                backup r = getBackup(backupname);
                db.Delete<backup>(r);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 根据备份文件的名称获得某备份文件
        /// </summary>
        /// <param name="backupname">备份文件名称</param>
        /// <returns>一个备份文件对象</returns>
        public backup getBackup(string backupname)
        {
            backup table = db.Queryable<backup>().Where(d => d.backupname == backupname).FirstOrDefault();
            return table;
        }


        /// <summary>
        /// 根据备份文件名称获得此备份文件对象的id
        /// </summary>
        /// <param name="backupname">备份文件名称</param>
        /// <returns>备份文件id</returns>
        public int getBackupId(string backupname)
        {
            int id = 0;
            backup table = db.Queryable<backup>().Where(d => d.backupname == backupname).FirstOrDefault();
            if (table != null) id = table.id;
            return id;
        }

        #endregion

        ///〈summary〉 
        ///直接删除目录下的所有文件及文件夹(保留目录)  
        ///〈/summary〉 
        ///〈param name="strDir"〉目录地址〈/param〉 
        public void deleteFiles(string strDir)
        {
            if (Directory.Exists(strDir))
            {
                string[] strDirs =
                Directory.GetDirectories(strDir);
                string[] strFiles =
                Directory.GetFiles(strDir);
                foreach (string strFile in strFiles)
                {
                    File.Delete(strFile);
                }
                foreach (string strdir in strDirs)
                {
                    Directory.Delete(strdir, true);
                }
            }
        }
    }
}
