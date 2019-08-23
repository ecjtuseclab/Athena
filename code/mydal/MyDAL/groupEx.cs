using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athena.model;
using MySqlSugar;
using Athena.Idal;
using Athena.tool.ACEPagination;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员： 周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：对分组表的数据处理
//最后修改时间:2018-01-25
//修改日志：
// 2017-12-17 新增前端为ACE相关的方法（王露）

namespace Athena.mydal
{
   public class groupEx:RepositoryBase<group>,IgroupEx
    {
        #region 对象静态实例
        private static groupEx _instance;
        public static groupEx Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new groupEx();
                }
                return _instance;
            }
        }
        #endregion

        /// <summary>
        ///  判断新增的分组是否合法，即分组名称不能重复
        /// </summary>
        /// <param name="fullname">分组名称</param>
        /// <param name="encode">分组编码</param>
        /// <returns>合法：true;不合法：false</returns>     
        public bool isLeagalGroupname(string fullname, int encode)
        {
            bool flag = false;
            int count = db.Queryable<group>().Where(d => d.groupcode == encode && d.groupname == fullname).Count();
            if (0 == count)
                flag = true;
            else
                flag = false;
            return flag;
        }
        /// <summary>
        /// 根据分组id获取分组实体
        /// </summary>
        /// <param name="groupid"></param>
        /// <returns></returns>
        public group getGroup(int groupid)
        {
            try
            {
                group table = db.Queryable<group>().Where(d => d.id == groupid).FirstOrDefault();
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 根据组名获取分组数据列表
        /// </summary>
        /// <param name="groupname"></param>
        /// <returns></returns>
        public List<group> getGroupList(string groupname)
        {
            List<group> grouplist = new List<group>();
            if (!string.IsNullOrEmpty(groupname))
            {
                grouplist = getEntityList().Where(r => r.groupname == groupname).ToList();
            }
            else
            {
                grouplist = getEntityList();
            }
            return grouplist;
        }
        /// <summary>
        /// 根据组名删除
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <returns>bool</returns>
        public bool deleteByName(string groupName)
        {
            try
            {
                db.Delete<group>(a => a.groupname == groupName);
                return true;
            }
            catch (Exception) { return false; }
        }

        /// <summary>
        /// 差异更新
        /// </summary>
        /// <param name="id">跟新数据主码</param>
        /// <param name="dictionary">需要更新的数据</param>
        /// <returns>返回更新结果（true/false）</returns>
        public bool update(int id, Dictionary<string, object> dictionary)
        {
            try
            {
                db.Update<group>(dictionary, p => p.id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 表单提交
        /// </summary>
        /// <param name="groupEntity">表单提交的一条分组数据</param>
        /// <param name="keyValue">主键</param>
        public bool SubmitForm(group groupEntity, string keyValue)
        {
           if (isLeagalGroupname(groupEntity.groupname, (int)groupEntity.groupcode))
           {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    int id = Convert.ToInt32(keyValue);
                    groupEntity.id = id;
                    db.Update<group>(groupEntity);
                }
                else
                {
                    db.Insert<group>(groupEntity);
                }
                return true;
           }
           else
           {
                return false;
           }
         }
        /// <summary>
        /// 动态根据字段名，字段值查询指定数据
        /// </summary>
        /// <param name="key">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns>List<group></returns>
        public List<group> getDynamicGroup(string key, string value)
        {
            string sql = string.Format("select * from group where {0}={1}", key, value);
            List<group> glist = db.SqlQueryDynamic(sql);
            List<group> Iglist = new List<group>();
            foreach (group g in glist)
            {
                Iglist.Add(g as group);
            }
            return Iglist;
        }
        /// <summary>
        /// 根据分组名获取分组的一条数据
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <returns></returns>
        public group getGroup(string groupName)
        {
            group table = db.Queryable<group>().Where(d => d.groupname == groupName).FirstOrDefault();
            return table as group;
        }

        /// <summary>
        /// group表分页
        /// </summary>
        /// <param name="pageIndex">当前页的索引</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns></returns>
        public List<group> getPageGroup(int pageIndex, int pageSize)
        {
            int pageCount = 0;
            List<group> glist = new List<group>();
            glist = db.Queryable<group>()
                .Where(c => c.id > 1).OrderBy(it => it.id)
                .ToPageList(pageIndex, pageSize, ref pageCount);//pageCount总条数
            List<group> Iglist = new List<group>();
            foreach (group g in glist)
            {
                Iglist.Add(g as group);
            }
            return Iglist;
        }

        /// <summary>
        /// 根据组名获取主键id
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public int getGroupId(string groupName)
        {
            try
            {
                int id = 0;
                group table = db.Queryable<group>().Where(d => d.groupname == groupName).FirstOrDefault();
                if (table != null) id = table.id;
                return id;
            }
            catch (Exception) { return -1; }
        }
        #region ACE
        public PagedData<group> GetPageData(ACEPagination page, string keyword)
        {
            //查询相关的字段，根据页面的指定的字段
            PagedData<group> pageData = new PagedData<group>();
            pageData = TakePageData(page);
            try
            {
                if (string.IsNullOrEmpty(keyword))
                    pageData = TakePageData(page);
                else if (changeParse(keyword))
                    pageData.DataList = pageData.DataList.Where(a => a.groupcode == Convert.ToInt32(keyword)).ToList<group>();
                else
                    pageData.DataList = pageData.DataList.Where(a => a.groupname.Contains(keyword)).ToList<group>();
                return pageData;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 类型转换成功与否判断
        /// </summary>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        public bool changeParse(string keyword)
        {
            try
            {
                Convert.ToInt32(keyword);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        } 
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupIds">字符串拼接的组ids，拼接格式（1,2,3）</param>
        /// <param name="cellHeaders">字符串拼接的字段名(id,groupName)</param>
        /// <param name="cellHeaderNames">字符串拼接的表单别名(ID,组名)</param>
        /// <returns>bool</returns>
        public bool excelExport(string groupIds, string cellHeaders, string cellHeaderNames)
        {

            try
            {
                return true;
            }
            catch (Exception) { return false; }
        }

        /// <summary>
        /// excel导入
        /// </summary>
        public Dictionary<string, string> getCellHeaderName(string[] cellHeaderNames, string[] cellHeaderNameArr)
        {
            Dictionary<string, string> cellheader = new Dictionary<string, string>();
            //添加到字典
            for (int i = 0; i < cellHeaderNames.Length; i++)
            {
                for (int j = 0; j < cellHeaderNames.Length; j++)
                {
                    cellheader.Add(cellHeaderNames[i], cellHeaderNameArr[i]);
                }
            }
            return cellheader;
        }

        /// <summary>
        /// 空方法：后期完善
        /// </summary>
        /// <returns></returns>
        public bool excelImport()
        {
            try
            {
                return true;
            }
            catch (Exception) { return false; }
        }

        /// <summary>
        /// 空方法：后期完善
        /// </summary>
        /// <returns></returns>
        public bool pdfExport()
        {
            try
            {
                return true;
            }
            catch (Exception) { return false; }
        }
        /// <summary>
        /// 空方法：后期完善
        /// </summary>
        /// <returns></returns>
        public bool wordImport()
        {
            try
            {
                return true;
            }
            catch (Exception) { return false; }
        }
        /// <summary>
        /// 空方法：后期完善
        /// </summary>
        /// <returns></returns>
        public bool wordExport()
        {
            try
            {
                return true;
            }
            catch (Exception) { return false; }
        }


        public bool updateById(int id)
        {
            throw new NotImplementedException();
        }

        public bool updateByGroupName(string groupName)
        {
            throw new NotImplementedException();
        }

        public bool updateAll()
        {
            throw new NotImplementedException();
        }
    }
}
