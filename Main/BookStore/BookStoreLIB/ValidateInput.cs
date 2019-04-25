using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows;
using System.Text.RegularExpressions;


namespace BookStoreLIB
{
    public class ValidateInput
    {
        public bool validateUsername(string username)
        {
            //checks if the first character is a letter
            bool valUserName = !String.IsNullOrEmpty(username) && Char.IsLetter(username[0]);

            if (valUserName)
            {
                return true;
            }
            else
            {

                return false;
            }

        }
        public bool validateFullname(string username)
        {
            //checks if the first character is a letter
            bool valUserName = !String.IsNullOrEmpty(username) && Char.IsLetter(username[0]);

            if (valUserName)
            {
                return true;
            }
            else
            {

                return false;
            }

        }
        public bool validatePass(string password)
        {
            //validates password by making sure the user enters 6 characters containing letters
            //and numbers


            var hasNumber = new Regex(@"[0-9]+"); //numbers 0-9
            var hasLetter = new Regex(@"[a-zA-Z]+"); //upper and lower letters
            var has6Chars = new Regex(@".{6,}"); //atleast 6 chars
            bool isLetter = !String.IsNullOrEmpty(password) && Char.IsLetter(password[0]);

            var isValidated = hasNumber.IsMatch(password) && hasLetter.IsMatch(password) && has6Chars.IsMatch(password) && isLetter;
            if (isValidated)
            {
                return true;
            }
            else
            {

                return false;
            }
        }

        public bool passwordConfirmation(string password, string confirm)
        {
            //checks whether the password box and the confirm password box matches
      

            if (password == confirm)
                return true;
            else

            return false;


        }
    }
}
