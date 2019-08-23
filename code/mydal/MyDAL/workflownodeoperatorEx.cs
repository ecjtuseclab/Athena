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
//模块及代码页功能描述：对工作流节点操作者表的数据处理
//最后修改时间:2018-01-25
//修改日志：

namespace Athena.mydal
{
    public class workflownodeoperatorEx : RepositoryBase<workflownodeoperator>,IworkflownodeoperatorEx
    {
        private static workflownodeoperatorEx _instance;
        public static workflownodeoperatorEx Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new workflownodeoperatorEx();
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
                db.Update<workflownodeoperator>(dictionary, p => p.id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public workflownodeoperator getworkflownodeoperator(int wfnodeactionid, int operatorid)
        {
            workflownodeoperator table = db.Queryable<workflownodeoperator>().
                Where(d => d.nodeactionid == wfnodeactionid && d.operatorid == operatorid).FirstOrDefault()
                as workflownodeoperator;
            return table;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operatortype"></param>
        /// <returns></returns>
        public List<workflownodeoperator> getWorkflowNodeoperatorList(string operatortype)
        {

            List<workflownodeoperator> workflownodeoperatorlist = new List<workflownodeoperator>();
            if (!string.IsNullOrEmpty(operatortype))
            {
                int optype = Convert.ToInt32(operatortype);
                workflownodeoperatorlist = getEntityList().Where(r => r.operatortype == optype).ToList();
            }
            else
            {
                workflownodeoperatorlist = getEntityList();
            }
            return workflownodeoperatorlist;
        }
        /// <summary>
        /// 通过id获取节点操作者对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public workflownodeoperator getWorkflowNodeoperatorEntity(int id)
        {
            try
            {
                workflownodeoperator table = db.Queryable<workflownodeoperator>().Where(d => d.id == id).FirstOrDefault();
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        ///通过节点动作id获取节点操作者数据列表后删除该数据列表
        /// </summary>
        /// <param name="nodeactionid"></param>
        /// <returns></returns>
        public void deleteWfNodeoperatorListByNodeactionid(int nodeactionid)
        {
            List<workflownodeoperator> workflownodeoperatorlist = db.Queryable<workflownodeoperator>().Where(d => d.nodeactionid == nodeactionid).ToList();
            if (workflownodeoperatorlist != null)
            {
                foreach (workflownodeoperator wfnodeoperator in workflownodeoperatorlist)
                {
                    delete(wfnodeoperator);
                }
            }
        }
        /// <summary>
        /// 提交表单
        /// </summary>
        /// <param name="workflownodeoperatorEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(workflownodeoperator workflownodeoperatorEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                int id = Convert.ToInt32(keyValue);
                workflownodeoperatorEntity.id = id;
                db.Update<workflownodeoperator>(workflownodeoperatorEntity);
            }
            else
            {
                if (getworkflownodeoperator(workflownodeoperatorEntity.nodeactionid, workflownodeoperatorEntity.operatorid) == null)
                {
                    db.Insert<workflownodeoperator>(workflownodeoperatorEntity);
                }
            }
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
        public PagedData<workflownodeoperator> GetPageData(ACEPagination page, string keyword)
        {
            PagedData<workflownodeoperator> pageData = new PagedData<workflownodeoperator>();
            pageData = TakePageData(page);
            try
            {
                if (string.IsNullOrEmpty(keyword))
                    pageData = TakePageData(page);
                else if (changeParse(keyword))
                    pageData.DataList = pageData.DataList.Where(a => a.id == Convert.ToInt32(keyword)).ToList<workflownodeoperator>();
                else if (changeParse(keyword))
                    pageData.DataList = pageData.DataList.Where(a => a.operatorid == Convert.ToInt32(keyword)).ToList<workflownodeoperator>();
                return pageData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion
        public List<workflownodeoperator> getEntityList(int workflowID)
        {
            List<workflownodeoperator> wfnoo = new List<workflownodeoperator>();
            wfnoo = db.Queryable<workflownodeoperator>().In(
                p => p.nodeactionid,
                workflownodeactionEx.Instance.getEntityList(workflowID).Select(e => e.id)
                ).ToList();
            return wfnoo;
        }
        public bool deletebynodeactionid(int nodeactionid)
        {
            try
            {
                db.Delete<workflownodeoperator>("nodeactionid=@nodeactionid", new { nodeactionid = nodeactionid });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool deletebynodeactionids(List<int> nodeactionids)
        {
            return db.Delete<workflownodeaction>("nodeactionid", nodeactionids);
        }
    }
}
