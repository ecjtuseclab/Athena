
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc.Html;
using System.Reflection;
using Athena.common.Util;
namespace System.Web.Mvc
{
    public abstract class WebViewPageBase<T> : WebViewPage<T>
    {
        protected WebViewPageBase()
            : base()
        { }
        public string Serialize(object obj)
        {

            return Json.ToJson(obj);
        }
        public IHtmlString RawSerialize(object obj)
        {
            IHtmlString ss = this.Raw(this.Serialize(obj));
            return this.Raw(this.Serialize(obj));
        }
        public IHtmlString Raw(string value)
        {
            return this.Html.Raw(value);
        }
        public IHtmlString Raw(object value)
        {
            return this.Html.Raw(value);
        }
        public MvcHtmlString Partial(string partialViewName)
        {
            return this.Html.Partial(partialViewName);
        }
        public string ContentUrl(string contentPath)/* 将虚拟（相对）路径转换为应用程序绝对路径。 */
        {
            return this.Url.Content(contentPath);
        }

        public IHtmlString RefStyle(string contentPath)
        {
            return this.Html.RefStyle(contentPath);
        }
        public IHtmlString RefScript(string contentPath)
        {
            return this.Html.RefScript(contentPath);
        }

        public HtmlString SelectOptions(IEnumerable<SelectOption> optionList, string defaultText = "--请选择--")
        {
            return this.SelectOptions((object)optionList, defaultText);
        }
        public HtmlString SelectOptions(object optionList, string defaultText = "--请选择--")
        {
            StringBuilder htmlBuilder = new StringBuilder();

            const string optionFormat = "<option value=\"{0}\">{1}</option>";
            if (!string.IsNullOrEmpty(defaultText))
            {
                htmlBuilder.AppendFormat(optionFormat, string.Empty, defaultText);
            }

            dynamic d = optionList;
            foreach (var option in d)
            {
                htmlBuilder.AppendFormat(optionFormat, option.Value, option.Text);
            }
            return new HtmlString(htmlBuilder.ToString());
        }

    }

    public class SelectOption
    {
        public SelectOption()
        {
        }
        public SelectOption(string value, string text)
        {
            this.Value = value;
            this.Text = text;
        }
        public string Value { get; set; }
        public string Text { get; set; }

        public static SelectOption Create(object instance, string valueProp = "Id", string textProp = "Name")
        {

            Dictionary<string, object> dic = Json.ToObject<Dictionary<string, object>>(Json.ToJson(instance));
            SelectOption option = new SelectOption();

            if (dic.Keys.Contains(valueProp) && dic.Keys.Contains(textProp))
            {
                option.Value = dic[valueProp] == null ? null : dic[valueProp].ToString();
                option.Text = dic[textProp] == null ? null : dic[textProp].ToString();
            }
            else
            {
                if (option.Value == null)
                {
                    int i = 0;
                    foreach (var k in dic.Keys)//获取第2个
                    {
                        if (i == 0)
                        {
                            option.Value = dic[k] == null ? null : dic[k].ToString();

                        }
                        if (i == 1)
                        {
                            option.Text = dic[k] == null ? null : dic[k].ToString();
                        }
                        i++;
                    }
                }
            }
            return option;
        }
        public static List<SelectOption> CreateList<T>(IEnumerable<T> instanceList, string valueProp = "Id", string textProp = "Name")
        {
            List<SelectOption> options = new List<SelectOption>();
            foreach (var instance in instanceList)
            {
                SelectOption option = SelectOption.Create(instance, valueProp, textProp);
                options.Add(option);
            }

            return options;
        }
    }

}
