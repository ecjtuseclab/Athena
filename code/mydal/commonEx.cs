using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlSugar;
using Athena.Idal;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：公共方法
//最后修改时间:2018-01-25
//修改日志：

namespace Athena.mydal
{
    public class commonEx:IcommonEx
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
        public int pageCount<T>(Dictionary<string, object> searchStr) where T : class,new()
        {
            var page = db.Queryable<T>()
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
        public List<T> getPage<T>(int pageIndex, int pageSize, Dictionary<string, object> searchStr) where T : class,new()
        {
            //设置分页的默认参数
            int pageCount = 0;
            var page = db.Queryable<T>()
                .Where(getWhere(searchStr))
                // .Where(a=>a.id>0)
                .OrderBy("id")
                .ToPageList<T>(pageIndex, pageSize, ref pageCount);
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
                foreach (var dic in searchStr)
                {
                    //取出ctrl，action
                    searchStr.Remove("ctrl");
                    searchStr.Remove("action");
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
    }
}
