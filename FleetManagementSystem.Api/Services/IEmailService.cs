namespace FleetManagementSystem.Api.Services;

public interface IEmailService
{
    void SendPasswordResetEmail(string to, string token);
}
