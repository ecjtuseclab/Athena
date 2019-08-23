using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace System.Web.Mvc.Html
{
    public static class DialogExtensions
    {
        public static IDisposable Dialog(this HtmlHelper htmlHelper, int width)
        {
            DialogSetting set = new DialogSetting(width);
            return Dialog(htmlHelper, set);
        }
        public static IDisposable Dialog(this HtmlHelper htmlHelper, string withModel, string id, int width)
        {
            DialogSetting set = new DialogSetting(withModel, id, width);
            return Dialog(htmlHelper, set);
        }
        public static IDisposable Dialog(this HtmlHelper htmlHelper, DialogSetting set = null)
        {
            HtmlDialog dialog = new HtmlDialog(htmlHelper.ViewContext, set);
            dialog.BeginDialog();
            return dialog;
        }


        public static HtmlString BeginDialog(this HtmlHelper htmlHelper, int width)
        {
            DialogSetting set = new DialogSetting(width);
            return BeginDialog(htmlHelper, set);
        }
        public static HtmlString BeginDialog(this HtmlHelper htmlHelper, string withModel, string id, int width)
        {
            DialogSetting set = new DialogSetting(withModel, id, width);
            return BeginDialog(htmlHelper, set);
        }
        public static HtmlString BeginDialog(this HtmlHelper htmlHelper, DialogSetting set = null)
        {
            StringBuilder htmlBuilder = new StringBuilder();

            if (set == null)
                set = DialogSetting.DefaultSetting;

            htmlBuilder.CAppend("<div data-bind=\"with:{0}\">", set.WithModel).AppendLine();
            htmlBuilder.CAppend("    <div data-bind=\"display:IsShow()\" class=\"modal fade\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"myModalLabel\" aria-hidden=\"true\" id=\"{0}\" data-backdrop=\"false\" data-keyboard=\"true\">", set.Id).AppendLine();
            htmlBuilder.CAppend("        <div class=\"modal-dialog\" style=\"width:{0}px;\">", set.Width.ToString()).AppendLine();
            htmlBuilder.CAppend("            <div class=\"modal-content\" style=\"width:{0}px;\">", set.Width.ToString()).AppendLine();

            /* header */
            htmlBuilder.CAppend("                <div class=\"modal-header\">").AppendLine();
            htmlBuilder.CAppend("                    <label data-bind=\"text:Title\"></label>").AppendLine();
            htmlBuilder.CAppend("                    <button type=\"button\" class=\"close\" data-dismiss=\"modal\" data-bind=\"click:function(){ IsShow(false);}\"><span aria-hidden=\"true\">&times;</span></button>").AppendLine();
            htmlBuilder.CAppend("                </div>").AppendLine();

            return new HtmlString(htmlBuilder.ToString());
        }
        public static HtmlString EndDialog(this HtmlHelper htmlHelper)
        {
            StringBuilder htmlBuilder = new StringBuilder();

            htmlBuilder.CAppend("            </div>").AppendLine();
            htmlBuilder.CAppend("        </div>").AppendLine();
            htmlBuilder.CAppend("    </div>").AppendLine();
            htmlBuilder.CAppend("</div>").AppendLine();

            return new HtmlString(htmlBuilder.ToString());
        }

        internal static StringBuilder CAppend(this StringBuilder sb, string value)
        {
            sb.Append(value);
            return sb;
        }
        internal static StringBuilder CAppend(this StringBuilder sb, string format, object arg0)
        {
            sb.AppendFormat(format, arg0);
            return sb;
        }
    }



    public class HtmlDialog : IDisposable
    {
        readonly ViewContext _viewContext;
        DialogSetting _set;
        bool _disposed;

        public HtmlDialog(ViewContext viewContext, DialogSetting set)
        {
            if (viewContext == null)
            {
                throw new ArgumentNullException("viewContext");
            }

            if (set == null)
                set = DialogSetting.DefaultSetting;

            this._viewContext = viewContext;
            this._set = set;
        }

        public void Dispose()
        {
            if (this._disposed)
            {
                return;
            }

            this.EndDialog();
            this._disposed = true;
        }

        public void BeginDialog()
        {
            StringBuilder htmlBuilder = new StringBuilder();

            htmlBuilder.CAppend("<div data-bind=\"with:{0}\">", this._set.WithModel);
            htmlBuilder.AppendLine();
            htmlBuilder.CAppend("    <div data-bind=\"display:IsShow()\" class=\"modal fade\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"myModalLabel\" aria-hidden=\"true\" id=\"{0}\" data-backdrop=\"false\" data-keyboard=\"true\">", this._set.Id);
            htmlBuilder.AppendLine();
            htmlBuilder.CAppend("        <div class=\"modal-dialog\" style=\"width:{0}px;\">", this._set.Width.ToString());
            htmlBuilder.AppendLine();
            htmlBuilder.CAppend("            <div class=\"modal-content\" style=\"width:{0}px;\">", this._set.Width.ToString());
            htmlBuilder.AppendLine();
            htmlBuilder.CAppend("                <div class=\"modal-header\">");
            htmlBuilder.AppendLine();
            htmlBuilder.CAppend("                    <label data-bind=\"text:Title\"></label>");
            htmlBuilder.AppendLine();
            htmlBuilder.CAppend("                    <button type=\"button\" class=\"close\" data-dismiss=\"modal\" data-bind=\"click:function(){ IsShow(false);}\"><span aria-hidden=\"true\">&times;</span></button>");
            htmlBuilder.AppendLine();
            htmlBuilder.CAppend("                </div>");
            htmlBuilder.AppendLine();
            htmlBuilder.CAppend("                <div class=\"modal-body\">");
            htmlBuilder.AppendLine();

            this._viewContext.Writer.Write(htmlBuilder.ToString());
        }
        public void EndDialog()
        {
            StringBuilder htmlBuilder = new StringBuilder();

            htmlBuilder.CAppend("                </div>");
            htmlBuilder.AppendLine();
            htmlBuilder.CAppend("                <div class=\"modal-footer\">");
            htmlBuilder.AppendLine();
            htmlBuilder.CAppend("                    <button type=\"button\" class=\"btn btn-default\" data-bind=\"click:Save,visible:ShowSaveButton\">保存</button>");
            htmlBuilder.AppendLine();
            htmlBuilder.CAppend("                    <button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\" data-bind=\"click:function(){ IsShow(false);}\">关闭</button>");
            htmlBuilder.AppendLine();
            htmlBuilder.CAppend("                </div>");
            htmlBuilder.AppendLine();
            htmlBuilder.CAppend("            </div>");
            htmlBuilder.AppendLine();

            htmlBuilder.CAppend("        </div>");
            htmlBuilder.AppendLine();
            htmlBuilder.CAppend("    </div>");
            htmlBuilder.AppendLine();
            htmlBuilder.CAppend("</div>");
            htmlBuilder.AppendLine();

            this._viewContext.Writer.Write(htmlBuilder.ToString());
        }
    }

    public class DialogSetting
    {
        string _withModel = "Dialog";
        string _id = "Dialog";
        int _width = 800;

        public static readonly DialogSetting DefaultSetting = new DialogSetting();

        public DialogSetting()
        {
        }
        public DialogSetting(int width)
        {
            this._width = width;
        }
        public DialogSetting(string withModel, string id)
        {
            this._withModel = withModel;
            this._id = id;
        }
        public DialogSetting(string withModel, string id, int width)
        {
            this._withModel = withModel;
            this._id = id;
            this._width = width;
        }

        public string WithModel { get { return this._withModel; } set { this._withModel = value; } }
        public string Id { get { return this._id; } set { this._id = value; } }
        public int Width { get { return this._width; } set { this._width = value; } }
    }
}