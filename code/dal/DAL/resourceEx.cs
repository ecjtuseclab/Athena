using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Athena.model;
using Athena.Idal;
using SqlSugar;
using Athena.tool.ACEPagination;
using Athena.tool;
using Athena.Idal;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员： 周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：对资源表的数据处理(张梦丽)
//最后修改时间:2018-01-26
//修改日志：
//2017-04-11 新增获取所有对象条数方法(张婷婷)
//2018-01-26 新增获取指定ID实例的方法（周庆）

namespace Athena.dal
{
   
    public class resourceEx : RepositoryBase<resource>, IresourceEx
    {
        #region 对象静态实例
        private static resourceEx _instance;
        public static resourceEx Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new resourceEx();
                }
                return _instance;
            }
        }
        #endregion
        #region 1.基本操作
        /// <summary>
        /// 判断新增的资源是否合法，即资源名称不能重复
        /// </summary>
        /// <param name="resourcename">资源名称</param>
        /// <returns>合法：true;不合法：false</returns>
        public bool isLeagalResourcename(string resourcename)
        {
            bool flag = false;
            int count = db.Queryable<resource>().Where(d => d.resourcename == resourcename).Count();
            if (0 == count)
                flag = true;
            else
                flag = false;
            return flag;

        }
        /// <summary>
        /// 判断修改的数据字典是否合法，即字典名称不能重复
        /// </summary>
        /// <param name="resname">名称</param>
        /// <param name="value">值</param>
        /// <param name="reurl">含义</param>
        /// <param name="resscript">备注</param>
        /// <param name="parentID">上级</param>
        /// <returns></returns>
        public bool isLegalEntity(string resname, int restype, string reurl, string resscript, string resowner, 
            int resleval, string resleftico, string resrightico, string resclass, int resnum, int order, string des)
        {
            bool flag = false;
            int count = db.Queryable<resource>().Where(
                d => d.resourcename == resname && d.resourcetype == restype && d.resourceurl == reurl
                && d.resourcescript == resscript && d.resourceowner == resowner && d.resourceleval == resleval
                && d.resourceleftico == resleftico && d.resourcerightico == resrightico && d.resourcenumber == resnum
                && d.order == order && d.description == des).Count();
            if (0 == count)
                flag = true;
            else
                flag = false;
            return flag;
        }

        /// <summary>
        /// 根据资源名称删除资源
        /// </summary>
        /// <param name="resourcename"></param>
        /// <returns>成功：true;失败:false</returns>
        public bool delete(string resourcename)
        {
            try
            {
                resource r = getResource(resourcename);
                db.Delete<resource>(r as resource);
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 差异更新
        /// </summary>
        /// <param name="id">更新数据主码</param>
        /// <param name="dictionary">需要更新的数据</param>
        /// <returns>返回更新结果（true/false）</returns>
        public bool update(int id, Dictionary<string, object> dictionary)
        {
            try
            {
                db.Update<resource>(dictionary, p => p.id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 根据资源id获取角色数据
        /// </summary>
        /// <param name="id">资源id</param>
        /// <returns>一条角色数据</returns>
        public resource getResource(int id)
        {
            try
            {
                resource table = db.Queryable<resource>().Where(d => d.id == id).FirstOrDefault();
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 根据资源的名称获得某资源
        /// </summary>
        /// <param name="resourcename">资源名称</param>
        /// <returns>一个资源对象</returns>
        public resource getResource(string resourcename)
        {
            resource table = db.Queryable<resource>().Where(d => d.resourcename == resourcename).FirstOrDefault();
            return table as resource;
        }

        /// <summary>
        /// 根据资源名称获得此资源对象的id
        /// </summary>
        /// <param name="resourcename">资源名称</param>
        /// <returns>资源id</returns>
        public int getResourceId(string resourcename)
        {
            int id = 0;
            resource table = db.Queryable<resource>().Where(d => d.resourcename == resourcename).FirstOrDefault();
            if (table != null) id = table.id;
            return id;
        }
        /// <summary>
        /// 表单提交
        /// </summary>
        /// <param name="resourceEntity">表单提交的一条资源数据</param>
        /// <param name="keyValue">资源主键</param>
        public void SubmitForm(resource resourceEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                int id = Convert.ToInt32(keyValue);
                resourceEntity.id = id;
                db.Update<resource>(resourceEntity);
            }
            else
            {
                if (getResource(resourceEntity.resourcename) == null)
                {
                    db.Insert<resource>(resourceEntity);
                }
            }

        }
        /// <summary>
        /// 通过资源名称获取若干条资源数据
        /// </summary>
        /// <param name="resourcename">资源名称</param>
        /// <returns>若干条资源数据</returns>
        public List<resource> getResourceList(string resourcename)
        {
            List<resource> resourcelist = new List<resource>();
            if (!string.IsNullOrEmpty(resourcename))
            {
                resourcelist = getResourceListByresourcename(resourcename, resourcelist);
            }
            else
            {
                resourcelist = getEntityList();
            }
            return resourcelist;
        }
        /// <summary>
        /// 获取对应资源名的数据以及它的父数据与相关数据
        /// </summary>
        /// <param name="resourcename">资源名</param>
        /// <param name="resourcelist">资源列表</param>
        /// <returns></returns>
        public List<resource> getResourceListByresourcename(string resourcename, List<resource> resourcelist)
        {
            List<resource> allresourcelist = getEntityList();
            resource reso = getSuperiorNode(resourcename);
            resourcelist.Add(reso);
            if (reso.resourceowner != "0")
                getResourceListByresourcename(reso.resourcename, resourcelist);
            return resourcelist;
        }

        /// <summary>
        /// 获取对应资源名的数据以及它的子数据与相关数据
        /// </summary>
        /// <param name="resourcename">资源名</param>
        /// <returns></returns>
        public List<resource> getResourceListByresourceowner(int id)
        {
            List<resource> resourcelist = db.Queryable<resource>().Where(d => d.resourceowner == id.ToString()).ToList();
            return resourcelist;
        }

        /// <summary>
        /// 通过资源名获取资源的上级节点数据
        /// </summary>
        /// <param name="resourcename">资源名</param>
        /// <returns>资源的上级节点数据</returns>
        public resource getSuperiorNode(string resourcename)
        {
            resource re = getResource(resourcename);
            List<resource> allresourcelist = getEntityList();
            resource resource = allresourcelist.Where(r => r.id.ToString() == re.resourceowner).ToList().FirstOrDefault();
            return resource;
        }
        #endregion

        #region 重载的方法
        /// <summary>
        /// 新增资源
        /// </summary>
        /// <param name="table"></param>
        /// <returns>成功：新增资源数据的主码；失败：-1</returns>
        public override int insert(resource table)
        {
            try
            {
                if (isLeagalResourcename(table.resourcename))
                {
                    return db.Insert<resource>(table).ObjToInt();
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
        /// 复制粘贴  （PS：可做优化，使用批量删除方法）
        /// 用字符分割符根据‘，’分割出字符串id值，再根据动作数据id获取动作数据，再插入一样的数据
        /// </summary>
        /// <param name="ids">传入的参数由动作数据id+','组成的字符串</param>
        /// <returns>bool值</returns>
        public override bool copyAndPaste(string ids)
        {
            try
            {
                string[] id = ids.Split(',');
                List<resource> table = new List<resource>();

                for (int i = 0; i < id.Length; i++)
                {
                    table.Add(getEntityById(int.Parse(id[i])));
                    //copyAndPasteInert(table);                    
                }

                if (table.Count > 0)
                {
                    copyAndPasteInert(table);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 复制粘贴  （PS：可做优化，使用批量删除方法）
        /// 用字符分割符根据‘，’分割出字符串id值，再根据动作数据id获取动作数据，再插入一样的数据
        /// </summary>
        /// <param name="table">传入的参数为List<resource></param>
        /// <returns></returns>
        public void copyAndPasteInert(List<resource> table)
        {
            int new_id = 0;

            foreach (resource r in table)
            {
                new_id = db.Insert<resource>(r).ObjToInt();
                if (new_id != 0)
                {
                    List<resource> resourcelist = getResourceListByresourceowner(r.id);

                    if (resourcelist.Count > 0)
                    {
                        for (int i = 0; i < resourcelist.Count; i++)
                        {
                            resourcelist[i].resourceowner = new_id.ToString();
                        }
                        copyAndPasteInert(resourcelist);
                    }
                }
            }
        }

        public List<resource> getResourceList(int id)
        {
            List<resource> templist = new List<resource>();

            List<resource> resourcelist = getResourceListByresourceowner(id);

            if (resourcelist.Count > 0)
            {
                templist.AddRange(resourcelist);

                foreach (resource r in resourcelist)
                {
                    templist.AddRange(getResourceList(r.id));
                }
            }
            return templist;
        }
        #endregion

        #region ACE
        #region  分页+查询
        //var q = this.DbContext.Query<WikiDocument>().FilterDeleted().WhereIfNotNullOrEmpty(keyword, a => a.Title.Contains(keyword) || a.Tag.Contains(keyword));
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="page">分页信息</param>
        /// <param name="keyword">关键词</param>
        /// <returns>返回分页数据</returns>
        public PagedData<resource> GetPageData(ACEPagination page, string keyword)
        {
            //查询相关的字段，根据页面的指定的字段
            PagedData<resource> pageData = new PagedData<resource>();
            pageData = TakePageData(page);
            try
            {
                if (string.IsNullOrEmpty(keyword))
                    pageData = TakePageData(page);
                else
                    pageData.DataList = pageData.DataList.Where(a => a.resourcename.Contains(keyword)).ToList<resource>();
                return pageData;
            }
            catch (Exception)
            {
                return null;
            }
        }
        //获取树形分页数据
        public PagedData<DataTableTree> GetTreePageData(ACEPagination page, string keyword)
        {

            //查询相关的字段，根据页面的指定的字段
            List<DataTableTree> listtree = new List<DataTableTree>();
            PagedData<DataTableTree> pageData = new PagedData<DataTableTree>(listtree, 100, 1, 20);
            List<resource> res = new List<resource>();
            List<resource> resall = new List<resource>();

            res = this.getPageGroup(page.Page, page.PageSize);  //获取当前页资源数据
            //pageData = TakePageData(page);
            try
            {
                if (string.IsNullOrEmpty(keyword))
                {
                    resall = this.getEntityList();//获取的资源总条数
                    DataTableTree.AppendChildren(res, ref listtree, null, 0, a => a.id.ToString(), a => a.resourceowner);
                    pageData = new PagedData<DataTableTree>(listtree, resall.Count, page.Page, page.PageSize);
                }
                else
                {
                    resall = this.getEntityList().Where(a => a.resourcename.Contains(keyword)).ToList<resource>();//获取的资源总条数
                    DataTableTree.AppendChildren(resall, ref listtree, null, 0, a => a.id.ToString(), a => a.resourceowner);
                    pageData = new PagedData<DataTableTree>(listtree, resall.Count, page.Page, page.PageSize);
                }
                // pageData.DataList = listtree;
                // pageData.TotalCount = resall. Count; ////总数据条数


                //pageData.DataList = pageData.DataList.Where(a => a.resourcename.Contains(keyword)).ToList<resource>();
                return pageData;
            }
            catch (Exception)
            {
                return null;
            }
            //return pageData;
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
        /// 树形表格删除
        /// </summary>
        /// <param name="id"></param>
        public void deleteById(string id)
        {
            try
            {
                bool existsChildren = db.Queryable<resource>().Where(a => a.resourceowner == id).Any();
                if (existsChildren)
                {
                    throw new Exception("删除失败！操作的对象包含了下级数据");
                }
                db.Delete<resource>(a => a.id.ToString() == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion
    }
}
