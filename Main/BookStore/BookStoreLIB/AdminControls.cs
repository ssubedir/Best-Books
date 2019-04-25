using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BookStoreLIB
{
    public class AdminControls
    {
        SqlConnection conn;
        DataSet dsinv;

        public AdminControls()
        {
            conn = new SqlConnection(Properties.Settings.Default.connectionString);
        }

        public bool RemoveBookbyISBN(string ISBN)
        {

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM BookData WHERE BookData.ISBN = @ISBN";
                cmd.Parameters.AddWithValue("@ISBN", ISBN);
                conn.Open();
                cmd.ExecuteNonQuery();

                try
                {
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.Connection = conn;
                    cmd2.CommandText = "SELECT * FROM BookData WHERE BookData.ISBN = @ISBN";
                    cmd2.Parameters.AddWithValue("@ISBN", ISBN);
                    conn.Open();
                    cmd2.ExecuteNonQuery();

                    int r = (int)cmd.ExecuteNonQuery();

                    if (r > 0) //if row is found then book has not been deleted
                    {
                        return false;
                    }
                    else //if no book has been found then the remove book operation was a success
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    return false;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

        }

        public bool RemoveBookbyTitle(string Title)
        {

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM BookData WHERE BookData.Title = @Title";
                cmd.Parameters.AddWithValue("@Title", Title);
                conn.Open();
                cmd.ExecuteNonQuery();


                try
                {
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.Connection = conn;
                    cmd2.CommandText = "SELECT * FROM BookData WHERE BookData.Title = @Title";
                    cmd2.Parameters.AddWithValue("@Title", Title);
                    conn.Open();
                    cmd2.ExecuteNonQuery();


                    int r = (int)cmd.ExecuteNonQuery();

                    if (r > 0) //if row is found then book has not been deleted
                    {
                        return false;
                    }
                    else //if no book has been found then the remove book operation was a success
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    return false;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public Boolean AddBook(string isbn, string catid, string title, string author, string price, string supplier, string publisher, string edition, string year )
        {

            var conn = new SqlConnection(Properties.Settings.Default.connectionString);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO dbo.BookData ([ISBN], [CategoryID], [Title], [Author], [Price], [SupplierID], [Year], [Edition], [Publisher])" +
                    "VALUES (@ISBN, @CategoryID,@Title, @Author, @Price, @Supplier, @Year, @Edition, @Publisher)";
                cmd.Parameters.AddWithValue("@ISBN", isbn);
                cmd.Parameters.AddWithValue("@CategoryID", catid);
                cmd.Parameters.AddWithValue("@Title", title);
                cmd.Parameters.AddWithValue("@Author", author);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.Parameters.AddWithValue("@Supplier", supplier);
                cmd.Parameters.AddWithValue("@Year", year);
                cmd.Parameters.AddWithValue("@Edition", edition);
                cmd.Parameters.AddWithValue("@Publisher", publisher);
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
            return true;
        }

        public bool UpdateInventory(string isbn, int quantity)
        {

            try
            {
                //UPDATE BookData SET BookData.Quantity = 10 WHERE BookData.ISBN= '007533738-X'
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE BookData SET BookData.Stock = @Stock WHERE BookData.ISBN=@ISBN";
                cmd.Parameters.AddWithValue("@Stock", quantity);
                cmd.Parameters.AddWithValue("@ISBN", isbn);
                conn.Open();

                int r = (int)cmd.ExecuteNonQuery();

                if (r > 0)
                {
                    return true;
                }
                else {
                    return false;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
            finally {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }


        public DataSet loadInv()
        {
            try
            {

                String SQL = "SELECT ISBN, Title, Author, Price FROM BookData";
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQL;
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                conn.Open();
                da.Fill(ds, "Inventory");
                conn.Close();
                return ds;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            return dsinv;
        }

        public DataSet loadInv2()
        {
            try
            {

                String SQL = "SELECT ISBN, Title, Author, Price, Stock FROM BookData";
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQL;
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                conn.Open();
                da.Fill(ds, "Inventory");
                conn.Close();
                return ds;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            return dsinv;
        }


    }
}
