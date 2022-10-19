/* Address Class used in Person class */
public class Address : Safe_Input
{
    public String? City { get; set; }
    public int Number { get; set; }
    public String? Street { get; set; }
    public int Zip_Code { get; set; }

    public Address(bool Buffer)
    {
        Console.WriteLine("Enter your city: ");
        City = Console.ReadLine();
        Number = Safe_Input("Enter your Living quarter's number: ", "Error: incorrect type provided. Try again: ", 1);
        Console.WriteLine("Enter the name of your street: ");
        Street = Console.ReadLine();
        Zip_Code = Safe_Input("Enter your Zip Code: ", "Error: incorrect type provided. Try again: ", 1);
    }

    //This constructor is used when the program is being fed JSON files.
    public Address() { }

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