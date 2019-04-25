using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;


/*
All these test cases were created by Dennis Martinez-Lopez 
These test cases deal with the login page (Admin status), remove book page (remove book success/fail) and inventory window (updated quantity success/failure)
*/

namespace BookStoreLIB
{
    [TestClass]
    public class UnitTest5
    {
        UserData userdata = new UserData();
        AdminControls ac = new AdminControls();

        string inputName;

        //a
        [TestMethod]
        public void TestMethod1()
        {
            //check admin status
            inputName = "martined";

            int expectedReturn = 1;

            int actualReturn = userdata.checkAdmin(inputName);

            Assert.AreEqual(expectedReturn, actualReturn);

        }

        //a
        [TestMethod]
        public void TestMethod2()
        {
            //check admin status
            inputName = "subedir";

            int expectedReturn = 1;

            int actualReturn = userdata.checkAdmin(inputName);

            Assert.AreEqual(expectedReturn, actualReturn);
        }

        [TestMethod]
        public void TestMethod3()
        {
            //check admin status
            inputName = "user1";

            int expectedReturn = -1;

            int actualReturn = userdata.checkAdmin(inputName);

            Assert.AreEqual(expectedReturn, actualReturn);
        }

        //a
        [TestMethod]
        public void TestMethod4()
        {
            //check admin status 
            inputName = "test123";

            int expectedReturn = 2;

            int actualReturn = userdata.checkAdmin(inputName);

            Assert.AreEqual(expectedReturn, actualReturn);
        }

        //a
        [TestMethod]
        public void TestMethod5()
        {
            //remove book by isbn - success

            string isbn = "000770595-6";

            bool expectedReturn = false;

            bool actualReturn = ac.RemoveBookbyISBN(isbn);

            Assert.AreEqual(expectedReturn, actualReturn);
        }

        //a
        [TestMethod]
        public void TestMethod6()
        {
            //remove book by title success

            string title = "Curb Dance";

            bool expectedReturn = false;

            bool actualReturn = ac.RemoveBookbyTitle(title);

            Assert.AreEqual(expectedReturn, actualReturn);

        }

        [TestMethod]
        public void TestMethod7()
        {
            //remove book by isbn fail

            string isbn = "119683963-XJ98";

            bool expectedReturn = false;

            bool actualReturn = ac.RemoveBookbyISBN(isbn);

            Assert.AreEqual(expectedReturn, actualReturn);
        }

        [TestMethod]
        public void TestMethod8()
        {
            //remove book by title fail

            string title = "fdfsdfsd";

            bool expectedReturn = false;

            bool actualReturn = ac.RemoveBookbyTitle(title);

            Assert.AreEqual(expectedReturn, actualReturn);

        }

        //a
        [TestMethod]
        public void TestMethod9()
        {
            //update inventory 

            string isbn = "027241711-4";
            int quantity = 10;

            bool expectedReturn = true;

            bool actualReturn = ac.UpdateInventory(isbn, quantity);

            Assert.AreEqual(expectedReturn, actualReturn);

        }

        //a
        [TestMethod]
        public void TestMethod10()
        {
            //update inventory 

            string isbn = "021767389-9";
            int quantity = 5;

            bool expectedReturn = true;

            bool actualReturn = ac.UpdateInventory(isbn, quantity);

            Assert.AreEqual(expectedReturn, actualReturn);

        }

        [TestMethod]
        public void TestMethod11()
        {
            //update inventory 

            string isbn = " ";
            int quantity = 0;

            bool expectedReturn = false;

            bool actualReturn = ac.UpdateInventory(isbn, quantity);

            Assert.AreEqual(expectedReturn, actualReturn);

        }

        [TestMethod]
        public void TestMethod12()
        {
            //update inventory 

            string isbn = "1";
            int quantity = 5;

            bool expectedReturn = false;

            bool actualReturn = ac.UpdateInventory(isbn, quantity);

            Assert.AreEqual(expectedReturn, actualReturn);

        }





    }
}
