using Aspose.Cells;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace DataSharing.Common
{
    /// <summary>
    /// DataTable对象转换类
    /// </summary>
    public class DataTableConverter
    {
        /// <summary>
        /// 读取excel文件 保存到DataTable
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="sheetName"></param>
        /// <param name="isFirstRowColumn"></param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(string fileName, string sheetName = null, bool isFirstRowColumn = true)
        {
            FileStream fs = null;
            ISheet sheet = null;
            DataTable data = new DataTable();
            IWorkbook workbook = null;
            int startRow = 1;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);

                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);//读取标题
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);

                            if (cell != null)
                            {
                                // cell.SetCellType(CellType.String);
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                            {
                                dataRow[j] = row.GetCell(j).ToString();
                            }

                        }
                        data.Rows.Add(dataRow);
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 读取excel文件 保存到DataTable
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fs"></param>
        /// <param name="sheetName"></param>
        /// <param name="isFirstRowColumn"></param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(string fileName, Stream fs, string sheetName = null, bool isFirstRowColumn = true)
        {
            ISheet sheet = null;
            DataTable data = new DataTable();
            IWorkbook workbook = null;
            int startRow = 1;
            try
            {
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);

                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);//读取标题
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);

                            if (cell != null)
                            {
                                // cell.SetCellType(CellType.String);
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                            {
                                dataRow[j] = row.GetCell(j).ToString();
                            }

                        }
                        data.Rows.Add(dataRow);
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 读取excel文件 保存到DataTable
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fs"></param>
        /// <param name="columnTypeDic"></param>
        /// <param name="sheetName"></param>
        /// <param name="isFirstRowColumn"></param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(string fileName, Stream fs, Dictionary<string, string> columnTypeDic, string sheetName = null, bool isFirstRowColumn = true)
        {
            ISheet sheet = null;
            DataTable data = new DataTable();
            IWorkbook workbook = null;
            int startRow = 1;
            try
            {
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);

                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);//读取标题
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);

                            if (cell != null)
                            {
                                // cell.SetCellType(CellType.String);
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    if (!columnTypeDic.ContainsKey(cellValue))
                                    {
                                        throw new Exception($"字段'{cellValue}'不存在");
                                    }
                                    DataColumn column = new DataColumn(columnTypeDic[cellValue]);
                                    //DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                            {
                                dataRow[j] = row.GetCell(j).ToString();
                            }

                        }
                        data.Rows.Add(dataRow);
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }


        /// <summary>
        /// 读取csv文件成DataTable数据[如果是Unicode存储，列之间使用tab间隔，如果是utf-8存储，使用逗号间隔]
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static DataTable CSV2DataTable(string fileName)
        {
            DataTable dt = new DataTable();
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, new UnicodeEncoding());
            //记录每次读取的一行记录
            string strLine = "";
            //记录每行记录中的各字段内容
            string[] aryLine;
            //标示列数
            int columnCount = 0;
            //标示是否是读取的第一行
            bool IsFirst = true;

            //逐行读取CSV中的数据
            while ((strLine = sr.ReadLine()) != null)
            {
                aryLine = strLine.Split(',');
                if (IsFirst == true)
                {
                    IsFirst = false;
                    columnCount = aryLine.Length;
                    //创建列
                    for (int i = 0; i < columnCount; i++)
                    {
                        DataColumn dc = new DataColumn(aryLine[i]);
                        dt.Columns.Add(dc);
                    }
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < columnCount; j++)
                    {
                        dr[j] = aryLine[j];
                    }
                    dt.Rows.Add(dr);
                }
            }

            sr.Close();
            fs.Close();
            return dt;
        }


        /// <summary>
        /// 读取csv文件成DataTable数据[如果是Unicode存储，列之间使用tab间隔，如果是utf-8存储，使用逗号间隔]
        /// </summary>
        /// <param name="fs"></param>
        /// <returns></returns>
        public static DataTable CSV2DataTable(Stream fs)
        {
            DataTable dt = new DataTable();
            StreamReader sr = new StreamReader(fs, new UnicodeEncoding());
            //记录每次读取的一行记录
            string strLine = "";
            //记录每行记录中的各字段内容
            string[] aryLine;
            //标示列数
            int columnCount = 0;
            //标示是否是读取的第一行
            bool IsFirst = true;

            //逐行读取CSV中的数据
            while ((strLine = sr.ReadLine()) != null)
            {
                aryLine = strLine.Split(',');
                if (IsFirst == true)
                {
                    IsFirst = false;
                    columnCount = aryLine.Length;
                    //创建列
                    for (int i = 0; i < columnCount; i++)
                    {
                        DataColumn dc = new DataColumn(aryLine[i]);
                        dt.Columns.Add(dc);
                    }
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < columnCount; j++)
                    {
                        dr[j] = aryLine[j];
                    }
                    dt.Rows.Add(dr);
                }
            }

            sr.Close();
            fs.Close();
            return dt;
        }

        /// <summary>
        /// 读取csv文件成DataTable数据[如果是Unicode存储，列之间使用tab间隔，如果是utf-8存储，使用逗号间隔]
        /// </summary>
        /// <param name="fs">stream流对象</param>
        /// <param name="columnTypeDic">字段列名类型映射字典</param>
        /// <returns></returns>
        public static DataTable CSV2DataTable(Stream fs, Dictionary<string, string> columnTypeDic)
        {
            DataTable dt = new DataTable();
            StreamReader sr = new StreamReader(fs, new UnicodeEncoding());
            //记录每次读取的一行记录
            string strLine = "";
            //记录每行记录中的各字段内容
            string[] aryLine;
            //标示列数
            int columnCount = 0;
            //标示是否是读取的第一行
            bool IsFirst = true;

            //逐行读取CSV中的数据
            while ((strLine = sr.ReadLine()) != null)
            {
                aryLine = strLine.Split(',');
                if (IsFirst == true)
                {
                    IsFirst = false;
                    columnCount = aryLine.Length;
                    //创建列
                    for (int i = 0; i < columnCount; i++)
                    {
                        DataColumn dc = new DataColumn(columnTypeDic[aryLine[i]]);
                        //DataColumn dc = new DataColumn(aryLine[i]);
                        dt.Columns.Add(dc);
                    }
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < columnCount; j++)
                    {
                        dr[j] = aryLine[j];
                    }
                    dt.Rows.Add(dr);
                }
            }

            sr.Close();
            fs.Close();
            return dt;
        }

        /// <summary>
        /// 将DataTable数据写入csv文件[如果是Unicode存储，列之间使用tab间隔，如果是utf-8存储，使用逗号间隔]
        /// </summary>
        /// <param name="dt">DataTable数据</param>
        /// <param name="AbosultedFilePath">绝对路径</param>
        public static void DataTable2CSV(DataTable dt, string AbosultedFilePath)
        {
            FileStream fs = new FileStream(AbosultedFilePath, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, new UnicodeEncoding());
            //Tabel header
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                sw.Write(dt.Columns[i].ColumnName);
                sw.Write("\t");
            }
            sw.WriteLine("");
            //Table body
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    sw.Write(DelQuota(dt.Rows[i][j].ToString()));
                    sw.Write("\t");
                }
                sw.WriteLine("");
            }
            sw.Flush();
            sw.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DelQuota(string str)
        {
            string result = str;
            string[] strQuota = { "~", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "`", ";", "'", ",", ".", "/", ":", "/,", "<", ">", "?" };
            for (int i = 0; i < strQuota.Length; i++)
            {
                if (result.IndexOf(strQuota[i]) > -1)
                    result = result.Replace(strQuota[i], "");
            }
            return result;
        }

        /// <summary>
        /// DataTable数据导出成csv
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool DatatableToCSV(DataTable dt, string fileName)
        {
            bool createFLAG = false;

            StringBuilder sb = new StringBuilder();
            string line = "";

            if (dt != null && dt.Rows.Count > 0)
            {
                //table head
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    line += string.Format("\"{0}\",", dt.Columns[i].ColumnName);
                }

                line = line.TrimEnd(',');
                sb.AppendLine(line);

                //every row
                foreach (DataRow row in dt.Rows)
                {
                    line = "";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        line += string.Format("\"{0}\",", row[j].ToString().Replace("\"", "\"\""));
                    }

                    line = line.TrimEnd(',');
                    sb.AppendLine(line);
                }

                //write file
                //日志文件夹路径
                string LogFileWJJ = AppDomain.CurrentDomain.BaseDirectory + "LogCSV";

                if (System.IO.File.Exists(LogFileWJJ) == false)
                {
                    //不存在MyLog文件夹就创建
                    Directory.CreateDirectory(LogFileWJJ);
                }

                //当前日期的文件夹路径
                string jinTianWJJ = LogFileWJJ + "\\" + DateTime.Now.ToString("yyyy-MM-dd");

                if (System.IO.File.Exists(jinTianWJJ) == false)
                {
                    //不存在当前日期的文件夹就创建
                    Directory.CreateDirectory(jinTianWJJ);
                }

                //日志TXT文件
                string csvName = jinTianWJJ + "\\" + fileName + ".csv";

                System.IO.File.WriteAllText(csvName, sb.ToString(), Encoding.UTF8);
                createFLAG = true;
            }//end if 


            return createFLAG;

        }


        /// <summary>
        /// DataTable数据导出Excel
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filepath"></param>
        public static void DataTableToExcel(DataTable data, string filepath)
        {
            try
            {

                //Workbook book = new Workbook("E:\\test.xlsx"); //打开工作簿
                Workbook book = new Workbook(); //创建工作簿
                Worksheet sheet = book.Worksheets[0]; //创建工作表

                Cells cells = sheet.Cells; //单元格
                                           //创建样式
                Style style = book.DefaultStyle;
                style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin; //应用边界线 左边界线  
                style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin; //应用边界线 右边界线  
                style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin; //应用边界线 上边界线  
                style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin; //应用边界线 下边界线   
                style.HorizontalAlignment = TextAlignmentType.Center; //单元格内容的水平对齐方式文字居中
                style.Font.Name = "宋体"; //字体
                style.Font.IsBold = true; //设置粗体
                style.Font.Size = 11; //设置字体大小
                                      //style.ForegroundColor = System.Drawing.Color.FromArgb(153, 204, 0); //背景色
                                      //style.Pattern = Aspose.Cells.BackgroundType.Solid; //背景样式
                                      //style.IsTextWrapped = true; //单元格内容自动换行

                //表格填充数据
                int Colnum = data.Columns.Count;//表格列数 
                int Rownum = data.Rows.Count;//表格行数 
                                             //生成行 列名行 
                for (int i = 0; i < Colnum; i++)
                {
                    cells[0, i].PutValue(data.Columns[i].ColumnName); //添加表头
                    cells[0, i].SetStyle(style); //添加样式
                                                 //cells.SetColumnWidth(i, data.Columns[i].ColumnName.Length * 2 + 1.5); //自定义列宽
                                                 //cells.SetRowHeight(0, 30); //自定义高
                }
                //生成数据行 
                for (int i = 0; i < Rownum; i++)
                {
                    for (int k = 0; k < Colnum; k++)
                    {
                        cells[1 + i, k].PutValue(data.Rows[i][k].ToString()); //添加数据
                        cells[1 + i, k].SetStyle(style); //添加样式
                    }
                    cells[1 + i, 5].Formula = "=B" + (2 + i) + "+C" + (2 + i);//给单元格设置计算公式，计算班级总人数
                }
                sheet.AutoFitColumns(); //自适应宽
                book.Save(filepath); //保存
                GC.Collect();
            }
            catch (Exception e)
            {
                Console.WriteLine("生成excel出错：" + e.Message);
                //logger.Error("生成excel出错：" + e.Message);
            }
        }

        /// <summary>
        /// 导出DataTable To Stream
        /// </summary>
        /// <param name="data">DataTable对象</param>
        /// <returns></returns>
        public static Stream DataTableToExcel(DataTable data)
        {

            try
            {
                Workbook book = new Workbook(); //创建工作簿
                Worksheet sheet = book.Worksheets[0]; //创建工作表

                Cells cells = sheet.Cells; //单元格
                                           //创建样式
                Style style = book.DefaultStyle;
                style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin; //应用边界线 左边界线  
                style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin; //应用边界线 右边界线  
                style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin; //应用边界线 上边界线  
                style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin; //应用边界线 下边界线   
                style.HorizontalAlignment = TextAlignmentType.Center; //单元格内容的水平对齐方式文字居中
                style.Font.Name = "宋体"; //字体
                style.Font.IsBold = true; //设置粗体
                style.Font.Size = 11; //设置字体大小
                                      //style.ForegroundColor = System.Drawing.Color.FromArgb(153, 204, 0); //背景色
                                      //style.Pattern = Aspose.Cells.BackgroundType.Solid; //背景样式
                                      //style.IsTextWrapped = true; //单元格内容自动换行

                //表格填充数据
                int Colnum = data.Columns.Count;//表格列数 
                int Rownum = data.Rows.Count;//表格行数 
                                             //生成行 列名行 
                for (int i = 0; i < Colnum; i++)
                {
                    cells[0, i].PutValue(data.Columns[i].ColumnName); //添加表头
                    cells[0, i].SetStyle(style); //添加样式
                                                 //cells.SetColumnWidth(i, data.Columns[i].ColumnName.Length * 2 + 1.5); //自定义列宽
                                                 //cells.SetRowHeight(0, 30); //自定义高
                }
                //生成数据行 
                for (int i = 0; i < Rownum; i++)
                {
                    for (int k = 0; k < Colnum; k++)
                    {
                        cells[1 + i, k].PutValue(data.Rows[i][k].ToString()); //添加数据
                        cells[1 + i, k].SetStyle(style); //添加样式
                    }
                    cells[1 + i, 5].Formula = "=B" + (2 + i) + "+C" + (2 + i);//给单元格设置计算公式，计算班级总人数
                }
                sheet.AutoFitColumns(); //自适应宽
                return book.SaveToStream(); //保存
            }
            catch (Exception e)
            {
                Console.WriteLine("生成excel出错：" + e.Message);
                return null;
            }
            finally
            {
                GC.Collect();
            }
        }

        /// <summary>
        /// 导出DataTable To Stream
        /// </summary>
        /// <param name="data">DataTable对象</param>
        /// <param name="columnNameDic">字段名称映射字典</param>
        /// <returns></returns>
        public static Stream DataTableToExcel(DataTable data, Dictionary<string, string> columnNameDic)
        {

            try
            {
                Workbook book = new Workbook(); //创建工作簿
                Worksheet sheet = book.Worksheets[0]; //创建工作表

                Cells cells = sheet.Cells; //单元格
                                           //创建样式
                Style style = book.DefaultStyle;
                style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin; //应用边界线 左边界线  
                style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin; //应用边界线 右边界线  
                style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin; //应用边界线 上边界线  
                style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin; //应用边界线 下边界线   
                style.HorizontalAlignment = TextAlignmentType.Center; //单元格内容的水平对齐方式文字居中
                style.Font.Name = "宋体"; //字体
                style.Font.IsBold = true; //设置粗体
                style.Font.Size = 11; //设置字体大小
                                      //style.ForegroundColor = System.Drawing.Color.FromArgb(153, 204, 0); //背景色
                                      //style.Pattern = Aspose.Cells.BackgroundType.Solid; //背景样式
                                      //style.IsTextWrapped = true; //单元格内容自动换行

                //表格填充数据
                int Colnum = data.Columns.Count;//表格列数 
                int Rownum = data.Rows.Count;//表格行数 
                                             //生成行 列名行 
                for (int i = 0; i < Colnum; i++)
                {
                    string columnEnName = data.Columns[i].ColumnName;
                    cells[0, i].PutValue(columnNameDic[columnEnName]); //添加表头
                    cells[0, i].SetStyle(style); //添加样式
                                                 //cells.SetColumnWidth(i, data.Columns[i].ColumnName.Length * 2 + 1.5); //自定义列宽
                                                 //cells.SetRowHeight(0, 30); //自定义高
                }
                //生成数据行 
                for (int i = 0; i < Rownum; i++)
                {
                    for (int k = 0; k < Colnum; k++)
                    {
                        cells[1 + i, k].PutValue(data.Rows[i][k].ToString()); //添加数据
                        cells[1 + i, k].SetStyle(style); //添加样式
                    }
                    //cells[1 + i, 5].Formula = "=B" + (2 + i) + "+C" + (2 + i);//给单元格设置计算公式，计算班级总人数
                }
                sheet.AutoFitColumns(); //自适应宽
                book.Worksheets.RemoveAt("Evaluation Warning");
                return book.SaveToStream(); //保存
            }
            catch (Exception e)
            {
                Console.WriteLine("生成excel出错：" + e.Message);
                return null;
            }
            finally
            {
                GC.Collect();
            }
        }


        /// <summary>
        /// DataTable写入Excel 
        /// </summary>
        /// <param name="data">数据源</param>
        /// <param name="cell">开始写入行数 （第一条是0）</param>
        /// <param name="isSetRow">是否显示序号</param>
        /// <param name="fontSize">字体大小</param>
        /// <returns>xls文件字节数组</returns>
        public static byte[] DataTableToBytes(DataTable data, int cell = 0, bool isSetRow = true, short fontSize = 16)
        {

            byte[] byteData = null;

            HSSFWorkbook book = new HSSFWorkbook();
            ISheet sheet = book.CreateSheet("Sheet1");

            //设置表格样式
            ICellStyle cellStyle = book.CreateCellStyle();
            cellStyle.BorderBottom = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.BorderTop = BorderStyle.Thin;
            cellStyle.WrapText = true;
            //设置字体样式
            IFont fonts = book.CreateFont();
            //fonts.Boldweight = (short)FontBoldWeight.Bold;   //字体加粗样式    
            fonts.FontHeightInPoints = fontSize;                   //设置字体大小
            fonts.FontName = "宋体";
            fonts.Color = HSSFColor.Black.Index;             //设置字体颜色
            cellStyle.SetFont(fonts);
            cellStyle.Alignment = HorizontalAlignment.Center;   //居中      
            cellStyle.VerticalAlignment = VerticalAlignment.Center;//垂直对齐 

            if (data != null)
            {
                int rownum = 1;
                for (int i = 0; i < data.Rows.Count; i++)  //遍历模板excel中的行
                {
                    IRow currentRow = sheet.CreateRow(i + cell);
                    if (isSetRow)
                    {
                        ICell icell1top0 = currentRow.CreateCell(0);
                        icell1top0.SetCellValue(rownum);
                        icell1top0.CellStyle = cellStyle;
                    }

                    for (int j = 1; j <= data.Columns.Count; j++)
                    {
                        ICell icell1top0 = currentRow.CreateCell(j);

                        //遍历列
                        if (isSetRow)
                        {
                            icell1top0 = currentRow.CreateCell(j);


                            if (data.Columns[j - 1].DataType == System.Type.GetType("System.DateTime"))
                            {

                                if (!string.IsNullOrEmpty(data.Rows[i][j - 1].ToString()))
                                    icell1top0.SetCellValue(Convert.ToDateTime(data.Rows[i][j - 1]).ToString("yyyy-MM-dd"));
                                else
                                    icell1top0.SetCellValue(data.Rows[i][j - 1].ToString());
                            }
                            else if (data.Columns[j - 1].DataType == System.Type.GetType("System.Int32") || data.Columns[j - 1].DataType == System.Type.GetType("System.Int64"))
                            {
                                int intCount = 0;
                                int.TryParse(data.Rows[i][j - 1].ToString(), out intCount);
                                icell1top0.SetCellValue(intCount);
                            }
                            else if (data.Columns[j - 1].DataType == System.Type.GetType("System.Decimal"))
                            {
                                double doubleCount = 0;
                                double.TryParse(data.Rows[i][j - 1].ToString(), out doubleCount);
                                icell1top0.SetCellValue(doubleCount);
                            }
                            else
                            {

                                string thisData = data.Rows[i][j - 1].ToString();
                                if (!string.IsNullOrEmpty(thisData) && thisData.Length>40)
                                {
                                    //业务要求长字符串要求列内文字换行
                                    StringBuilder noteString = new StringBuilder("");
                                    string[] arrDt = thisData.Split('；');
                                    foreach (string dt in arrDt)
                                    {
                                        noteString.Append(dt + "\n");
                                    }
                                    icell1top0.SetCellValue(noteString.ToString());
                                }
                                else
                                {
                                    icell1top0.SetCellValue(thisData);
                                }
                            }

                            icell1top0.CellStyle = cellStyle;
                        }
                        else
                        {
                            icell1top0 = currentRow.CreateCell(j - 1);

                            if (data.Columns[j].DataType == System.Type.GetType("System.DateTime"))
                            {

                                if (!string.IsNullOrEmpty(data.Rows[i][j - 1].ToString()))
                                {
                                    icell1top0.SetCellValue(Convert.ToDateTime(data.Rows[i][j - 1]).ToString("yyyy-MM-dd"));
                                }

                                else
                                    icell1top0.SetCellValue(data.Rows[i][j - 1].ToString());
                            }

                            else if (data.Columns[j].DataType == System.Type.GetType("System.Int32") || data.Columns[j - 1].DataType == System.Type.GetType("System.Decimal"))
                            {
                                object value = data.Rows[i][j - 1];
                                try
                                {

                                    icell1top0.SetCellValue(Convert.ToInt32(value));
                                }
                                catch (Exception)
                                {
                                    icell1top0.SetCellValue(value.ToString());
                                }
                            }
                            else
                                icell1top0.SetCellValue(data.Rows[i][j - 1].ToString());

                            icell1top0.CellStyle = cellStyle;
                        }

                    }
                    rownum++;
                }
            }

            using (MemoryStream ms = new MemoryStream())
            {
                book.Write(ms);
                byteData = ms.ToArray();
            }
            return byteData;
        }


        /// <summary>
        /// 将datatable转化为Excel
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>xls文件字节数组</returns>
        public static byte[] DataTableToBytes(DataTable dt)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var workBook = new HSSFWorkbook();
                var sheet = workBook.CreateSheet("Sheet1");

                IRow headRow = sheet.CreateRow(0);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    ICell cell = headRow.CreateCell(i);
                    cell.SetCellValue(dt.Columns[i] == null ? "" : dt.Columns[i].ToString());
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IRow newRow = sheet.CreateRow(i + 1);
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            ICell cell = newRow.CreateCell(j);
                            cell.SetCellValue(dt.Rows[i][j] == null ? "" : dt.Rows[i][j].ToString());
                        }
                    }
                }
                workBook.Write(ms);
                workBook.Clear();
                workBook.Close();

                return ms.ToArray();
            }
        }

        /// <summary>
        /// 将datatable转化为Excel
        /// </summary>
        /// <param name="dt">DataTable对象</param>
        /// <param name="nameDic">标题中英文名称映射</param>
        /// <returns>xls文件字节数组</returns>
        public static byte[] DataTableToBytes(DataTable dt, Dictionary<string, string> nameDic)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var workBook = new HSSFWorkbook();
                var sheet = workBook.CreateSheet("Sheet1");

                IRow headRow = sheet.CreateRow(0);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    ICell cell = headRow.CreateCell(i);
                    string columnCnName = nameDic[dt.Columns[i].ColumnName];
                    cell.SetCellValue(columnCnName);

                    //cell.SetCellValue(dt.Columns[i] == null ? "" : dt.Columns[i].ToString());
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IRow newRow = sheet.CreateRow(i + 1);
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            ICell cell = newRow.CreateCell(j);
                            cell.SetCellValue(dt.Rows[i][j] == null ? "" : dt.Rows[i][j].ToString());
                        }
                    }
                }
                workBook.Write(ms);
                workBook.Clear();
                workBook.Close();

                return ms.ToArray();
            }
        }

    }



    /// <summary>
    /// dataTable比较类
    /// </summary>
    public class DataTableCompareHelper
    {
        /// <summary>
        /// 第一个dataTable
        /// </summary>
        public DataTable Dt1 { get; set; }

        /// <summary>
        /// 第二个dataTable
        /// </summary>
        public DataTable Dt2 { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataTable1">第一个dataTable</param>
        /// <param name="dataTable2">第二个dataTable</param>
        public DataTableCompareHelper(DataTable dataTable1, DataTable dataTable2)
        {
            this.Dt1=dataTable1;
            this.Dt2=dataTable2;
        }

        /// <summary>
        /// 判断两个dataTable字段和属性是否相同
        /// </summary>
        /// <returns></returns>
        public bool Equals()
        {
            bool flag = true;
            Type type = typeof(DataTable);
            var fields = type.GetFields();
            var props = type.GetProperties();
            foreach (var field in fields)
            {
                var objFirstValue = field.GetValue(this.Dt1)?.ToString();
                var objSecondValue = field.GetValue(this.Dt2)?.ToString();

                if (objFirstValue!=objSecondValue)
                {
                    Console.WriteLine($"对应字段:{field.Name}的值不匹配  |  FirstObjValue:{objFirstValue}  |  SencondObjValue{objSecondValue}");
                    flag=false;
                }
            }

            foreach (var prop in props)
            {
                var objFirstValue = prop.GetValue(this.Dt1)?.ToString();
                var objSecondValue = prop.GetValue(this.Dt2)?.ToString();

                if (objFirstValue!=objSecondValue)
                {
                    Console.WriteLine($"对应字段:{prop.Name}的值不匹配  |  FirstObjValue:{objFirstValue}  |  SencondObjValue{objSecondValue}");
                    flag=false;
                }
            }

            return flag;
        }

    }


}
