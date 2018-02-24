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
    class ClientDA
    {

        //Define file to save the data 

        private static string clFilePath = Application.StartupPath + @"\Data\Clients.dat";
        private static string clTempFilePath = Application.StartupPath + @"\Data\ClientsTemp.dat";

        //Save Method
        public static void Save(Client aclient)
        {
            Validator checker = new Validator();
            if (checker.isUniqueID(clFilePath, aclient.ClientId.ToString()))
            {
                StreamWriter sWriter = new StreamWriter(clFilePath, true);
                sWriter.WriteLine(aclient.ClientId + "," + aclient.ClientName + "," + aclient.ClientPhone + "," + aclient.ClientFax + "," + aclient.ClientEmail + "," + aclient.ClientCity + "," + aclient.ClientStreet + "," + aclient.ClientPostCode + "," + aclient.ClientCredit);
                sWriter.Close();
                MessageBox.Show("Client saved successfully!");
            } 
        }

        //Search by ID       
        public static Client SearchID(int searchedId)
        {
            Client aclient = new Client();
            StreamReader sReader = new StreamReader(clFilePath);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');
                if (searchedId == Convert.ToInt32(fields[0]))
                {
                    aclient.ClientId = Convert.ToInt32(fields[0]);
                    aclient.ClientName = fields[1];
                    aclient.ClientPhone = fields[2];
                    aclient.ClientFax = fields[3];
                    aclient.ClientEmail = fields[4];
                    aclient.ClientCity = fields[5];
                    aclient.ClientStreet = fields[6];
                    aclient.ClientPostCode = fields[7];
                    aclient.ClientCredit = Convert.ToDecimal(fields[8]);
                    sReader.Close();
                    return aclient;
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            return null;    
        }

        //Search by name        
        public static Client SearchName(string searchedname)
        {
            Client aclient = new Client();
            StreamReader sReader = new StreamReader(clFilePath);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');
                if (string.Equals(searchedname, fields[1], StringComparison.OrdinalIgnoreCase))
                {
                    aclient.ClientId = Convert.ToInt32(fields[0]);
                    aclient.ClientName = fields[1];
                    aclient.ClientPhone = fields[2];
                    aclient.ClientFax = fields[3];
                    aclient.ClientEmail = fields[4];
                    aclient.ClientCity = fields[5];
                    aclient.ClientStreet = fields[6];
                    aclient.ClientPostCode = fields[7];
                    aclient.ClientCredit = Convert.ToDecimal(fields[8]);
                    sReader.Close();
                    return aclient;                    
                }
                line = sReader.ReadLine();       
            }
            sReader.Close();
            return null;                  
        }

        //List Method           
        public static void List(ListView listview)
        {
            listview.Items.Clear();
            StreamReader sReader = new StreamReader(clFilePath);
            string line = sReader.ReadLine();

            while (line != null)
            {

                string[] fields = line.Split(',');
                ListViewItem client = new ListViewItem(fields[0]);
                client.SubItems.Add(fields[1]);
                client.SubItems.Add(fields[2]);
                client.SubItems.Add(fields[3]);
                client.SubItems.Add(fields[6]);
                client.SubItems.Add(fields[7]);
                client.SubItems.Add(fields[5]);
                client.SubItems.Add(fields[4]);
                client.SubItems.Add(fields[8]);
                listview.Items.Add(client);
                line = sReader.ReadLine();                        
            }
            sReader.Close();
        }
        
        //List search result or single Client
        public static void ListSearch(Client client, ListView listview)
        {
            listview.Items.Clear();
            ListViewItem list = new ListViewItem(client.ClientId.ToString());
            list.SubItems.Add(client.ClientName);
            list.SubItems.Add(client.ClientPhone);
            list.SubItems.Add(client.ClientFax);
            list.SubItems.Add(client.ClientStreet);
            list.SubItems.Add(client.ClientPostCode);
            list.SubItems.Add(client.ClientCity);
            list.SubItems.Add(client.ClientEmail);
            list.SubItems.Add(client.ClientCredit.ToString());
            listview.Items.Add(list);
           
        }

        // Delete Method       
        public static void Delete(int clientID)
        {
            StreamReader sReader = new StreamReader(clFilePath);
            string line = sReader.ReadLine();
            StreamWriter sWriter = new StreamWriter(clTempFilePath, true);
            while (line != null)
            {
                string[] fields = line.Split(',');
                if (clientID != Convert.ToInt32(fields[0]))
                {
                    sWriter.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3] + "," + fields[4] + "," + fields[5] + "," + fields[6] + "," + fields[7] + "," + fields[8]);
                    sWriter.Close();                        
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            sWriter.Close();
            File.Delete(clFilePath);
            File.Move(clTempFilePath, clFilePath);
            MessageBox.Show("Client Successfully deleted!");        
        }                                                       

        //Update Method            
        public static void Update(Client aclient)
        {
           
                StreamReader sReader = new StreamReader(clFilePath);
                string line = sReader.ReadLine();
                StreamWriter sWriter = new StreamWriter(clTempFilePath, true);
                while (line != null)
                {
                    string[] fields = line.Split(',');
                    if (aclient.ClientId != Convert.ToInt32(fields[0]))
                    {
                        sWriter.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3] + "," + fields[4] + "," + fields[5] + "," + fields[6] + "," + fields[7] + "," + fields[8]);
                    }
                    line = sReader.ReadLine();
                }
                sWriter.WriteLine(aclient.ClientId + "," + aclient.ClientName + "," + aclient.ClientPhone + "," + aclient.ClientFax + "," + aclient.ClientEmail + "," + aclient.ClientCity + "," + aclient.ClientStreet + "," + aclient.ClientPostCode + "," + aclient.ClientCredit);
                sWriter.Close();
                sReader.Close();
                File.Delete(clFilePath);
                File.Move(clTempFilePath, clFilePath);
                MessageBox.Show("Client Successfully updated!");

          

        }


    }
}
