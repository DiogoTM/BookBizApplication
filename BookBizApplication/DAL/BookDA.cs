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
    class BookDA
    {
        //Creating the author data

        private static string autFilePath = Application.StartupPath + @"\Data\Authors.dat";
    
        //Creates a authors' name list
        public static void authorSelection(ComboBox combobox)
        {
            StreamReader sReader = new StreamReader(autFilePath);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');
                combobox.Items.Add(fields[2] + ", " + fields[1]);
                
                line = sReader.ReadLine();
            }
            sReader.Close();
        }

        //Creating publisher data

        private static string pubFilePath = Application.StartupPath + @"\Data\Publishers.dat";

        //Creates a publishers' name list
        public static void publisherSelection(ComboBox combobox)
        {
            StreamReader sReader = new StreamReader(pubFilePath);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');
                combobox.Items.Add(fields[0]);
                line = sReader.ReadLine();
            }
            sReader.Close();
        }


        //Store data in this file
        private static string bkFilePath = Application.StartupPath + @"\Data\Books.dat";
        private static string bkTempFilePath = Application.StartupPath + @"\Data\TempBooks.dat";


        //Save Method
        public static void Save(Book aBook)
        {
            Validator checker = new Validator();
            if (checker.isUniqueLongID(bkFilePath, aBook.BkISBN.ToString()))
            {
                StreamWriter sWriter = new StreamWriter(bkFilePath, true);
                sWriter.WriteLine(aBook.BkISBN + "," + aBook.BkTitle + "," + aBook.BkQOH + "," + aBook.BkDate + "," + aBook.BkAuthor.AutID + "," + aBook.BkCategory + "," + aBook.BkPublisher + "," + aBook.BkUnitPrice);
                sWriter.Close();
                MessageBox.Show("Book data saved succesfully!");
            }

        }
                                                  
        //Search Methods: ID
        public static Book SearchISBN(long isbn)
        {
            Book bk = new Book();
            StreamReader sReader = new StreamReader(bkFilePath);
            string line = sReader.ReadLine();
            while (line != null)
            {
                string[] fields = line.Split(',');
                if (isbn == Convert.ToInt64(fields[0]))
                {
                    bk.BkISBN = Convert.ToInt64(fields[0]);
                    bk.BkTitle = fields[1];
                    bk.BkQOH = Convert.ToInt32(fields[2]);
                    bk.BkDate = Convert.ToDateTime(fields[3]);
                    bk.BkAuthor = AuthorDA.SearchID(Convert.ToInt32(fields[4]));
                    bk.BkCategory = fields[5];
                    bk.BkPublisher = fields[6];
                    bk.BkUnitPrice = Convert.ToDecimal(fields[7]);
                    sReader.Close();
                    return bk;
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            return null;
        }

        //Book Title
        public static Book SearchTitle(string title)
        {
            Book bk = new Book();
            StreamReader sReader = new StreamReader(bkFilePath);
            string line = sReader.ReadLine();
            while (line != null)
            {
                string[] fields = line.Split(',');
                if (fields[1].Contains(title))
                {
                    bk.BkISBN = Convert.ToInt64(fields[0]);
                    bk.BkTitle = fields[1];
                    bk.BkQOH = Convert.ToInt32(fields[2]);
                    bk.BkDate = Convert.ToDateTime(fields[3]);
                    bk.BkAuthor = AuthorDA.SearchID(Convert.ToInt32(fields[4]));
                    bk.BkCategory = fields[5];
                    bk.BkPublisher = fields[6];
                    bk.BkUnitPrice = Convert.ToDecimal(fields[7]);
                    sReader.Close();
                    return bk;
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            return null;
        }

        //Author

        public static Book SearchAuthor(int id)
        {
            Book bk = new Book();
            StreamReader sReader = new StreamReader(bkFilePath);
            string line = sReader.ReadLine();
            while (line != null)
            {
                string[] fields = line.Split(',');
                if (Convert.ToInt32(fields[4]) == id)
                {
                    bk.BkISBN = Convert.ToInt64(fields[0]);
                    bk.BkTitle = fields[1];
                    bk.BkQOH = Convert.ToInt32(fields[2]);
                    bk.BkDate = Convert.ToDateTime(fields[3]);
                    bk.BkAuthor = AuthorDA.SearchID(Convert.ToInt32(fields[4]));
                    bk.BkCategory = fields[5];
                    bk.BkPublisher = fields[6];
                    bk.BkUnitPrice = Convert.ToDecimal(fields[7]);
                    sReader.Close();
                    return bk;
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            return null;
        }         

        //List Method
        public static void ListBooks(ListView listview)
        {
            listview.Items.Clear();
            StreamReader sReader = new StreamReader(bkFilePath);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');
                ListViewItem bk = new ListViewItem(fields[0]);
                Author aAuthor = AuthorDA.SearchID(Convert.ToInt32(fields[4]));                           
                bk.SubItems.Add(fields[1]);
                bk.SubItems.Add(fields[2]);
                bk.SubItems.Add(fields[3]);
                bk.SubItems.Add(aAuthor.AutLastN + ", " + aAuthor.AutFirstN);
                bk.SubItems.Add(fields[5]);
                bk.SubItems.Add(fields[6]);
                bk.SubItems.Add(fields[7]);
                listview.Items.Add(bk);
                line = sReader.ReadLine();
            }
            sReader.Close();
        }

        //List Search Results
        public static void ListSearchResult(Book bk, ListView listview)
        {
            listview.Items.Clear();
            ListViewItem list = new ListViewItem(bk.BkISBN.ToString());
            list.SubItems.Add(bk.BkTitle);
            list.SubItems.Add(bk.BkQOH.ToString());
            list.SubItems.Add(bk.BkDate.ToString());
            list.SubItems.Add(bk.BkAuthor.AutLastN + ", " + bk.BkAuthor.AutFirstN);
            list.SubItems.Add(bk.BkCategory);
            list.SubItems.Add(bk.BkPublisher);
            list.SubItems.Add(bk.BkUnitPrice.ToString());              
            listview.Items.Add(list);     
        }

        //Delete Method    
        public static void Delete(long isbn)
        {
            StreamReader sReader = new StreamReader(bkFilePath);
            string line = sReader.ReadLine();
            StreamWriter sWriter = new StreamWriter(bkTempFilePath, true);

            while (line != null)
            {
                string[] fields = line.Split(',');
                if ((isbn) != (Convert.ToInt64(fields[0])))
                {
                    sWriter.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3] + "," + fields[4] + "," + fields[5] + "," + fields[6] + "," + fields[7]);
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            sWriter.Close();
            File.Delete(bkFilePath);
            File.Move(bkTempFilePath, bkFilePath);
            MessageBox.Show("Book Sucessfully deleted!");

        }

        //Update Method     
        public static void Update(Book aBook)
        {
            StreamReader sReader = new StreamReader(bkFilePath);
            string line = sReader.ReadLine();
            StreamWriter sWriter = new StreamWriter(bkTempFilePath, true);

            while (line != null)
            {
                string[] fields = line.Split(',');
                if ((aBook.BkISBN) != (Convert.ToInt64(fields[0])))
                {
                    sWriter.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3] + "," + fields[4] + "," + fields[5] + "," + fields[6] + "," + fields[7]);
                }
                line = sReader.ReadLine();
            }
            sWriter.WriteLine(aBook.BkISBN + "," + aBook.BkTitle + "," + aBook.BkQOH + "," + aBook.BkDate + "," + aBook.BkAuthor.AutID + "," + aBook.BkCategory + "," + aBook.BkPublisher + "," + aBook.BkUnitPrice);
            sReader.Close();
            sWriter.Close();
            File.Delete(bkFilePath);
            File.Move(bkTempFilePath, bkFilePath);
            
            MessageBox.Show("Book Data Successfully updated!");
            

        }
    }
}
