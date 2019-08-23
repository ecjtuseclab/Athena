using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Athena.model; 
using Athena.basedal;
using Athena.Idal;
using Athena.dal;
using Athena.tool.Code;
using Athena.common.Util.WebControl;
using Athena.tool.ACEPagination;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 张婷婷 王露 陈祚松 艾美珍 张梦丽 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：文章控制器，添加ACE相关方法
//最后修改时间：2018-01-26
//修改日志：
//2018-1-24 文章控制器，添加ACE相关方法（张婷婷）

namespace Web.Areas.ArticleManage.Controllers
{    
    public class ArticleController : WebController
    {
        public ArticleController()
        {
            #region BootstrapTemplate
            this.noauth_actions.Add("ArticleGetGridJson");
            this.noauth_actions.Add("ArticleDetails");
            this.noauth_actions.Add("ArticleGetFormJson");
            this.noauth_actions.Add("ArticleSubmitForm");
            this.noauth_actions.Add("ArticleDeleteForm");
            this.noauth_actions.Add("ArticlecopyAndPasteForm");
            this.noauth_actions.Add("ArticleForm");
            #endregion

            #region ACETemplate
            this.noauth_actions.Add("ArticleIndex");
            this.noauth_actions.Add("ArticleGetModels");
            this.noauth_actions.Add("ArticleAdd");
            this.noauth_actions.Add("ArticleUpdate");
            this.noauth_actions.Add("ArticleDelete");
            #endregion
        }

        public override ActionResult Index()
        {
            return View();
        }
        public override ActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// 获取表格的json数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            pagination.records =IdalCommon.IarticleEx.getArticleList(keyword).Count;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("title", keyword);
            List<article> articlelist = IdalCommon.IarticleEx.getPaginationEntityList(pagination.page, pagination.rows, dictionary);
            var data = new
            {
                rows = articlelist,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            string str = data.ToJson();
            return Content(data.ToJson());
        }
        //获取搜索结果
        public ActionResult GetSearchGridJson(Pagination pagination, string key)
        {
            try
            {
                pagination.records = IdalCommon.IarticleEx.getArticleList(key, 0).Count;//第二个参数0表示SortID为空
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("title", key);
                List<article> articlelist = IdalCommon.IarticleEx.getPaginationEntityList(pagination.page, pagination.rows, dictionary).OrderByDescending(r => r.id).ToList();
                var data = new
                {
                    rows = articlelist,
                    total = pagination.total,
                    page = pagination.page,
                    records = pagination.records
                };
                return Content(data.ToJson());
            }
            catch (Exception ex)
            {
                return Content(ex.ToJson());
            }
        }

        /// <summary>
        /// 获取表单提交的数据的json格式
        /// </summary>
        /// <param name="keyValue">分组对象的主键</param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            if (keyValue == "undefined")
            {
                keyValue=null;
            }
            int id = Convert.ToInt32(keyValue);
            var data = IdalCommon.IarticleEx.getArticle(id);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 提交表单数据
        /// </summary>
        /// <param name="article">文章对象</param>
        /// <param name="keyValue">文章对象的主键</param>
        /// <returns></returns>
        
        [ValidateInput(false)]
        public ActionResult SubmitForm(article article, string keyValue)
        {
            keyValue = !string.IsNullOrEmpty(ctx.Request["keyValue"]) ? ctx.Request["keyValue"] : article.id.ToString();
            if (Convert.ToInt32(keyValue) > 0)
            {
                IdalCommon.IarticleEx.SubmitForm(article, keyValue);
            }
            else
            {
                IdalCommon.IarticleEx.SubmitForm(article, "");
            }
            //IdalCommon.IarticleEx.SubmitForm(article, keyValue);
            return Success("操作成功。");
        } 
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue">分组对象的主键</param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            int key = Convert.ToInt32(keyValue);
            IdalCommon.IarticleEx.delete(key);
            return Success("删除成功。");
        }
        /// <summary>
        /// 复制粘贴
        /// </summary>
        /// <param name="keyValue">分组对象的主键</param>
        /// <returns></returns>
        public ActionResult copyAndPasteForm(string keyValue)
        {
            IdalCommon.IarticleEx.copyAndPaste(keyValue);
            return Success("粘贴成功。");
        }

        
        #region ACE

        /// <summary>
        /// 查看详情
        /// </summary>
        /// <returns></returns>
        public override ActionResult Details()
        {
            return View();
        } 
        [HttpGet]
        [ValidateInput(false)]
        public ActionResult GetModels(ACEPagination pagination, string keyword, string filters)
        {
            PagedData<article> pagedData = new PagedData<article>();
            if (!string.IsNullOrEmpty(filters))
            {
                filters = SqlFilter.SqlFilters(filters);   //过滤特殊字符
                pagedData = IdalCommon.IarticleEx.mutiplySearch(filters, pagination, "article");
            }           
            else
            {
                pagedData = IdalCommon.IarticleEx.GetPageData(pagination, keyword);
            }
            return this.SuccessData(pagedData);
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="page">分页信息</param>
        /// <param name="keyword">关键词</param>
        /// <returns>返回分页数据</returns>
        [ValidateInput(false)]
        public ActionResult Add(article a)
        {
            a.inserttime = DateTime.Now;
            a.edittime = DateTime.Now;
            IdalCommon.IarticleEx.insert(a);
            return this.AddSuccessMsg();
        }
        //[HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(article a)
        {
            IdalCommon.IarticleEx.update(a);
            return this.UpdateSuccessMsg();
        }

        //[HttpPost]
        public ActionResult Delete(article a)
        {
            IdalCommon.IarticleEx.delete(a);
            return this.DeleteSuccessMsg();
        }
        #endregion
    }
}
