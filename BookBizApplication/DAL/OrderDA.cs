using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBizApplication.BLL;
using BookBizApplication.DAL;
using BookBizApplication.Validation;
using System.Windows.Forms;
using System.IO;


namespace BookBizApplication.DAL
{
    class OrderDA
    {


        //Creating the combo boxes
        private static string clFilePath = Application.StartupPath + @"\Data\Clients.dat";
        private static string bkFilePath = Application.StartupPath + @"\Data\Books.dat";
        public static void clientSelection(ComboBox combobox)
        {
            StreamReader sReader = new StreamReader(clFilePath);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');
                combobox.Items.Add(fields[1]);
                line = sReader.ReadLine();
            }
            sReader.Close();
        }

        public static void bookSelection(ComboBox combobox)
        {
            StreamReader sReader = new StreamReader(bkFilePath);
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

        private static string ordFilePath = Application.StartupPath + @"\Data\Order.dat";
        private static string ordTempFilePath = Application.StartupPath + @"\Data\TempOrder.dat";

        //Save Method
        public static void SaveOrder(Order aorder)
        {
            StreamWriter sWriter = new StreamWriter(ordFilePath, true);
            sWriter.WriteLine(aorder.OrdId + "," + aorder.OrdClient.ClientId + "," + aorder.OrdBbook.BkISBN + "," + aorder.OrdDate + "," + aorder.OrdBkQuantity + ", " + aorder.OrdCost + "," + aorder.OrdType);
            sWriter.Close();
            MessageBox.Show("Order saved successfully!");
        }

        //Determining the orderID

        public static int OrderID()
        {                
            Order aorder = new Order();
            int ordId = 0;
            StreamReader sReader = new StreamReader(ordFilePath);
            string line = sReader.ReadLine();
            while (line != null)
            {
                string[] fields = line.Split(',');
                line = sReader.ReadLine();
                ordId = Convert.ToInt32(fields[0]);
            }
            sReader.Close();             
            return ordId;           
        }
        //Search by ID        
        public static Order SearchID(int searchedId)
        {
            Order aorder = new Order();
            StreamReader sReader = new StreamReader(ordFilePath);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');
                if (searchedId == Convert.ToInt32(fields[0]))
                {
                    aorder.OrdId = Convert.ToInt32(fields[0]);
                    aorder.OrdClient = ClientDA.SearchID(Convert.ToInt32(fields[1]));
                    aorder.OrdBbook = BookDA.SearchISBN(Convert.ToInt64(fields[2]));
                    aorder.OrdDate = Convert.ToDateTime(fields[3]);
                    aorder.OrdBkQuantity = Convert.ToInt32(fields[4]);
                    aorder.OrdCost = Convert.ToDecimal(fields[5]);
                    aorder.OrdType = fields[6];
                    sReader.Close();
                    return aorder;
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            return null;
        }

        //Search by Client ID     
        public static Order SearchByClientID(int searchedid)
        {
            Order aorder = new Order();
            StreamReader sReader = new StreamReader(ordFilePath);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');
                if (searchedid == Convert.ToInt32(fields[1]))
                {
                    aorder.OrdId = Convert.ToInt32(fields[0]);
                    aorder.OrdClient = ClientDA.SearchID(Convert.ToInt32(fields[1]));
                    aorder.OrdBbook = BookDA.SearchISBN(Convert.ToInt64(fields[2]));
                    aorder.OrdDate = Convert.ToDateTime(fields[3]);
                    aorder.OrdBkQuantity = Convert.ToInt32(fields[4]);
                    aorder.OrdCost = Convert.ToDecimal(fields[5]);
                    aorder.OrdType = fields[6];
                    sReader.Close();
                    return aorder;
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            return null;
        }
        //Search by Client Name           
        public static Order SearchByClientName(string searchedname)
        {
            Order aorder = new Order();
            StreamReader sReader = new StreamReader(ordFilePath);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');
                if (searchedname == ClientDA.SearchName(aorder.OrdClient.ClientName).ToString())
                {
                    aorder.OrdId = Convert.ToInt32(fields[0]);
                    aorder.OrdClient = ClientDA.SearchID(Convert.ToInt32(fields[1]));
                    aorder.OrdBbook = BookDA.SearchISBN(Convert.ToInt64(fields[2]));
                    aorder.OrdDate = Convert.ToDateTime(fields[3]);
                    aorder.OrdBkQuantity = Convert.ToInt32(fields[4]);
                    aorder.OrdCost = Convert.ToDecimal(fields[5]);
                    aorder.OrdType = fields[6];
                    sReader.Close();
                    return aorder;
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            return null;
        }

        //Search by book

        public static Order SearchByBook(long searchedid)
        {
            Order aorder = new Order();
            StreamReader sReader = new StreamReader(ordFilePath);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');
                if (searchedid == Convert.ToInt64(fields[2]))
                {
                    aorder.OrdId = Convert.ToInt32(fields[0]);
                    aorder.OrdClient = ClientDA.SearchID(Convert.ToInt32(fields[1]));
                    aorder.OrdBbook = BookDA.SearchISBN(Convert.ToInt64(fields[2]));
                    aorder.OrdDate = Convert.ToDateTime(fields[3]);
                    aorder.OrdBkQuantity = Convert.ToInt32(fields[4]);
                    aorder.OrdCost = Convert.ToDecimal(fields[5]);
                    aorder.OrdType = fields[6];
                    sReader.Close();
                    return aorder;
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            return null;
        }
        //List Method 
        public static void ListOrder(ListView listview)
        {
            listview.Items.Clear();

            StreamReader sReader = new StreamReader(ordFilePath);
            string line = sReader.ReadLine();


            while (line != null)
            {

                string[] fields = line.Split(',');
                ListViewItem order = new ListViewItem(fields[0]);
                order.SubItems.Add(fields[1]);
                order.SubItems.Add(fields[2]);
                order.SubItems.Add(BookDA.SearchISBN(Convert.ToInt64(fields[2])).BkTitle.ToString());
                order.SubItems.Add(BookDA.SearchISBN(Convert.ToInt64(fields[2])).BkUnitPrice.ToString());
                order.SubItems.Add(fields[4]);
                order.SubItems.Add(fields[6]);
                order.SubItems.Add(fields[3]);       
                order.SubItems.Add(fields[5]);
                listview.Items.Add(order);
                line = sReader.ReadLine();
            }
            sReader.Close();
        }

        //List single order
        public static void ListSearch(Order aorder, ListView listview)
        {
            listview.Items.Clear();
            ListViewItem list = new ListViewItem(aorder.OrdId.ToString());
            list.SubItems.Add(aorder.OrdClient.ToString());
            list.SubItems.Add(aorder.OrdBbook.ToString());
            list.SubItems.Add(aorder.OrdBbook.BkTitle);
            list.SubItems.Add(aorder.OrdBbook.BkUnitPrice.ToString());         
            list.SubItems.Add(aorder.OrdDate.ToString());
            list.SubItems.Add(aorder.OrdBkQuantity.ToString());
            list.SubItems.Add(aorder.OrdCost.ToString());
            list.SubItems.Add(aorder.OrdType);          
            listview.Items.Add(list);                        
        }

        //Delete Method   
        public static void Delete(int ordId)
        {
            StreamReader sReader = new StreamReader(ordFilePath);
            string line = sReader.ReadLine();
            StreamWriter sWriter = new StreamWriter(ordTempFilePath, true);

            while (line != null)
            {
                string[] fields = line.Split(',');
                if ((ordId) != (Convert.ToInt32(fields[0])))
                {
                    sWriter.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3] + "," + fields[4] + "," + fields[5] + "," + fields[6]);
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            sWriter.Close();
            File.Delete(ordFilePath);
            File.Move(ordTempFilePath, ordFilePath);
            MessageBox.Show("Order Sucessfully deleted!");

        }

        //Update Method     
        public static void Update(Order aorder)
        {
            DialogResult ans = MessageBox.Show("Are you sure you want to update the order information?", "Update Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (ans == DialogResult.Yes)
            {
                StreamReader sReader = new StreamReader(ordFilePath);
                string line = sReader.ReadLine();
                StreamWriter sWriter = new StreamWriter(ordTempFilePath, true);

                while (line != null)
                {
                    string[] fields = line.Split(',');
                    if ((aorder.OrdId) != (Convert.ToInt32(fields[0])))
                    {
                       sWriter.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3] + "," + fields[4] + "," + fields[5] + "," + fields[6]);
                    }
                    line = sReader.ReadLine();
                }
                sWriter.WriteLine(aorder.OrdId + "," + aorder.OrdClient.ClientId + "," + aorder.OrdBbook.BkISBN + "," + aorder.OrdDate + "," + aorder.OrdBkQuantity + ", " + aorder.OrdCost + "," + aorder.OrdType);
                sReader.Close();
                sWriter.Close();
                File.Delete(ordFilePath);
                File.Move(ordTempFilePath, ordFilePath);
                MessageBox.Show("Order Sucessfully deleted!");
            }

        }


    }
}

      