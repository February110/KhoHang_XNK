using KhoHang_XNK.Repositories;

namespace KhoHang_XNK.Extensions
{
    public static class ExcelExportExtensions
    {
        // Tốt hơn nên inject service thay vì tạo mới
        public static MemoryStream ToExcel<T>(this List<T> data,
            IExcelExportService exportService,
            string sheetName,
            Dictionary<string, string> columnMappings,
            string? title = null)
        {
            return exportService.ExportToExcel(data, sheetName, columnMappings, title);
        }

        public static MemoryStream ToExcel<T>(this List<T> data,
            IExcelExportService exportService,
            ExcelExportOptions options)
        {
            return exportService.ExportToExcel(data, options);
        }

        // Hoặc nếu muốn giữ nguyên cách cũ (không khuyến nghị vì vi phạm DI)
        public static MemoryStream ToExcelDirect<T>(this List<T> data,
            string sheetName,
            Dictionary<string, string> columnMappings,
            string? title = null)
        {
            var service = new ExcelExportService();
            return service.ExportToExcel(data, sheetName, columnMappings, title);
        }

        public static MemoryStream ToExcelDirect<T>(this List<T> data,
            ExcelExportOptions options)
        {
            var service = new ExcelExportService();
            return service.ExportToExcel(data, options);
        }
    }
}
