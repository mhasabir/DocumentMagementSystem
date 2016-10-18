using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Record
{
    class UserCl : IUser
    {
        DatabaseCl temp = new DatabaseCl();
      
        string user;

        public string getUser(string username)
        {
            user = temp.userTableSelect(username);
            return user;
        }

        public void setuser(string user)
        {
            temp.userTableSelect(user);
        }

        public string UserId()
        {
            return temp.getUserId();
        }


        public string passwordVerification(string pass)
        {

            string message1 = "Invalid Password";
            string message2 = "verified";
            string value;
            value = temp.getPassword();

            if (pass.Equals(value))
            {
                return message2;
            }
            else
                return message1;
        }
        public string UserName()
        {
            return temp.getUsername();


        }

        public string department()
        {
            return temp.getDepartment();

        }
        public string userType()
        {
            return temp.getUsertype();
        }
        public string showProfilePicture()
        {
            return temp.showImage();
        }
        public string emailId()
        {
            return temp.getEmail();

        }

          
        }
    }

