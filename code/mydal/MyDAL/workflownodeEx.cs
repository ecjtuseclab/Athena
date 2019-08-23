using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athena.model;
using Athena.Idal;
using MySqlSugar;
using Athena.tool.ACEPagination;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：对工作流节点表的数据处理
//最后修改时间:2018-01-25
//修改日志：

namespace Athena.mydal
{
    public class workflownodeEx : RepositoryBase<workflownode>, IworkflownodeEx
    {
        private static workflownodeEx _instance;
        public static workflownodeEx Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new workflownodeEx();
                }
                return _instance;
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
                // db.Update<workflownode>(dictionary, p => p.id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 根据名称获得节点数据
        /// </summary>
        /// <param name="wfnodename">节点名称</param>
        /// <returns></returns>
        public List<workflownode> getWfnodeList(string wfnodename)
        {
            List<workflownode> wfnodelist = new List<workflownode>();
            if (!string.IsNullOrEmpty(wfnodename))
            {
                wfnodelist = getEntityList().Where(r => r.wfnodename == wfnodename).ToList();
            }
            else
            {
                wfnodelist = getEntityList();
            }
            return wfnodelist;
        }
        #region ACE
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public PagedData<workflownode> GetPageData(ACEPagination page, string keyword)
        {
            PagedData<workflownode> pageData = new PagedData<workflownode>();
            pageData = TakePageData(page);
            try
            {
                if (string.IsNullOrEmpty(keyword))
                    pageData = TakePageData(page);
                else if (changeParse(keyword))
                    pageData.DataList = pageData.DataList.Where(a => a.id == Convert.ToInt32(keyword)).ToList<workflownode>();
                else
                    pageData.DataList = pageData.DataList.Where(a => a.wfnodename.Contains(keyword)).ToList<workflownode>();
                return pageData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        public List<workflownode> getEntityList(int workflowID)
        {

            return db.Queryable<workflownode>().Where(p => p.wfid == workflowID).ToList();
        }
    }
}
