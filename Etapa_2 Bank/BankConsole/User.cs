using Newtonsoft.Json;
namespace BankConsole;

public class User 
{
    [JsonProperty]
    protected int ID{get;set;}
    [JsonProperty]
    protected string Name { get; set; }
    [JsonProperty]
    protected  string Email { get; set; }
    [JsonProperty]
    protected  decimal Balance { get; set; } 
    [JsonProperty]           
    protected  DateTime RegisterDate { get; set; }

    public User(){}


    
    public User(int ID,string Name,string Email, decimal Balance )
    {
        this.ID= ID;
        this.Name=Name;
        this.Email=Email;
        this.RegisterDate= DateTime.Now;
    }

    public DateTime GetRegisterDay ()
    {
        return RegisterDate;
    }

    public int GetID ()
    {
        return ID;
    }

    public  virtual void SetBalance(decimal amount)
    {
        decimal quantity = 0;

        if(amount < 0)
            quantity= 0;
        else
        quantity= amount;

        this.Balance+= quantity;
    }

    public virtual string ShowData()
    {
        return $"ID: {this.ID}\nNomre: { this.Name}\nCorreo: { this.Email}\nSaldo: { this.Balance}\nFecha de registro: {this.RegisterDate.ToShortDateString()}";
    }

    public string ShowData(string initialMenssage)
    {
        
        return $"{initialMenssage}\nNomre: { this.Name}\nCorreo: { this.Email}\nSaldo: { this.Balance}\nFecha de registro: {this.RegisterDate}";
    }

    
}
