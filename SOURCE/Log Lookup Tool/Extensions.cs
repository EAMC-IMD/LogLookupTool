using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace Log_Lookup_Tool {
    static class Extensions {
        public static string ToNameCase(this string input) {
            switch (input) {
                case null: return"";
                case "": return "";
                default: return input[0].ToString().ToUpper() + input.Substring(1).ToLower();
            }
        }
        public static void ExportToExcel(this System.Data.DataTable dataTable, string ExcelFile = null) {
            try {
                int ColumnsCount;
                if (dataTable is null || (ColumnsCount = dataTable.Columns.Count) == 0) {
                    throw new NullReferenceException("ExportToExcel: Null or empty input table!\n");
                }
                Excel.Application excel = new Excel.Application();
                excel.Workbooks.Add();
                Excel._Worksheet worksheet = excel.ActiveSheet;

                object[] Header = new object[ColumnsCount];

                for (int i = 0; i < ColumnsCount; i++)
                    Header[i] = dataTable.Columns[i].ColumnName;

                Excel.Range HeaderRange = worksheet.Range[worksheet.Cells[1,1], worksheet.Cells[1,ColumnsCount]];
                HeaderRange.Value = Header;
                HeaderRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                HeaderRange.Font.Bold = true;

                int RowsCount = dataTable.Rows.Count;
                object[,] Cells = new object[RowsCount, ColumnsCount];

                for (int j = 0; j < RowsCount; j++)
                    for (int i = 0; i < ColumnsCount; i++)
                        Cells[j, i] = dataTable.Rows[j][i];

                worksheet.Range[worksheet.Cells[2, 1], worksheet.Cells[RowsCount + 1, ColumnsCount]].Value = Cells;
                worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[RowsCount + 1, ColumnsCount]].Columns.AutoFit();

                if (!string.IsNullOrEmpty(ExcelFile)) {
                    try {
                        worksheet.SaveAs(ExcelFile);
                        excel.Quit();
                    } catch (Exception ex) {
                        throw new Exception($"ExportToExcel: Excel file could not be saved. {ex.Message}");
                    }
                } else {
                    excel.Visible = true;
                }
            } catch (Exception ex) {
                throw new Exception($"ExportToExcel: {ex.Message}");
            }
        }
    }
}
