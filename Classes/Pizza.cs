/* Individual Pizza Class */
public class Pizza : Safe_Input
{
    public Size size { get; set; }
    //Enum containing the pizza's size
    public List<Pizza_Type> Pizza_Types = new List<Pizza_Type>();
    //List<Pizza_Type> containing the types of pizza
    public List<Add_On> Add_Ons = new List<Add_On>();
    //List<Add_On> containing optional Add_Ons
    public double Price { get; set; }

    //This constructor is used when we want to create a pizza without having to manually instantiating it
    public Pizza(Size size, List<Pizza_Type> pizza_Types, List<Add_On> Add_Ons)
    {
        this.size = size;
        this.Pizza_Types = pizza_Types;
        this.Add_Ons = Add_Ons;
        this.Price = Calculate_Price();
    }

    public Pizza(bool buffer)
    {

        //The While Below lets the user choose the size of pizza they want.
        bool Correct_Enum = false;
        do
        {
            size = (Size)Safe_Input("Choose Pizza Size: \n1: Small (+ 1€) \n2: Medium (+ 2€) \n3: Large (+ 3€) \nYour Choice: ", "Error: Incorrect Input Type.\n Try again: ", 1);
            //The above code transforms an int into an ENUM. Simplified Syntax: Field_Name = (Enum_Name)int
            Correct_Enum = Safe_Enum("Your number isn't in the list!", (int)size, Size.Small);
        } while (!Correct_Enum);

        Console.WriteLine("Current price: " + Calculate_Price().ToString());

        //The while below lets the user choose the type of pizza they want.
        Correct_Enum = false;
        do
        {
            Pizza_Types.Add((Pizza_Type)Safe_Input("Choose a Topping and Pizza Type: (* 1.25 €/type)\n1: Tomato_Sauce \n2: Cheese_Sauce \n3: Vegetarian \n4: All_Dressed\n5: Pineapple \n0: Save and continue \nYour Choice: ", "Error: Incorrect Input Type.\n Try again: ", 1));
            Correct_Enum = Safe_Enum("Your number isn't in the list!", (int)Pizza_Types[Pizza_Types.Count - 1], Pizza_Type.Tomato_sauce);
        } while (!Correct_Enum);
        Pizza_Types.RemoveAt(Pizza_Types.Count - 1);

        Console.WriteLine("Current price: " + Calculate_Price().ToString());

        //The while below lets the user choose the Adds_On that they want.
        Correct_Enum = false;
        do
        {
            Add_Ons.Add((Add_On)Safe_Input("Choose an Extra to your meal (* 1.5 €/add on). Skip this step by pressing 0. \n1: Coca Cola\n2: Fruit Juice \n3: Fries \n4: Nuggets \n5: Beer \nYour choice: ", "Error: Incorrect Input Type.\n Try again: ", 1));
            Correct_Enum = Safe_Enum("Your number isn't in the list!", (int)Add_Ons[Add_Ons.Count - 1], Add_On.Coca_cola);
        } while (!Correct_Enum);
        Add_Ons.RemoveAt(Add_Ons.Count - 1);

        Console.WriteLine("Final Price: " + Calculate_Price().ToString());
        this.Price = Calculate_Price();
    }

    public Pizza()
    {
        this.Price = Calculate_Price();
    }

    public double Calculate_Price()
    {
        double price = 0;
        price = price + (int)size;
        price = price * ((Pizza_Types.Count * 0.25) + 1);
        price = price * ((Add_Ons.Count * 0.5) + 1);
        return price;
    }


    /* --- THE FOLLOWING METHODS ENSURE THAT THE USER INPUT IS COMPLIANT WITH OUR CONDITIONS --- */
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

    public bool Safe_Enum(String Error_Text, int Enum_Number, Size size)
    {
        if (Enum.GetNames(typeof(Size)).Length + 1 <= Enum_Number || 0 >= Enum_Number)
        {
            Console.WriteLine(Error_Text);
            return false;
        }
        return true;
    }
    //Returns true if the int given is in the range of the enum list provided.
    //Enum.GetNames(typeof(Size)).Length returns the number of items in the enum list.

    public bool Safe_Enum(String Error_Text, int Enum_Number, Pizza_Type pizza_Type)
    {
        if (Enum.GetNames(typeof(Pizza_Type)).Length + 1 <= Enum_Number || 0 > Enum_Number)
        {
            Console.WriteLine(Error_Text);
            Pizza_Types.RemoveAt(Pizza_Types.Count - 1);
            return false;
        }
        else if (Enum_Number != 0)
        {
            Console.WriteLine("Topping Added!");
            return false;
        }
        return true;
    }
    //Returns true if the number given is 0.
    //If a wrong number is entered, it deletes the last index of the array to make sure there is no wrong entry in the list.

    public bool Safe_Enum(String Error_Text, int Enum_Number, Add_On Add_ons)
    {
        if (Enum.GetNames(typeof(Add_On)).Length + 1 <= Enum_Number || 0 > Enum_Number)
        {
            Console.WriteLine(Error_Text);
            Pizza_Types.RemoveAt(Add_Ons.Count - 1);
            return false;
        }
        else if (Enum_Number != 0)
        {
            Console.WriteLine("Extra Added!");
            return false;
        }
        return true;
    }
    //Returns true if the number given is 0.
    //If a wrong number is entered, it deletes the last index of the array to make sure there is no wrong entry in the list.
    //TODO: Make sure no number is present twice
}

public enum Size
{
    Small = 1,
    Medium = 2,
    Large = 3
}

public enum Pizza_Type
{
    Tomato_sauce = 1,
    Cheese_sauce = 2,
    Vegetarian = 3,
    All_dressed = 4,
    Pineapple = 5
}

public enum Add_On
{
    Coca_cola = 1,
    Fruit_juice = 2,
    Fries = 3,
    Nuggets = 4,
    Beer = 5
}