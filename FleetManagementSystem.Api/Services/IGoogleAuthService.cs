using System.Threading.Tasks;

namespace FleetManagementSystem.Api.Services;

public interface IGoogleAuthService
{
    Task<string> VerifyGoogleTokenAndGetJwtAsync(string accessToken);
}
