namespace BBankAPI.controllers;
using Microsoft.AspNetCore.Mvc;

using BBankAPI.Services;
using BBankAPI.Data.BankModels;
using Microsoft.AspNetCore.Authorization;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ClientController: ControllerBase 
{
    private readonly ClientService _services;
    public ClientController(ClientService service)
    {
        _services = service;

    }
    [HttpGet]
    public async Task<IEnumerable<Client>> Get()
    {
        return await _services.GetAll();
    }

    [HttpGet("{id}")]
    public async Task< ActionResult<Client>> GetById(int id)
    {
        var client = await _services.GetById(id);
        if(client is null)
            return ClientNotFound(id);
        return client;
    }
    
    [Authorize (Policy = "SuperAdmin")]
    [HttpPost]
    public async Task <IActionResult> Create(Client client)
    {
        var newClient= await _services.Create(client);
       

        return CreatedAtAction(nameof(GetById),new {id= newClient.Id},newClient);
    }
    
    [Authorize (Policy = "SuperAdmin")]
    [HttpPut("{id}")]
    public async Task< IActionResult> Update(int id,Client client)
    {
        if(id != client.Id)
            return BadRequest(new {message = $"El ID [{id}] de la URL no coincide con el ID({client.Id}) del cuerpo de la solicitud."});
        
        var ClientToUpdate = await _services.GetById(id);

        if(ClientToUpdate is not null)
        {
            await _services.Update(id,ClientToUpdate);
            return NoContent();
        }
        else
        {
            return ClientNotFound(id);
        }
        
    }

    [Authorize (Policy = "SuperAdmin")]
    [HttpDelete("{id}")]
    public async Task< IActionResult> Delete(int id)
    {
        var ClientToDelete = await _services.GetById(id);

        if(ClientToDelete is not null)
        {
            await _services.Delete(id);
            return Ok();
        }
        else
        {
            return ClientNotFound(id);
        }
        
    }   

    public NotFoundObjectResult ClientNotFound (int id)
    {
        return NotFound(new {message = $"El cliente con ID = {id} no existe."});
    }
} 