using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBizApplication.BLL;
using BookBizApplication.Validation;
using System.Windows.Forms;
using System.IO;


namespace BookBizApplication.DAL
{
    class AuthorDA
    {
       
        //Store data in this file
        private static string autFilePath = Application.StartupPath + @"\Data\Authors.dat";
        private static string autTempFilePath = Application.StartupPath + @"\Data\AuthorsTemp.dat";

        //Save Method
        public static void Save(Author aAuthor)
        {
            Validator checker = new Validator();
            if (checker.isUniqueID(autFilePath, aAuthor.AutID.ToString()))
            {
                StreamWriter sWriter = new StreamWriter(autFilePath, true);
                sWriter.WriteLine(aAuthor.AutID + "," + aAuthor.AutFirstN + "," + aAuthor.AutLastN + "," + aAuthor.AutEmail);
                sWriter.Close();
                MessageBox.Show("Author data saved succesfully!");
            }

        }

        //Search Methods: ID
        public static Author SearchID(int autId)
        {
            Author aut = new Author();
            StreamReader sReader = new StreamReader(autFilePath);
            string line = sReader.ReadLine();
            while (line != null)
            {
                string[] fields = line.Split(',');
                if (autId == Convert.ToInt32(fields[0]))
                {
                    aut.AutID = Convert.ToInt32(fields[0]);
                    aut.AutFirstN = fields[1];
                    aut.AutLastN = fields[2];
                    aut.AutEmail = fields[3];
                    sReader.Close();
                    return aut;
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            return null;
        }

        //First Name
        public static Author SearchFirst(string autFName)
        {
            Author emp = new Author();
            StreamReader sReader = new StreamReader(autFilePath);
            string line = sReader.ReadLine();
            while (line != null)
            {
                string[] fields = line.Split(',');
                if (string.Equals(autFName, fields[1], StringComparison.OrdinalIgnoreCase))
                {
                    emp.AutID = Convert.ToInt32(fields[0]);
                    emp.AutFirstN = fields[1];
                    emp.AutLastN = fields[2];
                    emp.AutEmail = fields[3];                   
                    sReader.Close();
                    return emp;
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            return null;
        }

        //Last Name
        public static Author SearchLast(string autLName)
        {
            Author emp = new Author();
            StreamReader sReader = new StreamReader(autFilePath);
            string line = sReader.ReadLine();
            while (line != null)
            {
                string[] fields = line.Split(',');
                if (string.Equals(autLName, fields[2], StringComparison.OrdinalIgnoreCase))
                {
                    emp.AutID = Convert.ToInt32(fields[0]);
                    emp.AutFirstN = fields[1];
                    emp.AutLastN = fields[2];
                    emp.AutEmail = fields[3];                
                    sReader.Close();
                    return emp;
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            return null;
        }

        //List Method
        public static void ListAuthors(ListView listview)
        {
            listview.Items.Clear();
            StreamReader sReader = new StreamReader(autFilePath);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');
                ListViewItem aut = new ListViewItem(fields[0]);
                aut.SubItems.Add(fields[1]);
                aut.SubItems.Add(fields[2]);
                aut.SubItems.Add(fields[3]);                
                listview.Items.Add(aut);
                line = sReader.ReadLine();
            }
            sReader.Close();
        }
        
        //List search result or single Author
        public static void ListSearchResult(Author aut, ListView listview)
        {
            listview.Items.Clear();
            ListViewItem list = new ListViewItem(aut.AutID.ToString());
            list.SubItems.Add(aut.AutFirstN);
            list.SubItems.Add(aut.AutLastN);
            list.SubItems.Add(aut.AutEmail);                  
            listview.Items.Add(list);     
        }

        //Delete Method      
        public static void Delete(int autId)
        {
            StreamReader sReader = new StreamReader(autFilePath);
            string line = sReader.ReadLine();
            StreamWriter sWriter = new StreamWriter(autTempFilePath, true);

            while (line != null)
            {
                string[] fields = line.Split(',');
                if ((autId) != (Convert.ToInt32(fields[0])))
                {
                    sWriter.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3]);
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            sWriter.Close();
            File.Delete(autFilePath);
            File.Move(autTempFilePath, autFilePath);
            MessageBox.Show("Author Sucessfully deleted!");

        }

        //Update Method              
        public static void Update(Author aut)
        {
            DialogResult ans = MessageBox.Show("Are you sure you want to update the Author information?", "Update Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (ans == DialogResult.Yes)
            {
                StreamReader sReader = new StreamReader(autFilePath);
                StreamWriter sWriter = new StreamWriter(autTempFilePath, true);

                string line = sReader.ReadLine();

                while (line != null)
                {
                    string[] fields = line.Split(',');
                    if ((Convert.ToInt32(fields[0])) != (aut.AutID))
                    {
                        sWriter.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3]);
                    }
                    line = sReader.ReadLine();
                }
                sWriter.WriteLine(aut.AutID + "," + aut.AutFirstN + "," + aut.AutLastN + "," + aut.AutEmail);
                sReader.Close();
                sWriter.Close();
                File.Delete(autFilePath);
                File.Move(autTempFilePath, autFilePath);
                MessageBox.Show("Author Data Successfully updated!");
            }

        }
    }
}
