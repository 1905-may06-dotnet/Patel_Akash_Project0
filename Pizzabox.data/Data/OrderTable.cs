using System;
using System.Collections.Generic;

namespace Pizzaboxdata.Data
{
    public partial class OrderTable
    {
        public OrderTable()
        {
            PizzaTable = new HashSet<PizzaTable>();
        }

        public int OrderIdPk { get; set; }
        public string UsernameFk { get; set; }
        public double? OrderTotalCost { get; set; }
        public DateTime? OrderDateTime { get; set; }
        public string LocationFk { get; set; }

        public virtual LocationTable LocationFkNavigation { get; set; }
        public virtual UserTable UsernameFkNavigation { get; set; }
        public virtual ICollection<PizzaTable> PizzaTable { get; set; }

        public void displayOrderDetails()
        {
            Console.WriteLine( $"Order ID: {OrderIdPk}, OrderTotalCost: {OrderTotalCost}, Date: {OrderDateTime}, Location: {LocationFk} ");
        }

        //old legacy method
        /*
        public void displayUserDetails()
        {
            Console.WriteLine($"Username: {UsernameFk}");
        }
        */

    }
    
}
