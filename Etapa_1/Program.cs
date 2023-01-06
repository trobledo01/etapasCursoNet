#region  Variables
int opcion = 0;
int numRetiros = 0;
int[] retiros = new int[10];
int[,] efectivo = new int[10, 2];
#endregion

#region menu
do
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.DarkBlue;
    Console.WriteLine(" *************************BANCO*************************\n");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("1. Ingresar la cantidad de retiros hechos por el usuario.");
    Console.WriteLine("2. Revivar la cantidad entregada de billetes y monedas.");
    Console.WriteLine("3. Salir.\n");

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write("Ingresa la opción:  ");
    Console.ForegroundColor = ConsoleColor.Cyan;
    opcion = int.Parse(Console.ReadLine());

    switch (opcion)
    {
        case 1:
            numRetirosM();

            break;
        case 2:
            mostrarEfectivo(numRetiros);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Presiona 'enter' para continuar..." );
            Console.ReadKey();
            break;

    }

} while (opcion <= 2 && opcion >= 1);

Console.ForegroundColor = ConsoleColor.DarkGray;

#endregion 

#region numRetiros

void numRetirosM()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write("¿Cuántos retiros se hicieron (máximo 10)?\t");
    Console.ForegroundColor = ConsoleColor.Cyan;
    numRetiros = int.Parse(Console.ReadLine());

    retirosM(numRetiros);

}

void retirosM(int numRetiros)
{
    Console.Clear();
    for (int i = 0; i < numRetiros; i++)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"Ingresa la cantidad del retiro [#{i + 1}]:\t");
        Console.ForegroundColor = ConsoleColor.Cyan;
        retiros[i] = int.Parse(Console.ReadLine());

        calcularEfectivo(retiros[i],i);

    }



}


#endregion

#region efectivo

void calcularEfectivo(int cantidad,int i)
{
    int billetes= 0;
    int monedas = 0;
    while(cantidad >= 500)
    {
        cantidad-= 500;
        billetes++;
    }

    while(cantidad >= 200)
    {
        cantidad-= 200;
        billetes++;
    }

    while(cantidad >= 100)
    {
        cantidad-= 100;
        billetes++;
    }

    while(cantidad >= 50)
    {
        cantidad-= 50;
        billetes++;
    }

    while(cantidad >= 20)
    {
        cantidad-= 20;
        billetes++;
    }

    while(cantidad >= 10)
    {
        cantidad-= 10;
        monedas++;
    }

    while(cantidad >= 5)
    {
        cantidad-= 5;
        monedas++;
    }

    while(cantidad >= 2)
    {
        cantidad-= 2;
        monedas++;
    }

    while(cantidad >= 1)
    {
        cantidad-= 1;
        monedas++;
    }

    efectivo[i,0] = billetes;
    efectivo[i,1] = monedas;

}

void mostrarEfectivo(int numRetiros)
{
    Console.Clear();
    for(int j = 0; j< numRetiros; j++)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Retiro #{j+1}:");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Billetes entregados: ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"{efectivo[j,0]}");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Monedas entregadas: ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"{efectivo[j,1]}\n");
    }
}


#endregion
