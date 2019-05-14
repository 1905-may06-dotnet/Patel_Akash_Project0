using System;
using System.Collections.Generic;
using System.Text;

namespace Pizzaboxdomain
{
    class PizzaUser
    {
        
        //requirements      
        /*- should be able to view its order history <- this should be in data
       - should be able to only order from 1 location/day
       - should be able to only order 1 time within a 2 hour period
       - should be able to only order if an account exists*/

        public DateTime lastOrder = new DateTime(2019, 5, 13, 13, 0, 15);
        public bool canorder = false;

        public void CheckOrderConditions(UserAccount user, PizzaLocation loc)
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
            if(user!=null)
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
