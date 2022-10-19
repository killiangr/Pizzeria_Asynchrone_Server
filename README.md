# Pizzeria_Asynchrone_Server

A pizzeria wants to computerize its order-production-delivery system in order to improve its performance. Here's how
she would like the process to go once computerized.
When a customer calls the restaurant, the clerk who takes the call always begins by asking the customer if this is their
first order in this restaurant. If the customer has already ordered, the clerk asks for their phone number in order to find
the customer's contact details and confirm their address. When the customer is on his first order, the clerk must then
enter the full contact details of the customer (surname, first name, address, telephone number, and the date of his first
order). He then enters the command.
An order must include one (or more) pizza(s), and possibly additional drink type products (cola, orange juice, etc.)
according to a volume to be specified. The pizzas are available in three different sizes (small, medium, large) and
several types (eg tomato/cheese sauce, vegetarian, all dressed, etc.). The price of a pizza depends on its size and type.
The order includes the following information: order number, time, date, customer name , clerk name, items ordered.
The pizza(s) ordered is (are) then prepared by the cooks and then sent for delivery.
Once the order is placed, a confirmation message is sent to:
– customer. Message of support for his order;
– kitchen. Message confirming pizza choice;
– delivery man. Order message with customer details with a delay of 5 min
– committed. Order opening confirmation message.
The delivery person then takes the pizza(s) and the related invoice, adds the other products and leaves to make the
delivery. When the address indicated is correct and found, he delivers the order to the customer and receives the
payment and he sends a confirmation message to the pizzeria. He returns to the pizzeria with the double of the bill and
the money.
On his return, the clerk asks the delivery person for the invoice, enters the order number, the delivery person's number,
collects the money and closes the order by sending two messages to
– delivery man. Closing message of the order and the amount of its payment.
– Clerk. Order closing confirmation message.
At any time, the clerk can know the status of an order (in preparation, in delivery or closed). Orders that could not be
delivered are marked as losses by the clerk and are closed. Each honored order increases the number of orders made by
the customer.
1. Please give the UML schemas (class diagram, use cases) corresponding to your conceptual analysis of this
case.
2. Propose a C# application that simulates the operation of the pizzeria with communication.
