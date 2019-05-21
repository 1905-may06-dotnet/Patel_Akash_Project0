using System;
using System.Collections.Generic;
using System.Text;
using Pizzaboxdata.Data;
using System.Linq;

namespace Pizzaboxdomain
{
    public class PizzaLocations
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
        public string selectLocation(PizzaContext PC, string Username)
        {
            String tempstring; //temp string to check user input
            int tempint = 0; //temp int to check user input
            bool cont = false; //temp bool to check if we need to keep looping
            DateTime lastOrder; //temp var to hold datetime of users last order
            int compare = 0; //temp int to compare the current datetime and the users last order
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

                //this last part is a check on where the user has ordered in the last 24 hours
                //the below statement returns the entry that has the users most recent order
                OrderTable x = PC.OrderTable.Where<OrderTable>(u => u.UsernameFk == Username).OrderByDescending(y => y.OrderDateTime).FirstOrDefault<OrderTable>();
                
                //Step 2, add 24 hours to the datetime obtained in step 1 and compare that to the current datetime.  If the result
                //is less than 0 prompt the user that they cannot order at the current location because they have ordered within 24 hours
                //also set cont to false so that this loop does not end.  If they have not ordered within 24hours, do nothing and let the 
                //loop terminate as usual.

                //check to see if the user had a last order at all
                if (x == null)
                {
                   //if theres no last order, we don't need to do anything
                }
                else
                {
                    //cast datetime in database to datetime time.
                    lastOrder = (DateTime)x.OrderDateTime;

                    //compare users last order plus 24 hours to current time.
                    compare = (DateTime.Now).CompareTo(lastOrder.AddHours(24.00));
                    if (compare >= 0)
                    {
                        //this means that the users last order was at least 24 hours ago
                        //so that means we don't need to do anything since the order is valid
                    }
                    else
                    {
                        //this means the user has ordered within the last 24 hours
                        //so we need to make sure that they are ordering at the same location
                        //the 2hour check was done earlier before reaching this point so we dont need to worry about that

                        //first we perform a sql query to determine the location of the last order
                        x = PC.OrderTable.Where<OrderTable>(u => u.UsernameFk == Username).OrderByDescending(y => y.OrderDateTime).FirstOrDefault<OrderTable>();
                        string lastlocation = x.LocationFk;

                        if (locations[tempint - 1].Equals(lastlocation))
                        {
                            //we do not need to do anything since they are ordering at the same location
                        }
                        else
                        {
                            Console.WriteLine("you are ordering at a different location within 24 hours");
                            Console.WriteLine($"please change your location to {lastlocation}");
                            //set cont to true to continue the loop
                            cont = true;
                        }

                    }
                }


            }
            while (cont);
            return locations[tempint-1];
        }

        //This shows orders and sales
        public void viewOrders(PizzaContext PC, string location)
        {
            int i = 0;
            var x = PC.OrderTable.Where<OrderTable>(u => u.LocationFk.Equals(location)).ToList();
            foreach (var obj in x)
            {

                obj.displayOrderDetails();
                //x[i].orderIDpk contains the orderID primary key, so im searching the pizza table for entries that match the
                // order id
                var y = PC.PizzaTable.Where<PizzaTable>(u => u.OrderIdFk.Equals(x[i].OrderIdPk));

                foreach (var obj2 in y)
                {
                    obj2.displayPizzaDetails();

                }
                i += 1;
            }


        }



        //need SQL to implement this
        public void viewInventory(PizzaContext PC, string location)
        {
            var x = PC.LocationTable.Where<LocationTable>(u => u.LocationPk.Equals(location)).ToList();
            foreach (var obj in x)
            {
                obj.displayLocationDetails();
            }
        }

        //need SQL to implement this
        public void viewUsers(PizzaContext PC, string location)
        {
            var x = PC.OrderTable.Where<OrderTable>(u => u.LocationFk.Equals(location)).Select(z => z.UsernameFk).Distinct().ToList();
            int i = 1;
            foreach (var obj in x)
            {
                Console.WriteLine($"User {i}: {obj}");
                i += 1;
            }
        }


    }
}
