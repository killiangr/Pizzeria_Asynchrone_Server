/*This class will be the equivalent of the Pizzeria's server through which the different requests will pass. It does not contain any field, only methods to pass informations between all actors*/
public class Central_Server
{
    //The constructor must be private so it does not get created more than once
    private Central_Server() { }

    //The Statistics instance must be stored in a private static fields so it can be accessed at all times.
    private static Central_Server? _instance;

    //This Method must be called to make sure that the Statistics class is unique.
    public static Central_Server GetInstance()
    {
        if (_instance == null)
        {
            _instance = new Central_Server();
        }
        return _instance;
    }

    //This method simulates how an order is taken care of.
    //This is ONLY to get the POV of an ordering customer.
    //For an out-of-body experience, please refer to Scenarios/All_Scenarios.
    public void Main_Transaction()
    {
        //First, the customer registers
        Customer ordering_Customer = Register_User();
        //Then, we find a manager to take the order
        Manager assigned_Manager = Find_Manager();
        //After that, the client chooses what he wants
        Order current_order = Manager_Handle_Order(assigned_Manager, ordering_Customer);
        //We find a free cook to cook the order
        Pizzaiolo assigned_Pizzaiolo = Find_Pizzaiolo();
        //The cook cooks the food
        Pizzaiolo_Accept_Order(assigned_Pizzaiolo, current_order);
        //We find a delivery man
        Delivery_Man assigned_Delivery_Man = Find_Delivery_Man();
        //The delivery man deliers the food
        Deliver_Food(assigned_Delivery_Man, current_order);
        //The manager closes the order
        Terminate_Order(assigned_Manager, current_order);
    }

    //This method lets the user either create a new user, or register as an already existing user.
    public Customer Register_User()
    {
        Console.WriteLine("Which client is getting this order?");
        Customer? ordering_Customer = null;

        while (ordering_Customer == null)
        {
            Console.WriteLine("Are you a new customer? y/n");
            if (Console.ReadLine() == "y")
            {
                ordering_Customer = new Customer();
                break;
            }
            Console.WriteLine("Would you like a list of all customers ? y/n");
            if (Console.ReadLine() == "y")
            {
                Print_Customers();
            }
            Console.WriteLine("Enter your name: ");
            String? name = Console.ReadLine();
            ordering_Customer = Register_User(name!);
        }
        return ordering_Customer;
    }

    //This method is an overload of the previous one, as it registers the user directly by name.
    public Customer Register_User(String name)
    {
        Customer? ordering_Customer = null;
        name = name.ToLower();
        foreach (Customer all_customers in Statistics.GetInstance().Get_All_Persons().OfType<Customer>())
        {
            if (name == all_customers.Name!.ToLower())
            {
                ordering_Customer = all_customers;
                break;
            }
        }
        return ordering_Customer!;
    }

    //This method finds a manager that is ready to take an order.
    public Manager Find_Manager()
    {
        Manager? assigned_Manager = null;
        while (assigned_Manager == null)
        {
            foreach (Manager all_manager in Statistics.GetInstance().Get_All_Persons().OfType<Manager>())
            {
                if (!all_manager.Is_Full())
                {
                    assigned_Manager = all_manager;
                    break;
                }
                else
                {
                    Console.WriteLine("Manager full!");
                }
            }
            Thread.Sleep(2000);
        }
        return assigned_Manager;
    }

    //This method lets a manager pick up and manage an order
    public Order Manager_Handle_Order(Manager assigned_Manager, Customer current_Customer)
    {
        Order current_Order = assigned_Manager.Take_Order(current_Customer);
        return current_Order;
    }

    //This method finds an available pizzaiolo
    public Pizzaiolo Find_Pizzaiolo()
    {
        Pizzaiolo? assigned_Pizzaiolo = null;
        //Loop below keeps going until there is a free pizzaiolo available
        while (assigned_Pizzaiolo == null)
        {
            //For every Pizzaiolo present in the Person class (Assured by the .OfType<Pizzaiolo>)
            //We also need to refresh the list everytime so we copy it again and again and again
            foreach (Pizzaiolo all_pizzaiolo in Statistics.GetInstance().Get_All_Persons().OfType<Pizzaiolo>())
            {
                if (all_pizzaiolo.Return_Status() == "Free")
                {
                    all_pizzaiolo.Change_Status(2);
                    assigned_Pizzaiolo = all_pizzaiolo;
                    break;
                }
            }
            //If it is not found, wait 2 seconds before trying again
            Thread.Sleep(2000);
        }
        return assigned_Pizzaiolo;
    }

    //This method lets a pizzaiolo take an order and cook it
    public void Pizzaiolo_Accept_Order(Pizzaiolo assigned_Pizzaiolo, Order current_Order)
    {
        assigned_Pizzaiolo.Accept_Order(current_Order);
    }

    //This method goes through all the persons to find an available delivery man
    public Delivery_Man Find_Delivery_Man()
    {
        Delivery_Man? assigned_Delivery_Man = null;
        while (assigned_Delivery_Man == null)
        {
            foreach (Delivery_Man all_Delivery_Man in Statistics.GetInstance().Get_All_Persons().OfType<Delivery_Man>())
            {
                if (all_Delivery_Man.Return_Status() == "Free")
                {
                    assigned_Delivery_Man = all_Delivery_Man;
                    break;
                }
            }
            Thread.Sleep(2000);
        }
        return assigned_Delivery_Man;
    }

    //This method lets the delivery man deliver the food when it is time
    public void Deliver_Food(Delivery_Man assigned_Delivery_Man, Order current_Order)
    {
        assigned_Delivery_Man.Take_Order(current_Order);
    }

    //This method lets the manager know that it is timme to close the order
    public void Terminate_Order(Manager assigned_Manager, Order current_Order)
    {
        assigned_Manager.Close_Order(current_Order);
    }

    //This method simply prints the name of all customers
    public void Print_Customers()
    {
        Console.WriteLine("#------------------------#");
        foreach (Customer all_customers in Statistics.GetInstance().Get_All_Persons().OfType<Customer>())
        {
            Console.WriteLine(all_customers.Name);
        }
        Console.WriteLine("#------------------------#");
    }
}