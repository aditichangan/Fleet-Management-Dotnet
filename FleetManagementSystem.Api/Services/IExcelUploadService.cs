using Microsoft.AspNetCore.Http;

namespace FleetManagementSystem.Api.Services;

public interface IExcelUploadService
{
    void Save(IFormFile file);
}
