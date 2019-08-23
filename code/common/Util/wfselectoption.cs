using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.common.Util
{
   public class wfselectoption
    {
        public wfselectoption() { ;}
        public object value { get; set; }
        public object name { get; set; }
        public wfselectoption(object value, string name)
        {
            this.value = value;
            this.name = name;
        }
        public static wfselectoption Create(object instance, string valueProp = "Id", string textProp = "Name")
        {

            Dictionary<string, object> dic = Json.ToObject<Dictionary<string, object>>(Json.ToJson(instance));
            wfselectoption option = new wfselectoption();

            if (dic.Keys.Contains(valueProp) && dic.Keys.Contains(textProp))
            {
                option.value = dic[valueProp] == null ? null : dic[valueProp].ToString();
                option.name = dic[textProp] == null ? null : dic[textProp].ToString();
            }
            else
            {
                if (option.value == null)
                {
                    int i = 0;
                    foreach (var k in dic.Keys)//获取第2个
                    {
                        if (i == 0)
                        {
                            option.value = dic[k] == null ? null : dic[k].ToString();

                        }
                        if (i == 1)
                        {
                            option.name = dic[k] == null ? null : dic[k].ToString();
                        }
                        i++;
                    }
                }
            }
            return option;
        }
        public static List<wfselectoption> CreateList<T>(IEnumerable<T> instanceList, string valueProp = "Id", string textProp = "Name")
        {
            List<wfselectoption> options = new List<wfselectoption>();
            foreach (var instance in instanceList)
            {
                wfselectoption option = wfselectoption.Create(instance, valueProp, textProp);
                options.Add(option);
            }

            return options;
        }
    }
}
