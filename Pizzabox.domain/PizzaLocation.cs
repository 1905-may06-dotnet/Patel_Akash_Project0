using System;
using System.Collections.Generic;
using System.Text;

namespace Pizzaboxdomain
{
    class PizzaLocations
    {
        //contains a list of all current locations
        List<string> locations = new List<string>();

        public PizzaLocations()
        {
            locations.Add("1111 west pine street");
            locations.Add("7373 east shoulder avenue");
            locations.Add("8787 harbor road");
        }

        public void showLocations()
        {
            int count = 1; //counter variable to number out locations
            foreach (string address in locations)
            {
                //print each location in the list
                Console.WriteLine($"Address {count}: {address}");
                count++;
            }
        }

        //have user pick a location
        public string selectLocation()
        {
            String tempstring; //temp string to check user input
            int tempint = 0; //temp int to check user input
            bool cont = false; //temp bool to check if we need to keep looping
            Console.WriteLine("Please enter the number above corresponding to your location");
            do
            {
                tempstring = Console.ReadLine();
                if (Int32.TryParse(tempstring, out tempint))
                {

                    if (tempint > 0 && tempint <= locations.Count)
                    {
                        //show user the location that they want to order from
                        Console.WriteLine($"your location is {locations[tempint-1]} ");
                        //set continue to false so that the while loop ends
                        cont = false;
                    }
                    else
                    {
                        //this might be problematic if locations.count is 0 or 1
                        Console.WriteLine($"You entered an incorrect value for location.  Please enter a number between 1 and {locations.Count}");
                        //set continue to true to continue the loop
                        cont = true;
                    }
                }
                else
                {
                    //this might be problematic if locations.count is 0 or 1
                    Console.WriteLine($"You entered an incorrect value for location.  Please enter a number between 1 and {locations.Count}");
                    //set continue to true to continue the loop
                    cont = true;
                }
            }
            while (cont);
            return locations[tempint-1];
        }

        //need SQL to implement this
        public void viewOrders()
        {
            Console.WriteLine("this method has not been implemented");
        }

        //need SQL to implement this
        public void viewSales()
        {
            Console.WriteLine("this method has not been implemented");
        }

        //need SQL to implement this
        public void viewInventory()
        {
            Console.WriteLine("this method has not been implemented");
        }

        //need SQL to implement this
        public void viewUsers()
        {
            Console.WriteLine("this method has not been implemented");
        }


    }
}
