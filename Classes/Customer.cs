/* Customer subclass that extends Persons */
using ActiveMQ.Artemis.Client;
public class Customer : Person
{
    List<Order>? Orders_Ordered = new List<Order>();
    public int Successful_Orders { get; set; }
    public float Money_Spent { get; set; }

    public DateTime First_Order_Date { get; set; }

    //                           \/This calls the SuperClass constructor, and then continues with this constructor.
    public Customer(bool buffer) : base(buffer)
    {
        base.type = (Person_Types)1;
        Orders_Ordered = null;
        First_Order_Date = DateTime.Now;
        Money_Spent = 0;
    }

    //This constructor is called when the program is fed a JSON file
    public Customer() : base()
    {
    }

    //This method is used to call the Manager and let them know that a client is there to give an order.
    public async Task Customer_Calls(dynamic producer)
    {
        await producer.SendAsync(new Message(base.Name));
        Console.WriteLine("Customer " + base.Name + " has sent a message.");
    }
}