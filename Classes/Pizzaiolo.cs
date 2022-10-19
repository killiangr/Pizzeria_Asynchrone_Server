/* Pizzaiolo subclass that extends Persons */
using ActiveMQ.Artemis.Client;
public class Pizzaiolo : Person
{
    Order? order_Taken = null;
    Cooking_Status status = Cooking_Status.Free;
    //Order currently being taken care of

    public Pizzaiolo(bool buffer) : base(buffer)
    {
        base.type = (Person_Types)3;
    }

    public Pizzaiolo() : base()
    {
    }

    public void Change_Status(int cooking_Status_Integer)
    {
        if (cooking_Status_Integer == 1 || cooking_Status_Integer == 2)
        {
            status = (Cooking_Status)cooking_Status_Integer;
        }
        else
        {

            Console.WriteLine("Error: incorrect input in Pizzaiolo.Change_Status. Defaulted to free.");
            status = Cooking_Status.Free;
        }
    }

    public string Return_Status()
    {
        return status.ToString();
    }

    public void Accept_Order(Order order)
    {
        order_Taken = order;
        status = Cooking_Status.Cooking;
        Task.Delay(1000).Wait();
        Finish_Cooking();
    }

    public void Finish_Cooking()
    {
        Order? temp_Order = order_Taken;
        status = Cooking_Status.Free;
        order_Taken = null;
    }

    public async Task Listen_For_Orders(dynamic consumer, dynamic producer)
    {
        Statistics statitics = Statistics.GetInstance();
        int message = await consumer.ReceiveAsync();
        order_Taken = statitics.Fetch_Order_By_ID(message);
        status = Cooking_Status.Cooking;
        status = Cooking_Status.Free;
        Console.WriteLine("Pizzaiolo " + base.Name + " has finished cooking order " + order_Taken.ID + ".");
        Send_Finished_Message(producer);
    }

    public async Task Stop_Listening(dynamic consumer)
    {
        var message = await consumer.ReceiveAsync(new CancellationTokenSource().Token);
        Console.WriteLine("The pizzaiolo has stopped listening");
    }

    public async Task Send_Finished_Message(dynamic producer)
    {
        await producer.SendAsync(new Message(order_Taken!.ID));
        Console.WriteLine("The pizzaiolo has sent a message containing the order ID" + order_Taken.ID + ".");
    }
}

public enum Cooking_Status
{
    Free = 1,
    Cooking = 2
}