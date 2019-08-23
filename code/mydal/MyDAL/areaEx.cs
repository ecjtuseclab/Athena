using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athena.model;
using MySqlSugar;
using Athena.Idal;
using Athena.tool.ACEPagination;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：对区域表的数据处理（张婷婷）
//最后修改时间:2018-01-25
//修改日志：

namespace Athena.mydal
{
    public class areaEx:RepositoryBase<area>,IareaEx
    {
        #region 对象静态实例
        private static areaEx _instance;
        public static areaEx Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new areaEx();
                }
                return _instance;
            }
        }
        #endregion

        #region 1.基本操作
        /// <summary>
        ///  判断新增的区域是否合法，即区域名称不能重复
        /// </summary>
        /// <param name="fullname">区域名称</param>
        /// <param name="encode">区域编码</param>
        /// <returns>合法：true;不合法：false</returns>    
        public bool isLeagalAreaname(string fullname, string encode)
        {
            bool flag = false;
            int count = db.Queryable<area>().Where(d => d.encode == encode || d.fullname == fullname).Count();
            if (0 == count)
                flag = true;
            else
                flag = false;
            return flag;
        }
        /// <summary>
        ///判断是否为合法的区域实体
        /// </summary>
        /// <param name="parentid">上级id</param>
        /// <param name="layers">级别</param>
        /// <param name="encode">编码</param>
        /// <param name="fullname">区域名</param>
        /// <returns></returns>
        public bool isLegalEntity(string parentid, int layers, string encode, string fullname)
        {
            bool flag = false;
            int count = db.Queryable<area>().Where(d => d.parentid == parentid && d.layers == layers && d.encode == encode && d.fullname == fullname).Count();
            if (0 == count)
                flag = true;
            else
                flag = false;
            return flag;
        }
        /// <summary>
        /// 新增区域
        /// </summary>
        /// <param name="table"></param>
        /// <returns>成功：新增区域数据的主码；失败：-1</returns>
        public override int insert(area entity)
        {
            try
            {
                if (isLeagalAreaname(entity.fullname,entity.encode))
                {
                    return db.Insert<area>(entity).ObjToInt();
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
        /// 根据区域名称删除区域
        /// </summary>
        /// <param name="fullname"></param>
        /// <returns>成功：true;失败:false</returns>
        public bool delete(string fullname)
        {
            try
            {
                area r = getArea(fullname);
                db.Delete<area>(r as area);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        ///// <summary>
        ///// 更新区域
        ///// </summary>
        ///// <param name="table"></param>
        ///// <returns>成功：更新区域数据的主码；失败：-1</returns>
        //public override bool update(area entity)
        //{
        //    try
        //    {
        //        if (isLeagalAreaname(entity.fullname, entity.encode))
        //        {
        //            return db.Update<area>(entity).ObjToBool();
        //        }
        //        else
        //            return false;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
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
                db.Update<area>(dictionary, p => p.id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 根据区域的名称获得某区域
        /// </summary>
        /// <param name="fullname">区域名称</param>
        /// <returns>一个区域对象</returns>
        public area getArea(string fullname)
        {
            area table = db.Queryable<area>().Where(d => d.fullname == fullname).FirstOrDefault();
            return table;
        }

        /// <summary>
        /// 根据区域的id获得区域对象
        /// </summary>
        /// <param name="areaid">区域id</param>
        /// <returns>一个区域对象</returns>
        public List<area> getParentArea()
        {
            List<area> table = db.Queryable<area>().Where(d => d.parentid == null).ToList();
            List<area> Iareas = new List<area>();
            foreach (area a in table)
            {
                Iareas.Add(a as area);
            }
            return Iareas;
        }


        /// <summary>
        /// 根据区域名称获得此区域对象的id
        /// </summary>
        /// <param name="fullname">区域名称</param>
        /// <returns>区域id</returns>
        public int getAreaId(string fullname)
        {
            int id = 0;
            area table = db.Queryable<area>().Where(d => d.fullname == fullname).FirstOrDefault();
            if (table != null) id = table.id;
            return id;
        }
        /// <summary>
        /// 通过名称获取数据
        /// </summary>
        /// <param name="fullname">区域名称</param>
        /// <returns></returns>
        public List<area> getAreaList(string fullname)
        {
            List<area> AreaList = new List<area>();
            if (!string.IsNullOrEmpty(fullname))
                AreaList = db.Queryable<area>().Where(d => d.fullname == fullname).ToList();
            else
                AreaList = getEntityList();
            return AreaList;
        }

        /// <summary>
        /// 获取对应资源名的数据以及它的子数据与相关数据
        /// </summary>
        /// <param name="Areaname">资源名</param>
        /// <returns></returns>
        public List<area> getAreaListByAreaowner(int id)
        {
            List<area> Arealist = db.Queryable<area>().Where(d => Convert.ToInt32(d.parentid) == id).ToList();
            return Arealist;
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
                List<area> table = new List<area>();

                for (int i = 0; i < id.Length; i++)
                {
                    table.Add(getEntityById(int.Parse(id[i])));
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
        /// <param name="table">传入的参数为List<Area></param>
        /// <returns></returns>
        public void copyAndPasteInert(List<area> table)
        {
            int new_id = 0;

            foreach (area r in table)
            {
                new_id = db.Insert<area>(r).ObjToInt();
                if (new_id != 0)
                {
                    List<area> Arealist = getAreaListByAreaowner(r.id);

                    if (Arealist.Count > 0)
                    {
                        for (int i = 0; i < Arealist.Count; i++)
                        {
                            Arealist[i].parentid = new_id.ToString();
                        }
                        copyAndPasteInert(Arealist);
                    }
                }
            }
        }
        #endregion

        #region ACE
        #region 分页
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="page">分页信息</param>
        /// <param name="keyword">关键词</param>
        /// <returns>返回分页数据</returns>
        public PagedData<area> GetPageData(ACEPagination page, string keyword)
        {
            //查询相关的字段，根据页面的指定的字段
            PagedData<area> pageData = new PagedData<area>();
            pageData = TakePageData(page);
            try
            {
                if (string.IsNullOrEmpty(keyword))
                    pageData = TakePageData(page);
                else if (changeParse(keyword))
                    pageData.DataList = pageData.DataList.Where(a => a.encode.Contains(keyword)).ToList<area>();
                else
                    pageData.DataList = pageData.DataList.Where(a => a.fullname.Contains(keyword)).ToList<area>();
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
        /// <returns>List<area></returns>
        public List<area> getDynamicArea(string key, string value)
        {
            string sql = string.Format("select * from area where {0}={1}", key, value);
            List<area> alist = db.SqlQueryDynamic(sql);
            List<area> Ialist = new List<area>();
            foreach (area g in alist)
            {
                Ialist.Add(g as area);
            }
            return Ialist;
        }

        /// <summary>
        /// area表分页
        /// </summary>
        /// <param name="pageIndex">当前页的索引</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns></returns>
        public List<area> getPageArea(int pageIndex, int pageSize)
        {
            int pageCount = 0;
            List<area> alist = new List<area>();
            alist = db.Queryable<area>()
                .Where(c => c.id > 1).OrderBy(it => it.id)
                .ToPageList(pageIndex, pageSize, ref pageCount);//pageCount总条数
            List<area> Ialist = new List<area>();
            foreach (area g in alist)
            {
                Ialist.Add(g as area);
            }
            return Ialist;
        }
        /// <summary>
        /// 树形表格删除
        /// </summary>
        /// <param name="id"></param>
        public void deleteById(string id)
        {
            try
            {
                bool existsChildren = db.Queryable<area>().Where(a => a.parentid == id).Any();
                if (existsChildren)
                {
                    throw new Exception("删除失败！操作对象包含了下级数据");
                }
                db.Delete<area>(a => a.id.ToString() == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion


    }
}
