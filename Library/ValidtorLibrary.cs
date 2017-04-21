using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Net.Mail;

namespace Todo.Library
{
    public class ValidtorLibrary
    {
        public bool IsValidEmailAddress(String email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}