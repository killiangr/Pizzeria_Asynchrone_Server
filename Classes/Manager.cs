/* Manager Subclass that extends Persons */
using ActiveMQ.Artemis.Client;
public class Manager : Person
{
    public int Orders_Managed { get; set; }
    //Number of orders managed in total
    List<Order> Currently_Managing = new List<Order>();
    //Contains the open orders that are not closed yet that need to be managed
    public int Capacity { get; set; }
    //Manager's order capacity. Manager becomes unavailable when the Currently_Managing is full

    public Manager(bool buffer) : base(buffer)
    {
        base.type = (Person_Types)4;
        Currently_Managing = null!;
        Capacity = Safe_Input("How many orders can this manager manage at the same time ?\n", "Error: invalid type given. Try again: ", 1);
        Orders_Managed = 0;
    }

    //This constructor is called when the program is fed JSON files
    public Manager() : base()
    {
    }

    //Returns true if the manager is full
    public bool Is_Full()
    {
        return (Orders_Managed == Capacity);
    }

    //This method is used for the POV experience.
    public Order Take_Order(Customer current_Customer)
    {
        Console.WriteLine("Manager " + base.Name + " is now waiting for client " + current_Customer.Name + " to choose what he is ordering.");
        Thread.Sleep(1000);
        Order order = Statistics.GetInstance().Register_Order(current_Customer);
        Currently_Managing.Add(order);
        Console.WriteLine("Manager " + base.Name + " is now managing order " + order.ID.ToString() + " along with " + Orders_Managed.ToString() + " other orders.");
        Orders_Managed++;
        return order;
    }

    //This method is used for the async await scenario experience.
    public async Task<Order> Take_Order(Customer current_Customer, List<Pizza> Pizzas, int Sleep_Time, int Sleep_Time_2)
    {
        await Task.Delay(Sleep_Time_2);
        Console.WriteLine("Manager " + base.Name + " is now waiting for client " + current_Customer.Name + " to choose what he is ordering.");
        await Task.Delay(Sleep_Time);
        Order order = Statistics.GetInstance().Register_Order(current_Customer, Pizzas);
        Currently_Managing.Add(order);
        Console.WriteLine("Manager " + base.Name + " is now managing order " + order.ID.ToString() + " along with " + Orders_Managed.ToString() + " other orders.");
        Orders_Managed++;
        return order;
    }

    //This method is used for the sending messages broker scenario experience
    public async Task Take_Order(Customer current_Customer, List<Pizza> Pizzas, dynamic producer)
    {
        Console.WriteLine("Manager " + base.Name + " is now serving client " + current_Customer.Name + ".");
        Order order = Statistics.GetInstance().Register_Order(current_Customer, Pizzas);
        Console.WriteLine("Manager " + base.Name + " is now managing order " + order.ID.ToString() + " along with " + Orders_Managed.ToString() + " other orders.");
        Orders_Managed++;
        await producer.SendAsync(new Message(order.ID));
        Console.WriteLine("Manager " + base.Name + " has sent a message to get the command " + order.ID.ToString() + " cooked and ready.");
    }

    //This method is used to receive messages during the message broker scenario experience
    public async Task<List<string>> WaitForMessage(dynamic consumer)
    {
        List<string> all_Customer_Names = new List<string>();
        Console.WriteLine("Manager " + base.Name + " is listening...");
        while (all_Customer_Names.Count < 4)
        {
            string message = await consumer.ReceiveAsync();
            all_Customer_Names.Add(message);
            message = "";
        }
        Console.WriteLine("Manager " + base.Name + " has received all the customers' name");
        return all_Customer_Names;
    }

    public async Task Stop_Listening(dynamic consumer)
    {
        var message = await consumer.ReceiveAsync(new CancellationTokenSource().Token);
        Console.WriteLine("Manager " + base.Name + " has stopped listening...");
    }

    public void Close_Order(Order order)
    {
        for (int i = 0; i < Currently_Managing.Count; i++)
        {
            if (Currently_Managing[i] == order)
            {
                Currently_Managing.RemoveAt(i);
                break;
            }
        }
        Orders_Managed--;
    }
}