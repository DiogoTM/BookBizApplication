using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BookBizApplication.BLL;
using System.IO;


namespace BookBizApplication.Validation
{
    class Validator
    {
        public  bool isValidID(TextBox text, int size)
        {
            int tempID;
            if ((text.TextLength != size) || !(int.TryParse(text.Text, out tempID)))
            {
                MessageBox.Show("Invalid Id, please enter a " + size + " digit number.", "INVALID ID");
                text.Clear();
                text.Focus();
                return false;                 
            }
            return true;
        }
        public bool isValidLongID(TextBox text, int size)
        {
            long tempID;
            if ((text.TextLength != size) || !(long.TryParse(text.Text, out tempID)))
            {
                MessageBox.Show("Invalid Id, please enter a " + size + " digit number.", "INVALID ID");
                text.Clear();
                text.Focus();
                return false;
            }
            return true;
        }
        public  bool isValidName(TextBox text, string what)
        {
            
            for (int i = 0; i < text.TextLength; i++)
            {
                if (char.IsDigit(text.Text, i) || (char.IsWhiteSpace(text.Text, i)))
                {
                    MessageBox.Show("Invalid" + what + " Name,Please enter another name.", "INVALID NAME");
                    text.Clear();
                    text.Focus();
                    return false;
                }
               
            }
            if (string.IsNullOrEmpty(text.Text) || string.IsNullOrWhiteSpace(text.Text))
            {
                MessageBox.Show("Invalid" + what + " Name,Please enter another name.", "INVALID NAME");
                text.Clear();
                text.Focus();
                return false;
            }
            return true;
        }

        public  bool isUniqueID(string FilePath, string id)
        {
            StreamReader sReader = new StreamReader(FilePath);
            string line = sReader.ReadLine();
            while (line != null)
            {
                string[] fields = line.Split(',');
                if (Convert.ToInt32(id) == Convert.ToInt32(fields[0]))
                {
                    MessageBox.Show("Invalid ID, must be a unique number.", "INVALID ID");
                    sReader.Close();
                    return false;
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            return true;
        }

        public bool isUniqueLongID(string FilePath, string id)
        {
            StreamReader sReader = new StreamReader(FilePath);
            string line = sReader.ReadLine();
            while (line != null)
            {
                string[] fields = line.Split(',');
                if (Convert.ToInt64(id) == Convert.ToInt64(fields[0]))
                {
                    MessageBox.Show("Invalid ID, must be a unique number.", "INVALID ID");
                    sReader.Close();
                    return false;
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            return true;
        }

        public bool isNotNull(string item, string what)
        {
            if (string.IsNullOrEmpty(item) || string.IsNullOrWhiteSpace(item))
            {
                MessageBox.Show("You left the field " + what + " blank, please select/write a value.");
                return false;
            }
            return true;
            
        }
    }
}

