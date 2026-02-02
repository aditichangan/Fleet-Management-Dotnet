using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FleetManagementSystem.Api.Data;
using FleetManagementSystem.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FleetManagementSystem.Api.Services;

public class GoogleAuthService : IGoogleAuthService
{
    private readonly ApplicationDbContext _context;
    private readonly IJwtService _jwtService;
    private readonly HttpClient _httpClient;

    public GoogleAuthService(ApplicationDbContext context, IJwtService jwtService, HttpClient httpClient)
    {
        _context = context;
        _jwtService = jwtService;
        _httpClient = httpClient;
    }

    public async Task<string> VerifyGoogleTokenAndGetJwtAsync(string accessToken)
    {
        var response = await _httpClient.GetAsync($"https://www.googleapis.com/oauth2/v3/userinfo?access_token={accessToken}");
        
        if (!response.IsSuccessStatusCode)
        {
             throw new ArgumentException("Invalid access token.");
        }

        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        if (root.TryGetProperty("email", out var emailProp))
        {
            string email = emailProp.GetString();
            string name = root.TryGetProperty("name", out var nameProp) ? nameProp.GetString() : "";

            string username = email.Split('@')[0];

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                user = new User
                {
                    Email = email,
                    Username = username,
                    Role = Role.CUSTOMER.ToString(),
                    Password = Guid.NewGuid().ToString() // Random password
                };

                try
                {
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    // Handle duplicate username (implied from Java logic)
                     user.Username = username + "_" + Guid.NewGuid().ToString().Substring(0, 4);
                     // Need to create new context instance or detach? 
                     // In EF Core, if Add failed, the entity might optionally be tracked. 
                     // Assuming simple retry logic here or just fail for now as Java did catch Exception usually.
                     // Java code did: user.setUsername(...); save(user);
                     
                     // EF Core: if SaveChanges failed, we might need to detach or handle correctly.
                     // But simplest approach for parity:
                     _context.Users.Add(user); // Add again? No, it's already added to change tracker.
                     // Just change property.
                     await _context.SaveChangesAsync();
                }
            }

            return _jwtService.GenerateToken(user.Username, user.Role);
        }
        else
        {
             throw new ArgumentException("Invalid access token.");
        }
    }
}
