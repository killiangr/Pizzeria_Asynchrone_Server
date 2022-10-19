/* Delivery_Man subclass that extends Persons */
public class Delivery_Man : Person
{
    public int deliveries_Made { get; set; }
    public int successful_Deliveries { get; set; }
    Order? order_Taken = null;
    Delivery_Status? status = Delivery_Status.Free;

    //This constructor is used when creating a Delivery_Man from the console
    public Delivery_Man(bool buffer) : base(buffer)
    {
        base.type = (Person_Types)2;
        deliveries_Made = 0;
        successful_Deliveries = 0;
    }

    //This constructor is used when importing a JSON file
    public Delivery_Man() : base()
    {
    }

    //The status changes depending on the delivery man's location.
    public void Change_Status(int delivery_Status_Integer)
    {
        if (delivery_Status_Integer >= 1 && delivery_Status_Integer <= 3)
        {
            status = (Delivery_Status)delivery_Status_Integer;
        }
        else
        {
            Console.WriteLine("Error: incorrect input in Delivery_Man.Change_Status. Defaulted to free.");
            status = Delivery_Status.Free;
        }
    }

    //This method is called to ask if the delivery man is free or not
    public string Return_Status()
    {
        return status.ToString()!;
    }

    //This method is called when a new order is being acknowledged and picked up
    public void Take_Order(Order order)
    {
        Console.WriteLine("Delivery Man " + base.Name + " has taken order ID " + order.ID.ToString() + " and is delivering it.");
        order_Taken = order;
        Change_Status(1);
        order.Change_Order_Status(3);
        Task.Delay(1000).Wait();
        Lose_Or_Deliver_Order_Random();
    }

    //[!] CALL Lose_Or_Deliver_Order_Random FUNCTION FOR A CHANCE TO LOSE THE ORDER
    public void Deliver_Order()
    {
        Console.WriteLine("Delivery Man " + base.Name + " has delivered order ID " + order_Taken!.ID.ToString() + " and returning to the Pizza place.");
        Change_Status(2);
        order_Taken.Change_Order_Status(4);
        Task.Delay(1000).Wait();
        Finish_Order();
    }

    public void Finish_Order()
    {
        Console.WriteLine("Delivery Man " + base.Name + " has returned to the pizza place and is giving the money and invoice to the assigned manager.");
        Change_Status(1);
        deliveries_Made++;
        successful_Deliveries++;
        order_Taken!.Change_Order_Status(5);
        Task.Delay(1000).Wait();
        order_Taken = null;
    }

    public void Lose_Order()
    {
        Console.WriteLine("Delivery Man " + base.Name + " has lost the order " + order_Taken!.ID.ToString() + " :(\n He is coming back to the pizza place.");
        Change_Status(1);
        order_Taken.Change_Order_Status(6);
        Task.Delay(1000).Wait();
        order_Taken = null;
    }

    //CALL THIS FUNCTION INSTEAD OF DELIVER ORDER
    public void Lose_Or_Deliver_Order_Random()
    {
        var Rand = new Random();
        //Generates a number between 1 and 11. If 1 is chosen, the Delivery Man loses the command.
        if (Rand.Next(1, 11) == 1)
        {
            Lose_Order();
        }
        else
        {
            Deliver_Order();
        }
    }

    public async Task<List<int>> Receive_Message(dynamic consumer)
    {
        List<int> order_IDs = new List<int>();
        Console.WriteLine("Delivery Man " + base.Name + " has started listening");
        while (order_IDs.Count != 4)
        {
            int message = await consumer.ReceiveAsync();
            order_IDs.Add(message);
        }
        return order_IDs;
    }

    public async Task Stop_Listening(dynamic consumer)
    {
        var message = await consumer.ReceiveAsync(new CancellationTokenSource().Token);
        Console.WriteLine("Delivery Man " + base.Name + " has stopped listening");
    }
}

public enum Delivery_Status
{
    Free = 1,
    Delivering_Order = 2,
    Coming_Back = 3
}