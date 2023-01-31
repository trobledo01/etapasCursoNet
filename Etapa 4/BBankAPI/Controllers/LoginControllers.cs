namespace BBankAPI.controllers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BBankAPI.Services;
using BBankAPI.Data.BankModels;
using BBankAPI.Data.DTOs;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly LoginService _loginServices;

    private IConfiguration _config;

    public LoginController(LoginService loginServices, IConfiguration config)
    {
        _loginServices = loginServices;
        _config = config;


    }

    [HttpPost("authenticate")]

    public async Task<IActionResult> Login(AdminDto adminDto)
    {
        var admin = await _loginServices.GetAdmin(adminDto);

        if (admin is null)
            return BadRequest(new { message = "Credenciales invalidas" });

        string jwtToken= GenerateToken(admin);
        return Ok(new { token = jwtToken });

    }

    private string GenerateToken(Administrator admin)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name,admin.Name),
            new Claim(ClaimTypes.Email,admin.Email),
            new Claim("AdminType", admin.AdminType)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JWT:Key").Value));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var securityToken = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: creds
        );

        string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

        return token;


    }
}

