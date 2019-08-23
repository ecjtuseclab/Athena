using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Athena.model;
using SqlSugar;
using Athena.Idal;
using Athena.tool.ACEPagination;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：对动作表的数据处理（艾美珍）
//最后修改时间:2018-01-25
//修改日志：
//2017-04-11 新增获取所有对象条数方法（张婷婷）
//2017-12-17 新增前端为ACE相关的方法（王露）

namespace Athena.dal
{
    public class actionEx : RepositoryBase<action>, IactionEx
    {

        #region 对象静态实例
        private static actionEx _instance;
        public static actionEx Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new actionEx();
                }
                return _instance;
            }
        }
        #endregion

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
                db.Update<action>(dictionary, p => p.id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 根据动作名称获得动作数据
        /// </summary>
        /// <param name="actionname">动作名称</param>
        /// <returns>一条动作数据</returns>
        public action getAction(string actionname)
        {
            try
            {
                action table = db.Queryable<action>().Where(d => d.actionname == actionname).FirstOrDefault();
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 根据动作名称获得动作数据id
        /// </summary>
        /// <param name="actionname">动作名称</param>
        /// <returns>动作数据id</returns>
        public int getActionId(string actionname)
        {
            try
            {
                int id = 0;
                action table = db.Queryable<action>().Where(d => d.actionname == actionname).FirstOrDefault();
                if (table != null) id = table.id;
                return id;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// 表单提交
        /// </summary>
        /// <param name="roleEntity">表单提交的一条角色数据</param>
        /// <param name="keyValue">主键</param>
        public void SubmitForm(action actionEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                int id = Convert.ToInt32(keyValue);
                actionEntity.id = id;
                db.Update<action>(actionEntity);
            }
            else
            {
                if (getAction(actionEntity.actionname) == null)
                {
                    db.Insert<action>(actionEntity);
                }
            }
        }

        /// <summary>
        /// 根据组名获取分组数据列表
        /// </summary>
        /// <param name="groupname"></param>
        /// <returns></returns>
        public List<action> getActionList(string actionname)
        {
            List<action> grouplist = new List<action>();
            if (!string.IsNullOrEmpty(actionname))
            {
                grouplist = getEntityList().Where(r => r.actionname == actionname).ToList();
            }
            else
            {
                grouplist = getEntityList();
            }
            return grouplist;
        }


        #region ACE
        #region 分页
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="page">分页信息</param>
        /// <param name="keyword">关键词</param>
        /// <returns>返回分页数据</returns>
        public PagedData<action> GetPageData(ACEPagination page, string keyword)
        {
            //查询相关的字段，根据页面的指定的字段
            PagedData<action> pageData = new PagedData<action>();
            pageData = TakePageData(page);
            try
            {
                if (string.IsNullOrEmpty(keyword))
                    pageData = TakePageData(page);
                else if (changeParse(keyword))
                    pageData.DataList = pageData.DataList.Where(a => a.actioncode == Convert.ToInt32(keyword)).ToList<action>();
                else
                    pageData.DataList = pageData.DataList.Where(a => a.actionname.Contains(keyword)).ToList<action>();
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
        /// <summary>
        /// 动态根据字段名，字段值查询指定数据
        /// </summary>
        /// <param name="key">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns>List<action></returns>
        public List<action> getDynamicAction(string key, string value)
        {
            string sql = string.Format("select * from action where {0}={1}", key, value);
            List<action> alist = db.SqlQueryDynamic(sql);
            List<action> Ialist = new List<action>();
            foreach (action a in alist)
            {
                Ialist.Add(a as action);
            }
            return Ialist;
        }
        /// <summary>
        /// group表分页
        /// </summary>
        /// <param name="pageIndex">当前页的索引</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns></returns>
        public List<action> getPageAction(int pageIndex, int pageSize)
        {
            int pageCount = 0;
            List<action> alist = new List<action>();
            alist = db.Queryable<action>()
                .Where(c => c.id > 1).OrderBy(it => it.id)
                .ToPageList(pageIndex, pageSize, ref pageCount);//pageCount总条数
            List<action> Ialist = new List<action>();
            foreach (action g in alist)
            {
                Ialist.Add(g as action);
            }
            return Ialist;
        }



        #endregion

        public List<action> getActionListbywfid(int wfid)
        {
       
               workflow wf= workflowEx.Instance.getEntityById(wfid);
             if(wf!=null)
             {
                 return db.Queryable<action>().Where(p => p.actionowner == wf.wfname).ToList();
             }
             return null;
        }
    }
}
