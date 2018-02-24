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
    public partial class ChangePswd : Form
    {
        public ChangePswd()
        {
            InitializeComponent();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            Employee aEmployee = EmployeeDA.SearchID(Convert.ToInt32(textBoxLogin.Text));
            if (aEmployee == null)
            {
                MessageBox.Show("Login not found, please retry.");
                textBoxLogin.Clear();
                textBoxLogin.Focus();
                return;
            }

            if (aEmployee.UserPassword != textBoxOldPsw.Text)
            {
                MessageBox.Show("Original password is wrong, please retry.");
                textBoxOldPsw.Clear();
                textBoxOldPsw.Focus();
                return;
            }

            aEmployee.UserPassword = textBoxNewPsw.Text;
            EmployeeDA.Update(aEmployee);            
                
        }
    }
}
