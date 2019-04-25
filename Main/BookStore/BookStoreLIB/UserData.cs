using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookStoreLIB
{
    public class UserData
    {
        public int UserID { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public bool LogIn(string loginname, string password)
        {
            var dbUser = new DALUserInfo();
            UserID = dbUser.LogIn(loginname, password);

            if (UserID > 0)
            {
                LoginName = loginname;
                Password = password;
                return true;
            }
            else
                return false;

        }

        public int checkAdmin(string username) {

            DALUserInfo dalUser = new DALUserInfo();
            return dalUser.checkAdmin(username);
        }
    }
}