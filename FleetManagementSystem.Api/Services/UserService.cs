using FleetManagementSystem.Api.Data;
using FleetManagementSystem.Api.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace FleetManagementSystem.Api.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public User AddUser(User user)
    {
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    public User GetUserByUsername(string username)
    {
        return _context.Users.FirstOrDefault(u => u.Username == username);
    }

    public string GenerateResetToken(string email)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == email);
        if (user != null)
        {
            var token = Guid.NewGuid().ToString();
            user.ResetToken = token;
            user.ResetTokenExpiry = DateTime.Now.AddHours(1);
            _context.SaveChanges();
            return token;
        }
        return null;
    }

    public bool ResetPassword(string token, string newPassword)
    {
        var user = _context.Users.FirstOrDefault(u => u.ResetToken == token);
        if (user != null)
        {
            if (user.ResetTokenExpiry.HasValue && user.ResetTokenExpiry.Value > DateTime.Now)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
                user.ResetToken = null;
                user.ResetTokenExpiry = null;
                _context.SaveChanges();
                return true;
            }
        }
        return false;
    }

    public bool ValidateCredentials(string username, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == username);
        if (user != null)
        {
            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }
        return false;
    }
}
