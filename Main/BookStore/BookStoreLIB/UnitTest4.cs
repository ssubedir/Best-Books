using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookStoreLIB
{
    [TestClass]
    public class UnitTest4
    {
        [TestMethod]
        public void TestMethod1()
        {
            string isbn = "6383724";
            string catid = "2";
            string title = "Tom and Jerry";
            string author = "Bob";
            string price = "20003";
            string supplier = "5";
            string publisher = "5";
            string edition = "5";
            string year = "1994";

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
        }
    }
}
