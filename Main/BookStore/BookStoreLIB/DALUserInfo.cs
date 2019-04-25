using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows;

namespace BookStoreLIB
{
    public class DALUserInfo
    {
        public int Register(string Fullname, string userName, string password, int user)
        {
            var conn = new SqlConnection(Properties.Settings.Default.connectionString);
            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandText = " IF EXISTS (Select[User DB].Username from dbo.[User DB] WHERE Username = @UserName) BEGIN  Select 1 END ELSE BEGIN insert into [User DB] ([UserName],[Full Name],[Password],[Type]) values (@UserName,@Fullname,@Password, @User) END";
               
                cmd.Parameters.AddWithValue("@Fullname", Fullname);
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@User", user);
                conn.Open();
                int userId = (int)cmd.ExecuteScalar();

                return userId;
               
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return -1;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
        public int LogIn(string userName, string password)
        {
            var conn = new SqlConnection(Properties.Settings.Default.connectionString);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select [User DB].Id from dbo.[User DB] WHERE Username = @UserName AND Password = @Password ";
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@Password", password);
                conn.Open();
                int userId = (int)cmd.ExecuteScalar();
                if (userId > 0) return userId;
                else return -1;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return -1;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public int checkAdmin(string userName)
        {
            var conn = new SqlConnection(Properties.Settings.Default.connectionString);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select [User DB].Type from dbo.[User DB] WHERE Username = @UserName";
                cmd.Parameters.AddWithValue("@UserName", userName);
                conn.Open();

                string isAdmin = (string)cmd.ExecuteScalar();

                int result = Int32.Parse(isAdmin);

                return result;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return -1;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    
                }
                    

            }
        }
    }
}
