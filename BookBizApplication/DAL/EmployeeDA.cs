using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBizApplication.BLL;
using System.Windows.Forms;
using System.IO;
using BookBizApplication.Validation;


namespace BookBizApplication.DAL
{
    class EmployeeDA
    {
        //Store data in this files

        private static string empFilePath = Application.StartupPath + @"\Data\Employees.dat";
        private static string empTempFilePath = Application.StartupPath + @"\Data\EmployeesTemp.dat";

        //Save Method
        public static void Save(Employee aEmployee)
        {
            Validator checker = new Validator();
            if (checker.isUniqueID(empFilePath, aEmployee.EmpId.ToString()))
            {
                StreamWriter sWriter = new StreamWriter(empFilePath, true);
                sWriter.WriteLine(aEmployee.EmpId + "," + aEmployee.EmpFName + "," + aEmployee.EmpLName + "," + aEmployee.EmpEmail + "," + aEmployee.EmpPhone + "," + aEmployee.EmpPosition + "," + aEmployee.UserLevel + "," + aEmployee.UserPassword);
                sWriter.Close();
                MessageBox.Show("Employee data saved succesfully!");
            }
            return;
            
        }
        
        //Search Methods: ID
        public static Employee SearchID(int empId)
        {
            Employee emp = new Employee();
            StreamReader sReader = new StreamReader(empFilePath);
            string line = sReader.ReadLine();
            while (line!=null)
            {
                string[] fields = line.Split(',');
                if (empId == Convert.ToInt32(fields[0]))
                {
                    emp.EmpId = Convert.ToInt32(fields[0]);
                    emp.EmpFName = fields[1];
                    emp.EmpLName = fields[2];
                    emp.EmpEmail = fields[3];
                    emp.EmpPhone = fields[4];
                    emp.EmpPosition = fields[5];
                    emp.UserLevel = Convert.ToInt32(fields[6]);
                    emp.UserPassword = fields[7];
                    sReader.Close();
                    return emp;
                }
                line = sReader.ReadLine();                   
            }
            sReader.Close();
            return null;
        }

        //First Name
        public static Employee SearchFirst(string empFName)
        {
            Employee emp = new Employee();
            StreamReader sReader = new StreamReader(empFilePath);
            string line = sReader.ReadLine();
            while (line != null)
            {
                string[] fields = line.Split(',');
                if (string.Equals(empFName,fields[1], StringComparison.OrdinalIgnoreCase))
                {
                    emp.EmpId = Convert.ToInt32(fields[0]);
                    emp.EmpFName = fields[1];
                    emp.EmpLName = fields[2];
                    emp.EmpEmail = fields[3];
                    emp.EmpPhone = fields[4];
                    emp.EmpPosition = fields[5];
                    emp.UserLevel = Convert.ToInt32(fields[6]);
                    emp.UserPassword = fields[7];
                    sReader.Close();
                    return emp;
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            return null;
        }

        //Last Name
        public static Employee SearchLast(string empLName)
        {
            Employee emp = new Employee();
            StreamReader sReader = new StreamReader(empFilePath);
            string line = sReader.ReadLine();
            while (line != null)
            {
                string[] fields = line.Split(',');
                if (string.Equals(empLName, fields[2], StringComparison.OrdinalIgnoreCase))
                {
                    emp.EmpId = Convert.ToInt32(fields[0]);
                    emp.EmpFName = fields[1];
                    emp.EmpLName = fields[2];
                    emp.EmpEmail = fields[3];
                    emp.EmpPhone = fields[4];
                    emp.EmpPosition = fields[5];
                    emp.UserLevel = Convert.ToInt32(fields[6]);
                    emp.UserPassword = fields[7];
                    sReader.Close();
                    return emp;
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            return null;
        }
        
        //List Method             
        public static void ListEmployees(ListView listview)
        {
            listview.Items.Clear();
            StreamReader sReader = new StreamReader(empFilePath);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');
                ListViewItem emp = new ListViewItem(fields[0]);
                emp.SubItems.Add(fields[1]);
                emp.SubItems.Add(fields[2]);
                emp.SubItems.Add(fields[5]);
                emp.SubItems.Add(fields[4]);
                emp.SubItems.Add(fields[3]);
                listview.Items.Add(emp);
                line = sReader.ReadLine();
            }
            sReader.Close();
        }
       
        //List search results
        public static void ListSearchResult(Employee emp, ListView listview)
        {
            listview.Items.Clear();

            ListViewItem list = new ListViewItem(emp.EmpId.ToString());
            list.SubItems.Add(emp.EmpFName);
            list.SubItems.Add(emp.EmpLName);
            list.SubItems.Add(emp.EmpPosition);
            list.SubItems.Add(emp.EmpPhone);                
            list.SubItems.Add(emp.EmpEmail);

            listview.Items.Add(list);
            
        }

        //Delete Method   
        public static void Delete(int empId)
        {
            StreamReader sReader = new StreamReader(empFilePath);
            string line = sReader.ReadLine();
            StreamWriter sWriter = new StreamWriter(empTempFilePath,true);
         
            while (line != null)
            {
                string[] fields = line.Split(',');
                if (empId != (Convert.ToInt32(fields[0])))
                {
                    sWriter.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3] + "," + fields[4] + "," + fields[5] + "," + fields[6] + "," + fields[7]);
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            sWriter.Close();
            File.Delete(empFilePath);
            File.Move(empTempFilePath, empFilePath);
            MessageBox.Show("Employee Sucessfully deleted!");

        }

        //Update Method         
        public static void Update(Employee emp)
        {
            DialogResult ans = MessageBox.Show("Are you sure you want to update the employee information?", "Update Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (ans == DialogResult.Yes)
            {
                StreamReader sReader = new StreamReader(empFilePath);
                StreamWriter sWriter = new StreamWriter(empTempFilePath, true);

                string line = sReader.ReadLine();

                while (line != null)
                {
                    string[] fields = line.Split(',');
                    if ((Convert.ToInt32(fields[0])) != (emp.EmpId))
                    {
                        sWriter.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3] + "," + fields[4] + "," + fields[5] + "," + fields[6] + "," + fields[7]);
                    }
                    line = sReader.ReadLine();
                }
                sWriter.WriteLine(emp.EmpId + "," + emp.EmpFName + "," + emp.EmpLName + "," + emp.EmpEmail + "," + emp.EmpPhone + "," + emp.EmpPosition + "," + emp.UserLevel + "," + emp.UserPassword);
                sReader.Close();
                sWriter.Close();
                File.Delete(empFilePath);
                File.Move(empTempFilePath, empFilePath);
                MessageBox.Show("Employee record successfully updated");
            }                                                           
            

        }  

    }
}
