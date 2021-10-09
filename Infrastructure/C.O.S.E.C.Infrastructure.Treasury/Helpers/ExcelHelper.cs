using NPOI.SS.UserModel;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;

namespace C.O.S.E.C.Infrastructure.Treasury.Helpers
{
    /// <summary>
    /// Excel工具
    /// </summary>
    public static class ExcelHelper
    {
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
            var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var workbook = WorkbookFactory.Create(fs);
            //获取sheet信息
            ISheet sheet;
            var ds = new DataSet();
            if (!string.IsNullOrEmpty(sheetName))
            {
                sheet = workbook.GetSheet(sheetName);
                if (sheet == null)
                {
                    //logger.LogError($"{filePath}未找到sheet:{sheetName}");
                    return null;
                }
                var dt = ReadExcelFunc(sheet);
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
                        DataTable dt = ReadExcelFunc(sheet);
                        if (dt != null) ds.Tables.Add(dt);
                    }
                }
            }
            return ds;
        }

        /// <summary>
        /// 读取Excel多Sheet数据
        /// </summary>
        /// <param name="fs">文件流</param>
        /// <param name="sheetName">Sheet名</param>
        /// <returns></returns>
        public static DataSet ReadExcelToDataSet(FileStream fs, string sheetName = null)
        {
            var workbook = WorkbookFactory.Create(fs);
            //获取sheet信息
            ISheet sheet;
            var ds = new DataSet();
            if (!string.IsNullOrEmpty(sheetName))
            {
                sheet = workbook.GetSheet(sheetName);
                if (sheet == null)
                {
                    //logger.LogError($"{filePath}未找到sheet:{sheetName}");
                    return null;
                }
                var dt = ReadExcelFunc(sheet);
                ds.Tables.Add(dt);
            }
            else
            {
                //遍历获取所有数据
                var sheetCount = workbook.NumberOfSheets;
                for (var i = 0; i < sheetCount; i++)
                {
                    sheet = workbook.GetSheetAt(i);
                    if (sheet == null) continue;
                    var dt = ReadExcelFunc(sheet);
                    if (dt != null)
                        ds.Tables.Add(dt);
                }
            }
            return ds;
        }

        /// <summary>
        /// 读取Excel信息
        /// </summary>
        /// <param name="sheet">sheet</param>
        /// <returns></returns>
        private static DataTable ReadExcelFunc(ISheet sheet)
        {
            var dt = new DataTable();
            //获取列信息
            var cells = sheet.GetRow(sheet.FirstRowNum);
            //空数据化返回
            if (cells == null) return null;
            var cellsCount = cells.PhysicalNumberOfCells;
            //空列返回
            if (cellsCount == 0) return null;
            var cellIndex = sheet.FirstRowNum;
            var listColumns = new List<string>();
            var isFindColumn = false;
            while (!isFindColumn)
            {
                var emptyCount = 0;
                listColumns.Clear();
                for (var i = 0; i < cellsCount; i++)
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

            foreach (var columnName in listColumns.Where(columnName => !dt.Columns.Contains(columnName)))
            {
                dt.Columns.Add(columnName, typeof(string));
            }
            //开始获取数据
            var rowsCount = sheet.PhysicalNumberOfRows;
            const int rowIndex = 1;
            //空数据化返回
            if (rowsCount <= 1) { return null; }
            for (var i = rowIndex; i < rowsCount; i++)
            {
                cells = sheet.GetRow(i);
                var dr = dt.NewRow();
                for (var j = 0; j < dt.Columns.Count; j++)
                {
                    //这里可以判断数据类型
                    dr[j] = cells.GetCell(j).CellType switch
                    {
                        CellType.String => cells.GetCell(j).StringCellValue,
                        CellType.Numeric => cells.GetCell(j).NumericCellValue.ToString(CultureInfo.InvariantCulture),
                        CellType.Unknown => cells.GetCell(j).StringCellValue,
                        _ => dr[j]
                    };
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}
