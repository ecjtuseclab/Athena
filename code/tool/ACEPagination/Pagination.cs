using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：ace分页
//最后修改时间:2018-01-25
//修改日志：

namespace Athena.tool.ACEPagination
{   
    public class ACEPagination
    {
        int _page = 1;
        int _pageSize = 20;
        public ACEPagination()
        {
        }
        public ACEPagination(int page, int pageSize)
        {
            this.Page = page;
            this.PageSize = pageSize;
        }
        /// <summary>
        /// 当前页
        /// </summary>
        public int Page { get { return this._page; } set { this._page = value; } }
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get { return this._pageSize; } set { this._pageSize = value; } }

        public PagedData ToPagedData()
        {
            PagedData pageData = new PagedData(this);
            return pageData;
        }
        public PagedData<T> ToPagedData<T>()
        {
            PagedData<T> pageData = new PagedData<T>(this);
            return pageData;
        }
    }
}
