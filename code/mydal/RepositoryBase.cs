using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlSugar;
using Athena.Idal;
using Athena.tool.ACEPagination;
using Athena.common.Util.Web;
using Athena.tool.Code;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：数据处理公共方法
//最后修改时间:2018-01-25
//修改日志：

namespace Athena.mydal
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity :class,new()
    {
        private SqlSugarClient _db;
        public SqlSugarClient db
        {
            get
            {
                if (_db == null)
                {
                    _db = DB.GetInstance();
                }
                return _db;
            }
        }
      
        #region 共用方法
        /// <summary>
        /// 插入某条数据的方法
        /// </summary>
        /// <param name="entity">某条要插入的数据</param>
        /// <returns>插入完成返回的主键值</returns>
        public virtual int insert(TEntity entity)
        {
            try
            {
                return db.Insert<TEntity>(entity).ObjToInt();
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// 批量插入数据的方法
        /// </summary>
        /// <param name="tableList">插入List类型的group数据</param>
        /// <returns></returns>
        public int insertList(List<TEntity> tableList)
        {
            int res = 0;
            foreach (TEntity item in tableList)
                res = insert(item);
            return res;
        }

        /// <summary>
        /// 更新某条数据的方法
        /// </summary>
        /// <param name="table">某条要更新的数据</param>
        /// <returns>bool值</returns>
        public virtual bool update(TEntity entity)
        {
            try
            {
                db.Update<TEntity>(entity);
                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }       

        /// <summary>
        /// 删除某条数据的方法
        /// </summary>
        /// <param name="entity">某条要删除的数据</param>
        /// <returns>bool值</returns>
        public bool delete(TEntity entity)
        {
            try
            {
                db.Delete<TEntity>(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 根据ID删除某条数据的方法
        /// </summary>
        /// <param name="id">某条要删除的数据id</param>
        // <returns>bool值</returns>
        public bool delete(int id)
        {
            db.Delete<TEntity>("id = @id", new { id = id });
            return true;
        }

        /// <summary>
        /// 根据List id 批量删除
        /// </summary>
        /// <param name="ids">List<ids></param>
        /// <returns>bool</returns>
        public bool deleteList(List<int> ids)
        {
            try
            {
                //方法1
                //foreach (int item in ids)
                //{
                //    delete(item);
                //}

                //方法2
                db.Delete<TEntity>("id", ids);

                return true;
            }
            catch (Exception) { return false; }
        }

        /// <summary>
        /// 删除所有数据的方法
        /// </summary>
        /// <returns>bool值</returns>
        public bool deleteAll()
        {
            try
            {
                db.Delete<TEntity>(getEntityList());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 通过查询条件获取数据列表
        /// </summary>
        /// <param name="searchStr">查询条件</param> 
        /// <returns></returns>
        public List<TEntity> getEntityList(Dictionary<string, object> searchStr)
        {
            List<TEntity> entitylist = new List<TEntity>();
            foreach (var dic in searchStr)
            {
                if (dic.Value != null)
                {
                    entitylist = db.Queryable<TEntity>()
                   .Where(getWhere(searchStr))
                   .OrderBy("id")
                   .ToList();
                }
                else
                {
                    entitylist = getEntityList();
                }

            }
            return entitylist;
        }
        /// <summary>
        /// 获取指定页数据列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前页数据量</param>
        /// <param name="searchStr">查询条件</param>
        /// <returns></returns>
        public List<TEntity> getPaginationEntityList(int pageIndex, int pageSize, Dictionary<string, object> searchStr)
        {
            List<TEntity> entitylist = new List<TEntity>();
            var entitydata = getEntityList(searchStr);
            var pagerfirst = (pageIndex - 1) * pageSize;
            
            entitylist = entitydata.Skip(pagerfirst).Take(pageSize).ToList();
            
            return entitylist;
        }

        /// <summary>
        /// 获取指定页数据列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前页数据量</param>
        /// <param name="searchStr">查询条件</param>
        /// <returns></returns>
        public List<TEntity> getPaginationEntityList(int pageIndex, int pageSize, List<TEntity> searchStr)
        {
            List<TEntity> entitylist = new List<TEntity>();
            var pagerfirst = (pageIndex - 1) * pageSize;
            entitylist = searchStr.Skip(pagerfirst).Take(pageSize).ToList();
            return entitylist;
        }
        /// <summary>
        /// 复制粘贴  （PS：可做优化，使用批量删除方法）
        /// 用字符分割符根据‘，’分割出字符串id值，再根据动作数据id获取动作数据，再插入一样的数据
        /// </summary>
        /// <param name="ids">传入的参数由动作数据id+','组成的字符串</param>
        /// <returns>bool值</returns>
        public virtual bool copyAndPaste(string ids)
        {
            try
            {
                string[] id = ids.Split(',');
                TEntity table = new TEntity();
                for (int i = 0; i < id.Length; i++)
                {
                    table = getEntityById(int.Parse(id[i]));
                    db.Insert<TEntity>(table);
                }
                //db.Delete<TEntity, int>(it => it.id, new int[] { 20, 22 });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 根据动作数据id获得动作数据
        /// </summary>
        /// <param name="actionid">动作数据id</param>
        /// <returns>一条动作数据</returns>
        public TEntity getEntityById(int id)
        {
            try
            {
                TEntity table = db.Queryable<TEntity>().Where("id = @id", new { id = id }).FirstOrDefault();
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获得所有动作数据
        /// </summary>
        /// <returns>动作数据List</returns>
        public List<TEntity> getEntityList()
        {
            try
            {
                List<TEntity> table = db.Queryable<TEntity>().ToList();
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion       

        #region
        /// <summary>
        /// 总条数
        /// </summary>
        /// <param name="pageIndex">当前页为第pageIndex</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="searchStr">接收前台传过来的参数，
        /// 按格式拼接转换成字典类型，查询条件，
        /// 拼接格式为("key=groupname|string，value=总经理")</param>
        /// <returns>int<group></returns>
        public int pageCount<TEntity>(Dictionary<string, object> searchStr) where TEntity : class,new()
        {
            var page = db.Queryable<TEntity>()
                .Where(getWhere(searchStr)).Count();
            return page;
        }
        /// <summary>
        /// 多条件+分页查询
        /// </summary>
        /// <param name="pageIndex">当前页为第pageIndex</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="searchStr">接收前台传过来的参数，
        /// 按格式拼接转换成字典类型，查询条件，
        /// 拼接格式为("key=groupname|string，value=总经理")</param>
        /// <returns>List<group></returns>
        public List<TEntity> getPage<TEntity>(int pageIndex, int pageSize, Dictionary<string, object> searchStr) where TEntity : class,new()
        {
            //设置分页的默认参数
            int pageCount = 0;
            var page = db.Queryable<TEntity>()
                .Where(getWhere(searchStr))
                //.Where(a=>a.id>0)
                .OrderBy("id")
                .ToPageList<TEntity>(pageIndex, pageSize, ref pageCount);
            return page;
        }
        /// <summary>
        /// 返回where 条件
        /// </summary>
        /// <param name="searchStr">查询条件，拼接格式为("key=groupname|string，value=总经理")</param>
        /// <returns>string ：拼接后的where条件</returns>
        public string getWhere(Dictionary<string, object> searchStr)
        {
            string value = string.Empty;
            string where = " ''=''";
            if (searchStr != null)
            {
                searchStr.Remove("ctrl");
                searchStr.Remove("action");
                foreach (var dic in searchStr)
                {
                    string filedName = dic.Key;                        //字段名
                    Type fieldType = dic.Value.GetType();               //字段类型    
                    if (fieldType.IsValueType)//值类型
                        where += string.Format("and (({0} = {1})or({2}=0)) ", filedName, dic.Value, dic.Value);
                    else
                        where += string.Format("and (({0} like '%{1}%' )or ('{2}'=''))", filedName, dic.Value, dic.Value);//字符类型
                }
            }
            return where;
        }
        #endregion
        public List<TEntity> getPage(int pageIndex, int pageSize, Dictionary<string, object> searchStr)
        {
            throw new NotImplementedException();
        }

        public int pageCount()
        {
            throw new NotImplementedException();
        }

        public List<TEntity> getSearch(string strwhere)
        {
            try
            {
                List<TEntity> table = db.SqlQuery<TEntity>(strwhere);
                return table;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region ACE分页
        public PagedData<TEntity> TakePageData(ACEPagination page)
        {
            PagedData<TEntity> pageData = page.ToPagedData<TEntity>();
            pageData.DataList = getPageGroup(page.Page, page.PageSize);  //获取当前页数据
            pageData.TotalCount = getEntityList().Count();                           //总数据条数
            return pageData;                                                                            //pageData:返回分页信息
        }
        /// <summary>
        /// TEntity表分页
        /// </summary>
        /// <param name="pageIndex">当前页的索引</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns></returns>
        public List<TEntity> getPageGroup(int pageIndex, int pageSize)
        {
            int pageCount = 0;
            List<TEntity> glist = new List<TEntity>();
            glist = db.Queryable<TEntity>()
                .Where("id>0")
                .OrderBy("id")
                .ToPageList(pageIndex, pageSize, ref pageCount);//pageCount总条数
            List<TEntity> Iglist = new List<TEntity>();
            foreach (TEntity g in glist)
            {
                Iglist.Add(g as TEntity);
            }
            return Iglist;
        }
        #endregion
        #region ACE高级查询
        /// <summary>
        /// 返回where 条件,拼装sql eq ，nc，bw，bn，ew
        /// </summary>
        /// <param name="searchStr">查询条件，拼接格式为("key=groupname，value=总经理")</param>
        /// <returns>string ：拼接后的where条件</returns>
        /// 前台的数据格式："{\"groupOp\":\"AND\",\"rules\":[{\"field\":\"id\",\"op\":\"eq\",\"data\":\"54\"},{\"field\":\"actioncode\",\"op\":\"lt\",\"data\":\"454\"}]}"
        public List<TEntity> getSql(MutiplySearch.Search searchStr, string tablename)
        {
            //定义变量：字段(操作符)字段值，sql逻辑连接符（and，or）
            string groupOp = string.Empty;
            List<MutiplySearch.rules> rules = new List<MutiplySearch.rules>();
            string field = string.Empty;
            string opStr = string.Empty;
            string data = string.Empty;

            string where = " ''='' ";
            groupOp = searchStr.groupOp;
            rules = searchStr.rules;
            foreach (var index in rules)
            {
                field = index.field;//字段名
                opStr = index.op;       //操作符              
                string op = MutiplySearch.getSqlOp(opStr);
                data = index.data; //字段值
                if (op == "cn" || op == "nc")//如果操作符是“包含”或者“不包含”
                    where += string.Format("{0} (({1} {2} '%{3}%')or({4}=0)) ", groupOp, field, op, data, data);
                else if (op == "bw")//如果操作符是"开始于"
                    where += string.Format("{0} (({1} {2} '%{3}')or({4}='')) ", groupOp, field, op, data, data);
                else if (op == "bn")//如果操作符是“结束于”
                    where += string.Format("{0} (({1} {2} '{3}%')or({4}='')) ", groupOp, field, op, data, data);
                else
                    where += string.Format("{0} (({1} {2} '{3}')or('{4}'='')) ", groupOp, field, op, data, data);
            }

            string sql = string.Format("select * from [{0}] where {1}", tablename, where);
            List<TEntity> resList = db.SqlQuery<TEntity>(sql);
            return resList;
        }
        /// <summary>
        /// 高级查询+分页
        /// </summary>
        /// <param name="searchStr">查询条件，拼接格式为("key=groupname，value=总经理")</param>
        /// <returns>string ：拼接后的where条件</returns>
        /// 前台的数据格式："{\"groupOp\":\"AND\",\"rules\":[{\"field\":\"id\",\"op\":\"eq\",\"data\":\"54\"},{\"field\":\"actioncode\",\"op\":\"lt\",\"data\":\"454\"}]}"
        public PagedData<TEntity> mutiplySearch(string filters, ACEPagination pagination, string tableName)
        {

            PagedData<TEntity> pagedData = TakePageData(pagination);
            MutiplySearch.Search obj = Json.ToObject<MutiplySearch.Search>(filters);
            List<TEntity> list = getSql(obj, tableName);
            pagedData.TotalCount = list.Count();
            pagedData.DataList = list;
            return pagedData;
        }

        #endregion
    
    
    }
}
