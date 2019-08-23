using System.Web.Mvc;

namespace Web.Areas.ArticleManage
{
    public class ArticleManageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ArticleManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ArticleManage_default",
                "ArticleManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
