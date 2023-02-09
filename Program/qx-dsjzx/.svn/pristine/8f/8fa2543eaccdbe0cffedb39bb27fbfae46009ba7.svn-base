using Microsoft.AspNetCore.Http;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramsNetCore.Common
{
    public static class PublicFunction
    {
        /// <summary>
        /// 获取excel文件内类容并转换成列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="file"></param>
        /// <returns></returns>
        public static List<T> ImportExcel<T>(IFormFile file) where T : new()
        {
            List<T> _xlist = new List<T>();
            Type _xtype = typeof(T);
            var _xproperties = _xtype.GetProperties();
            try
            {
                string _xfile = Path.GetExtension(file.FileName).ToLower();
                MemoryStream _ms = new MemoryStream();
                file.CopyTo(_ms);
                _ms.Seek(0, SeekOrigin.Begin);
                IWorkbook _xbook;
                if (_xfile == ".xlsx")
                {
                    _xbook = new XSSFWorkbook(_ms);
                }
                else if (_xfile == ".xls")
                {
                    _xbook = new HSSFWorkbook(_ms);
                }
                else
                {
                    _xbook = null;
                }
                ISheet _xsheet = _xbook.GetSheetAt(0);

                int _xCountRow = _xsheet.LastRowNum + 1;//获取总行数

                if (_xCountRow - 1 == 0)
                {
                    return _xlist;
                }

                for (int i = 1; i < _xCountRow; i++)
                {
                    //获取第i行的数据
                    var _xrow = _xsheet.GetRow(i);
                    if (_xrow != null)
                    {
                        T xmodel = Activator.CreateInstance<T>();
                        //循环的验证单元格中的数据
                        for (int j = 0; j < _xproperties.Length; j++)
                        {
                            var _xproperty = _xproperties[j];
                            _xproperty.SetValue(xmodel, _xrow.GetCell(j) != null ? _xrow.GetCell(j).ToString() : string.Empty, null);
                        }
                        _xlist.Add(xmodel);
                    }
                }
                return _xlist;
            }
            catch (Exception e)
            {
                return _xlist;
            }
        }

        /// <summary>
        /// 导入EXCEL数据  
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>

        public static async Task<DataTable> ImportToDataTable(IFormFile excelfile)
        {
            var result = await Task.Factory.StartNew(() => {

                DataTable dt = new DataTable();
                var filePath = excelfile.FileName.Split('.');
                if (filePath[1].ToLower() == ".xls")
                {//.xls
                    #region .xls文件处理:HSSFWorkbook

                    HSSFWorkbook hssfworkbook;
                    try
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            excelfile.CopyTo(ms);
                            ms.Seek(0, SeekOrigin.Begin);
                            hssfworkbook = new HSSFWorkbook(ms);
                        }

                        ISheet sheet = hssfworkbook.GetSheetAt(0);
                        System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
                        var maxRowIndex = 0;

                        while (rows.MoveNext())
                        {
                            IRow row = (HSSFRow)rows.Current;
                            var r = row.Cells.Select(p => p.StringCellValue).ToList();
                            var _result = row.Cells.Any(t => !string.IsNullOrEmpty(t.StringCellValue));
                            if (_result)
                            {
                                maxRowIndex = row.RowNum;
                                break;
                            }
                        }

                        HSSFRow headerRow = (HSSFRow)sheet.GetRow(maxRowIndex);//取第二行

                        var _ColumnIndex = new List<int>();

                        //一行最后一个方格的编号 即总的列数 
                        for (int j = 0; j < (headerRow.LastCellNum); j++)
                        {
                            //SET EVERY COLUMN NAME
                            HSSFCell cell = (HSSFCell)headerRow.GetCell(j);

                            if (!string.IsNullOrEmpty(cell.StringCellValue))
                            {
                                dt.Columns.Add(cell.ToString());
                                _ColumnIndex.Add(cell.ColumnIndex);
                            }
                        }

                        while (rows.MoveNext())
                        {
                            IRow row = (HSSFRow)rows.Current;
                            DataRow dr = dt.NewRow();

                            if (row.RowNum <= headerRow.RowNum) continue;

                            for (int i = 0; i < _ColumnIndex.Count; i++)
                            {
                                if (i >= dt.Columns.Count)
                                {
                                    break;
                                }
                                ICell cell = row.GetCell(_ColumnIndex[i]);

                                if ((i == 0) && cell == null)//每行第一个cell为空,break
                                {
                                    break;
                                }

                                if (cell == null)
                                {
                                    dr[i] = null;
                                }
                                else
                                {
                                    switch (cell.CellType)
                                    {
                                        case CellType.String:
                                            dr[i] = cell.StringCellValue;
                                            break;
                                        case CellType.Numeric:

                                            if (DateUtil.IsCellDateFormatted(cell))
                                            {
                                                dr[i] = cell.DateCellValue;
                                            }
                                            else
                                            {
                                                dr[i] = cell.NumericCellValue;
                                            }
                                            break;
                                        default:
                                            dr[i] = null;
                                            break;
                                    }
                                }
                            }

                            dt.Rows.Add(dr);

                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message, e);
                    }

                    #endregion
                }
                else
                {//.xlsx
                    #region .xlsx文件处理:XSSFWorkbook

                    XSSFWorkbook hssfworkbook;
                    try
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            excelfile.CopyTo(ms);
                            ms.Seek(0, SeekOrigin.Begin);
                            hssfworkbook = new XSSFWorkbook(ms);
                        }
                        ISheet sheet = hssfworkbook.GetSheetAt(0);
                        // ISheet sheet = hssfworkbook.GetSheet(sheetName);
                        System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

                        var maxRowIndex = 0;

                        while (rows.MoveNext())
                        {
                            IRow row = (XSSFRow)rows.Current;
                            var _result = row.Cells.Any(t => !string.IsNullOrEmpty(t.StringCellValue));
                            if (_result)
                            {
                                maxRowIndex = row.RowNum;
                                break;
                            }
                        }
                        XSSFRow headerRow = (XSSFRow)sheet.GetRow(maxRowIndex);//取第二行
                        var _ColumnIndex = new List<int>();

                        //一行最后一个方格的编号 即总的列数 
                        for (int j = 0; j < (headerRow.LastCellNum); j++)
                        {
                            //SET EVERY COLUMN NAME
                            XSSFCell cell = (XSSFCell)headerRow.GetCell(j);

                            if (!string.IsNullOrEmpty(cell.StringCellValue))
                            {
                                dt.Columns.Add(cell.ToString());
                                _ColumnIndex.Add(cell.ColumnIndex);
                            }
                        }

                        while (rows.MoveNext())
                        {
                            IRow row = (XSSFRow)rows.Current;
                            DataRow dr = dt.NewRow();

                            if (row.RowNum == 0) continue;

                            for (int i = 0; i < _ColumnIndex.Count; i++)
                            {
                                if (i >= dt.Columns.Count)
                                {
                                    break;
                                }

                                ICell cell = row.GetCell(_ColumnIndex[i]);

                                if ((i == 0) && (cell == null))//每行第一个cell为空,break
                                {
                                    break;
                                }

                                if (cell == null)
                                {
                                    dr[i] = null;
                                }
                                else
                                {
                                    switch (cell.CellType)
                                    {
                                        case CellType.String:
                                            dr[i] = cell.StringCellValue;
                                            break;
                                        case CellType.Numeric:

                                            if (DateUtil.IsCellDateFormatted(cell))
                                            {
                                                dr[i] = cell.DateCellValue;
                                            }
                                            else
                                            {
                                                dr[i] = cell.NumericCellValue;
                                            }
                                            break;
                                        default:
                                            dr[i] = null;
                                            break;
                                    }
                                }
                            }
                            dt.Rows.Add(dr);
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message, e);
                    }
                    #endregion
                }
                return dt;

            });
            return result;
        }

        public static async Task<bool> ExportExcelAsync(string fileName, DataTable dtSource, string myDateFormat = "yyyy-MM")
        {
            var result = await await Task.Factory.StartNew(async () =>
            {
                try
                {
                    using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                    {
                        XSSFWorkbook wb = new XSSFWorkbook();
                        ISheet sheet = wb.CreateSheet("Sheet1");
                        IRow rowHeader = sheet.CreateRow(0);
                        for (int i = 0; i < dtSource.Columns.Count; i++)
                        {
                            DataColumn column = dtSource.Columns[i];
                            rowHeader.CreateCell(i).SetCellValue(column.Caption);
                        }

                        short decimalformat = HSSFDataFormat.GetBuiltinFormat("0.00");
                        short dateformat = wb.CreateDataFormat().GetFormat(myDateFormat);
                        ICellStyle styleDecimal = wb.CreateCellStyle();
                        styleDecimal.DataFormat = decimalformat;
                        ICellStyle styleDate = wb.CreateCellStyle();
                        styleDate.DataFormat = dateformat;
                        ICellStyle styleNormal = wb.CreateCellStyle();

                        for (int i = 0; i < dtSource.Rows.Count; i++)
                        {
                            DataRow dr = dtSource.Rows[i];
                            IRow ir = sheet.CreateRow(i + 1);
                            for (int j = 0; j < dr.ItemArray.Length; j++)
                            {
                                ICell icell = ir.CreateCell(j);
                                object cellValue = dr[j];
                                Type type = cellValue.GetType();
                                if (type == typeof(decimal) || type == typeof(double) || type == typeof(int) || type == typeof(float))
                                {
                                    icell.SetCellValue(Convert.ToDouble(cellValue));
                                    icell.CellStyle = styleDecimal;
                                }
                                else if (type == typeof(DateTime))
                                {
                                    icell.SetCellValue(Convert.ToDateTime(cellValue).ToString(myDateFormat));
                                    icell.CellStyle = styleNormal;
                                }
                                else if (type == typeof(bool))
                                {
                                    icell.SetCellValue(Convert.ToBoolean(cellValue) ? "是" : "否");
                                    icell.CellStyle = styleNormal;
                                }
                                else
                                {
                                    icell.SetCellValue(cellValue.ToString());
                                    icell.CellStyle = styleNormal;
                                }
                            }
                        }
                        wb.Write(fs);
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            });
           
            return result;
        }


        /// DataTable导出到Excel的MemoryStream
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        public static async Task<MemoryStream> DataTableToExcelAsync(DataTable dtSource, string strHeaderText)
        {
            var result =await Task.Factory.StartNew(() => {

                HSSFWorkbook workbook = new HSSFWorkbook();
                HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet();

                #region 右击文件 属性信息
                {
                    DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                    dsi.Company = "NPOI";
                    workbook.DocumentSummaryInformation = dsi;

                    SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                    si.Author = "文件作者信息"; //填加xls文件作者信息
                    si.ApplicationName = "创建程序信息"; //填加xls文件创建程序信息
                    si.LastAuthor = "最后保存者信息"; //填加xls文件最后保存者信息
                    si.Comments = "作者信息"; //填加xls文件作者信息
                    si.Title = "标题信息"; //填加xls文件标题信息
                    si.Subject = "主题信息";//填加文件主题信息
                    si.CreateDateTime = System.DateTime.Now;
                    workbook.SummaryInformation = si;
                }
                #endregion

                HSSFCellStyle dateStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                HSSFDataFormat format = (HSSFDataFormat)workbook.CreateDataFormat();
                dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

                //取得列宽
                /*
                int[] arrColWidth = new int[dtSource.Columns.Count];
                foreach (DataColumn item in dtSource.Columns)
                {
                    arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
                }
                for (int i = 0; i < dtSource.Rows.Count; i++)
                {
                    for (int j = 0; j < dtSource.Columns.Count; j++)
                    {
                        int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                        if (intTemp > arrColWidth[j])
                        {
                            arrColWidth[j] = intTemp;
                        }
                    }
                }
                 */
                int rowIndex = 0;
                foreach (DataRow row in dtSource.Rows)
                {
                    #region 新建表，填充表头，填充列头，样式
                    if (rowIndex == 65535 || rowIndex == 0)
                    {
                        if (rowIndex != 0)
                        {
                            sheet = (HSSFSheet)workbook.CreateSheet();
                        }

                        #region 表头及样式
                        {
                            HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);
                            headerRow.HeightInPoints = 25;
                            headerRow.CreateCell(0).SetCellValue(strHeaderText);

                            HSSFCellStyle headStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                            //  headStyle.Alignment = CellHorizontalAlignment.CENTER;
                            HSSFFont font = (HSSFFont)workbook.CreateFont();
                            font.FontHeightInPoints = 20;
                            font.Boldweight = 700;
                            headStyle.SetFont(font);
                            headerRow.GetCell(0).CellStyle = headStyle;
                            // sheet.AddMergedRegion(new Region(0, 0, 0, dtSource.Columns.Count - 1));
                            //headerRow.Dispose();
                        }
                        #endregion


                        #region 列头及样式
                        {
                            HSSFRow headerRow = (HSSFRow)sheet.CreateRow(1);
                            HSSFCellStyle headStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                            //headStyle.Alignment = CellHorizontalAlignment.CENTER;
                            HSSFFont font = (HSSFFont)workbook.CreateFont();
                            font.FontHeightInPoints = 10;
                            font.Boldweight = 700;
                            headStyle.SetFont(font);
                            foreach (DataColumn column in dtSource.Columns)
                            {
                                headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                                headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

                                //设置列宽
                                //   sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);
                            }
                            // headerRow.Dispose();
                        }
                        #endregion

                        rowIndex = 2;
                    }
                    #endregion


                    #region 填充内容
                    HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                    foreach (DataColumn column in dtSource.Columns)
                    {
                        HSSFCell newCell = (HSSFCell)dataRow.CreateCell(column.Ordinal);

                        string drValue = row[column].ToString();

                        switch (column.DataType.ToString())
                        {
                            case "System.String"://字符串类型
                                newCell.SetCellValue(drValue);
                                break;
                            case "System.DateTime"://日期类型
                                System.DateTime dateV;
                                System.DateTime.TryParse(drValue, out dateV);
                                newCell.SetCellValue(dateV);

                                newCell.CellStyle = dateStyle;//格式化显示
                                break;
                            case "System.Boolean"://布尔型
                                bool boolV = false;
                                bool.TryParse(drValue, out boolV);
                                newCell.SetCellValue(boolV);
                                break;
                            case "System.Int16"://整型
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                int intV = 0;
                                int.TryParse(drValue, out intV);
                                newCell.SetCellValue(intV);
                                break;
                            case "System.Decimal"://浮点型
                            case "System.Double":
                                double doubV = 0;
                                double.TryParse(drValue, out doubV);
                                newCell.SetCellValue(doubV);
                                break;
                            case "System.DBNull"://空值处理
                                newCell.SetCellValue("");
                                break;
                            default:
                                newCell.SetCellValue("");
                                break;
                        }

                    }
                    #endregion

                    rowIndex++;
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    workbook.Write(ms);
                    ms.Flush();
                    ms.Position = 0;

                    //  sheet.Dispose();
                    //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
                    return ms;
                }

            });

            return result;
        }

    }
}
