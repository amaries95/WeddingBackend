using System.Security.Cryptography;
using System.Text;
using Application.Contracts.User;
using Application.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class UserService
{
    private readonly WeddingContext _weddingContext;

    public UserService(WeddingContext weddingContext)
    {
        _weddingContext = weddingContext;
    }

    public async Task<UserResponse<string>> Login(string username, string password)
    {
        var userResponse = new UserResponse<string>();
        var user = await _weddingContext.Users.FirstOrDefaultAsync(u =>
            u.UserName.ToLower().Equals(username.ToLower()));

        if (user == null)
        {
            userResponse.Success = false;
            userResponse.Message = "User not found";
        }
        else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        {
            userResponse.Success = false;
            userResponse.Message = "Incorrect password";
        }
        else
        {
            userResponse.Success = true;
            userResponse.Data = CreateToken(user);
        }

        return userResponse;
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA256(passwordSalt);
        var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }
}