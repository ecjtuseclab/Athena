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
//开发团队成员：周庆 张婷婷 王露 陈祚松 艾美珍 张梦丽 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：对article表进行数据处理（获取数据、新增、修改、树形删除、分页等）
//最后修改时间：2018-01-26
//修改日志：
//2018-01-24 对article表进行数据处理及新增ACE相关的方法（张婷婷）

namespace Athena.mydal
{    
    public class articleEx:RepositoryBase<article>,IarticleEx
    {
        #region 对象静态实例
        private static articleEx _instance;
        public static articleEx Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new articleEx();
                }
                return _instance;
            }
        }
        #endregion

        #region 1.基础操作
        /// <summary>
        ///  判断新增的区域是否合法，即区域名称不能重复
        /// </summary>
        /// <param name="title">区域名称</param>
        /// <param name="encode">区域编码</param>
        /// <returns>合法：true;不合法：false</returns>    
        public bool isLeagalArticlename(string title, string content)
        {
            bool flag = false;
            int count = db.Queryable<article>().Where(d => d.title == title && d.content == content).Count();
            if (0 == count)
                flag = true;
            else
                flag = false;
            return flag;
        }

        /// <summary>
        /// 新增区域
        /// </summary>
        /// <param name="table"></param>
        /// <returns>成功：新增区域数据的主码；失败：-1</returns>
        public override int insert(article entity)
        {
            try
            {
                if (isLeagalArticlename(entity.title, entity.content))
                {
                    entity.inserttime = DateTime.Now;
                    entity.edittime = DateTime.Now;
                    return db.Insert<article>(entity).ObjToInt();
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
        /// 更新区域
        /// </summary>
        /// <param name="table"></param>
        /// <returns>成功：更新区域数据的主码；失败：-1</returns>
        public override bool update(article entity)
        {
            try
            {
                if (isLeagalArticlename(entity.title, entity.content))
                {
                    entity.edittime = DateTime.Now;
                    return db.Update<article>(entity).ObjToBool();
                }
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
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
                db.Update<article>(dictionary, p => p.id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// 根据标题获得数据
        /// </summary>
        /// <param name="title">标题</param>
        /// <returns>一条数据</returns>
        public article getArticle(string title)
        {
            try
            {
                article table = db.Queryable<article>().Where(d => d.title == title).FirstOrDefault();
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 根据数据id获得数据
        /// </summary>
        /// <param name="articleid">数据id</param>
        /// <returns>一条数据</returns>
        public article getArticle(int articleid)
        {
            try
            {
                article table = db.Queryable<article>().Where(d => d.id == articleid).FirstOrDefault();
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获得所有数据
        /// </summary>
        /// <returns>数据List</returns>
        public List<article> getAllArticle()
        {
            try
            {
                List<article> table = db.Queryable<article>().ToList();
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 根据标题获得数据id
        /// </summary>
        /// <param name="title">标题</param>
        /// <returns>数据id</returns>
        public int getArticleId(string title)
        {
            try
            {
                int id = 0;
                article table = db.Queryable<article>().Where(d => d.title == title).FirstOrDefault();
                if (table != null) id = table.id;
                return id;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        #endregion

        #region 基本操作
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="title">标题</param>
        /// <returns></returns>
        public List<article> getArticleList(string title)
        {
            try
            {
                List<article> articleList = new List<article>();
                if (!string.IsNullOrEmpty(title))
                {
                    articleList = getEntityList().Where(d => d.title == title).ToList();
                }
                else
                {
                    articleList = getEntityList();
                }
                return articleList;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 通过标题及文章类型获取数据列表
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="sortID">文章类型</param>
        /// <returns></returns>
        public List<article> getArticleList(string title, int sortID)
        {
            List<article> alist = new List<article>();
            if (sortID == 0)
            {
                if (!string.IsNullOrEmpty(title))
                {
                    alist = db.Queryable<article>().Where(a => a.title.Contains(title)).ToList();
                }
                else
                {
                    alist = getEntityList();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(title))
                {
                    alist = db.Queryable<article>().Where(a => a.title.Contains(title)).Where(a => a.SortID == sortID).ToList();
                }
                else
                {
                    alist = getArticleList(sortID);
                }
            }
            return alist;
        }
        /// <summary>
        /// 通过文章类型获取数据
        /// </summary>
        /// <param name="sortID"></param>
        /// <returns></returns>
        public List<article> getArticleList(int sortID)
        {
            try
            {
                List<article> alist = db.Queryable<article>().Where(a => a.SortID == sortID).ToList();
                return alist;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 表单提交
        /// </summary>
        /// <param name="roleEntity">表单提交的一条角色数据</param>
        /// <param name="keyValue">主键</param>
        public bool SubmitForm(article articleEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                int id = Convert.ToInt32(keyValue);
                articleEntity.id = id;
                articleEntity.edittime = Convert.ToDateTime(DateTime.Now.ToString());
                articleEntity.inserttime = Convert.ToDateTime(DateTime.Now.ToString());
                db.Update<article>(articleEntity);
            }
            else
            {
                articleEntity.edittime = Convert.ToDateTime(DateTime.Now.ToString());
                articleEntity.inserttime = Convert.ToDateTime(DateTime.Now.ToString());
                db.Insert<article>(articleEntity);
            }
            return true;
        }

        /// <summary>
        /// 动态根据字段名，字段值查询指定数据
        /// </summary>
        /// <param name="key">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns>List<group></returns>
        public List<article> getDynamicArticle(string key, string value)
        {
            string sql = string.Format("select * from article where {0}={1}", key, value);
            List<article> alist = db.SqlQueryDynamic(sql);
            List<article> Ialist = new List<article>();
            foreach (article a in alist)
            {
                Ialist.Add(a as article);
            }
            return Ialist;
        }

        /// <summary>
        /// article表分页
        /// </summary>
        /// <param name="pageIndex">当前页的索引</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns></returns>
        public List<article> getPageGroup(int pageIndex, int pageSize)
        {
            int pageCount = 0;
            List<article> alist = new List<article>();
            alist = db.Queryable<article>()
                .Where(c => c.id > 1).OrderBy(it => it.id)
                .ToPageList(pageIndex, pageSize, ref pageCount);//pageCount总条数
            List<article> Ialist = new List<article>();
            foreach (article g in alist)
            {
                Ialist.Add(g as article);
            }
            return Ialist;
        }


        #endregion

        #region ACE
        public PagedData<article> GetPageData(ACEPagination page, string keyword)
        {
            //查询相关的字段，根据页面的指定的字段
            PagedData<article> pageData = new PagedData<article>();
            pageData = TakePageData(page);
            try
            {
                if (string.IsNullOrEmpty(keyword))
                    pageData = TakePageData(page);
                else if (changeParse(keyword))
                    pageData.DataList = pageData.DataList.Where(a => a.id == Convert.ToInt32(keyword)).ToList<article>();
                else
                    pageData.DataList = pageData.DataList.Where(a => a.title.Contains(keyword)).ToList<article>();
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
    }
}
