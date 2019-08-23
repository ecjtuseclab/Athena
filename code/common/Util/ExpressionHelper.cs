using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Athena.common.Util
{
    public enum SearchMethod
    {
        /// <summary>  
        /// 等于  
        /// </summary>  
        Equal = 0,

        /// <summary>  
        /// 小于  
        /// </summary>  
        LessThan = 1,

        /// <summary>  
        /// 大于  
        /// </summary>  
        GreaterThan = 2,

        /// <summary>  
        /// 小于等于  
        /// </summary>  
        LessThanOrEqual = 3,

        /// <summary>  
        /// 大于等于  
        /// </summary>  
        GreaterThanOrEqual = 4,

        /// <summary>  
        /// Like  
        /// </summary>  
        Like = 5,
        /// <summary>  
        /// 不等于  
        /// </summary>  
        NotEqual = 6
    }  

    public class ExpressionHelper
    {
        /// <summary>
        /// 将条件组合成一个Lambda表达式
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="field">类的字段</param>
        /// <param name="method">条件方法</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CreateExpression<T>(string field,SearchMethod method,object value) where T:class
        {
            ParameterExpression left = Expression.Parameter(typeof(T), "m");//m=>
            Expression right = null;
            switch (method)
            {
                case SearchMethod.Equal:
                    right = Expression.Equal(Expression.Convert(Expression.Property(left, typeof(T).GetProperty(field)), value.GetType())
                       , Expression.Constant(value));
                    break;
                case SearchMethod.GreaterThan:
                    right = Expression.GreaterThan(Expression.Convert(Expression.Property(left, typeof(T).GetProperty(field)), value.GetType())
                       , Expression.Constant(value));
                    break;
                case SearchMethod.GreaterThanOrEqual:
                    right = Expression.GreaterThanOrEqual(Expression.Convert(Expression.Property(left, typeof(T).GetProperty(field)), value.GetType())
                       , Expression.Constant(value));
                    break;
                case SearchMethod.LessThan:
                    var b = IsNullableType(typeof(T).GetProperty(field).PropertyType);
                    //right = Expression.LessThan(Expression.Property(left, typeof(T).GetProperty(field))
                    //   , Expression.Constant(value));
                    right = Expression.LessThan(Expression.Convert(Expression.Property(left, typeof(T).GetProperty(field)), value.GetType())
                       , Expression.Constant(value));
                    break;
                case SearchMethod.LessThanOrEqual:
                    right = Expression.LessThanOrEqual(Expression.Convert(Expression.Property(left, typeof(T).GetProperty(field)), value.GetType())
                        , Expression.Constant(value));
                    break;
                case SearchMethod.Like:
                    right = Expression.Call
                     (
                        Expression.Convert(Expression.Property(left, typeof(T).GetProperty(field)), value.GetType()),  //m.DataSourceName
                        typeof(string).GetMethod("Contains", new Type[] { typeof(string) }),// 反射使用.Contains()方法                         
                        Expression.Constant(value)           // .Contains(optionName)
                     );
                    break;
                case SearchMethod.NotEqual:
                    right = Expression.NotEqual(Expression.Convert(Expression.Property(left, typeof(T).GetProperty(field)), value.GetType())
                        , Expression.Constant(value));
                    break;
            }
            Expression<Func<T, bool>> finalExpression
             = Expression.Lambda<Func<T, bool>>(right, new ParameterExpression[] { left });
            return finalExpression;
        }

        public static Expression<Func<T, bool>> CreateExpression<T>(Expression<Func<T, object>> expr, SearchMethod method, object value) where T : class
        {
            var field = GetNameByExpress<T>(expr);
            return CreateExpression<T>(field,method,value);
        }

        private static string GetNameByExpress<T>(Expression<Func<T, object>> expr) where T : class
        {
            var pname = "";
            if (expr.Body is UnaryExpression)
            {
                var uy = expr.Body as UnaryExpression;
                pname = (uy.Operand as MemberExpression).Member.Name;
            }
            else
            {
                pname = (expr.Body as MemberExpression).Member.Name;
            }

            return pname;
        }

        private static bool IsNullableType(Type theType)
        {
            return (theType.IsGenericType && theType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)));
        }
    }
}

