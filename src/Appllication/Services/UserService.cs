using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Contracts.User;
using Application.Data;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace Application.Services;

public class UserService
{
    private readonly WeddingContext _weddingContext;
    private readonly IConfiguration _configuration;

    public UserService(WeddingContext weddingContext, IConfiguration configuration)
    {
        _weddingContext = weddingContext;
        _configuration = configuration;
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

    public async Task<UserResponse<string>> Register(string userName, string password)
    {
        CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

        var user = new User()
        {
            UserName = userName,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

         _weddingContext.Users.Add(user);
        await _weddingContext.SaveChangesAsync();

        return new UserResponse<string>()
        {
            Id = user.Id,
            Success = true
        };
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA256();

        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA256(passwordSalt);
        var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        return computeHash.SequenceEqual(passwordHash);
    }

    private string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, user.Id.ToString()),
            new (ClaimTypes.Name, user.UserName)
        };

        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(30),
            SigningCredentials = credentials
        };

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken token = tokenHandler.CreateToken(securityTokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}