using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using CsvHelper;
using EmployeeManagement.Application.Contracts.Infrastructure;
using EmployeeManagement.Application.Features.Employees.Queries.GetEmployeesExport;

namespace EmployeeManagement.Infrastructure.FileExport
{
    class CSVExporter : ICSVExporter
    {
        public byte[] ExportEmployeesToCsv(List<EmployeeExportDTO> employeeExportDtos)
        {

            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                using var csvWriter = new CsvWriter(streamWriter, CultureInfo.CurrentCulture);
                csvWriter.WriteRecords(employeeExportDtos);
            }

            return memoryStream.ToArray();
        }
    }
}
