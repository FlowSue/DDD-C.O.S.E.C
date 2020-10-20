using Microsoft.Extensions.Logging;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace C.O.S.E.C.Infrastructure.Treasury.Helpers
{
    public class ExcelHeler
    {

        private static ILogger<ExcelHeler> logger;
        /// <summary>
        /// 读取Excel多Sheet数据
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="sheetName">Sheet名</param>
        /// <returns></returns>
        public static DataSet ReadExcelToDataSet(string filePath, string sheetName = null)
        {
            if (!File.Exists(filePath))
            {
                //logger.LogError($"未找到文件{filePath}");
                return null;
            }
            //获取文件信息
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            IWorkbook workbook = WorkbookFactory.Create(fs);
            //获取sheet信息
            ISheet sheet = null;
            DataSet ds = new DataSet();
            if (!string.IsNullOrEmpty(sheetName))
            {
                sheet = workbook.GetSheet(sheetName);
                if (sheet == null)
                {
                    //logger.LogError($"{filePath}未找到sheet:{sheetName}");
                    return null;
                }
                DataTable dt = ReadExcelFunc(workbook, sheet);
                ds.Tables.Add(dt);
            }
            else
            {
                //遍历获取所有数据
                int sheetCount = workbook.NumberOfSheets;
                for (int i = 0; i < sheetCount; i++)
                {
                    sheet = workbook.GetSheetAt(i);
                    if (sheet != null)
                    {
                        DataTable dt = ReadExcelFunc(workbook, sheet);
                        if (dt != null) ds.Tables.Add(dt);
                    }
                }
            }
            return ds;
        }

        /// <summary>
        /// 读取Excel多Sheet数据
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="sheetName">Sheet名</param>
        /// <returns></returns>
        public static DataSet ReadExcelToDataSet(FileStream fs, string sheetName = null)
        {
            IWorkbook workbook = WorkbookFactory.Create(fs);
            //获取sheet信息
            ISheet sheet = null;
            DataSet ds = new DataSet();
            if (!string.IsNullOrEmpty(sheetName))
            {
                sheet = workbook.GetSheet(sheetName);
                if (sheet == null)
                {
                    //logger.LogError($"{filePath}未找到sheet:{sheetName}");
                    return null;
                }
                DataTable dt = ReadExcelFunc(workbook, sheet);
                ds.Tables.Add(dt);
            }
            else
            {
                //遍历获取所有数据
                int sheetCount = workbook.NumberOfSheets;
                for (int i = 0; i < sheetCount; i++)
                {
                    sheet = workbook.GetSheetAt(i);
                    if (sheet != null)
                    {
                        DataTable dt = ReadExcelFunc(workbook, sheet);
                        if (dt != null)
                            ds.Tables.Add(dt);
                    }
                }
            }
            return ds;
        }

        /// <summary>
        /// 读取Excel信息
        /// </summary>
        /// <param name="workbook">工作区</param>
        /// <param name="sheet">sheet</param>
        /// <returns></returns>
        private static DataTable ReadExcelFunc(IWorkbook workbook, ISheet sheet)
        {
            DataTable dt = new DataTable();
            //获取列信息
            IRow cells = sheet.GetRow(sheet.FirstRowNum);
            //空数据化返回
            if (cells == null) return null;
            int cellsCount = cells.PhysicalNumberOfCells;
            //空列返回
            if (cellsCount == 0) return null;
            int emptyCount = 0;
            int cellIndex = sheet.FirstRowNum;
            List<string> listColumns = new List<string>();
            bool isFindColumn = false;
            while (!isFindColumn)
            {
                emptyCount = 0;
                listColumns.Clear();
                for (int i = 0; i < cellsCount; i++)
                {
                    if (string.IsNullOrEmpty(cells.GetCell(i).StringCellValue))
                    {
                        emptyCount++;
                    }
                    listColumns.Add(cells.GetCell(i).StringCellValue);
                }
                //这里根据逻辑需要，空列超过多少判断
                if (emptyCount == 0)
                {
                    isFindColumn = true;
                }
                cellIndex++;
                cells = sheet.GetRow(cellIndex);
            }

            foreach (string columnName in listColumns)
            {
                if (dt.Columns.Contains(columnName))
                {
                    //如果允许有重复列名，自己做处理
                    continue;
                }
                dt.Columns.Add(columnName, typeof(string));
            }
            //开始获取数据
            int rowsCount = sheet.PhysicalNumberOfRows;
            var rowIndex = 1;
            DataRow dr = null;
            //空数据化返回
            if (rowsCount <= 1) { return null; }
            for (int i = rowIndex; i < rowsCount; i++)
            {
                cells = sheet.GetRow(i);
                dr = dt.NewRow();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    //这里可以判断数据类型
                    switch (cells.GetCell(j).CellType)
                    {
                        case CellType.String:
                            dr[j] = cells.GetCell(j).StringCellValue;
                            break;
                        case CellType.Numeric:
                            dr[j] = cells.GetCell(j).NumericCellValue.ToString();
                            break;
                        case CellType.Unknown:
                            dr[j] = cells.GetCell(j).StringCellValue;
                            break;
                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}
