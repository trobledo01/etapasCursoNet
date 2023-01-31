using Microsoft.EntityFrameworkCore;
using BBankAPI.Data;
using BBankAPI.Data.BankModels;
using BBankAPI.Data.DTOs;
namespace BBankAPI.Services;



public class LoginService
{
    private readonly BankContext _context;

    public LoginService (BankContext context)
    {
        _context = context;
    }

    public async Task<Administrator?> GetAdmin(AdminDto admin)
    {
        return await _context.Administrators.SingleOrDefaultAsync(x=> x.Email == admin.Email && x.Pwd == admin.Psw);
    }

}