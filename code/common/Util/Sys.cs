using System;
using System.Diagnostics;
using System.IO;
using System.Web;

namespace Athena.common.Util
{
    /// <summary>
    /// 系统操作
    /// </summary>
    public class Sys
    {
        #region Line(换行符)

        /// <summary>
        /// 换行符
        /// </summary>
        public static string Line
        {
            get
            {
                return Environment.NewLine;
            }
        }

        #endregion

        #region Guid

        /// <summary>
        /// Guid
        /// </summary>
        public static Guid Guid
        {
            get { return Guid.NewGuid(); }
        }

        #endregion

        #region GetType(获取类型)

        /// <summary>
        /// 获取类型,对可空类型进行处理
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public static Type GetType<T>()
        {
            return Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
        }

        #endregion

        #region GetPhysicalPath(获取物理路径)

        /// <summary>
        /// 获取物理路径
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        public static string GetPhysicalPath(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                return string.Empty;
            if (HttpContext.Current == null)
            {
                if (relativePath.StartsWith("~"))
                    relativePath = relativePath.Remove(0, 2);
                return Path.GetFullPath(relativePath);
            }
            if (relativePath.StartsWith("~"))
                return HttpContext.Current.Server.MapPath(relativePath);
            if (relativePath.StartsWith("/") || relativePath.StartsWith("\\"))
                return HttpContext.Current.Server.MapPath("~" + relativePath);
            return HttpContext.Current.Server.MapPath("~/" + relativePath);
        }

        #endregion

        #region StartProcess(启动进程)

        /// <summary>
        /// 启动进程
        /// </summary>
        /// <param name="processName">进程名称</param>
        public static void StartProcess(string processName)
        {
            Process.Start(processName);
        }

        #endregion
    }
}
