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
//模块及代码页功能描述：对工作流节点动作表的数据处理
//最后修改时间:2018-01-25
//修改日志：

namespace Athena.mydal
{
    public class workflownodeactionEx :RepositoryBase<workflownodeaction>,IworkflownodeactionEx
    {
        private static workflownodeactionEx _instance;
        public static workflownodeactionEx Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new workflownodeactionEx();
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
                db.Update<workflownodeaction>(dictionary, p => p.id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //是否删除? 和RepositoryBase中getEntity方法实现的功能是一样的
        public List<workflownodeaction> getInitworkflownodeaction()
        {
            List<workflownodeaction> wfnas = db.Queryable<workflownodeaction>().ToList();
            List<workflownodeaction> Iwfnas = new List<workflownodeaction>();
            foreach (workflownodeaction wfna in wfnas)
            {
                Iwfnas.Add(wfna as workflownodeaction);
            }
            return Iwfnas;
        }

        public workflownodeaction getworkflownodeaction(int workflowid, string actionname)
        {
            workflownodeaction table = db.Queryable<workflownodeaction>().Where(d => d.wfid == workflowid && d.nodeactionname == actionname).FirstOrDefault();
            return table as workflownodeaction;
        }

        public workflownodeaction getworkflownodeaction(int workflownodeactionid)
        {
            workflownodeaction table = db.Queryable<workflownodeaction>().Where(d => d.id == workflownodeactionid).FirstOrDefault();
            return table as workflownodeaction;
        }

        public List<workflownodeaction> getcountersignnodeaction(workflownodeaction nodeaction)
        {
            List<workflownodeaction> nodeactions = db.Queryable<workflownodeaction>().Where(d => d.currentnodeid == nodeaction.currentnodeid && d.nextnodeid == nodeaction.nextnodeid && d.nodetype == nodeaction.nodetype).ToList();
            List<workflownodeaction> Iwfnas = new List<workflownodeaction>();
            foreach (workflownodeaction wfna in nodeactions)
            {
                Iwfnas.Add(wfna as workflownodeaction);
            }
            return Iwfnas;
        }
        /// <summary>
        /// 通过节点动作名获取工作流节点动作列表上
        /// </summary>
        /// <param name="nodeactionname">节点动作名称</param>
        /// <returns></returns>
        public List<workflownodeaction> getWorkflowNodeactionList(string nodeactionname)
        {
            List<workflownodeaction> workflowlist = new List<workflownodeaction>();
            if (!string.IsNullOrEmpty(nodeactionname))
            {
                workflowlist = getEntityList().Where(r => r.nodeactionname == nodeactionname).ToList();
            }
            else
            {
                workflowlist = getEntityList();
            }
            return workflowlist;
        }
        /// <summary>
        /// 通过id获取节点动作对象
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public workflownodeaction getWorkflowNodeaction(int id)
        {
            try
            {
                workflownodeaction table = db.Queryable<workflownodeaction>().Where(d => d.id == id).FirstOrDefault();
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 通过节点动作名称获取节点动作对象
        /// </summary>
        /// <param name="nodeactionname">节点动作名称</param>
        /// <returns></returns>
        public workflownodeaction getWorkflowNodeactionEntity(string nodeactionname)
        {
            try
            {
                workflownodeaction table = db.Queryable<workflownodeaction>().Where(d => d.nodeactionname == nodeactionname).FirstOrDefault();
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 提交表单
        /// </summary>
        /// <param name="workflownodeactionEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(workflownodeaction workflownodeactionEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                int id = Convert.ToInt32(keyValue);
                workflownodeactionEntity.id = id;
                db.Update<workflownodeaction>(workflownodeactionEntity);
            }
            else
            {
                if (getWorkflowNodeactionEntity(workflownodeactionEntity.nodeactionname) == null)
                {
                    db.Insert<workflownodeaction>(workflownodeactionEntity);
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
        public PagedData<workflownodeaction> GetPageData(ACEPagination page, string keyword)
        {
            PagedData<workflownodeaction> pageData = new PagedData<workflownodeaction>();
            pageData = TakePageData(page);
            try
            {
                if (string.IsNullOrEmpty(keyword))
                    pageData = TakePageData(page);
                else if (changeParse(keyword))
                    pageData.DataList = pageData.DataList.Where(a => a.id == Convert.ToInt32(keyword)).ToList<workflownodeaction>();
                else
                    pageData.DataList = pageData.DataList.Where(a => a.nodeactionname.Contains(keyword)).ToList<workflownodeaction>();
                return pageData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion


        public workflownodeaction getworkflownodeaction(int workflowid, int actionid)
        {
            return db.Queryable<workflownodeaction>().Where(p => p.wfid == workflowid && p.actionid == actionid).FirstOrDefault();
        }
        public List<workflownodeaction> getEntityList(int wfid)
        {
            return db.Queryable<workflownodeaction>().Where(p => p.wfid == wfid).ToList();
        }
    }
}
