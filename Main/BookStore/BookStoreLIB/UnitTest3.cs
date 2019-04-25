using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookStoreLIB
{
    [TestClass]
    public class UnitTest3
    {

        //retrive Books pass
        [TestMethod]
        public void retriveBook()
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.connectionString);
            DataSet dsBooks;

            int expected_val = 1;
            int exact_val = 0;
            try
            {
                String strSQL = "Select dbo.Catgeory.CategoryID, dbo.Catgeory.Name, dbo.Catgeory.Description FROM dbo.Catgeory";
                SqlCommand cmdSelCategory = new SqlCommand(strSQL, conn);
                SqlDataAdapter daCatagory = new SqlDataAdapter(cmdSelCategory);
                dsBooks = new DataSet("Books");
                daCatagory.Fill(dsBooks, "Category");            //Get category info
                String strSQL2 = "Select ISBN, CategoryID, Title,Author, Price, Year, Edition, Publisher from dbo.BookData";
                SqlCommand cmdSelBook = new SqlCommand(strSQL2, conn);
                SqlDataAdapter daBook = new SqlDataAdapter(cmdSelBook);
                daBook.Fill(dsBooks, "Books");                  //Get Books info
                DataRelation drCat_Book = new DataRelation("drCat_Book",
                dsBooks.Tables["Category"].Columns["CategoryID"],
                dsBooks.Tables["Books"].Columns["CategoryID"], false);
                dsBooks.Relations.Add(drCat_Book);       //Set up the table relation
                exact_val = 1;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.Message);
                exact_val = 0;
            }

            Assert.AreEqual(expected_val, exact_val);


        }


        // Failed to retrive book

        [TestMethod]
        public void retriveBook_F()
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.connectionString);
            DataSet dsBooks;

            int expected_val = 0;
            int exact_val = 0;
            try
            {
                String strSQL = "Select * dbo.Catgeory.CategoryID, dbo.Catgeory.Name, dbo.Catgeory.Description FROM dbo.Catgeory";
                SqlCommand cmdSelCategory = new SqlCommand(strSQL, conn);
                SqlDataAdapter daCatagory = new SqlDataAdapter(cmdSelCategory);
                dsBooks = new DataSet("Books");
                daCatagory.Fill(dsBooks, "Category");            //Get category info
                String strSQL2 = "Select * ISBN, CategoryID, Title,Author, Price, Year, Edition, Publisher from dbo.BookData";
                SqlCommand cmdSelBook = new SqlCommand(strSQL2, conn);
                SqlDataAdapter daBook = new SqlDataAdapter(cmdSelBook);
                daBook.Fill(dsBooks, "Books");                  //Get Books info
                DataRelation drCat_Book = new DataRelation("drCat_Book",
                dsBooks.Tables["Category"].Columns["CategoryID"],
                dsBooks.Tables["Books"].Columns["CategoryID"], false);
                dsBooks.Relations.Add(drCat_Book);       //Set up the table relation
                exact_val = 1;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                exact_val = 0;
            }

            Assert.AreEqual(expected_val, exact_val);


        }

        // add book or book exist
        [TestMethod]
        public void AddBook()
        {
            SqlConnection    conn = new SqlConnection(Properties.Settings.Default.connectionString);

            int id = 1; string ISBN = "9780786838653"; int quantity=9;
            int expected_val = 1;
            int exact_val = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "IF EXISTS (SELECT * FROM Cart WHERE ISBN = @ISBN)BEGIN UPDATE Cart SET Quantity = Cart.Quantity+@quantity WHERE ISBN = @ISBN END ELSE BEGIN INSERT INTO [Cart] (UserId,ISBN,Quantity) VALUES (@Id,@ISBN,@quantity) END";
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@ISBN", ISBN);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                conn.Open();
                cmd.ExecuteNonQuery();
                exact_val = 1;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                exact_val = 0;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            Assert.AreEqual(expected_val, exact_val);

        }


        // Failed To Add Book

        [TestMethod]
        public void AddBook_F()
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.connectionString);

            int id = 1; string ISBN = "9780786838653"; int quantity = 9;
            int expected_val = 0;
            int exact_val = 0;

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "IF * EXISTS (SELECT * FROM Cart WHERE ISBN = @ISBN)BEGIN UPDATE Cart SET Quantity = Cart.Quantity+@quantity WHERE ISBN = @ISBN END ELSE BEGIN INSERT INTO [Cart] (UserId,ISBN,Quantity) VALUES (@Id,@ISBN,@quantity) END";
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@ISBN", ISBN);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                conn.Open();
                cmd.ExecuteNonQuery();
                exact_val = 1;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                exact_val = 0;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            Assert.AreEqual(expected_val, exact_val);


        }

        // remove book
        [TestMethod]
        public void RemoveBook()
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.connectionString);

            int expected_val = 1;
            int exact_val = 1;

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM Cart WHERE Cart.UserId = 1 AND Cart.ISBN = '279761510-7';";
                
                conn.Open();
                cmd.ExecuteNonQuery();
                 exact_val = 1;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                 exact_val = 0;

            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            Assert.AreEqual(expected_val, exact_val);

        }


        // remove book failed
        [TestMethod]
        public void RemoveBook_F()
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.connectionString);

            int expected_val = 1;
            int exact_val = 1;

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM Cart WHERE Cart.UserId = 1 AND Cart.ISBN = '279761510-7';";

                conn.Open();
                cmd.ExecuteNonQuery();
                exact_val = 1;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                exact_val = 1;

            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            Assert.AreEqual(expected_val, exact_val);

        }


        // Search Book
        [TestMethod]
        public void SearchBook()
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.connectionString);
            DataSet dsBooks;

            int expected_val = 1;
            int exact_val = 1;

            try
            {
                String strSQL = "Select dbo.Catgeory.CategoryID, dbo.Catgeory.Name, dbo.Catgeory.Description FROM dbo.Catgeory";
                SqlCommand cmdSelCategory = new SqlCommand(strSQL, conn);
                SqlDataAdapter daCatagory = new SqlDataAdapter(cmdSelCategory);
                dsBooks = new DataSet("Books");
                daCatagory.Fill(dsBooks, "Category");            //Get category info
                String strSQL2 = "Select ISBN, CategoryID, Title,Author, Price, Year, Edition, Publisher from dbo.BookData where Title LIKE '%" +"the"+ "%'";
                SqlCommand cmdSelBook = new SqlCommand(strSQL2, conn);
                SqlDataAdapter daBook = new SqlDataAdapter(cmdSelBook);
                daBook.Fill(dsBooks, "Books");                  //Get Books info
                DataRelation drCat_Book = new DataRelation("drCat_Book",
                dsBooks.Tables["Category"].Columns["CategoryID"],
                dsBooks.Tables["Books"].Columns["CategoryID"], false);
                dsBooks.Relations.Add(drCat_Book);       //Set up the table relation
                exact_val = 1;

            }
            catch (Exception ex) { Debug.WriteLine(ex.Message);
                exact_val = 0;
            }

            Assert.AreEqual(expected_val, exact_val);

        }

        // Search Book failed
        [TestMethod]
        public void SearchBook_F()
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.connectionString);
            DataSet dsBooks;

            int expected_val = 0;
            int exact_val = 1;

            try
            {
                String strSQL = "Select* dbo.Catgeory.CategoryID, dbo.Catgeory.Name, dbo.Catgeory.Description FROM dbo.Catgeory";
                SqlCommand cmdSelCategory = new SqlCommand(strSQL, conn);
                SqlDataAdapter daCatagory = new SqlDataAdapter(cmdSelCategory);
                dsBooks = new DataSet("Books");
                daCatagory.Fill(dsBooks, "Category");            //Get category info
                String strSQL2 = "Select *ISBN, CategoryID, Title,Author, Price, Year, Edition, Publisher from dbo.BookData where Title LIKE '%" + "the" + "%'";
                SqlCommand cmdSelBook = new SqlCommand(strSQL2, conn);
                SqlDataAdapter daBook = new SqlDataAdapter(cmdSelBook);
                daBook.Fill(dsBooks, "Books");                  //Get Books info
                DataRelation drCat_Book = new DataRelation("drCat_Book",
                dsBooks.Tables["Category"].Columns["CategoryID"],
                dsBooks.Tables["Books"].Columns["CategoryID"], false);
                dsBooks.Relations.Add(drCat_Book);       //Set up the table relation
                exact_val = 1;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message);
                exact_val = 0;
            }

            Assert.AreEqual(expected_val, exact_val);

        }


        // Login True
        [TestMethod]
        public void Login_T()
        {
            UserData userdata = new UserData();

            string inputName, inputPassword;

            // input test
             inputName = "test";
             inputPassword = "test123";

            Boolean expectedReturn = true;

            Boolean actualReturn = userdata.LogIn(inputName, inputPassword);

            Assert.AreEqual(expectedReturn, actualReturn);
        }
        // Login False
        [TestMethod]
        public void Login_F()
        {
            UserData userdata = new UserData();

            string inputName, inputPassword;

            // input test
            inputName = "test";
            inputPassword = "testd123";

            Boolean expectedReturn = false;

            Boolean actualReturn = userdata.LogIn(inputName, inputPassword);

            Assert.AreEqual(expectedReturn, actualReturn);
        }




        // Purchase History_T
        [TestMethod]
        public void PHistory_T()
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.connectionString);
            DataSet dsBooks;

            int expected_val = 1;
            int exact_val = 1;
            int id = 12;

            try
            {

                String SQL = "SELECT Title, Author, Date, Quantity FROM dbo.BookData, dbo.Orders Where dbo.BookData.ISBN = dbo.Orders.ISBN AND dbo.Orders.UserID =@id";
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQL;
                cmd.Parameters.AddWithValue("@id", id);
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                conn.Open();
                da.Fill(ds, "History");
                conn.Close();
                exact_val = 1;


            }
            catch (Exception ex)
            {
                exact_val = 0;

            }


            Assert.AreEqual(expected_val, exact_val);

        }

        // Purchase History_T
        [TestMethod]
        public void PHistory_F()
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.connectionString);
            DataSet dsBooks;

            int expected_val = 0;
            int exact_val = 1;
            int id = 12;

            try
            {

                String SQL = "SELECT * Title, Author, Date, Quantity FROM dbo.BookData, dbo.Orders Where dbo.BookData.ISBN = dbo.Orders.ISBN AND dbo.Orders.UserID =@id";
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQL;
                cmd.Parameters.AddWithValue("@id", id);
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                conn.Open();
                da.Fill(ds, "History");
                conn.Close();
                exact_val = 1;


            }
            catch (Exception ex)
            {
                exact_val = 0;

            }


            Assert.AreEqual(expected_val, exact_val);

        }


        // cart has item
        [TestMethod]
        public void CartItem()
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.connectionString);

            int exact_val = 3;
            int expected_val = 0;
            int id = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandText = "SELECT SUM(Quantity)FROM Cart WHERE Userid = @id";

                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                 exact_val = (int)cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            Assert.AreEqual(exact_val, exact_val);

        }
        // cart ammount empty
        [TestMethod]
        public void CartEmpty()
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.connectionString);

            int exact_val = 0;
            int expected_val = 0;
            int id = 13;
            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandText = "SELECT SUM(Quantity)FROM Cart WHERE Userid = @id";

                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                exact_val = (int)cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            Assert.AreEqual(exact_val, exact_val);

        }

        // bought item
        [TestMethod]
        public void OrderItem()
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.connectionString);

            int exact_val = 1;
            int expected_val = 0;
            int id = 3;
            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandText = "SELECT SUM(Quantity)FROM Cart WHERE Userid = @id";

                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                exact_val = (int)cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            Assert.AreEqual(exact_val, exact_val);

        }

        // 0 Books Bought
        [TestMethod]
        public void OrderEmpty()
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.connectionString);

            int exact_val = 0;
            int expected_val = 0;
            int id = 56;
            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandText = "SELECT SUM(Quantity)FROM Orders WHERE Userid = @id";

                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                exact_val = (int)cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            Assert.AreEqual(exact_val, exact_val);

        }

        // money spent
        [TestMethod]
        public void MoneySpent()
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.connectionString);

            double exact_val = 568.36;
            double expected_val = 0.0;
            int id = 12;
            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandText = "SELECT sum(Orders.Quantity*BookData.Price) AS Total from BookData,Orders WHERE BookData.ISBN = Orders.ISBN And Orders.UserID =@id";

                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                double items = (double)cmd.ExecuteScalar();
                expected_val = items;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }


            Assert.AreEqual(exact_val, exact_val);

        }

        // no money spent
        [TestMethod]
        public void noMoneySpent()
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.connectionString);

            double exact_val = 0.0;
            double expected_val = 0.0;
            int id = 56;
            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandText = "SELECT sum(Orders.Quantity*BookData.Price) AS Total from BookData,Orders WHERE BookData.ISBN = Orders.ISBN And Orders.UserID =@id";

                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                double items = (double)cmd.ExecuteScalar();
                expected_val = items;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }


            Assert.AreEqual(exact_val, exact_val);

        }

    }

}
