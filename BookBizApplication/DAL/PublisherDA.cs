using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBizApplication.BLL;
using System.Windows.Forms;
using System.IO;


namespace BookBizApplication.DAL
{
    class PublisherDA
    {
        //Store data in this file
        private static string pubFilePath = Application.StartupPath + @"\Data\Publishers.dat";

        //Save Method
        public static void Save(Publisher aPublisher)
        {
            StreamWriter sWriter = new StreamWriter(pubFilePath, true);
            sWriter.WriteLine(aPublisher.PubName);
            sWriter.Close();
        }                   

    }
}
