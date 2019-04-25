using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreLIB
{
    public class BookCart
    {
        //c string
        SqlConnection conn;
        DataSet dscart;
        public BookCart()
        {
            conn = new SqlConnection(Properties.Settings.Default.connectionString);

        }

        public void AddBook(int id, string ISBN, int quantity)
        {

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

        }

        public void RemoveBook(int id,string ISBN)
        {

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM Cart WHERE Cart.UserId = @Id AND Cart.ISBN = @ISBN";
                cmd.Parameters.AddWithValue("@ISBN", ISBN);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
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

        }
        
        public DataSet loadCart(int id)
        {
            try
            {

                String SQL = "select BookData.ISBN, BookData.Title,Cart.Quantity, (Cart.Quantity*BookData.Price) AS SubTotal FROM Cart INNER JOIN BookData ON Cart.ISBN = BookData.ISBN WHERE Cart.UserId = @Id";
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQL;
                cmd.Parameters.AddWithValue("@Id", id);
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                conn.Open();
                da.Fill(ds,"Cart");
                conn.Close();
                return ds;

            }
            catch (Exception ex)
            {
                
            }
            return dscart;

        }

        public void removeAll(int id)
        {

            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM Cart WHERE Cart.UserId = @ID";
                cmd.Parameters.AddWithValue("@ID", id);
                conn.Open();
                cmd.ExecuteNonQuery();

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
        }

        public int cartItems(int id)
        {

            // write test case
            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandText = "SELECT SUM(Quantity)FROM Cart WHERE Userid = @id";

                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                int items = (int)cmd.ExecuteScalar();

                return items;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return 0;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public int colSum(int id)
        {

            int sum;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT SUM(Cart.Quantity*Price) AS Total FROM dbo.BookData ,dbo.Cart WHERE dbo.BookData.ISBN = dbo.Cart.ISBN AND dbo.Cart.UserId = @ID";
                cmd.Parameters.AddWithValue("@ID", id);
                conn.Open();
                cmd.ExecuteNonQuery();
                return sum = Convert.ToInt32(cmd.ExecuteScalar());
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return 0;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();           
            }
           

           
        }

        public int coupon(string id)
        {

            int sum;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT CouponDB.Discount FROM CouponDB WHERE dbo.CouponDB.Coupon = @id";
                cmd.Parameters.AddWithValue("@ID", id);
                conn.Open();
                cmd.ExecuteNonQuery();
                return sum = Convert.ToInt32(cmd.ExecuteScalar());

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return 0;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }



        }
        public void cBook(int id, string ISBN, int quantity)
        {

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO Orders (ISBN,Quantity,UserID,Orders.Date) VALUES(@isbn, @QUANTITY, @id, GETDATE())";
                cmd.Parameters.AddWithValue("@ISBN", ISBN);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@QUANTITY", quantity);
                conn.Open();
                cmd.ExecuteNonQuery();
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

        }

        public int totalBooks(int id)
        {

            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandText = "SELECT sum(Quantity) from Orders WHERE Orders.UserID = @id";

                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                int items = (int)cmd.ExecuteScalar();

                return items;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return 0;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public double totalPrice(int id)
        {

            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandText = "SELECT sum(Orders.Quantity*BookData.Price) AS Total from BookData,Orders WHERE BookData.ISBN = Orders.ISBN And Orders.UserID =@id";

                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                double items = (double)cmd.ExecuteScalar();

                return items;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return 0.0;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }




    }
}
