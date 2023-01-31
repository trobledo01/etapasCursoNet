namespace BBankAPI.controllers;
using Microsoft.AspNetCore.Mvc;

using BBankAPI.Services;
using BBankAPI.Data.BankModels;
using BBankAPI.Data.DTOs;
using Microsoft.AspNetCore.Authorization;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountServices;
    private readonly AccountTypeService _accountTypeServices;
    private readonly ClientService _clientServices;
    public AccountController(ClientService clientServices, AccountTypeService accountTypeServices, AccountService accountServices)
    {
        _accountServices = accountServices;
        _accountTypeServices = accountTypeServices;
        _clientServices = clientServices;

    }
    [HttpGet]
    public async Task<IEnumerable<AccountDtoOut>> Get()
    {
        return await _accountServices.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AccountDtoOut>> GetById(int id)
    {
        var account = await _accountServices.GetDtoById(id);
        if (account is null)
            return AccountNotFound(id);
        return account;
    }
    [Authorize (Policy = "SuperAdmin")]
    [HttpPost]
    public async Task<IActionResult> Create(AccountDtoIn account)
    {
        string validtionResult = await ValidateAccount(account);

        if(!validtionResult.Equals("Valid"))
            return BadRequest(new  { message = validtionResult });


        var newAccount = await _accountServices.Create(account);


        return CreatedAtAction(nameof(GetById), new { id = newAccount.Id }, newAccount);
    }

    [Authorize (Policy = "SuperAdmin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, AccountDtoIn account)
    {
        if (id != account.Id)
            return BadRequest(new { message = $"El ID [{id}] de la URL no coincide con el ID({account.Id}) del cuerpo de la solicitud." });

        var AccountToUpdate = await _accountServices.GetById(id);

        if (AccountToUpdate is not null)
        {
            string validtionResult = await ValidateAccount(account);

            if (!validtionResult.Equals("Valid"))
                return BadRequest(new  { message = validtionResult });
            await _accountServices.Update(account);
            return NoContent();
        }
        else
        {
            return AccountNotFound(id);
        }

    }

    [Authorize (Policy = "SuperAdmin")]

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var AccountToDelete = await _accountServices.GetById(id);

        if (AccountToDelete is not null)
        {
            await _accountServices.Delete(id);
            return Ok();
        }
        else
        {
            return AccountNotFound(id);
        }

    }

    public NotFoundObjectResult AccountNotFound(int id)
    {
        return NotFound(new { message = $"La cuenta con ID = {id} no existe." });
    }

    public async Task<string> ValidateAccount(AccountDtoIn account)
    {
        string result = "Valid";

        var accountType = await _accountTypeServices.GetById(account.AccountType);

        if (accountType is null)
            result = $"El tipo de cuenta {account.AccountType} no existe";

        var clientID = account.ClientId.GetValueOrDefault();

        var client = await _clientServices.GetById(clientID);

        if (client is null )
            result = $"El cliente {clientID} no existe";

        return result;
    }
}