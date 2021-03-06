﻿using System;
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
//模块及代码页功能描述：对工作流表的数据处理
//最后修改时间:2018-01-25
//修改日志：

namespace Athena.mydal
{
    public class workflowEx : RepositoryBase<workflow>, IworkflowEx
    {
        private static workflowEx _instance;
        public static workflowEx Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new workflowEx();
                }
                return _instance;
            }
        }
        /// <summary>
        /// 差异更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public bool update(int id, Dictionary<string, object> dictionary)
        {
            try
            {
                db.Update<workflow>(dictionary, p => p.id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 通过ID获取工作流对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public workflow getWorkflow(int id)
        {
            try
            {
                workflow table = db.Queryable<workflow>().Where(d => d.id == id).FirstOrDefault();
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public workflow getworkflow(string wfname)
        {
            workflow table = db.Queryable<workflow>().Where(d => d.wfname == wfname).FirstOrDefault();
            return table as workflow;
        }

        public int getworkflowId(string wfname)
        {
            int id = 0;
            workflow table = db.Queryable<workflow>().Where(d => d.wfname == wfname).FirstOrDefault();
            if (table != null)
                id = table.id;
            return id;
        }

        public List<workflow> getWorkflowList(string wfname)
        {
            List<workflow> workflowList = new List<workflow>();
            if (!string.IsNullOrEmpty(wfname))
            {
                workflowList = getEntityList().Where(r => r.wfname == wfname).ToList();
            }
            else
            {
                workflowList = getEntityList(); 
            }
            return workflowList;
        }

        public void SubmitForm(workflow workflowEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                int id = Convert.ToInt32(keyValue);
                workflowEntity.id = id;
                db.Update<workflow>(workflowEntity);
            }
            else
            {
                if (getworkflow(workflowEntity.wfname) == null)
                {
                    db.Insert<workflow>(workflowEntity);
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
        /// ACE分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public PagedData<workflow> GetPageData(ACEPagination page, string keyword)
        {
            PagedData<workflow> pageData = new PagedData<workflow>();
            pageData = TakePageData(page);
            try
            {
                if (string.IsNullOrEmpty(keyword))
                    pageData = TakePageData(page);
                else if (changeParse(keyword))
                    pageData.DataList = pageData.DataList.Where(a => a.id == Convert.ToInt32(keyword)).ToList<workflow>();
                else
                    pageData.DataList = pageData.DataList.Where(a => a.wfmemo.Contains(keyword)).ToList<workflow>();
                return pageData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion
    }
}
