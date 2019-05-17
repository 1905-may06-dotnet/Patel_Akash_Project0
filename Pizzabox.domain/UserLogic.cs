/*using System;
using System.Collections.Generic;
using System.Text;

//this entire section will be deleted and rewritten in the UserTable.cs class

namespace Pizzaboxdomain
{
    class PizzaUser
    {

        //requirements      

        //properties of a user;
        public DateTime lastOrder = new DateTime(2019, 5, 13, 13, 0, 15);
        public bool canorder = false;
        internal string username = "";
        internal string password = "";
        public bool isLoggedin = false;

        //constructor, it asks the user to either register or log in.
        public PizzaUser()
        {
            string tempstring = ""; // used to verify user input

            Console.WriteLine("Please enter l to log in, or r to register");

            do
            {
                tempstring = Console.ReadLine();
                if (tempstring.Equals("l"))
                {
                    this.login();
                }
                else if (tempstring.Equals("r"))
                {
                    this.register();
                }
                else
                {
                    Console.WriteLine("Incorrect input, please enter r for register or l for login");
                }
            }
            while (!(tempstring.Equals("r") || tempstring.Equals("l")));
        }

        //login a new user
        public void login()
        {
            //this can't be fully implemented yet, but the idea is to use the username as a key
            //and use that as a key to get the password in a hashtable or dictionary (which would be
            //made in the data part).  Then, we would check to see if the password entered by the user 
            //matches the password in the dictionary or hashtable.  If so, we successfully log in>
            Console.WriteLine("Please enter your username");
            username = Console.ReadLine();
            Console.WriteLine("Please enter your password");
            password = Console.ReadLine();
            Console.WriteLine("Login successful");
            isLoggedin = true;
        }



        public void register()
        {
            Console.WriteLine("Please enter your username");
            username = Console.ReadLine();
            Console.WriteLine("Please enter your password");
            password = Console.ReadLine();
            Console.WriteLine("Register successful! You have also been logged in!");
            isLoggedin = true;
        }


        public void logout()
        {
            isLoggedin = false;
        }



        public void CheckOrderConditions()
        {
            //initialize booleans to check all order conditions
            bool twohours = false;
            bool hasaccount = false;
            bool location = false;


            //this method is a bit wonky
            int compare = 0; //int used to determine if user has ordered within last two hours
                             //Check if it has been 2 hours since the user last ordered a pizza
            compare = (DateTime.Now).CompareTo(lastOrder.AddHours(2.00));
            if (compare >= 0)
            {
                twohours = true;
            }
            else
            {
                twohours = false;
            }

            //check to see if the user has an account
            if (password != null && username != null)
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
            //Check if it has been 24 hours since the user last ordered a pizza
            compare = (DateTime.Now).CompareTo(lastOrder.AddHours(24.00));
            if (compare >= 0)
            {
                location = true;
            }
            else
            {
                location = false;
            }


            canorder = (twohours && hasaccount && location);
        }
    }


}
*/