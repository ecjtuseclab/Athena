using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.common.Util
{
    public class SmsHelper
    {
        #region 短信平台JM
        /// <summary>
        /// 发送短信【JM短信平台】
        /// </summary>
        /// <param name="smsmodel">短信模板</param>
        /// <returns>
        ///0提交成功；  
        ///101 无此用户
        ///102 密码错
        ///103 提交过快（提交速度超过流速限制）
        ///104 系统忙（因平台侧原因，暂时无法处理提交的短信）
        ///105 敏感短信（短信内容包含敏感词）
        ///106 消息长度错（>536或<=0）
        ///107 包含错误的手机号码
        ///108 手机号码个数错（群发>50000或<=0;单发>200或<=0）
        ///109 无发送额度（该用户可用短信数已使用完）
        ///110 不在发送时间内
        ///111 超出该账户当月发送额度限制
        ///112 无此产品，用户没有订购该产品
        ///113 extno格式错（非数字或者长度不对）
        ///115 自动审核驳回
        ///116 签名不合法，未带签名（用户必须带签名的前提下）
        ///117 IP地址认证错,请求调用的IP地址不是系统登记的IP地址
        ///118 用户没有相应的发送权限
        ///119 用户已过期
        ///  </returns>
        public static string SendSmsByJM(SmsModel smsmodel)
        {
            try
            {
                string _needstatus = string.IsNullOrEmpty(smsmodel.needstatus) ? "false" : smsmodel.needstatus;
                string _product = string.IsNullOrEmpty(smsmodel.product) ? "" : ("&product=" + smsmodel.product);
                string param = "account=" + smsmodel.account + "&pswd=" + smsmodel.pswd + "&mobile=" + smsmodel.mobile + "&msg=" + smsmodel.msg + "&needstatus=" + _needstatus + _product;
                string res = HttpMethods.HttpPost(smsmodel.url, param);
                return res;
                //return res.Split(',')[1].Split('\n')[0];
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }

    #region 数据传输模型
    /// <summary>
    /// 短息发送接口模型
    /// </summary>
    public class SmsModel
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string account{get;set;}
        /// <summary>
        /// 密码
        /// </summary>
        public string pswd{get;set;}
        /// <summary>
        /// 短信接口地址
        /// </summary>
        public string url{get;set;}
        /// <summary>
        /// 电话
        /// </summary>
        public string mobile{get;set;}
        /// <summary>
        /// 短信内容
        /// </summary>
        public string msg{get;set;}
        /// <summary>
        /// 是否需要状态报告，默认为false
        /// </summary>
        public string needstatus{get;set;}
        /// <summary>
        /// 产品id（可不填）
        /// </summary>
        public string product { get; set; }

    }
    #endregion
}
