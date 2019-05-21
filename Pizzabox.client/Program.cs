using System;
using System.Collections.Generic;
using Pizzaboxdomain;
using Pizzaboxdata.Data;

namespace Pizzabox.client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, welcome to Pizza World!");
            string tempstring; //used to check user input
            PizzaContext PC = new PizzaContext();
            PizzaUser use1 = new PizzaUser(PC);
            int tempint; //int used for admin prompt

            //check if user is admin to prompt admin options
            if (use1.username == "admin")
            {
                Console.WriteLine("Welcome Admin!");
                do
                {
                    PizzaLocations loc1 = new PizzaLocations();
                    loc1.showLocations();
                    PizzaLocations PizLocation = new PizzaLocations();
                    String OrderLocation = PizLocation.selectLocation(PC, use1.username);
                    tempint = use1.PromptAdmin();
                    if (tempint == 1)
                    {
                        PizLocation.viewOrders(PC, OrderLocation);

                    }
                    else if (tempint == 2)
                    {
                        PizLocation.viewInventory(PC, OrderLocation);

                    }
                    else if (tempint == 3)
                    {
                        PizLocation.viewUsers(PC, OrderLocation);
                    }
                    else if (tempint == 4)
                    {
                        use1.logout();
                    }
                }
                while (tempint != 4);


            }
            //if user is not admin, follow through with normal logics
            else
            {
                Console.WriteLine("Enter 1 to see available locations, 2 to view order history, or 3 to log out");
                do
                {

                    tempstring = Console.ReadLine();
                    if (tempstring.Equals("1"))
                    {
                        //check to see if user has ordered within last two hours
                        if (use1.CheckTwoHourCondition(PC))
                        {
                            PizzaLocations loc1 = new PizzaLocations();
                            loc1.showLocations();
                            PizzaLocations PizLocation = new PizzaLocations();
                            String OrderLocation = PizLocation.selectLocation(PC, use1.username);
                            PizzaOrder CustomerOrder = new PizzaOrder();
                            CustomerOrder.Order(use1, OrderLocation, PC);

                        }
                        else
                        {
                            Console.WriteLine("You have ordered within the last 2 hours, please wait 2 hours before ordering again");
                            Console.WriteLine("Enter 1 to see available locations, 2 to view order history, or 3 to log out");
                        }


                    }
                    else if (tempstring.Equals("2"))
                    {

                        use1.showHistory(PC);
                        Console.WriteLine("Enter 1 to see available locations, 2 to view order history, or 3 to log out");

                    }
                    else if (tempstring.Equals("3"))
                    {
                        use1.isLoggedin = false;
                    }
                    else
                    {
                        Console.WriteLine("you entered an incorrect value,Enter 1 to see available locations, 2 to view order history, or 3 to log out");
                    }
                }
                while (!tempstring.Equals("3"));

            }

        }


    }
    }

