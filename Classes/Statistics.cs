using System.Text.Json;
/* Statistics that do not fit in other classes, and a collection of all orders.
 * This class is a singleton as it does not need to be instancied more than once.
 * Hence the private constructor and static constuctor. However, it should not be read in multiple threads.
 */
public class Statistics
{
    List<Order> TimeLine = new List<Order>();
    List<dynamic> Persons = new List<dynamic>();

    //The constructor must be private so it does not get created more than once
    private Statistics() { }

    //The Statistics instance must be stored in a private static fields so it can be accessed at all times.
    private static Statistics? _instance;

    //This Method must be called to make sure that the Statistics class is unique.
    public static Statistics GetInstance()
    {
        if (_instance == null)
        {
            _instance = new Statistics();
        }
        return _instance;
    }

    public List<dynamic> Get_All_Persons()
    {
        return Persons;
    }

    //The Following method returns an Order_ID that has not been used yet.
    public int Return_Free_Order_ID()
    {
        return TimeLine.Count;
    }

    public int Return_Free_Person_ID()
    {
        return Persons.Count;
    }

    public Order Fetch_Order_By_ID(int ID)
    {
        foreach (Order all_Orders in TimeLine)
        {
            if (all_Orders.ID == ID)
            {
                return all_Orders;
            }
        }
        return null!;
    }

    //The Following method registers a new order in the TimeLine.
    public Order Register_Order(Customer Client, List<Pizza> Pizzas)
    {
        Order new_Order = new Order(Client, Pizzas);
        TimeLine.Add(new_Order);
        return new_Order;
    }

    public Order Register_Order(Customer client)
    {
        Order new_Order = new Order(client);
        TimeLine.Add(new_Order);
        return new_Order;
    }

    public void Register_Person(Person person)
    {
        Persons.Add(person);
    }

    public void Calculate_Average_Successful_Commands()
    {
        if (TimeLine == null)
        {
            Console.WriteLine("Error: no command have been made yet!");
            return;
        }
        int successful_Orders = 0;
        foreach (Order all_Orders in TimeLine)
        {
            if (all_Orders.Return_Order_Status() == "successful")
            {
                successful_Orders++;
            }
        }
        double average_Successful_Commands = Convert.ToDouble(successful_Orders) / Convert.ToDouble(TimeLine.Count);
        Console.WriteLine("Average of successful commands = " + average_Successful_Commands.ToString());
    }

    public void Display_Order(int ID)
    {
        bool Was_Displayed = false;
        foreach (Order all_orders in TimeLine)
        {
            if (all_orders.ID == ID)
            {
                Was_Displayed = true;
                Console.WriteLine("\n#-------------------#" + "\nORDER ID: " + all_orders.ID + "\nPRICE: " + all_orders.Price + "\nORDERING CUTOMER: " + all_orders.Client.Name + "\nCURRENT STATUS: " + all_orders.Status);
                Print_DateTime_If_Not_Null(all_orders.Order_Time, "Order was filed at ");
                Print_DateTime_If_Not_Null(all_orders.Prepared_Time, "Prepration finished at ");
                Print_DateTime_If_Not_Null(all_orders.In_Delivery_Time, "Order was picked by a delivery man at ");
                Print_DateTime_If_Not_Null(all_orders.Delivered_Time, "Order was delivered at ");
                Print_DateTime_If_Not_Null(all_orders.Successful_Time, "Order was successfully closed at ");
                Print_DateTime_If_Not_Null(all_orders.Lost_Time, "Order was lost at ");
                Console.WriteLine("<---------------------->" + "\n        CONTENTS");
                foreach (Pizza all_pizzas in all_orders.Pizzas)
                {
                    Console.WriteLine("SIZE: " + all_pizzas.size);
                    Console.WriteLine("TYPES: " + all_pizzas.size);
                    foreach (Pizza_Type all_types in all_pizzas.Pizza_Types)
                    {
                        Console.WriteLine("    " + all_types.ToString());
                    }
                    if (all_pizzas.Add_Ons != null)
                    {
                        Console.WriteLine("ADD ONS:");
                        foreach (Add_On all_add_ons in all_pizzas.Add_Ons)
                        {
                            Console.WriteLine("    " + all_add_ons.ToString());
                        }
                    }
                }
                Console.WriteLine("#-------------------#");
            }
        }
        if (!Was_Displayed)
        {
            Console.WriteLine("Error: couldn't find order ID.");
        }
    }

    public void Display_Managers_Performance()
    {
        Console.WriteLine("\n#-------------------#");
        foreach (Manager all_managers in Persons.OfType<Manager>())
        {
            Console.WriteLine("Manager " + all_managers.Name + " has successfully managed " + all_managers.Orders_Managed + " orders.");
        }
        Console.WriteLine("#-------------------#");
    }

    public void Display_Delivery_Man_Performance()
    {
        Console.WriteLine("\n#-------------------#");
        foreach (Delivery_Man all_Delivery_Men in Persons.OfType<Delivery_Man>())
        {
            Console.WriteLine("Delivery Man " + all_Delivery_Men.Name + " has attempted " + all_Delivery_Men.deliveries_Made + " deliveries, " + all_Delivery_Men.successful_Deliveries + " of which were successful.");
        }
        Console.WriteLine("#-------------------#");
    }

    public void Display_TimeLine()
    {
        Console.WriteLine("\n#-------------------#" + "\n Displaying Timeline");
        List<DateTime> chronologically_Ordered_Timeline = new List<DateTime>();
        //First, let's add every valid date inside the new Array
        foreach (Order all_orders in TimeLine)
        {
            Add_If_Not_Null(all_orders.Order_Time, chronologically_Ordered_Timeline);
            Add_If_Not_Null(all_orders.Prepared_Time, chronologically_Ordered_Timeline);
            Add_If_Not_Null(all_orders.In_Delivery_Time, chronologically_Ordered_Timeline);
            Add_If_Not_Null(all_orders.Delivered_Time, chronologically_Ordered_Timeline);
            Add_If_Not_Null(all_orders.Successful_Time, chronologically_Ordered_Timeline);
            Add_If_Not_Null(all_orders.Lost_Time, chronologically_Ordered_Timeline);
        }
        //Then, we sort the array chronologically
        chronologically_Ordered_Timeline.Sort(delegate (DateTime x, DateTime y) { return y.CompareTo(x); });
        //We reverse it because it's anti-chronological
        chronologically_Ordered_Timeline.Reverse();
        //Finally, we find the occurence of each date, and display the corresponding order and event
        foreach (DateTime all_dates in chronologically_Ordered_Timeline)
        {
            foreach (Order all_orders in TimeLine)
            {
                if (all_orders.Order_Time != null && DateTime.Compare(all_orders.Order_Time, all_dates) == 0)
                {
                    Console.WriteLine("Order ID : " + all_orders.ID + " was ordered at " + all_orders.Order_Time.ToString("MM/dd/yyyy HH:mm"));
                }

                else if (all_orders.Prepared_Time != null && DateTime.Compare(all_orders.Prepared_Time, all_dates) == 0)
                {
                    Console.WriteLine("Order ID : " + all_orders.ID + " had finished cooking at " + all_orders.Prepared_Time.ToString("MM/dd/yyyy HH:mm"));
                }

                else if (all_orders.In_Delivery_Time != null && DateTime.Compare(all_orders.In_Delivery_Time, all_dates) == 0)
                {
                    Console.WriteLine("Order ID : " + all_orders.ID + " was picked up by a delivery man at " + all_orders.In_Delivery_Time.ToString("MM/dd/yyyy HH:mm"));
                }

                else if (all_orders.Delivered_Time != null && DateTime.Compare(all_orders.Delivered_Time, all_dates) == 0)
                {
                    Console.WriteLine("Order ID : " + all_orders.ID + " had finished cooking at " + all_orders.Delivered_Time.ToString("MM/dd/yyyy HH:mm"));
                }

                else if (all_orders.Successful_Time != null && DateTime.Compare(all_orders.Successful_Time, all_dates) == 0)
                {
                    Console.WriteLine("Order ID : " + all_orders.ID + " was successfully closed at " + all_orders.Successful_Time.ToString("MM/dd/yyyy HH:mm"));
                }

                else if (all_orders.Lost_Time != null && DateTime.Compare(all_orders.Prepared_Time, all_dates) == 0)
                {
                    Console.WriteLine("Order ID : " + all_orders.ID + " was lost at " + all_orders.Order_Time.ToString("MM/dd/yyyy HH:mm"));
                }
            }
        }
        Console.WriteLine("#-------------------#");
    }

    public void Add_If_Not_Null(DateTime new_Time, List<DateTime> C_O_Timeline)
    {
        if (new_Time != null)
        {
            C_O_Timeline.Add(new_Time);
        }
    }

    public void Print_DateTime_If_Not_Null(DateTime order_Time, String flavor_Text)
    {
        if (order_Time != null)
        {
            Console.WriteLine(flavor_Text + order_Time.ToString("MM/dd/yyyy HH:mm"));
        }
    }

    public void Create_Customer()
    {
        Persons.Add(new Customer(true));
    }

    public void Create_Pizzaiolo()
    {
        Persons.Add(new Pizzaiolo(true));
    }

    public void Create_Manager()
    {
        Persons.Add(new Manager(true));
    }

    public void Create_Delivery_Man()
    {
        Persons.Add(new Delivery_Man(true));
    }

    public void Populate_Pizzeria()
    {
        Load_Customers();
        Load_Delivery_Men();
        Load_Managers();
        Load_Pizzaiolos();
    }

    public void Depopulate_Pizzeria()
    {
        Persons.Clear();
    }

    public void Load_Customers()
    {
        int i = 0;
        bool Stop = false;
        while (!Stop)
        {
            i++;
            try
            {
                String path = "Persons_Archive/Customer_" + i.ToString() + ".json";
                String text = File.ReadAllText(path);
                Persons.Add(JsonSerializer.Deserialize<Customer>(text)!);
            }
            catch (Exception)
            {
                Stop = true;
            }
        }
    }

    public void Load_Customers(String file_Name)
    {
        try
        {
            String text = File.ReadAllText("Persons_Archive/" + file_Name + ".json");
            Persons.Add(JsonSerializer.Deserialize<Customer>(text)!);
        }
        catch (Exception)
        {
            Console.WriteLine("Error: File " + "Persons_Archive/" + file_Name + ".json" + " not found.");
        }
    }

    public void Load_Managers(String file_Name)
    {
        try
        {
            String text = File.ReadAllText("Persons_Archive/" + file_Name + ".json");
            Persons.Add(JsonSerializer.Deserialize<Manager>(text)!);
        }
        catch (Exception)
        {
            Console.WriteLine("Error: File " + "Persons_Archive/" + file_Name + ".json" + " not found.");
        }
    }

    public void Load_Delivery_Men(String file_Name)
    {
        try
        {
            String text = File.ReadAllText("Persons_Archive/" + file_Name + ".json");
            Persons.Add(JsonSerializer.Deserialize<Delivery_Man>(text)!);
        }
        catch (Exception)
        {
            Console.WriteLine("Error: File " + "Persons_Archive/" + file_Name + ".json" + " not found.");
        }
    }

    public void Load_Pizzaiolos(String file_Name)
    {
        try
        {
            String text = File.ReadAllText("Persons_Archive/" + file_Name + ".json");
            Persons.Add(JsonSerializer.Deserialize<Pizzaiolo>(text)!);
        }
        catch (Exception)
        {
            Console.WriteLine("Error: File " + "Persons_Archive/" + file_Name + ".json" + " not found.");
        }
    }

    public void Load_Delivery_Men()
    {
        int i = 0;
        bool Stop = false;
        while (!Stop)
        {
            i++;
            try
            {
                String path = "Persons_Archive/Delivery_Man_" + i.ToString() + ".json";
                String text = File.ReadAllText(path);
                Persons.Add(JsonSerializer.Deserialize<Delivery_Man>(text)!);
            }
            catch (Exception)
            {
                Stop = true;
            }
        }
    }

    public void Load_Managers()
    {
        int i = 0;
        bool Stop = false;
        while (!Stop)
        {
            i++;
            try
            {
                String path = "Persons_Archive/Manager_" + i.ToString() + ".json";
                String text = File.ReadAllText(path);
                Persons.Add(JsonSerializer.Deserialize<Manager>(text)!);
            }
            catch (Exception)
            {
                Stop = true;
            }
        }
    }

    public void Load_Pizzaiolos()
    {
        int i = 0;
        bool Stop = false;
        while (!Stop)
        {
            i++;
            try
            {
                String path = "Persons_Archive/Pizzaiolo_" + i.ToString() + ".json";
                String text = File.ReadAllText(path);
                Persons.Add(JsonSerializer.Deserialize<Pizzaiolo>(text)!);
            }
            catch (Exception)
            {
                Stop = true;
            }
        }
    }

    public Order Load_Order(String fileName)
    {
        Order? new_Order = null;
        //try
        //{
            String path = "Persons_Archive/" + fileName;
            String text = File.ReadAllText(path);
            new_Order = JsonSerializer.Deserialize<Order>(text);
        //}
        //catch (Exception)
        //{
            //Console.WriteLine("Error: couldn't find " + fileName + " in the Persons_Archive folder.");
        //}
        TimeLine.Add(new_Order!);
        return new_Order!;
    }

    public void Save_Orders()
    {
        int i = 1;
        bool Stop = false;
        while (!Stop)
        {
            String path = "Persons_Archive/Order_" + i.ToString() + ".json";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            else
            {
                Stop = true;
            }
            i++;
        }
        i = 1;
        foreach (Order all_Orders in TimeLine.OfType<Order>())
        {
            String jsonString = JsonSerializer.Serialize(all_Orders);
            File.WriteAllText("Persons_Archive/Order_" + i.ToString() + ".json", jsonString);
            i++;
        }
    }


    public void Save_Customers()
    {
        int i = 1;
        bool Stop = false;
        while (!Stop)
        {
            String path = "Persons_Archive/Customer_" + i.ToString() + ".json";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            else
            {
                Stop = true;
            }
            i++;
        }
        i = 1;
        foreach (Customer all_Customers in Persons.OfType<Customer>())
        {
            String jsonString = JsonSerializer.Serialize(all_Customers);
            File.WriteAllText("Persons_Archive/Customer_" + i.ToString() + ".json", jsonString);
            i++;
        }
    }

    public void Save_Delivery_Men()
    {
        int i = 1;
        bool Stop = false;
        while (!Stop)
        {
            try
            {
                String path = "Persons_Archive/Delivery_Man_" + i.ToString() + ".json";
                if (File.Exists(path)) {
                    File.Delete(path);
                }
            }
            catch (Exception)
            {
                Stop = true;
            }
            i++;
        }
        i = 1;
        foreach (Delivery_Man all_Delivery_Man in Persons.OfType<Delivery_Man>())
        {
            String jsonString = JsonSerializer.Serialize(all_Delivery_Man);
            File.WriteAllText("Persons_Archive/Delivery_Man_" + i.ToString() + ".json", jsonString);
            i++;
        }
    }

    public void Save_Managers()
    {
        int i = 1;
        bool Stop = false;
        while (!Stop)
        {
            try
            {
                String path = "Persons_Archive/Manager_" + i.ToString() + ".json";
                if (File.Exists(path)) {
                    File.Delete(path);
                }
            }
            catch (Exception)
            {
                Stop = true;
            }
            i++;
        }
        i = 1;
        foreach (Manager all_Managers in Persons.OfType<Manager>())
        {
            String jsonString = JsonSerializer.Serialize(all_Managers);
            File.WriteAllText("Persons_Archive/Manager_" + i.ToString() + ".json", jsonString);
            i++;
        }
    }

    public void Save_Pizzaiolos()
    {
        int i = 1;
        bool Stop = false;
        while (!Stop)
        {
            try
            {
                String path = "Persons_Archive/Pizzaiolos_" + i.ToString() + ".json";
                if (File.Exists(path)) {
                    File.Delete(path);
                }
            }
            catch (Exception)
            {
                Stop = true;
            }
            i++;
        }
        i = 1;
        foreach (Pizzaiolo all_Pizzaiolos in Persons.OfType<Pizzaiolo>())
        {
            String jsonString = JsonSerializer.Serialize(all_Pizzaiolos);
            File.WriteAllText("Persons_Archive/Pizziolo_" + i.ToString() + ".json", jsonString);
            i++;
        }
    }
}