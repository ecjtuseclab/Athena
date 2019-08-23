using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athena.Idal;
using Athena.model;
using MySqlSugar;
using Athena.tool.ACEPagination;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：对数据字典表的数据处理
//最后修改时间:2018-01-25
//修改日志：
//2017-12-17 新增前端为ACE相关的方法（王露）

namespace Athena.mydal
{
    public class propertyMappingEx : RepositoryBase<propertymapping>,IpropertyMappingEx
    {
        #region 对象静态实例
        private static propertyMappingEx _instance;
        public static propertyMappingEx Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new propertyMappingEx();
                }
                return _instance;
            }
        }
        #endregion
        /// <summary>
        ///  判断新增的数据字典是否合法，即字典名称不能重复
        /// </summary>
        /// <param name="fullname">数据字典名称</param>
        /// <returns>合法：true;不合法：false</returns>    
        public bool isLeagalEntityname(string fullname)
        {
            bool flag = false;
            int count = db.Queryable<propertymapping>().Where(d =>d.propertyName == fullname).Count();
            if (0 == count)
                flag = true;
            else
                flag = false;
            return flag;
        }
        /// <summary>
        /// 判断修改的数据字典是否合法，即字典名称不能重复
        /// </summary>
        /// <param name="fullname">名称</param>
        /// <param name="value">值</param>
        /// <param name="meaning">含义</param>
        /// <param name="remark">备注</param>
        /// <param name="parentID">上级</param>
        /// <returns></returns>
        public bool isLegalEntity(string fullname, string value, string meaning, string remark)
        {
            bool flag = false;
            int count = db.Queryable<propertymapping>().Where(d => d.propertyName == fullname && d.propertyValue == value && d.propertyMeaning == meaning && d.remark == remark).Count();
            if (0 == count)
                flag = true;
            else
                flag = false;
            return flag;
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
                db.Update<propertymapping>(dictionary, p => p.id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 根据propertyName获得数据
        /// </summary>
        /// <param name="id">propertyName</param>
        /// <returns>一条数据</returns>
        public propertymapping getPropertyMapping(string propertyName)
        {
            try
            {
                propertymapping table = db.Queryable<propertymapping>().Where(d => d.propertyName == propertyName).FirstOrDefault();
                return table as propertymapping;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 根据id获得数据
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>一条数据</returns>
        public propertymapping getPropertyMapping(int id)
        {
            try
            {
                propertymapping table = db.Queryable<propertymapping>().Where(d => d.id == id).FirstOrDefault();
                return table as propertymapping;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 根据parentid获得数据
        /// </summary>
        /// <param name="parentid">父节点</param>
        /// <returns>数据list</returns>
        public List<propertymapping> getPropertyMappingByParentid(int parentid)
        {
            try
            {
                List<propertymapping> table = db.Queryable<propertymapping>().Where(d => d.parentId == parentid).ToList();
                return table as List<propertymapping>;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 根据propertyMeaning获得数据id
        /// </summary>
        /// <param name="actionname">propertyMeaning</param>
        /// <returns>数据id</returns>
        public int getPropertyMappingId(string propertyMeaning)
        {
            try
            {
                int id = 0;
                propertymapping table = db.Queryable<propertymapping>().Where(d => d.propertyMeaning == propertyMeaning).FirstOrDefault();
                if (table != null) id = table.id;
                return id;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        /// <summary>
        /// 根据属性名获取分组数据列表
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public List<propertymapping> getPropertyMappingList(string propertyName)
        {
            List<propertymapping> propertymappinglist = new List<propertymapping>();
            if (!string.IsNullOrEmpty(propertyName))
            {
                propertymappinglist = getEntityList().Where(r => r.propertyName == propertyName).ToList();
            }
            else
            {
                propertymappinglist = getEntityList();
            }
            return propertymappinglist;
        }
        /// <summary>
        /// 根据属性名和父节点获得数据
        /// </summary>
        /// <param name="parentid">父节点</param>
        /// <param name="propertyName">属性名</param>
        /// <returns>数据</returns>
        public List<propertymapping> getPropertyMappingList(int parentid, string propertyName)
        {
            try
            {
                List<propertymapping> table = db.Queryable<propertymapping>()
                    .Where(d => (d.propertyName == propertyName || propertyName == "") && d.parentId == parentid)
                    .ToList();
                return table;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据属性名获取数据字典一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public propertymapping gettPropertyMapping(int id)
        {
            try
            {
                propertymapping table = db.Queryable<propertymapping>().Where(d => d.id == id).FirstOrDefault();
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 表单提交
        /// </summary>
        /// <param name="propertymappingEntity">表单提交的一条数据字典数据</param>
        /// <param name="keyValue">主键</param>
        public void SubmitForm(propertymapping propertymappingEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                int id = Convert.ToInt32(keyValue);
                propertymappingEntity.id = id;
                db.Update<propertymapping>(propertymappingEntity);
            }
            else
            {
                if (getPropertyMapping(propertymappingEntity.propertyName) == null)
                {
                    db.Insert<propertymapping>(propertymappingEntity);
                }
            }
        }
        #region ACE
        #region 分页+查询
        //var q = this.DbContext.Query<WikiDocument>().FilterDeleted().WhereIfNotNullOrEmpty(keyword, a => a.Title.Contains(keyword) || a.Tag.Contains(keyword));
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="page">分页信息</param>
        /// <param name="keyword">关键词</param>
        /// <returns>返回分页数据</returns>
        public PagedData<propertymapping> GetPageData(ACEPagination page, string keyword)
        {
            //查询相关的字段，根据页面的指定的字段
            PagedData<propertymapping> pageData = new PagedData<propertymapping>();
            pageData = TakePageData(page);
            try
            {
                if (string.IsNullOrEmpty(keyword))
                    pageData = TakePageData(page);
                else
                    pageData.DataList = pageData.DataList.Where(a => a.propertyName.Contains(keyword)).ToList<propertymapping>();
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
        #endregion


    }
}
