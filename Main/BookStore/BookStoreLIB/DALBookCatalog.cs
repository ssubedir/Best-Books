using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace BookStoreLIB
{
    class DALBookCatalog
    {
        SqlConnection conn;
        DataSet dsBooks;
        public DALBookCatalog()
        {
            conn = new SqlConnection(Properties.Settings.Default.connectionString);
        }
        public DataSet GetBookInfo()
        {
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
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return dsBooks;
        }

        public DataSet GetSearchBookInfo( string key)
        {
            try
            {
                String strSQL = "Select dbo.Catgeory.CategoryID, dbo.Catgeory.Name, dbo.Catgeory.Description FROM dbo.Catgeory";
                SqlCommand cmdSelCategory = new SqlCommand(strSQL, conn);
                SqlDataAdapter daCatagory = new SqlDataAdapter(cmdSelCategory);
                dsBooks = new DataSet("Books");
                daCatagory.Fill(dsBooks, "Category");            //Get category info
                String strSQL2 = "Select ISBN, CategoryID, Title,Author, Price, Year, Edition, Publisher from dbo.BookData where Title LIKE '%" + key + "%'";
                SqlCommand cmdSelBook = new SqlCommand(strSQL2, conn);
                SqlDataAdapter daBook = new SqlDataAdapter(cmdSelBook);
                daBook.Fill(dsBooks, "Books");                  //Get Books info
                DataRelation drCat_Book = new DataRelation("drCat_Book",
                dsBooks.Tables["Category"].Columns["CategoryID"],
                dsBooks.Tables["Books"].Columns["CategoryID"], false);
                dsBooks.Relations.Add(drCat_Book);       //Set up the table relation
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return dsBooks;

        }

        public DataSet loadItem(string isbn)
        {
            try
            {

                String SQL = "select ISBN,Title,Author,Price,Edition,Publisher,Stock  From BookData where ISBN = @isbn;";
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQL;
                cmd.Parameters.AddWithValue("@isbn", isbn);
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                conn.Open();
                da.Fill(ds, "Item");
                conn.Close();
                return ds;

            }
            catch (Exception ex)
            {

            }
            return dsBooks;

        }

        public DataSet loadHistory(int id)
        {
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
                return ds;

            }
            catch (Exception ex)
            {

            }
            return dsBooks;

        }
    }


}