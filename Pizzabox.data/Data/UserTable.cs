using System;
using System.Collections.Generic;
using System.Linq;

namespace Pizzaboxdata.Data
{
    public partial class UserTable
    {
        public UserTable()
        {
            OrderTable = new HashSet<OrderTable>();
        }

        public string UsernamePk { get; set; }
        public string Password { get; set; }

        public virtual ICollection<OrderTable> OrderTable { get; set; }
    }
    public class PizzaUser
    {

        //requirements      
        /*- should be able to view its order history <- this should be in data
       - should be able to only order from 1 location/day
       - should be able to only order 1 time within a 2 hour period
       - should be able to only order if an account exists*/

        //properties of a user;
        public DateTime lastOrder = new DateTime(2019, 5, 13, 13, 0, 15);
        public bool canorder = false;
        public string username = "";
        public string password = "";
        public bool isLoggedin = false;

        //constructor, it asks the user to either register or log in.
        public PizzaUser(PizzaContext PC)
        {
            string tempstring = ""; // used to verify user input

            Console.WriteLine("Please enter l to log in, or r to register");

            do
            {
                tempstring = Console.ReadLine();
                if (tempstring.Equals("l"))
                {
                    this.login(PC);
                }
                else if (tempstring.Equals("r"))
                {
                    this.register(PC);
                }
                else
                {
                    Console.WriteLine("Incorrect input, please enter r for register or l for login");
                }
            }
            while (isLoggedin == false);
        }


        public void showHistory(PizzaContext PC)
        {
            var x = PC.OrderTable.Where<OrderTable>(u => u.UsernameFk.Equals(username)).ToList();
            foreach (var obj in x)
            {
                obj.displayOrderDetails();
            }
        }

        //login a new user
        public void login(PizzaContext PC)
        {
            //obtain information from user regarding username
            Console.WriteLine("Please enter your username");
            username = Console.ReadLine();

            //check to see that the username matches an existing username in database
            UserTable x = PC.UserTable.Where<UserTable>(u => u.UsernamePk == username).FirstOrDefault<UserTable>();
            if (x == null)
            {
                //prompt user their username is not existant
                Console.WriteLine("You entered an incorrect username, please press l to login or r to register");

                //put some way to go back to first prompt where it asks for register or login
            }
            else
            {
                Console.WriteLine("Please enter your password");
                password = Console.ReadLine();
                if (x.Password.Equals(password))
                {
                    isLoggedin = true;
                    Console.WriteLine("Login successful");
                }
                else
                {
                    //tell user that they had a bad password
                    Console.WriteLine("You entered an invalid password, please press l to login or r to register");

                    //put some way to go back to first prompt
                }

            }

        }



        public void register(PizzaContext PC)
        {
            bool cont; //boolean to decide if loop should keep going
            do
            {
                //step 1 get valid information from the user
                Console.WriteLine("Please enter your username");
                username = Console.ReadLine();

                //ensure that the username is not taken in the database
                //check to see if the username matches an existing username in database
                UserTable x = PC.UserTable.Where<UserTable>(u => u.UsernamePk == username).FirstOrDefault<UserTable>();
                if (x == null)
                {
                    Console.WriteLine("Please enter your password, this CANNOT MATCH your username");
                    password = Console.ReadLine();
                    if (username.Equals(password))
                    {
                        Console.WriteLine("Your username matched your password.  This is not allowed");
                        cont = true;
                    }
                    else
                    {
                        cont = false;
                    }

                }
                else
                {
                    Console.WriteLine("Your username is already taken, please enter a new username");
                    cont = true;
                }
            }
            while (cont);


            //step 2 write the user information into the database
            PC.UserTable.Add(new UserTable() { UsernamePk = username, Password = password });
            PC.SaveChanges();
            Console.WriteLine("Register successful! You have also been logged in!");
            isLoggedin = true;
        }


        public void logout()
        {
            isLoggedin = false;
        }



        public bool CheckOrderConditions(PizzaContext PC, string Location)
        {
            //initialize booleans to check all order conditions
            bool twohours = false;
            bool hasaccount = false;
            bool location = false;


            //this method is a bit wonky
            int compare = 0; //int used to determine if user has ordered within last two hours
                             //Check if it has been 2 hours since the user last ordered a pizza

            //step 1 determine when the user's last order was
            //get the row of their last order by using firstordefault in conduction with an order by
            OrderTable x = PC.OrderTable.Where<OrderTable>(u => u.UsernameFk == username).OrderByDescending(y => y.OrderDateTime).FirstOrDefault<OrderTable>();

            //check to see if the user had a last order at all
            if (x == null)
            {
                twohours = true;
            }
            else
            {
                //cast datetime in database to datetime time.
                lastOrder = (DateTime)x.OrderDateTime;

                //compare users last order plus 2 hours to current time.
                compare = (DateTime.Now).CompareTo(lastOrder.AddHours(2.00));
                if (compare >= 0)
                {
                    //this means that the users last order was at least 2 hours ago
                    twohours = true;
                }
                else
                {
                    //this means the user has ordered within the last 2 hours
                    twohours = false;
                    Console.WriteLine("You have ordered within the last 2 hours, please wait 2 hours before ordering again");
                }
            }
            //check to see if the user has an account that is logged in
            if (isLoggedin)
            {
                hasaccount = true;
            }
            else
            {
                hasaccount = false;
            }

            //check to see if user has bought at this location in past 24 hr.
            //this method is a bit wonky
            compare = 0; //reset int compare to zero


            //step 1 determine when the user's last order at this particular location was
            //the location is obtained via the selectlocations method in the location class
            //get the row of their last order by using firstordefault in conduction with an order by using current location as the key
            x = PC.OrderTable.Where<OrderTable>(u => u.LocationFk == Location).OrderByDescending(y => y.OrderDateTime).FirstOrDefault<OrderTable>();

            //check to see if there is a last order by the customer at this location
            if (x == null)
            {
                location = true;
            }
            else
            {
                //because we know here that there was a last order, we can define our last order at this location
                //also cast it to the datetime type
                lastOrder = (DateTime)x.OrderDateTime;
                //compare current time to 24 hours plus last order
                compare = (DateTime.Now).CompareTo(lastOrder.AddHours(24.00));
                if (compare >= 0)
                {
                    location = true;
                }
                else
                {
                    location = false;
                    Console.WriteLine("You have ordered at this location within the last 24hours, please order later or at another location");
                }

            }


            canorder = (twohours && hasaccount && location);
            return canorder;
        }


    }
}
