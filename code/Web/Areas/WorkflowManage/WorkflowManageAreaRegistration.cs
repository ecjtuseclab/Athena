using System.Web.Mvc;

namespace Web.Areas.WorkflowManage
{
    public class WorkflowManageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "WorkflowManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "WorkflowManage_default",
                "WorkflowManage/{controller}/{action}/{id}",
                new { area = this.AreaName, controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "Web.Areas." + this.AreaName + ".Controllers" }
                // new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
