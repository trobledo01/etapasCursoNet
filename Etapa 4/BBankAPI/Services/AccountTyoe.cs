using Microsoft.EntityFrameworkCore;
using BBankAPI.Data;
using BBankAPI.Data.BankModels;
namespace BBankAPI.Services;

public class AccountTypeService
{
    private readonly BankContext _context;
    public AccountTypeService(BankContext context)
    {
        _context = context;

    }

  
    public async Task<AccountType?> GetById(int id)
    {
        return await _context.AccountTypes.FindAsync(id);
    }
}