using OfficeOpenXml.Style;
using OfficeOpenXml;
using NuGet.Packaging;

namespace KhoHang_XNK.Repositories
{
    public class ExcelExportService : IExcelExportService
    {
        public MemoryStream ExportToExcel<T>(List<T> data, string sheetName, Dictionary<string, string> columnMappings, string? title = null)
        {
            var options = new ExcelExportOptions
            {
                SheetName = sheetName,
                Title = title ?? $"DANH SÁCH {sheetName.ToUpper()}",
                ColumnMappings = columnMappings
            };

            return ExportToExcel(data, options);
        }

        public MemoryStream ExportToExcel<T>(List<T> data, ExcelExportOptions options)
        {

            ExcelPackage.License.SetNonCommercialPersonal("Anh Tai");
            // Set license context

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add(options.SheetName);

            int currentRow = 1;

            // Add title if provided
            if (!string.IsNullOrWhiteSpace(options.Title))
            {
                currentRow = AddTitle(worksheet, options.Title, options.ColumnMappings.Count, options.TitleFontSize);
                currentRow += 2; // Add spacing after title
            }

            // Add headers
            currentRow = AddHeaders(worksheet, options.ColumnMappings, currentRow, options);

            // Add data
            AddData(worksheet, data, options.ColumnMappings, options.CustomFormatters, currentRow, options.AddBorders);

            // Format worksheet
            if (options.AutoFitColumns && worksheet.Dimension != null)
            {
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
            }

            return SaveToMemoryStream(package);
        }

        private static int AddTitle(ExcelWorksheet worksheet, string title, int columnCount, int fontSize)
        {
            const int titleRow = 1;

            worksheet.Cells[titleRow, 1].Value = title;

            if (columnCount > 1)
            {
                worksheet.Cells[titleRow, 1, titleRow, columnCount].Merge = true;
            }

            var titleRange = worksheet.Cells[titleRow, 1, titleRow, columnCount];
            titleRange.Style.Font.Bold = true;
            titleRange.Style.Font.Size = fontSize;
            titleRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            titleRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            return titleRow;
        }

        private static int AddHeaders(ExcelWorksheet worksheet, Dictionary<string, string> columnMappings,
            int startRow, ExcelExportOptions options)
        {
            int colIndex = 1;
            foreach (var mapping in columnMappings)
            {
                worksheet.Cells[startRow, colIndex].Value = mapping.Value;
                colIndex++;
            }

            // Format headers
            var headerRange = worksheet.Cells[startRow, 1, startRow, columnMappings.Count];
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Font.Size = options.HeaderFontSize;
            headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            headerRange.Style.Fill.BackgroundColor.SetColor(options.HeaderBackgroundColor);
            headerRange.Style.Font.Color.SetColor(options.HeaderTextColor);

            if (options.AddBorders)
            {
                headerRange.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                headerRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                headerRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                headerRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                headerRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            }

            headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            headerRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            return startRow;
        }

        private static void AddData<T>(ExcelWorksheet worksheet, List<T> data,
            Dictionary<string, string> columnMappings, Dictionary<string, Func<object, string>> customFormatters,
            int headerRow, bool addBorders)
        {
            if (!data.Any()) return;

            var properties = typeof(T).GetProperties().ToDictionary(p => p.Name, p => p);

            for (int i = 0; i < data.Count; i++)
            {
                var row = headerRow + i + 1;
                var item = data[i];
                int colIndex = 1;

                foreach (var mapping in columnMappings)
                {
                    if (properties.TryGetValue(mapping.Key, out var prop))
                    {
                        var value = prop.GetValue(item);
                        string cellValue;

                        // Use custom formatter if available
                        if (customFormatters.TryGetValue(mapping.Key, out var formatter))
                        {
                            cellValue = formatter(value ?? string.Empty);
                        }
                        else
                        {
                            cellValue = FormatValue(value);
                        }

                        worksheet.Cells[row, colIndex].Value = cellValue;
                    }
                    colIndex++;
                }

                // Add borders to data rows
                if (addBorders)
                {
                    var dataRange = worksheet.Cells[row, 1, row, columnMappings.Count];
                    dataRange.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                }
            }
        }

        private static string FormatValue(object? value)
        {
            return value switch
            {
                null => string.Empty,
                DateTime dateTime => dateTime.ToString("dd/MM/yyyy"),
                decimal dec => dec.ToString("N2"),
                double dbl => dbl.ToString("N2"),
                float flt => flt.ToString("N2"),
                _ => value.ToString() ?? string.Empty
            };
        }

        private static MemoryStream SaveToMemoryStream(ExcelPackage package)
        {
            var stream = new MemoryStream();
            package.SaveAs(stream);
            stream.Position = 0;
            return stream;
        }
    }
}
