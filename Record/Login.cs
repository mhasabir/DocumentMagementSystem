using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Record
{
    public partial class Form1 : Form
    {
        UserCl ob = new UserCl();
        string usertype;

        List<Panel> listPanel = new List<Panel>();

        public Form1()
        {
            InitializeComponent();

        }



        private void Login_btn_Click(object sender, EventArgs e)
        {
                string username = User_name_text.Text;
                string password = Password_txt.Text;
                string message, user;
                string message2 = "verified";
                user = ob.getUser(username); 
   
                if (user.Equals("User Not Found"))
                { 
                    label2.Text = user; 
                }
                else
                {   usertype =ob.userType(); 
                    message =ob.passwordVerification(password);
                    if (message2.Equals(message))
                    {

                    if(usertype.Equals("Student"))
                    {
                        this.Hide();
                        StudentF objSF = new StudentF();
                        objSF.start(user,usertype);
                        objSF.ShowDialog();
                        this.Close();
                    }
                    else if (usertype.Equals("Teacher"))
                    {
                        this.Hide();
                        TeacherF objTF = new TeacherF();
                        objTF.start(user, usertype);
                        objTF.ShowDialog();
                        this.Close();
                    }
                    else if (usertype.Equals("Staff"))
                    {
                        this.Hide();
                        StaffF objSTF = new StaffF();
                        objSTF.start(user, usertype);
                        objSTF.ShowDialog();
                        this.Close();
                    }
                    else if (usertype.Equals("Admin"))
                    {
                        this.Hide();
                        AdminF obAF = new AdminF();
                        obAF.start(user, usertype);
                        obAF.ShowDialog();
                        this.Close();
                    }
                    }
                    else
                    { label2.Text = message;}
                    
                }
                
            
        }   

        private void User_name_text_Click(object sender, EventArgs e)
        {
            User_name_text.Clear();
            User_name_text.ForeColor = Color.Black;
        }


        private void User_name_text_Leave(object sender, EventArgs e)
        {
            if (User_name_text.Text == "")
            {
                User_name_text.Text = "User Name ";
                User_name_text.ForeColor = Color.Gray;
            }

        }


        private void Password_txt_Click(object sender, EventArgs e)
        {

            Password_txt.Clear();
            Password_txt.ForeColor = Color.Black;
        }


        private void Password_txt_Leave(object sender, EventArgs e)
        {

            if (Password_txt.Text == "")
            {
                Password_txt.Text = "Password ";
                Password_txt.ForeColor = Color.Gray;
            }

        }
        

    }

}
