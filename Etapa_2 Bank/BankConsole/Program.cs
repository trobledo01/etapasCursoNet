using System.Text.RegularExpressions;
using BankConsole;
#region prueba
//Client us1 = new Client(1,"Tomy","trobledo01@gmail.com",1000,'M');

/* Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine(us1.ShowData()); */
//Storage.AddUser(us1); 

//Employee us2 = new Employee(2,"Abby","abbyF1409@gmail.com",4000,"IT");

/* Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine(us2.ShowData());  */
//Storage.AddUser(us2); 
/* Console.ForegroundColor = ConsoleColor.DarkGray;  */
#endregion

#region argumentos

if (args.Length == 0)
    EmailService.SendEmail();
else
    ShowMenu();

void ShowMenu()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("Selecciona una opción:\n");
    Console.WriteLine("1.- Crear un usuario nuevo.");
    Console.WriteLine("2.- Eliminar un usuario existente.");
    Console.WriteLine("3.- Salir.");

    int option = 0;

    do
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        string input = Console.ReadLine();

        if (!int.TryParse(input, out option))
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Debes ingresar un número (1, 2 o 3).");
        }
        else if (option > 3)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Debes ingresar un número (1, 2 o 3).");

        }


    } while (option == 0 || option > 3);

    switch (option)
    {
        case 1:
            CreateUser();
            break;
        case 2:
            DeleteUser();
            break;
        case 3:
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Environment.Exit(0);
            break;

    }

}

void CreateUser()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.DarkGreen;
    Console.WriteLine("Ingresa la información del usuario:\n");
    //ID
    int ID = 0;
    bool isValidI;

    do
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("ID:\t");
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        string sID = Console.ReadLine();
        string allowedChars = "*/+.!@#$%^&()[]{}|<>_=':;,?¿¡abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ ";

        isValidI = sID.All(c => allowedChars.Contains(c)); ;

        if (isValidI == false)
        {
            ID = int.Parse(sID);
            
            
            
        }

        if (isValidI == true || ID <= 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("ID invalido por favor ingresa un ID correcto (numero > 0).");


        }

        if (Storage.SearchID(ID) == true )
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("ID ingresado ya existe, por favor ingresa un ID diferente (numero > 0).");
                ID = 0;
            }

        




    } while (ID <= 0);



    //NAME
    string name = "";
    bool isValid;

    do
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("Nombre:\t");
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        name = Console.ReadLine();
        string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ ";
        isValid = name.All(c => allowedChars.Contains(c));
        if (name.Equals(""))
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Debes ingresar nombre valido.");

        }
        else if (isValid == false)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Debes ingresar nombre valido .");

        }


    } while (!name.Equals("") && isValid == false);



    //EMAIL
    string email = "";
    do
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("Email:\t");
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        email = Console.ReadLine();

        if (IsValidEmail(email) == false)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Debes ingresar un correo con el formato correcto (ejemplo@email.com).");

        }

    } while (IsValidEmail(email) == false);



    //SALDO
    decimal balance = 0;
    do
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("Saldo:\t");
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        balance = decimal.Parse(Console.ReadLine());

    } while (balance < 0);



    char userType;
    do
    {
        //TIPO DE USUARIO
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("Escribe 'c' si el usuario es Cliente, 'e' si es empleado:\t");
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        userType = char.Parse(Console.ReadLine());

        if (!userType.Equals('c') && !userType.Equals('e'))
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Debes ingresar 'c' o 'e'.");

        }

    } while (!userType.Equals('c') && !userType.Equals('e'));



    User newUser;



    if (userType.Equals('c'))
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("Regimen Fiscal:\t");
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        char taxRegime = char.Parse(Console.ReadLine());

        newUser = new Client(ID, name, email, balance, taxRegime);
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("Departamento:\t");
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        string department = Console.ReadLine();

        newUser = new Employee(ID, name, email, balance, department);

    }

    Storage.AddUser(newUser);

    Console.ForegroundColor = ConsoleColor.DarkMagenta;
    Console.WriteLine("\n\nUsuario Creado con exito");

    Thread.Sleep(2000);
    ShowMenu();



}

void DeleteUser()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.DarkGreen;
    Console.WriteLine("Ingrese el ID del usuario a eliminar:\t");
    //ID
    int ID = 0;
    bool isValidI;

    do
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("ID:\t");
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        string sID = Console.ReadLine();
        string allowedChars = "*/+.!@#$%^&()[]{}|<>_=':;,?¿¡abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ ";

        isValidI = sID.All(c => allowedChars.Contains(c)); ;

        if (isValidI == false)
        {
            ID = int.Parse(sID);
        }

        if (isValidI == true || ID <= 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("ID invalido por favor ingresa un ID correcto (numero > 0).");


        }




    } while (ID < 0);

    string result = Storage.DeleteUser(ID);

    if (result.Equals("Success"))
    {
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine("\n\nUsuario Eliminado con exito");
        Thread.Sleep(2000);
        ShowMenu();

    }

}

bool IsValidEmail(string email)
{
    string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
    return Regex.IsMatch(email, pattern);
}



#endregion