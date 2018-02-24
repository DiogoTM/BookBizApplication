using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BookBizApplication.BLL;
using BookBizApplication.DAL;
using System.IO;

namespace BookBizApplication.GUI
{
    public partial class BookBizLogin : Form
    {
        public BookBizLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            
            //Create new form
            BookBizApp app = new BookBizApp();

            //Check if the data entered is correct

            if (EmployeeDA.SearchID(Convert.ToInt32(textBoxUserID.Text)) == null)
            {
                MessageBox.Show("User not found!");
                textBoxUserID.Clear();
                textBoxUserPswd.Clear();
                textBoxUserID.Focus();
                return;
            }

            if (EmployeeDA.SearchID(Convert.ToInt32(textBoxUserID.Text)).UserPassword != textBoxUserPswd.Text)
            {
                MessageBox.Show("Incorrect Password, please try again");
                textBoxUserPswd.Clear();
                textBoxUserPswd.Focus();
                return;
            }


            //Check if the id entered is from a user 

            if (EmployeeDA.SearchID(Convert.ToInt32(textBoxUserID.Text)).UserLevel == 0)
            {
                MessageBox.Show("Employee is not a user");
                textBoxUserID.Clear();
                textBoxUserPswd.Clear();
                textBoxUserID.Focus();
                return;
            }

            //Based on the login information, opens the right form to the correspondent user
            switch (EmployeeDA.SearchID(Convert.ToInt32(textBoxUserID.Text)).UserLevel)
            {
                case 1:
                    app.ChangeTab(3);
                    app.Show();
                    break;
                case 2:
                    app.ChangeTab(2);
                    app.Show();
                    break;
                case 3:
                    app.ChangeTab(1);
                    app.Show();
                    break;
                case 4:
                    app.ChangeTab(0);
                    app.Show();
                    break;
            }
            this.Hide();                           
        }
    }
}
