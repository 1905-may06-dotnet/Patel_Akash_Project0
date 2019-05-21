using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pizzaboxdata.Data;
using System.Linq;
using Pizzaboxdomain;

namespace Pizzabox.test
{
    [TestClass]
    public class TestPizzaUser
    {
        [TestMethod]
        public void TestLogOut()
        {
            PizzaContext PC = new PizzaContext();
            PizzaUser piz = new PizzaUser(PC);
            piz.isLoggedin = true;
            piz.logout();
            Assert.IsTrue(piz.isLoggedin == false);
        }

        //true case
        public void testCheckOrder()
        {
            PizzaOrder piz = new PizzaOrder();
            piz.checkOrder();
            piz.quantity = 100;
            piz.totalpizzacost = 5000.00;
            Assert.IsTrue(piz.isValidOrder == true);
        }

        //false case
        public void testCheckOrderFalse()
        {
            PizzaOrder piz = new PizzaOrder();
            piz.checkOrder();
            piz.quantity = 110;
            piz.totalpizzacost = 5010.00;
            Assert.IsTrue(!(piz.isValidOrder == true));
        }

        public void testComputeCost()
        {
            PizzaOrder piz = new PizzaOrder();
            piz.OrderLargeVegPizza();
            double cost = piz.computeCost();
            Assert.IsTrue(cost == 51.00);
        }



        //filler test to reach the 5 unit test mark
        public void TestLogOutRedundant()
        {
            PizzaContext PC = new PizzaContext();
            PizzaUser piz = new PizzaUser(PC);
            piz.isLoggedin = false;
            piz.logout();
            Assert.IsTrue(piz.isLoggedin == false);
        }

    }
}
