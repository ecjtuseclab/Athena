using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Athena.common.Util
{
    /// <summary>
    /// 异常信息封装类
    /// </summary>
    public class ExceptionMessage
    {
        #region 构造函数

        /// <summary>
        ///以自定义用户信息和异常对象实例化一个异常信息对象
        /// </summary>
        /// <param name="e"> 异常对象 </param>
        /// <param name="userMessage"> 自定义用户信息 </param>
        /// <param name="isHideStackTrace"> 是否隐藏异常堆栈信息 </param>
        public ExceptionMessage(Exception e, string userMessage = null, bool isHideStackTrace = false)
        {
            UserMessage = string.IsNullOrEmpty(userMessage) ? e.Message : userMessage;

            StringBuilder sb = new StringBuilder();
            ExMessage = string.Empty;
            int count = 0;
            string appString = "";
            while (e != null)
            {
                if (count > 0)
                {
                    appString += "　";
                }
                ExMessage = e.Message;
                sb.AppendLine(appString + "异常消息：" + e.Message);
                sb.AppendLine(appString + "异常类型：" + e.GetType().FullName);
                sb.AppendLine(appString + "异常方法：" + (e.TargetSite == null ? null : e.TargetSite.Name));
                sb.AppendLine(appString + "异常源：" + e.Source);
                if (!isHideStackTrace && e.StackTrace != null)
                {
                    sb.AppendLine(appString + "异常堆栈：" + e.StackTrace);
                }
                if (e.InnerException != null)
                {
                    sb.AppendLine(appString + "内部异常：");
                    count++;
                }
                e = e.InnerException;
            }
            ErrorDetails = sb.ToString();
            sb.Clear();
        }

        #region 属性

        /// <summary>
        ///用户信息，用于报告给用户的异常消息
        /// </summary>
        public string UserMessage { get; set; }
        /// <summary>
        ///直接的Exception异常信息，即e.Message属性值
        /// </summary>
        public string ExMessage { get; private set; }
        /// <summary>
        ///异常输出的详细描述，包含异常消息，规模信息，异常类型，异常源，引发异常的方法及内部异常信息
        /// </summary>
        public string ErrorDetails { get; private set; }
        #endregion

        #endregion

        /// <summary>
        /// 由错误码返回指定的自定义SqlException异常信息
        /// </summary>
        /// <param name="number"> </param>
        /// <returns> </returns>
        public static string GetSqlExceptionMessage(int number)
        {
            string msg = string.Empty;
            switch (number)
            {
                case 2:
                    msg = "连接数据库超时，请检查网络连接或者数据库服务器是否正常。";
                    break;
                case 17:
                    msg = "SqlServer服务不存在或拒绝访问。";
                    break;
                case 17142:
                    msg = "SqlServer服务已暂停，不能提供数据服务。";
                    break;
                case 2812:
                    msg = "指定存储过程不存在。";
                    break;
                case 208:
                    msg = "指定名称的表不存在。";
                    break;
                case 4060: //数据库无效。
                    msg = "所连接的数据库无效。";
                    break;
                case 18456: //登录失败
                    msg = "使用设定的用户名与密码登录数据库失败。";
                    break;
                case 547:
                    msg = "外键约束，无法保存数据的变更。";
                    break;
                case 2627:
                    msg = "主键重复，无法插入数据。";
                    break;
                case 2601:
                    msg = "未知错误。";
                    break;
            }
            return msg;
        }


    }
}
