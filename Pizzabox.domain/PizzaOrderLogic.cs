using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Pizzaboxdata.Data;

namespace Pizzaboxdomain
{
    class PizzaOrder:Pizza
    {
        string LocationAddress = "";
        double totalpizzacost = 0.0;
        List<Pizza> pizzalist = new List<Pizza>();
        bool isValidOrder = true;
        DateTime OrderDatetime;


        //main method used to order a pizza
        public void Order(PizzaUser user, String Location, PizzaContext PC)
        {
            bool isOrderFinished = false; //bool to check if user is finished with their order
            string tempstring = ""; //string used to check user input or hold temp data
            double cost = 0.0;

            
                //first, determine if the user wants to remove a pizza, order a preset pizza, or order a custom pizza
                Console.WriteLine("Enter r to remove a pizza, o to order a preset pizza, c to order a custom pizza, or f to finish/preview your order");

                //verify user input
                do
                {
                    tempstring = Console.ReadLine();
                    if (tempstring.Equals("r"))
                    {
                        //check to see that we have pizzas to remove
                        if (pizzalist.Count > 0)
                        {
                            removePizza();
                            Console.WriteLine("Enter r to remove a pizza, o to order a preset pizza, c to order a custom pizza, or f to finish/preview your order");

                        }
                    else
                        {
                            Console.WriteLine("You don't have any Pizzas to remove");
                        }
                    }
                    else if (tempstring.Equals("o"))
                    {
                    Pizza piz = new Pizza();
                    piz.OrderLargeVegPizza();
                    pizzalist.Add(piz);
                    cost = computeCost();
                    //determine how many pizzas we have so far
                    int sumpizza = 0;
                    foreach (var pizza in pizzalist)
                    {
                        sumpizza += pizza.quantity;

                    }
                    Console.WriteLine($"You ordered a pizza with: {piz.showPizza()}");
                    Console.WriteLine($"You have ordered {sumpizza} pizza(s) so far, with cost {cost}");
                    Console.WriteLine("Enter r to remove a pizza, o to order a preset pizza, c to order a custom pizza, or f to finish/preview your order");

                    }
                else if (tempstring.Equals("c"))
                {
                    Pizza piz = new Pizza();
                    piz.OrderPizza();
                    pizzalist.Add(piz);
                    cost = computeCost();
                    //determine how many pizzas we have so far
                    int sumpizza = 0;
                    foreach (var pizza in pizzalist)
                    {
                        sumpizza += pizza.quantity;

                    }

                    Console.WriteLine($"You ordered a pizza with: {piz.showPizza()}");
                    Console.WriteLine($"You have ordered {sumpizza} pizza(s) so far, with cost {cost}");
                    Console.WriteLine("Enter r to remove a pizza, o to order a preset pizza, c to order a custom pizza, or f to finish/preview your order");

                }
                else if (tempstring.Equals("f"))
                    {
                        //print the order to the user so they can verify whether or not it is correct
                        for(int I = 0; I<pizzalist.Count; I++)
                        {
                        Console.WriteLine($"Pizza {I + 1}: {pizzalist[I].showPizza()}");
                        }
                        //print the total cost
                        cost = computeCost();
                        Console.WriteLine($"Your order will cost: {cost}");
                        Console.WriteLine("Is this order correct? enter y for yes or any other key for no");
                        if(Console.ReadLine().Equals("y"))
                        {
                        isOrderFinished = true;
                        //enter order in the database

                        //step 0, find out what value i want for primary key since SQL wont do it for me
                        int orderID = 0;
                        OrderTable x = PC.OrderTable.OrderByDescending(y => y.OrderIdPk).FirstOrDefault<OrderTable>();
                        if(x==null)
                        {
                            orderID = 0;
                        }
                        else
                        {
                            orderID = x.OrderIdPk + 1;
                        }
                        


                        //step 1, enter the Order into the OrderTable
                        OrderTable O = new OrderTable() { UsernameFk = user.username, LocationFk = Location, OrderDateTime = DateTime.Now, OrderTotalCost = cost, OrderIdPk = orderID};
                        PC.OrderTable.Add(O);
                        PC.SaveChanges();



                        
                        //step 1.5 find out what value i want for primary key for pizzaDB since SQL wont do it for me
                        int PizzaID = 0;
                        PizzaTable z = PC.PizzaTable.OrderByDescending(y => y.PizzaIdPk).FirstOrDefault<PizzaTable>();
                        if (z == null)
                        {
                            PizzaID = 0;
                        }
                        else
                        {
                            PizzaID = z.PizzaIdPk + 1;
                        }


                        //step 2, enter the Pizzas into the PizzaTable
                        for (int i = 0; i<pizzalist.Count(); i++)
                         {
                            
                            PizzaTable P = new PizzaTable() { PizzaString = pizzalist[i].showPizza(), PizzaCount = pizzalist[i].quantity, OrderIdFk = orderID, PizzaIdPk = PizzaID+i};
                            PC.PizzaTable.Add(P);
                            PC.SaveChanges();
                         }
                        
    
                        }
                        else
                        {
                        Console.WriteLine("Enter r to remove a pizza, o to order a preset pizza, c to order a custom pizza, or f to finish/preview your order");
                        }

                    }
                    else
                    {
                        Console.WriteLine("You entered an invalid option, please enter r, o, c, or f");
                    }
                }
                while (!isOrderFinished);


        }

        //method used to remove a pizza from order
        public void removePizza()
        {
            //print to the user a list of pizzas they have
            for (int I = 0; I < pizzalist.Count; I++)
            {
                Console.WriteLine($"Pizza {I + 1}: {pizzalist[I].showPizza()}");
            }

            Console.WriteLine("Please enter the number of the pizza you want to remove");

            //ensure the user writes correct input
            string tempstring; //string used for verification
            int tempint; //int used to see what pizza user wants to remove
            bool cont; //boolean used to conintue or not
            do
            {
                tempstring = Console.ReadLine();
                if (Int32.TryParse(tempstring, out tempint))
                {

                    if (tempint >= 1 && tempint <= pizzalist.Count+1)
                    {
                        pizzalist.RemoveAt(tempint - 1);
                        cont = false;
                    }
                    else
                    {
                        Console.WriteLine($"You have entered an incorrect value, please enter a number between 1 and {pizzalist.Count}");
                        cont = true;
                    }
                }
                else
                {
                    Console.WriteLine($"You have entered an incorrect value, please enter a number between 1 and {pizzalist.Count}");
                    cont = true;
                }
            }
            while (cont);


        }

        //need SQL to implement this
        public void viewOrder()
        {
            Console.WriteLine("this method has not been implemented");
        }

        //need SQL to implement this
        public double computeCost()
        {
            int i; //track the index
            double sum = 0.0; //track the cost sum
            for(i = 0; i<pizzalist.Count; i++)
            {
                sum += pizzalist[i].GetPizzaCost(pizzalist[i]);
            }
            return sum;
        }

        //check to see if the order is valid as per project conditions
        //can't order more than 100 pizzas and cannot pay more than $5000
        public void checkOrder()
        {
            bool pizzacost = false;
            bool pizzacount = false;
            if(totalpizzacost<=5000.00)
            {
                pizzacost = true;
            }
            else
            {
                pizzacost = false;
                Console.WriteLine("Your order is over $5000, please make your order less expensive");
            }
            if(pizzalist.Count<=100)
            {
                pizzacount = true;

            }
            else
            {
                pizzacount = false;
                Console.WriteLine("your order has more than 100 pizzas, please make your order have 100 or less pizzas");
            }

           isValidOrder = (pizzacount && pizzacost);

        }

        

    }


}
