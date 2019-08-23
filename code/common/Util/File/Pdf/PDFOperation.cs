using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Web;
using System.Data;

namespace Athena.common.Util
{
    /// <summary>
    ///项目名称：腾杰超轻量级开发框架
    ///开发者：张梦丽
    ///模块名称和描述：PDF文档操作类
    ///最后更新时间：2017/4/6
    ///日志说明：我把open方法的文件路径生成放到里面了，加了pdf生成方法
    /// </summary>

    //------------------------------------调用--------------------------------------------
    //PDFOperation pdf = new PDFOperation();
    //pdf.Open(new FileStream(path, FileMode.Create));
    //pdf.SetBaseFont(@"C:\Windows\Fonts\SIMHEI.TTF");
    //pdf.AddParagraph("测试文档（生成时间：" + DateTime.Now + "）", 15, 1, 20, 0, 0);
    //pdf.Close();
    //-------------------------------------------------------------------------------------
    public class PDFOperation
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public PDFOperation()
        {
            rect = PageSize.A4;
            document = new Document(rect);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type">页面大小(如"A4")</param>
        public PDFOperation(string type)
        {
            SetPageSize(type);
            
            document = new Document(rect);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type">页面大小(如"A4")</param>
        /// <param name="marginLeft">内容距左边框距离</param>
        /// <param name="marginRight">内容距右边框距离</param>
        /// <param name="marginTop">内容距上边框距离</param>
        /// <param name="marginBottom">内容距下边框距离</param>
        public PDFOperation(string type, float marginLeft, float marginRight, float marginTop, float marginBottom)
        {
            SetPageSize(type);
            document = new Document(rect, marginLeft, marginRight, marginTop, marginBottom);
        }
        #endregion

        #region 私有字段
        private Font font;
        private Rectangle rect;   //文档大小
        private Document document;//文档对象
        private BaseFont basefont;//字体
        #endregion

        #region 设置字体
        /// <summary>
        /// 设置字体
        /// </summary>
        public void SetBaseFont(string path)
        {
            basefont = BaseFont.CreateFont(path, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        }

        /// <summary>
        /// 设置字体
        /// </summary>
        /// <param name="size">字体大小</param>
        public void SetFont(float size)
        {
            font = new Font(basefont, size);
        }
        #endregion

        #region 设置页面大小
        /// <summary>
        /// 设置页面大小
        /// </summary>
        /// <param name="type">页面大小(如"A4")</param>
        public void SetPageSize(string type)
        {
            switch (type.Trim())
            {
                case "A4":
                    rect = PageSize.A4;
                    break;
                case "A8":
                    rect = PageSize.A8;
                    break;
            }
        }
        #endregion

        #region 实例化文档
        /// <summary>
        /// 实例化文档
        /// </summary>
        /// <param name="os">文档相关信息（如路径，打开方式等）</param>
        public void GetInstance(Stream os)
        {
            PdfWriter.GetInstance(document, os);
        }
        #endregion

        #region 打开文档对象
        /// <summary>
        /// 打开文档对象
        /// </summary>
        /// <param name="os">文档相关信息（如路径，打开方式等）</param>
        public void Open(string filename)
        {
            string strFileName = "../../DownFiles/pdf/" + filename + ".pdf";
            string servicePath = HttpContext.Current.Server.MapPath(strFileName);
            Stream os = new FileStream(servicePath, FileMode.Create);
            GetInstance(os);
            document.Open();
        }
        ///// <summary>
        ///// 打开文档对象
        ///// </summary>
        ///// <param name="os">文档相关信息（如路径，打开方式等）</param>
        //public void Open(Stream os)
        //{
        //    GetInstance(os);
        //    document.Open();
        //}
        #endregion

        #region 关闭打开的文档
        /// <summary>
        /// 关闭打开的文档
        /// </summary>
        public void Close()
        {
            document.Close();
        }
        #endregion

        #region 添加段落
        /// <summary>
        /// 添加段落
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="fontsize">字体大小</param>
        public void AddParagraph(string content, float fontsize)
        {
            SetFont(fontsize);
            Paragraph pra = new Paragraph(content, font);
            document.Add(pra);
        }

        /// <summary>
        /// 添加段落
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="fontsize">字体大小</param>
        /// <param name="Alignment">对齐方式（1为居中，0为居左，2为居右）</param>
        /// <param name="SpacingAfter">段后空行数（0为默认值）</param>
        /// <param name="SpacingBefore">段前空行数（0为默认值）</param>
        /// <param name="MultipliedLeading">行间距（0为默认值）</param>
        public void AddParagraph(string content, float fontsize, int Alignment, float SpacingAfter, float SpacingBefore, float MultipliedLeading)
        {
            SetFont(fontsize);
            Paragraph pra = new Paragraph(content, font);
            pra.Alignment = Alignment;
            if (SpacingAfter != 0)
            {
                pra.SpacingAfter = SpacingAfter;
            }
            if (SpacingBefore != 0)
            {
                pra.SpacingBefore = SpacingBefore;
            }
            if (MultipliedLeading != 0)
            {
                pra.MultipliedLeading = MultipliedLeading;
            }
            document.Add(pra);
        }
        #endregion

        #region 添加图片
        /// <summary>
        /// 添加图片
        /// </summary>
        /// <param name="path">图片路径</param>
        /// <param name="Alignment">对齐方式（1为居中，0为居左，2为居右）</param>
        /// <param name="newWidth">图片宽（0为默认值，如果宽度大于页宽将按比率缩放）</param>
        /// <param name="newHeight">图片高</param>
        public void AddImage(string path, int Alignment, float newWidth, float newHeight)
        {
            Image img = Image.GetInstance(path);
            img.Alignment = Alignment;
            if (newWidth != 0)
            {
                img.ScaleAbsolute(newWidth, newHeight);
            }
            else
            {
                if (img.Width > PageSize.A4.Width)
                {
                    img.ScaleAbsolute(rect.Width, img.Width * img.Height / rect.Height);
                }
            }
            document.Add(img);
        }
        #endregion

        #region 添加链接、点
        /// <summary>
        /// 添加链接
        /// </summary>
        /// <param name="Content">链接文字</param>
        /// <param name="FontSize">字体大小</param>
        /// <param name="Reference">链接地址</param>
        public void AddAnchorReference(string Content, float FontSize, string Reference)
        {
            SetFont(FontSize);
            Anchor auc = new Anchor(Content, font);
            auc.Reference = Reference;
            document.Add(auc);
        }

        /// <summary>
        /// 添加链接点
        /// </summary>
        /// <param name="Content">链接文字</param>
        /// <param name="FontSize">字体大小</param>
        /// <param name="Name">链接点名</param>
        public void AddAnchorName(string Content, float FontSize, string Name)
        {
            SetFont(FontSize);
            Anchor auc = new Anchor(Content, font);
            auc.Name = Name;
            document.Add(auc);
        }
        #endregion

        /// <summary>
        /// 转换GridView为PDF文档   --itextsharp
        /// </summary>
        /// <param name="sdr_Context">SqlDataReader</param>
        /// <param name="title">标题名称</param>
        /// <param name="col_Width">表格总宽度</param>
        /// <param name="arr_Width">每列的宽度</param>
        public string exp_Pdf(DataTable sdr_Context, string pdf_Filename, string title, float col_Width, float[] arr_Width)
        {
            //表头
            string fontpath_Title= "c:\\windows\\FONTS\\simsun.ttc,1";//表头字体
            float fontsize_Title=14;//表头大小
            int fontStyle_Title=Font.BOLD;//样式
            BaseColor fontColor_Title=BaseColor.RED;//颜色
            //列头
            string fontpath_Col= "c:\\windows\\FONTS\\simsun.ttc,1";
            float fontsize_Col=11;
            int fontStyle_Col= Font.NORMAL;
            BaseColor fontColor_Col=BaseColor.BLACK;
            //正文字体、大小、样式、颜色
            string FontPath="c:\\windows\\FONTS\\simsun.ttc,1";
            float FontSize=11;
            int fontStyle_Context=Font.NORMAL;
            BaseColor fontColor_Context=BaseColor.BLACK;

            //表格中的数据
            string data = string.Empty;
            //在服务器端保存PDF时的文件名
           
          //  string strFileName = "../../DownFiles/pdf/" + pdf_Filename + ".pdf";
            string downLoadPath = "DownFiles/pdf/" + pdf_Filename + ".pdf";
            try
            {
                //标题字体
                BaseFont basefont_Title = BaseFont.CreateFont(
                  fontpath_Title,
                  BaseFont.IDENTITY_H,
                  BaseFont.NOT_EMBEDDED);
                Font font_Title = new Font(basefont_Title, fontsize_Title, fontStyle_Title, fontColor_Title);
                //表格列字体
                BaseFont basefont_Col = BaseFont.CreateFont(
                  fontpath_Col,
                  BaseFont.IDENTITY_H,
                  BaseFont.NOT_EMBEDDED);
                Font font_Col = new Font(basefont_Col, fontsize_Col, fontStyle_Col, fontColor_Col);
                //正文字体
                BaseFont basefont_Context = BaseFont.CreateFont(
                  FontPath,
                  BaseFont.IDENTITY_H,
                  BaseFont.NOT_EMBEDDED);
                Font font_Context = new Font(basefont_Context, FontSize, fontStyle_Context, fontColor_Context);

                //打开目标文档对象
                document.Open();

                //添加标题
                Paragraph p_Title = new Paragraph(title, font_Title);
                p_Title.Alignment = Element.ALIGN_CENTER;
                document.Add(p_Title);

                //根据数据表内容创建一个PDF格式的表
                // PdfPTable table = new PdfPTable(sdr_Context.Columns.Count);
                PdfPTable table = new PdfPTable(sdr_Context.Columns.Count);
                table.TotalWidth = col_Width;//表格总宽度
                table.LockedWidth = true;//锁定宽度
                table.SetWidths(arr_Width);//设置每列宽度

                //构建列头
                //设置列头背景色
                table.DefaultCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                //设置列头文字水平、垂直居中
                table.DefaultCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.DefaultCell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;

                // 告诉程序这行是表头，这样页数大于1时程序会自动为你加上表头。
                table.HeaderRows = 1;

                if (sdr_Context.Rows.Count != 0)
                {
                    for (int i = 0; i < sdr_Context.Columns.Count; i++)
                    {
                        table.AddCell(new Phrase(sdr_Context.Columns[i].ColumnName, font_Col));
                    }
                    // 添加数据
                    //设置标题靠左居中
                    table.DefaultCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    // 设置表体背景色
                    table.DefaultCell.BackgroundColor = BaseColor.WHITE;
                    foreach (DataRow row in sdr_Context.Rows)
                    {
                        foreach (DataColumn column in sdr_Context.Columns)
                        {   /*创建一个单元格*/
                            //cell = new iTextSharp.text.pdf.PdfPCell(); // 创建单元格
                            //cell.Colspan = 1;
                            data = row[column.ColumnName].ToString();
                            //table_t = new Paragraph(data, fonttitle);
                            //cell.AddElement(table_t);
                            //table.AddCell(cell);

                            table.AddCell(new Phrase(data, font_Context));
                        }
                    }
                }

                //如果最后一个单元格数据过多，不要移动到下一页显示
                table.SplitLate = false;
                //
                table.SplitRows = false;
                //在目标文档中添加转化后的表数据
                document.Add(table);
                //table.DefaultCell.Width = 0.5F;
               
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //关闭目标文件
                //document.Close();
              
              
            }
            return downLoadPath;
        }
   
    }
}