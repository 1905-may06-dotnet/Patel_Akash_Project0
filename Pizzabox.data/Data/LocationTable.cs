using System;
using System.Collections.Generic;

namespace Pizzaboxdata.Data
{
    public partial class LocationTable
    {
        public LocationTable()
        {
            OrderTable = new HashSet<OrderTable>();
        }

        public string LocationPk { get; set; }
        public int? PizzaDough { get; set; }
        public int? PizzaSauce { get; set; }
        public int? PizzaCheese { get; set; }
        public int? Mushrooms { get; set; }
        public int? Onions { get; set; }
        public int? Bellpepper { get; set; }
        public int? Spinache { get; set; }
        public int? Jalapeno { get; set; }

        public virtual ICollection<OrderTable> OrderTable { get; set; }

        public void displayLocationDetails()
        {
            Console.WriteLine($"Location: {LocationPk}, Mushrooms: {Mushrooms}, Onions: {Onions}, Spinache: {Spinache}, Jalapenos: {Jalapeno}, Bellpeppers: {Bellpepper}, Dough: {PizzaDough}, Cheese: {PizzaCheese}, Sauce: {PizzaSauce}");
        }
    }
}
