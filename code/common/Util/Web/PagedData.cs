using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.common.Util.Web
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

        public PagedData(Pagination paging)
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

        public PagedData(Pagination paging)
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
