using System;
using System.Collections.Generic;

namespace Pizzaboxdata.Data
{
    public partial class PizzaTable
    {
        public int PizzaIdPk { get; set; }
        public string PizzaString { get; set; }
        public int? PizzaCount { get; set; }
        public int? OrderIdFk { get; set; }

        public virtual OrderTable OrderIdFkNavigation { get; set; }

        public void displayPizzaDetails()
        {
            Console.WriteLine($"Pizza: {PizzaString}");
        }

    }
}
