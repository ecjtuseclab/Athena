using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smatrix.Crypto;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：对版权信息的处理（陈祚松）
//最后修改时间:2018-01-25
//修改日志：

namespace Athena.dal
{
    public class copyrightprotectionEx
    {
        private static string _copyright;
        public static string copyright
        {
            get { 
                if(string.IsNullOrEmpty(_copyright))
                {
                    _copyright=ConfigurationManager.AppSettings["copyright"];} return _copyright;
                }
            }

        private static  string _copyrightsign;
        public static string copyrightsign
        {
            get {
                if (string.IsNullOrEmpty(_copyrightsign))
                {
                    _copyrightsign = ConfigurationManager.AppSettings["copyrightsign"];
                }
                return _copyrightsign;
            }
        }

        private static string _copyrightpublickey;
        public static string copyrightpublickey {
            get {
                if (string.IsNullOrEmpty(_copyrightpublickey))
                {
                    _copyrightpublickey = ConfigurationManager.AppSettings["copyrightpublickey"];
                }
                return _copyrightpublickey;
            }
        }
        private  static TanSM2Ex _sm2;
        public static TanSM2Ex SM2 {
            get
            {
                if (_sm2 == null)
                {
                    _sm2 = new TanSM2Ex();
                }
                return _sm2;
            }
        }
        public static  bool verifysign()
        {
            if (string.IsNullOrEmpty(copyright))
            {
                throw new Exception("版权标识被移除！");
            }
            if (string.IsNullOrEmpty(copyrightsign))
            {
                throw new Exception("版权签名被移除！");
            }
            if (string.IsNullOrEmpty(copyrightpublickey))
            {
                throw new Exception("版权公钥被移除！");
            }
            if (!SM2.TanSM2Verify(copyright, copyrightpublickey, copyrightsign))
            {

                throw new  Exception("版权签名信息验证失败!");
            }
            return true;
        }
    }
}
