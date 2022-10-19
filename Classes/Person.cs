/* Class for persons. Superclass of Delivery_Man, Manager, Customer and Pizzaiolo */
public class Person : Safe_Input
{
    public String? Name { get; set; }
    public String? Surname { get; set; }
    public Address? Address { get; set; }
    public int ID { get; set; }
    public String? e_mail { get; set; }
    public Person_Types? type { get; set; }
    public int Phone { get; set; }

    //This constructor is used when creating a person from the console
    public Person(bool buffer)
    {
        Console.WriteLine("Enter your name: ");
        Name = Console.ReadLine();
        Console.WriteLine("Enter your surname: ");
        Surname = Console.ReadLine();
        Address = new Address(true);
        Statistics statistics = Statistics.GetInstance();
        ID = statistics.Return_Free_Person_ID();
        Console.WriteLine("Enter your e-mail: ");
        e_mail = Console.ReadLine();
        Phone = Safe_Input("Enter your mobile Phone's number: ", "Error: incorrect type provided. Try again: ", 1);
    }

    //This constructor is used when feeding the program JSON files
    public Person()
    {
        Statistics statistics = Statistics.GetInstance();
        ID = statistics.Return_Free_Person_ID();
    }

    public dynamic Safe_Input(String Welcome_Text, String Error_Text, int Type)
    {
        Console.WriteLine(Welcome_Text);
        bool Break = true;
        dynamic? obj = null;
        while (Break)
        {
            switch (Type)
            {
                case 1: //For int
                    try
                    {
                        obj = int.Parse(Console.ReadLine()!);
                        Break = false;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine(Error_Text);
                    }
                    break;
                case 2: //For float
                    try
                    {
                        obj = float.Parse(Console.ReadLine()!);
                        Break = false;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine(Error_Text);
                    }
                    break;
            }
        }
        return obj!;
    }
}

public enum Person_Types
{
    Customer = 1,
    Delivery_Man = 2,
    Pizzaiolo = 3,
    Manager = 4
}