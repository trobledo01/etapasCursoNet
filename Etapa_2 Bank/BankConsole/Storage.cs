using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace BankConsole;

public static class Storage
{
    static string filePath= AppDomain.CurrentDomain.BaseDirectory + @"\users.json";

    public static void AddUser(User user)
    {
        string json = "",usersInFile="";

        if(File.Exists(filePath))
            usersInFile=File.ReadAllText(filePath);

        var listUsers = JsonConvert.DeserializeObject<List<object>>(usersInFile);

        if(listUsers == null)
            listUsers = new List<object>();

        listUsers.Add(user);
        JsonSerializerSettings settings = new JsonSerializerSettings{Formatting = Formatting.Indented};

        json = JsonConvert.SerializeObject(listUsers,settings);

        File.WriteAllText(filePath,json);
    }

    public static List<User> GetNewUsers()
    {
        string usersInFile="";
        var listUsers = new List<User>();

        if(File.Exists(filePath))
            usersInFile=File.ReadAllText(filePath);

        var listOjects = JsonConvert.DeserializeObject<List<object>>(usersInFile);

        if(listOjects == null)
            return listUsers;

        foreach(object obj in listOjects)
        {
            User newUser;
            JObject user = (JObject)obj;

            if(user.ContainsKey("TaxRegime"))
                newUser = user.ToObject<Client>();
            else
                newUser = user.ToObject<Employee>();

        listUsers.Add(newUser);

        }
        var newUserList = listUsers.Where(user => user.GetRegisterDay().Date.Equals(DateTime.Today)).ToList();
        return newUserList;
    }

    public static string DeleteUser(int ID)
    {
        string usersInFile="";
        var listUsers = new List<User>();

        if(File.Exists(filePath))
            usersInFile=File.ReadAllText(filePath);

        var listOjects = JsonConvert.DeserializeObject<List<object>>(usersInFile);

        if(listOjects == null)
            return "There are no users in the file./n";

        foreach(object obj in listOjects)
        {
            User newUser;
            JObject user = (JObject)obj;

            if(user.ContainsKey("TaxRegime"))
                newUser = user.ToObject<Client>();
            else
                newUser = user.ToObject<Employee>();

        listUsers.Add(newUser);

        }
        var userToDelete = listUsers.Where(user => user.GetID() == ID).Single();
        listUsers.Remove(userToDelete);
        JsonSerializerSettings settings = new JsonSerializerSettings {Formatting = Formatting.Indented};

        string json = JsonConvert.SerializeObject(listUsers,settings);

        File.WriteAllText(filePath,json);

        return "Success";

    }

    public static bool SearchID(int ID)
    {
        string usersInFile="";
        var listUsers = new List<User>();

        if(File.Exists(filePath))
            usersInFile=File.ReadAllText(filePath);

        var listOjects = JsonConvert.DeserializeObject<List<object>>(usersInFile);

        if(listOjects == null)
            return false;

        foreach(object obj in listOjects)
        {
            User newUser;
            JObject user = (JObject)obj;

            if(user.ContainsKey("TaxRegime"))
                newUser = user.ToObject<Client>();
            else
                newUser = user.ToObject<Employee>();

        listUsers.Add(newUser);

        }
        var userToSearch = listUsers.FirstOrDefault(user => user.GetID() == ID);
        
        /* JsonSerializerSettings settings = new JsonSerializerSettings {Formatting = Formatting.Indented};

        string json = JsonConvert.SerializeObject(listUsers,settings);

        File.WriteAllText(filePath,json); */

        if(userToSearch == null)
        {
            return false;
        }else
        {
            return true;
        }

    }
}