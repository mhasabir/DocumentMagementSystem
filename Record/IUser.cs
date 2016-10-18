using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Record
{
    interface IUser
    {
           string getUser(string username);
           void setuser(string user);
           string UserId();
           string passwordVerification(string pass);
           string UserName();
           string department();
           string userType();
           string showProfilePicture();
           string emailId();
    }
}
