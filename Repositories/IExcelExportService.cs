using System.Drawing;

namespace KhoHang_XNK.Repositories
{
    public interface IExcelExportService
    {
        MemoryStream ExportToExcel<T>(List<T> data, string sheetName, Dictionary<string, string> columnMappings, string? title = null);
        MemoryStream ExportToExcel<T>(List<T> data, ExcelExportOptions options);
    }

    public class ExcelExportOptions
    {
        public string SheetName { get; set; } = "Sheet1";
        public string? Title { get; set; }
        public Dictionary<string, string> ColumnMappings { get; set; } = new();
        public Dictionary<string, Func<object, string>> CustomFormatters { get; set; } = new();
        public Color HeaderBackgroundColor { get; set; } = Color.LightBlue;
        public Color HeaderTextColor { get; set; } = Color.Black;
        public bool AutoFitColumns { get; set; } = true;
        public bool AddBorders { get; set; } = true;
        public int TitleFontSize { get; set; } = 16;
        public int HeaderFontSize { get; set; } = 12;
    }
}
