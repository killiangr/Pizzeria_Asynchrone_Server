using ActiveMQ.Artemis.Client;
public class All_Scenarios : Safe_Input
{

    public All_Scenarios() { }

    public void Main_Menu()
    {
        //First, we load the pizzeria no matter what:
        Statistics.GetInstance().Populate_Pizzeria();
        //Then, we enter the choice part:
        bool End_Simulation = false;
        int Choice;
        while (!End_Simulation)
        {
            Console.WriteLine("Welcome to PIZZA TIME, please choose your menu!");
            Console.WriteLine("0: Exit");
            Console.WriteLine("1: Manage Customers");
            Console.WriteLine("2: Manage Managers");
            Console.WriteLine("3: Manage Delivery_Men");
            Console.WriteLine("4: Manage Pizzaiolos");
            Console.WriteLine("5: Access Statistics");
            Console.WriteLine("6: Place an order as a Customer");
            Console.WriteLine("7: Access Scenarios");
            Console.WriteLine("8: Load Orders");
            Console.WriteLine("9: Save Orders");
            Choice = Safe_Input("Your Choice: ", "Error: incorrect input type detected. Try again: ", 1);
            switch (Choice)
            {
                case 0:
                    End_Simulation = true;
                    break;
                case 1:
                    Manage("Customer");
                    break;
                case 2:
                    Manage("Manager");
                    break;
                case 3:
                    Manage("Delivery_Man");
                    break;
                case 4:
                    Manage("Pizzaiolo");
                    break;
                case 5:
                    Access_Statistics();
                    break;
                case 6:
                    Central_Server.GetInstance().Main_Transaction();
                    break;
                case 7:
                    Access_Scenarios();
                    break;
                case 8:
                    Console.WriteLine("Enter the file's name to load the corresponding order!\n File's name: ");
                    Statistics.GetInstance().Load_Order(Console.ReadLine()!);
                    break;
                case 9:
                    Statistics.GetInstance().Save_Orders();
                    break;
                default:
                    Console.WriteLine("Error: incorrect number input. Try again.");
                    break;
            }
        }
    }

    public void Manage(String Who_To_Manage)
    {
        bool exit = false;
        int Choice;
        while (!exit)
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("0: Exit the Manage Menu");
            Console.WriteLine("1: Add a " + Who_To_Manage);
            Console.WriteLine("2: Save all " + Who_To_Manage + "s");
            Console.WriteLine("3: Delete a " + Who_To_Manage);
            Choice = Safe_Input("Your Choice: ", "Error: incorrect input type. Try again: ", 1);
            switch (Who_To_Manage)
            {
                case "Customer":
                    Manage_Customers(Choice);
                    break;
                case "Manager":
                    Manage_Managers(Choice);
                    break;
                case "Delivery_Man":
                    Manage_Delivery_Men(Choice);
                    break;
                case "Pizzaiolo":
                    Manage_Pizzaiolos(Choice);
                    break;
                default:
                    Console.WriteLine("Error: incorrect number input. Try again.");
                    break;
            }
            exit = true;
        }
    }
    public void Manage_Customers(int Choice)
    {
        switch (Choice)
        {
            case 1:
                Statistics.GetInstance().Create_Customer();
                break;
            case 2:
                Statistics.GetInstance().Save_Customers();
                break;
            case 3:
                //TODO: make a way to delete a customer
                break;
            default:
                break;
        }
    }

    public void Manage_Managers(int Choice)
    {
        switch (Choice)
        {
            case 1:
                Statistics.GetInstance().Create_Manager();
                break;
            case 2:
                Statistics.GetInstance().Save_Managers();
                break;
            case 3:
                //TODO: make a way to delete a customer
                break;
            default:
                break;
        }
    }

    public void Manage_Delivery_Men(int Choice)
    {
        switch (Choice)
        {
            case 1:
                Statistics.GetInstance().Create_Delivery_Man();
                break;
            case 2:
                Statistics.GetInstance().Save_Delivery_Men();
                break;
            case 3:
                //TODO: make a way to delete a customer
                break;
            default:
                break;
        }
    }

    public void Manage_Pizzaiolos(int Choice)
    {
        switch (Choice)
        {
            case 1:
                Statistics.GetInstance().Create_Pizzaiolo();
                break;
            case 2:
                Statistics.GetInstance().Save_Pizzaiolos();
                break;
            case 3:
                //TODO: make a way to delete a customer
                break;
            default:
                break;
        }
    }

    public void Access_Statistics()
    {
        Console.WriteLine("STATISTICS:");
        Console.WriteLine("What would you like to do?");
        Console.WriteLine("0: Exit");
        Console.WriteLine("1: Display Average successful command");
        Console.WriteLine("2: Display order by ID");
        Console.WriteLine("3: Display Manager Performances");
        Console.WriteLine("4: Display Delivery Man Performance");
        Console.WriteLine("5: Display Timeline");
        int Choice = Safe_Input("Your choice:", "Error: Incorrect type provided. Try again: ", 1);
        switch (Choice)
        {
            case 0:
                break;
            case 1:
                Statistics.GetInstance().Calculate_Average_Successful_Commands();
                break;
            case 2:
                Choice = Safe_Input("What Order would you like to display? Enter an ID: ", "Error: incorrect input type provided. Try again: ", 1);
                Statistics.GetInstance().Display_Order(Choice);
                break;
            case 3:
                Statistics.GetInstance().Display_Managers_Performance();
                break;
            case 4:
                Statistics.GetInstance().Display_Delivery_Man_Performance();
                break;
            case 5:
                Statistics.GetInstance().Display_TimeLine();
                break;
            default:
                Console.WriteLine("Error: incorrect number input. Going back to main menu.");
                break;
        }
    }

    public void Access_Scenarios()
    {
        Console.WriteLine("Which Scenario would you like to access? Type 0 to go back.");
        int Choice = Safe_Input("Enter your number here: ", "Error: incorrect input type provided. Try again: ", 1);
        switch (Choice)
        {
            case 0:
                break;
            case 1:
                //This waits until the methods finishes in order to move on.
                Task.Run(() => Scenario_1()).Wait();
                Statistics.GetInstance().Populate_Pizzeria();
                break;
            case 2:
                Task.Run(() => Scenario_2()).Wait();
                Statistics.GetInstance().Populate_Pizzeria();
                break;
            case 3:
                Scenario_3();
                Statistics.GetInstance().Populate_Pizzeria();
                break;
            default:
                Console.WriteLine("Error: incorrect number input. Returning to main menu.");
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

    /* Scénario 0: Normal POV
     * Actors: unimited
     * Presentation of a normal menu, from which you can access scenarios.
     */


    /* Scenario 1: Everything in Async
     * Actors: 4 clients, 1 assistant téléphonique, 1 cuisinier, 1 livreur
     * Pour l'assistant:
     *  a) les clients vont appeler tous en même temps
     *  b) l'assistant prend un client, client 1, il prend ses coordonnées, et il lui propose unmenu pour faire ses choix
     *  c) le client 1 prend du temps à choisir du menu, en attendant, l'assistant prend unautre client au téléphone, par exemple client 2
     *  d) l'assistant refait la même chose avec le client 2, lorsque le client 2 prend sontemps à regarder le menu, 
     *     l'assistant reprend le client 1 s'il a fait son choix oubien il prend un autre client ...
     *  e) une fois une commande pour un client est validé, l'assistant envoie la commande au cuisinier
     *
     * Pour le cuisinier:
     *  a) il prend une commande
     *  b) Il doit lancer la réparation de toutes les pizzas de la commande (en asynchrone)
     *  c) Chaque pizza a une méthode de préparation qui elle même est en asynchrone
     *  d) La commande est validé pour livraison lorsque tous les pizzas sont prêts
     *  e) Bonus: le cuisinier doit pouvoir lancer la préparation de plusieurs pizzas deplusieurs commandes en asynchrone
     *  f) Le cuisinier envoie la commande au livreur
     *
     * Pour le livreur:
     *  a) Livrer la commande (changement de statut)
     */
    public async Task Scenario_1()
    {
        //First, we make sure we load the right amount of people for this scenario. We get rid of everyone first:
        Statistics statistics = Statistics.GetInstance();
        Central_Server central_Server = Central_Server.GetInstance();
        statistics.Depopulate_Pizzeria();
        //Then, we load everyone needed for this operation:
        statistics.Load_Customers("Customer_1");
        statistics.Load_Customers("Customer_2");
        statistics.Load_Customers("Customer_3");
        statistics.Load_Customers("Customer_4");
        statistics.Load_Delivery_Men("Delivery_Man_1");
        statistics.Load_Managers("Manager_1");
        statistics.Load_Pizzaiolos("Pizzaiolo_1");
        //We load every actor in this scenario. First, the customers
        Customer customer_1 = central_Server.Register_User("Omnom");
        Customer customer_2 = central_Server.Register_User("BigBoi");
        Customer customer_3 = central_Server.Register_User("Kirby");
        Customer customer_4 = central_Server.Register_User("Shork");
        Console.WriteLine("Looking for manager...");
        //Second, the Manager
        Manager manager = central_Server.Find_Manager();
        Console.WriteLine("Manager found!");
        //Third, the Delivery Man
        Console.WriteLine("Looking for delivery man...");
        Delivery_Man delivery_Man = central_Server.Find_Delivery_Man();
        Console.WriteLine("Delivery man found!");
        //Fourth, the pizzaiolo
        Console.WriteLine("Looking for pizzaiolo...");
        Pizzaiolo pizzaiolo = central_Server.Find_Pizzaiolo();
        Console.WriteLine("Pizzaiolo found!");
        //Then, we get on with the scenario:
        //First, the clients all call. The manager will take client one.
        Task<Order> order_1_Task = manager.Take_Order(customer_1, new List<Pizza> { new Pizza(Size.Large, new List<Pizza_Type> { (Pizza_Type)1, (Pizza_Type)2 }, new List<Add_On> { (Add_On)1, (Add_On)2 }) }, 2500, 0);
        Task<Order> order_2_Task = manager.Take_Order(customer_2, new List<Pizza> { new Pizza(Size.Small, new List<Pizza_Type> { (Pizza_Type)2, (Pizza_Type)3 }, new List<Add_On> { (Add_On)1, (Add_On)2 }) }, 7000, 2000);
        Task<Order> order_3_Task = manager.Take_Order(customer_3, new List<Pizza> { new Pizza(Size.Large, new List<Pizza_Type> { (Pizza_Type)3, (Pizza_Type)4 }, new List<Add_On> { (Add_On)1, (Add_On)2 }) }, 4000, 6000);
        Task<Order> order_4_Task = manager.Take_Order(customer_4, new List<Pizza> { new Pizza(Size.Medium, new List<Pizza_Type> { (Pizza_Type)4, (Pizza_Type)5 }, new List<Add_On> { (Add_On)1, (Add_On)2 }) }, 2500, 8000);
        var orders_Tasks = new List<Task<Order>> {order_1_Task, order_2_Task, order_3_Task, order_4_Task};

        while(orders_Tasks.Count > 0) 
        {
            Task<Order> finished_Task = await Task.WhenAny(orders_Tasks);
            await finished_Task;
            orders_Tasks.Remove(finished_Task);
        }

        Order order_1 = await order_1_Task;
        Order order_2 = await order_2_Task;
        Order order_3 = await order_3_Task;
        Order order_4 = await order_4_Task;

        //Then, the manager sends all the orders to the cook, who will have no choice but take all the orders together
        pizzaiolo.Accept_Order(order_1);
        pizzaiolo.Accept_Order(order_2);
        pizzaiolo.Accept_Order(order_3);
        pizzaiolo.Accept_Order(order_4);
        //Finally, the delivery man will take all the order, and then deliver them one by one.
        delivery_Man.Take_Order(order_1);
        delivery_Man.Take_Order(order_2);
        delivery_Man.Take_Order(order_3);
        delivery_Man.Take_Order(order_4);
    }

    /* Scenario 2: With a message broker
     * Actors: 4 clients, 2 assistants téléphoniques, 3 cuisiniers, 2 livreurs
     * 
     * a) Les clients envoient une demande de prise en charge (sur un Queue “demande deprise en charge” de message broker)
     * b) Un assistant des deux doit prendre le message
     * c) L’assistant envoie la commande sur un queue ‘commande créée” sur le Message Broker
     * d) Le cuisinier qui écoute sur la queue “commande créée” prend en charge lacommande
     * e) Le cuisinier envoie un message sur une queue “commande prête pour livraison” qui est écouté par les livreurs
     * f) Un livreur prend en charge la livraison de la commande
     */
    public async Task Scenario_2()
    {
        //First, we make sure we load the right amount of people for this scenario. We get rid of everyone first:
        Statistics statistics = Statistics.GetInstance();
        Central_Server central_Server = Central_Server.GetInstance();
        statistics.Depopulate_Pizzeria();
        //Then, we load everyone needed for this operation:
        statistics.Load_Customers("Customer_1");
        statistics.Load_Customers("Customer_2");
        statistics.Load_Customers("Customer_3");
        statistics.Load_Customers("Customer_4");
        statistics.Load_Delivery_Men("Delivery_Man_1");
        statistics.Load_Delivery_Men("Delivery_Man_2");
        statistics.Load_Managers("Manager_1");
        statistics.Load_Managers("Manager_2");
        statistics.Load_Pizzaiolos("Pizzaiolo_1");
        statistics.Load_Pizzaiolos("Pizzaiolo_2");
        statistics.Load_Pizzaiolos("Pizzaiolo_3");
        //First, let's wake up everyone and load them here:
        //Customers first:
        Customer customer_1 = central_Server.Register_User("Omnom");
        Customer customer_2 = central_Server.Register_User("BigBoi");
        Customer customer_3 = central_Server.Register_User("Kirby");
        Customer customer_4 = central_Server.Register_User("Shork");
        List<Customer> all_customers = new List<Customer> { customer_1, customer_2, customer_3, customer_4 };

        //Then Managers:
        Console.WriteLine("Looking for first manager...");
        Manager manager_1 = central_Server.Find_Manager();
        Console.WriteLine("First Manager found!");
        Console.WriteLine("Looking for second manager...");
        Manager manager_2 = central_Server.Find_Manager();
        Console.WriteLine("Second Manager found!");

        //Third, the delivery men
        Console.WriteLine("Looking for first delivery man...");
        Delivery_Man delivery_Man_1 = central_Server.Find_Delivery_Man();
        Console.WriteLine("First delivery man found!");
        Console.WriteLine("Looking for second delivery man...");
        Delivery_Man delivery_Man_2 = central_Server.Find_Delivery_Man();
        Console.WriteLine("Second delivery man found!");

        //And finally, all the cooks
        Console.WriteLine("Looking for first pizzaiolo...");
        Pizzaiolo pizzaiolo_1 = central_Server.Find_Pizzaiolo();
        Console.WriteLine("First pizzaiolo found!");
        Console.WriteLine("Looking for second pizzaiolo...");
        Pizzaiolo pizzaiolo_2 = central_Server.Find_Pizzaiolo();
        Console.WriteLine("Second pizzaiolo found!");
        Console.WriteLine("Looking for third pizzaiolo...");
        Pizzaiolo pizzaiolo_3 = central_Server.Find_Pizzaiolo();
        Console.WriteLine("Third pizzaiolo found!");

        //We need to open a message broker here:
        //Then, we need to open a new connection factory
        var connectionFactory = new ConnectionFactory();
        var endpoint = Endpoint.Create("localhost", 8161, "guest", "guest");
        var connection = await connectionFactory.CreateAsync(endpoint);

        Console.WriteLine("Message broker opened!");

        //We have created a connection. Now let us start with the scenario.
        List<string>? manager_1_received_list = null, manager_2_received_list = null;

        //First, let's wake up the managers and tell them to listen to their phone.
        var consumer_manager_1 = await connection.CreateConsumerAsync("demande de prise en charge", RoutingType.Anycast);
        var consumer_manager_2 = await connection.CreateConsumerAsync("demande de prise en charge", RoutingType.Anycast);

        //Then, the customers send their names to the appropriate channel
        var producer_customer = await connection.CreateProducerAsync("demande de prise en charge", RoutingType.Anycast);
        await customer_1.Customer_Calls(producer_customer);
        await customer_2.Customer_Calls(producer_customer);
        await customer_3.Customer_Calls(producer_customer);
        await customer_4.Customer_Calls(producer_customer);


        //We create a loop to make sure both managers got the list
        while (manager_1_received_list == null || manager_2_received_list == null)
        {
            manager_1_received_list = await manager_1.WaitForMessage(consumer_manager_1);
            manager_2_received_list = await manager_2.WaitForMessage(consumer_manager_2);
        }

        //Let us create the respective orders for each client:
        List<List<Pizza>> all_Orders = new List<List<Pizza>>();
        all_Orders.Add(new List<Pizza> { new Pizza(Size.Large, new List<Pizza_Type> { (Pizza_Type)1, (Pizza_Type)2 }, new List<Add_On> { (Add_On)1, (Add_On)2 }) });
        all_Orders.Add(new List<Pizza> { new Pizza(Size.Small, new List<Pizza_Type> { (Pizza_Type)2, (Pizza_Type)3 }, new List<Add_On> { (Add_On)1, (Add_On)2 }) });
        all_Orders.Add(new List<Pizza> { new Pizza(Size.Large, new List<Pizza_Type> { (Pizza_Type)3, (Pizza_Type)4 }, new List<Add_On> { (Add_On)1, (Add_On)2 }) });
        all_Orders.Add(new List<Pizza> { new Pizza(Size.Medium, new List<Pizza_Type> { (Pizza_Type)4, (Pizza_Type)5 }, new List<Add_On> { (Add_On)1, (Add_On)2 }) });

        //We need the pizzaiolo to receive the message when it gets sent (the task used singular so I'm assuming we use one pizzaiolo), and send a confimrmation as soon as it's done
        var consumer_pizzaiolo = await connection.CreateConsumerAsync("commande créée", RoutingType.Anycast);
        var producer_pizzaiolo = await connection.CreateProducerAsync("commande prête pour livraison", RoutingType.Anycast);
        await pizzaiolo_1.Listen_For_Orders(consumer_pizzaiolo, producer_pizzaiolo);

        //Then, we create the producers for the managers
        var producer_manager = await connection.CreateProducerAsync("commande créée", RoutingType.Anycast);

        //Since they have the list, they can split the customers at random between them:
        var random = new Random();
        int i = 0;
        foreach (Customer all_Customers in all_customers)
        {
            int manager_number = random.Next(1, 3);
            if (manager_number == 1)
            {
                await manager_1.Take_Order(all_Customers, all_Orders[i], producer_manager);
            }
            else
            {
                await manager_2.Take_Order(all_Customers, all_Orders[i], producer_manager);
            }
            i++;
        }

        //We close the manager's receivers
        await manager_1.Stop_Listening(consumer_manager_1);
        await manager_2.Stop_Listening(consumer_manager_2);

        //We wait until the pizzaiolo receive the orders:
        await Task.Delay(5000);

        //And we send a stop for the pizzaiolo's receivers
        await pizzaiolo_1.Stop_Listening(consumer_pizzaiolo);

        //Finally, we awake the delivery men to let them listen to their phones
        var consumer_Delivery_Man_1 = await connection.CreateConsumerAsync("commande prête pour livraison", RoutingType.Anycast);
        var consumer_Delivery_Man_2 = await connection.CreateConsumerAsync("commande prête pour livraison", RoutingType.Anycast);
        List<int> received_Order_IDs = new List<int>();
        received_Order_IDs = await delivery_Man_1.Receive_Message(consumer_Delivery_Man_1);

        //We close the Delivery men's phones
        await delivery_Man_1.Stop_Listening(consumer_Delivery_Man_1);
        await delivery_Man_2.Stop_Listening(consumer_Delivery_Man_2);

        //Every delivery man should have gotten the same list of orders, now they just have to split between them:
        foreach (int all_Order_IDs in received_Order_IDs)
        {
            int delivery_Man_Number = random.Next(1, 3);
            if (delivery_Man_Number == 1)
            {
                delivery_Man_1.Take_Order(statistics.Fetch_Order_By_ID(all_Order_IDs));
            }
            else
            {
                delivery_Man_2.Take_Order(statistics.Fetch_Order_By_ID(all_Order_IDs));
            }
        }


        //We enter a buffer so the user can check everything for themselves:
        Console.WriteLine("Press any key to end the Scenario...");
        Console.ReadKey();
        //Finally, we end the connection.
        await connection.DisposeAsync();
    }

    public void Scenario_3()
    {

    }
}