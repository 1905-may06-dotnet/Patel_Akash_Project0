using System;
using System.Collections.Generic;
using Pizzaboxdomain;


namespace Pizzabox.domain
{
    class Program
    {
        static void Main(string[] args)
        {
           Console.WriteLine("Hello Pizza World!");
           Pizza Piz = new Pizza();
           Piz.OrderPizza();
           Console.WriteLine($"Your pizza will cost ${Piz.GetPizzaCost(Piz)}");
           // PizzaUser pizuse = new PizzaUser();
           // PizzaLocation loc = new PizzaLocation();
           // UserAccount user = new UserAccount();
           // pizuse.CheckOrderConditions(user, loc);
           // Console.WriteLine(pizuse.canorder);
        }
    }
    public class Pizza
    {
        public string size = "";
        public enum toppingsAvailable {mushrooms, onions, bellpepper, spinache, jalepeno};
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

        public void OrderPizza()
        {
            OrderSize();
            OrderCrust();
            OrderToppings();

            


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

            Console.WriteLine("Toppings Available");
            
            foreach (string str in Enum.GetNames(typeof(toppingsAvailable)))
            {
                
                tempstring += i + " " + str + "| ";
                i++;
            }
            Console.WriteLine(tempstring);


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

            // later on, when i need to actually know the toppings I need i will implement this
            //Console.WriteLine("Please enter the specific toppings you want");



            
        }
    }
}
