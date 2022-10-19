/* ORDER CLASS */
public class Order : Safe_Input
{
    public List<Pizza> Pizzas = new List<Pizza>();
    //Contains the pizzas within the order
    public double Price { get; set; }
    public Status Status { get; set; }
    //Contains enum status for the order status
    public Customer Client { get; set; }
    public int ID { get; set; }
    public DateTime Order_Time { get; set; }
    public DateTime Prepared_Time { get; set; }
    public DateTime In_Delivery_Time { get; set; }
    public DateTime Delivered_Time { get; set; }
    public DateTime Successful_Time { get; set; }
    public DateTime Lost_Time { get; set; }

    //This method is used for the POV experience
    public Order(Customer Client)
    {
        Price = 0;
        this.Client = Client;
        Statistics Statistic = Statistics.GetInstance();
        ID = Statistic.Return_Free_Order_ID();
        Status = (Status)1;
        int Choice = 0;
        do
        {
            Choice = Safe_Input("What would you like to do ? \n1: Add new Pizza \n2: Modify Order \n0: Finish order \nYour choice: ", "Error: incorrect input. Try again : ", 1);
            switch (Choice)
            {
                case 1:
                    Pizzas.Add(new Pizza(true));
                    break;
                case 2:
                    Pizzas.Clear();
                    Console.WriteLine("Order erased successfully.");
                    break;
                default:
                    break;
            }
        } while (Choice != 0);
        foreach (Pizza all_pizzas in Pizzas)
        {
            Price = all_pizzas.Price + Price;
        }
        Order_Time = DateTime.Now;
    }

    //This method is used when the program is being fed JSON files
    public Order()
    {
        Statistics Statistic = Statistics.GetInstance();
        ID = Statistic.Return_Free_Order_ID();
        Console.WriteLine("Whose client is this order from?");
        Client = Central_Server.GetInstance().Register_User(Console.ReadLine()!);
        foreach (Pizza all_pizzas in Pizzas)
        {
            Price = all_pizzas.Price + Price;
        }
    }

    //This method is used for the scenarios experience
    public Order(Customer Client, List<Pizza> Pizzas)
    {
        Price = 0;
        this.Client = Client;
        Statistics Statistic = Statistics.GetInstance();
        ID = Statistic.Return_Free_Order_ID();
        Order_Time = DateTime.Now;
        Status = (Status)1;
        this.Pizzas = Pizzas;
    }

    public string Return_Order_Status()
    {
        return Status.ToString();
    }

    public void Change_Order_Status(int order_Status_Integer)
    {
        switch (order_Status_Integer)
        {
            case 2: //When the order is being prepared
                Status = (Status)order_Status_Integer;
                Prepared_Time = DateTime.Now;
                break;
            case 3:
                Status = (Status)order_Status_Integer;
                In_Delivery_Time = DateTime.Now;
                break;
            case 4:
                Status = (Status)order_Status_Integer;
                Delivered_Time = DateTime.Now;
                break;
            case 5:
                Status = (Status)order_Status_Integer;
                Successful_Time = DateTime.Now;
                break;
            case 6:
                Status = (Status)order_Status_Integer;
                Lost_Time = DateTime.Now;
                break;
        }
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

/* ENUM for Order Status */
public enum Status
{
    waiting_for_preparation = 1,
    in_preparation = 2,
    in_delivery = 3,
    delivered = 4,
    successful = 5,
    lost = 6
}