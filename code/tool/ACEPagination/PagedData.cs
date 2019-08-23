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
    public class PagedData : PagedData<object>
    {
        public PagedData()
            : base()
        {
        }
        public PagedData(List<object> dataList)
            : base(dataList, 0)
        {
        }
        public PagedData(List<object> dataList, int totalCount)
            : base(dataList, totalCount, 0, 0)
        {
        }
        public PagedData(List<object> dataList, int totalCount, int currentPage, int pageSize)
            : base(dataList, totalCount, currentPage, pageSize)
        {
        }

        public PagedData(ACEPagination paging)
            : base(paging)
        {
        }
        public PagedData(int currentPage, int pageSize)
            : base(currentPage, pageSize)
        {
        }
        public PagedData(int totalCount, int currentPage, int pageSize)
            : base(new List<object>(), totalCount, currentPage, pageSize)
        {
        }
    }

    public class PagedData<T>
    {
        public PagedData()
            : this(new List<T>())
        {
        }
        public PagedData(List<T> dataList)
            : this(dataList, 0)
        {
        }
        public PagedData(List<T> dataList, int totalCount)
            : this(dataList, totalCount, 0, 0)
        {
        }
        public PagedData(List<T> dataList, int totalCount, int currentPage, int pageSize)
        {
            this.DataList = dataList;
            this.TotalCount = totalCount;
            this.CurrentPage = currentPage;
            this.PageSize = pageSize;
        }

        public PagedData(ACEPagination paging)
            : this(paging.Page, paging.PageSize)
        {
        }
        public PagedData(int currentPage, int pageSize)
            : this(new List<T>(), 0, currentPage, pageSize)
        {
        }
        public PagedData(int totalCount, int currentPage, int pageSize)
            : this(new List<T>(), totalCount, currentPage, pageSize)
        {
        }

        public int TotalCount { get; set; }
        public int TotalPage
        {
            get
            {
                if (this.TotalCount > 0)
                {
                    return this.TotalCount % this.PageSize == 0 ? this.TotalCount / this.PageSize : this.TotalCount / this.PageSize + 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<T> DataList { get; set; }
    }
}
