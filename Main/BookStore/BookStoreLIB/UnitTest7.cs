using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;


/*
All these test cases were created by Rahul  
*/

namespace BookStoreLIB
{
    [TestClass]
    public class UnitTest7
    {
        DALUserInfo user = new DALUserInfo();
        ValidateInput val = new ValidateInput();

        [TestMethod]
        public void TestMethod1()
        {
            string username = "Jimmy";
            string fullname = "Jimmy Johns";
            string password = "jimmy96";

            var conn = new SqlConnection(Properties.Settings.Default.connectionString);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = " IF EXISTS (Select[User DB].Username from dbo.[User DB] WHERE Username = @UserName) BEGIN  Select 1 END ELSE BEGIN insert into [User DB] ([UserName],[Full Name],[Password],[Type]) values (@UserName,@Fullname,@Password, @User) END";

                cmd.Parameters.AddWithValue("@Fullname", fullname);
                cmd.Parameters.AddWithValue("@UserName", username);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@User", 2);
                cmd.ExecuteScalar();

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
        [TestMethod]
        public void TestUserName()
        {

            bool validate = val.validateUsername("1Jimmy");
            Assert.IsFalse(validate);

        }
        [TestMethod]
        public void TestPassword()
        {


            bool validate = val.validatePass("Jimmy23");
            Assert.IsTrue(validate);

        }
        [TestMethod]
        public void TestConfirmPassword()
        {


            bool validate = val.passwordConfirmation("Password20", "Password20");
            Assert.IsTrue(validate);

        }

    }
}
