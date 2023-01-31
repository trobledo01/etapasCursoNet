using Microsoft.EntityFrameworkCore;
using BBankAPI.Data;
using BBankAPI.Data.BankModels;
namespace BBankAPI.Services;

public class ClientService
{
    private readonly BankContext _context;
    public ClientService(BankContext context)
    {
        _context = context;

    }

    public async Task<IEnumerable<Client>> GetAll()
    {
        return await  _context.Clients.ToListAsync();
    }

    public async Task<Client?> GetById(int id)
    {
        return await _context.Clients.FindAsync(id);
    }

    public async Task<Client> Create(Client newclient)
    {
        _context.Clients.Add(newclient);
        await _context.SaveChangesAsync();

        return newclient;
    }

    public async  Task Update(int id,Client client)
    {


        var existingClient = await GetById(id);
        if (existingClient is not null)
        {
            existingClient.Name = client.Name;
            existingClient.PhoneNumber = client.PhoneNumber;
            existingClient.Email = client.Email;
            await _context.SaveChangesAsync();
        }
    }

    public async Task Delete(int id)
    {
        var clientToDelete = await GetById(id);
        if (clientToDelete is not null)
        {
            _context.Clients.Remove(clientToDelete);
            await _context.SaveChangesAsync();

        }
    }

}