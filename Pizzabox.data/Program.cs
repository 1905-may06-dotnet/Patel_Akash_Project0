using System;
using Pizzaboxdata.Data;
using System.Linq;

namespace Pizzabox.data
{
    class Program
    {
        static void Main(string[] args)
        {
            
           PizzaContext PC = new PizzaContext();

            //LocationTable l = new LocationTable() {LocationPk = "8787 harbor road", Bellpepper = 1000, Jalapeno = 1000, Mushrooms = 1000, Onions = 1000, PizzaCheese = 1000, PizzaDough = 1000, PizzaSauce = 1000, Spinache = 1000};
            // PC.UserTable.Add(new UserTable() { UsernamePk = "username", Password = "password" });
            // UserTable x = PC.UserTable.Where<UserTable>(u => u.UsernamePk == "username").FirstOrDefault<UserTable>();
            // PC.SaveChanges();
            // Console.WriteLine(x.UsernamePk);
            PizzaUser pizuse = new PizzaUser(PC);

            
        }
    }
}
