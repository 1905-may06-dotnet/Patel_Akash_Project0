using System;
using System.Collections.Generic;
using Pizzaboxdomain;


namespace Pizzaboxdomain
{
    class Program
    {
        static void Main(string[] args)
        {

        Console.WriteLine("Hello, welcome to Pizza World!");
        string tempstring; //used to check user input
        PizzaUser use1 = new PizzaUser();
             do
             {
                Console.WriteLine("Enter 1 to see available locations, 2 to view order history, or 3 to log out");
                tempstring = Console.ReadLine();
                 if (tempstring.Equals("1"))
                 {
                    PizzaLocations loc1 = new PizzaLocations();
                    loc1.showLocations();
                    PizzaLocations PizLocation = new PizzaLocations();
                    String OrderLocation = PizLocation.selectLocation();
                    PizzaOrder CustomerOrder = new PizzaOrder();
                    CustomerOrder.Order();
                }
                 else if (tempstring.Equals("2"))
                 {
                     Console.WriteLine("viewing order history has not yet been implemented");
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
    public class Pizza
    {
        public string size = "";
        public List<string> toppingsAvailable = new List<string>
        {
            "mushrooms", "onions", "bellpepper", "spinache", "jalepeno"
        };
        public enum pizzaSize { s = 1, m, l };
        public List<string> toppings = new List<string>();
        public int numToppings = 0;
        public string crust = "";

        public double GetPizzaCost(Pizza Piz)
        {
            double sizecost = 0.0;
            double crustcost = 0.0;
            double toppingcost = 0.0;

            //determine cost due to pizza size
            if (Piz.size.Equals("s"))
            {
                sizecost = 2.5;
            }
            else if (Piz.size.Equals("m"))
            {
                sizecost = 3.5;
            }
            else if (Piz.size.Equals("l"))
            {
                sizecost = 4.0;
            }

            //determine cost due to pizza curst

            if (Piz.crust.Equals("s"))
            {
                crustcost = 0.0;
            }
            else if (Piz.crust.Equals("l"))
            {
                crustcost = 1.0;
            }

            toppingcost = numToppings * 0.25;

            return (sizecost + crustcost + toppingcost);
        }

        public string showPizza()
        {
            string constring = ""; //string to contain list of all the toppings customer has ordered
            foreach (string str in toppings)
            {
                constring += str + ", ";
            }

            return($"size: {size}, crust: {crust}, and toppings: {constring}");
        }

        public void OrderPizza()
        {
            OrderSize();
            OrderCrust();
            OrderToppings();
        }

        public void OrderLargeVegPizza()
        {
            size = "l";
            crust = "l";
            numToppings = 5;
            toppings.Add("mushrooms");
            toppings.Add("onions");
            toppings.Add("bellpepper");
            toppings.Add("spinache");
            toppings.Add("jalepeno");
           
        }

        public void OrderSize()
        {
            string tempstring; //temporary string to ensure user enters correct information 

            //prompt user for pizza size
            Console.WriteLine("What size Pizza do you want?");
            Console.WriteLine("Please enter s for small, m for medium, or l for large.");
            tempstring = Console.ReadLine();

            //ensure that pizza size entered by user is valid
            do
            {
                if (tempstring.Equals("s") || tempstring.Equals("m") || tempstring.Equals("l"))
                {
                    size = tempstring;
                }
                else
                {
                    Console.WriteLine("You entered an incorrect value for size, please enter s, m or l");
                    tempstring = Console.ReadLine();
                }
            }
            while (tempstring.Equals(size) == false);

            //confirm to user their size
            Console.WriteLine("your pizza size is " + size);
        }

        public void OrderCrust()
        {
            string tempstring=""; //temporary string to ensure user enters correct information

            //prompt user for pizza crust type
            Console.WriteLine("Do you want thin or thick crust?");
            Console.WriteLine("Please enter s for thin crust or l for thick crust.");
            tempstring = Console.ReadLine();

            //ensure that pizza crust entered by user is valid
            do
            {
                if (tempstring.Equals("s") || tempstring.Equals("l"))
                {
                    crust = tempstring;
                }
                else
                {
                    Console.WriteLine("You entered an incorrect value for crust, please enter s for thin or l for thick");
                    tempstring = Console.ReadLine();
                }
            }
            while (tempstring.Equals(crust) == false);

            //confirm to user their size
            Console.WriteLine("your pizza crust is " + crust);
        }

        public void OrderToppings()
        {
            //show to user the list of available toppings
            int tempint = 0; //int used to verify user input for numtoppings
            string tempstring = ""; //string used to store temp data / verify user input
            int i = 0; //used to list out the number of each topping
            bool cont = false; //boolean to determine if while loop to verify user input should continue



            //prompt user how many toppings they want
            Console.WriteLine("How many topics do you want? (Mininum of 2, Maximum of 5)");
            
            //ensure that the user entered correct number of toppings
            do
            {
                tempstring = Console.ReadLine();
                if (Int32.TryParse(tempstring, out tempint))
                {
                    
                    if(tempint>=2 && tempint<=5)
                    {
                        numToppings = tempint;
                        Console.WriteLine($"you ordered {tempint} toppings "); //show user numtoppings
                        cont = false;
                    }
                    else
                    {
                        Console.WriteLine("You entered an incorrect value for number of toppings.  Please enter a number between 2 and 5");
                        cont = true;
                    }
                }
                else
                {
                    Console.WriteLine("You entered an incorrect value for number of toppings.  Please enter a number between 2 and 5");
                    cont = true;
                }
            }
            while (cont);

            //Determine what toppings the user actually wants
            
            //print available toppings to the user
            Console.WriteLine("Toppings Available");
            tempstring = ""; //initialize tempstring to empty
            foreach (string str in toppingsAvailable)
            {
                tempstring += i + " " + str + "| ";
                i++;
            }
            Console.WriteLine(tempstring);
            Console.WriteLine("Please enter the number corresponding to the topping you want");

            //loop and allow the user to choose their individual toppings
            for (int I = 0; I<numToppings; I++)
            {
                do
                {
                    tempstring = Console.ReadLine();
                    if (Int32.TryParse(tempstring, out tempint))
                    {

                        if (tempint >= 0 && tempint <= toppingsAvailable.Count-1)
                        {
                            toppings.Add(toppingsAvailable[tempint]);
                            Console.WriteLine($"Topping {tempint}: {toppingsAvailable[tempint]}"); //show user numtoppings
                            cont = false;
                        }
                        else
                        {
                            Console.WriteLine($"You entered an incorrect value for the topping.  Please enter a number between 0 and {toppingsAvailable.Count-1}");
                            cont = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"You entered an incorrect value for your topping.  Please enter a number between 0 and {toppingsAvailable.Count-1}");
                        cont = true;
                    }
                }
                while (cont);


            }




        }
    }
}
