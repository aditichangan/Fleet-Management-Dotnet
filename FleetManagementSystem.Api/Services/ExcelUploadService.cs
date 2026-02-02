using System;
using Microsoft.AspNetCore.Http;

namespace FleetManagementSystem.Api.Services;

public class ExcelUploadService : IExcelUploadService
{
    public void Save(IFormFile file)
    {
        // TODO: Implement Excel parsing using NPOI
        // Java code uses Apache POI.
        // We will implement this later if needed for parity.
        Console.WriteLine($"Uploading file: {file.FileName}");
    }
}
