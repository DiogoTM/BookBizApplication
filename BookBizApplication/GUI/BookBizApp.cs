using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using BookBizApplication.BLL;
using BookBizApplication.DAL;
using BookBizApplication.Validation;


namespace BookBizApplication.GUI
{
    public partial class BookBizApp : Form
    {


        public BookBizApp()
        {
            InitializeComponent();

            //Disables the access to any other tab that the currently user is not supossed to use        
            tabPage1.Enabled = false;
            tabPage2.Enabled = false;
            tabPage3.Enabled = false;
            tabPage4.Enabled = false;

            //Automatic ComboBoxes
            OrderDA.clientSelection(comboBoxOrdClient);
            OrderDA.bookSelection(comboBoxOrdISBN);
            BookDA.authorSelection(comboBoxBkAuthor);
            BookDA.publisherSelection(comboBoxBkPub);                          
        }
        //Automatic ComboBoxes Method
        public void UpdateComboBoxes()
        {
            comboBoxOrdClient.Items.Clear();
            comboBoxOrdISBN.Items.Clear();
            comboBoxBkAuthor.Items.Clear();
            comboBoxBkPub.Items.Clear();
            OrderDA.clientSelection(comboBoxOrdClient);
            OrderDA.bookSelection(comboBoxOrdISBN);
            BookDA.authorSelection(comboBoxBkAuthor);
            BookDA.publisherSelection(comboBoxBkPub);
        }               
        

        //General use buttons and methods ====================================
        private void buttonEmpExit_Click(object sender, EventArgs e)
        {
            //Exit Message and confirmation
            if (MessageBox.Show("Are you sure you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        //Change User Button
        private void buttonChangeUser_Click(object sender, EventArgs e)
        {
            this.Hide();
            BookBizLogin login = new BookBizLogin();
            login.Show();
        }
        
        //Change Password Button
        private void buttonChgPswd_Click(object sender, EventArgs e)
        {
            ChangePswd changeApp = new ChangePswd();
            changeApp.Show();             
        }

        //Code to give the information of what tab will be available, it is used in the login form
        public void ChangeTab(int tabindex)
        {
            tabControl.SelectedIndex = tabindex;
            tabControl.SelectedTab.Enabled = true;
        }

        // Info Button
        private void buttonInfo_Click(object sender, EventArgs e)
        {
            Info info = new Info();
            info.Show();
        }

        //=====================================================================================================================================
        //=============================BEGINNING OF MIS MANAGER PROGRAM========================================================================
        //=====================================================================================================================================


        private Employee createEmployee()
        {
            //Create object employee
            Employee aEmp = new Employee();

            aEmp.EmpId = Convert.ToInt32(textBoxEmpId.Text);
            aEmp.EmpFName = textBoxEmpFName.Text;
            aEmp.EmpLName = textBoxEmpLName.Text;
            aEmp.EmpEmail = textBoxEmpEmail.Text;
            aEmp.EmpPhone = maskedTextBoxEmpPhone.Text;
            aEmp.EmpPosition = comboBoxEmpPos.Text;

            // Determine if the employee is a user or not based on his position
            if (comboBoxEmpPos.SelectedIndex > 3)
            {
                aEmp.UserLevel = 0;
                aEmp.UserPassword = "notauser";
            }

            //Determine the employee user level

            switch (comboBoxEmpPos.SelectedIndex)
            {
                case 0:
                    aEmp.UserLevel = 4;
                    aEmp.UserPassword = textBoxUserPswd.Text;
                    break;
                case 1:
                    aEmp.UserLevel = 3;
                    aEmp.UserPassword = textBoxUserPswd.Text;
                    break;
                case 2:
                    aEmp.UserLevel = 2;
                    aEmp.UserPassword = textBoxUserPswd.Text;
                    break;
                case 3:
                    aEmp.UserLevel = 1;
                    aEmp.UserPassword = textBoxUserPswd.Text;
                    break;
            }
            return aEmp;
        }

        //Add Employee
        private void buttonEmpAdd_Click(object sender, EventArgs e)
        {
            Validator checker = new Validator();                                                                                                                                                                                                                                                                     
            if (checker.isValidID(textBoxEmpId, 5) && checker.isValidName(textBoxEmpFName, "First")
                && checker.isValidName(textBoxEmpLName, "Last") && checker.isNotNull(comboBoxEmpPos.Text, "Position")
                && checker.isNotNull(textBoxEmpEmail.Text, "E-mail") && checker.isNotNull(maskedTextBoxEmpPhone.Text, "Phone"))
            {                                                                                            
                //Save the data to the file
                EmployeeDA.Save(createEmployee());
                UpdateComboBoxes();
            }       
        }

        //Enable the buttons delete and update in case of a valid entered ID
        private void textBoxEmpId_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxEmpId.Text))
            {
                buttonEmpDelete.Enabled = false;
                buttonEmpUpdate.Enabled = false;
                return;
            }
            if (EmployeeDA.SearchID(Convert.ToInt32(textBoxEmpId.Text)) != null)
            {
                buttonEmpDelete.Enabled = true;
                buttonEmpUpdate.Enabled = true;
                return;
            }
            buttonEmpDelete.Enabled = false;
            buttonEmpUpdate.Enabled = false;

        }
              
        //The password Textbox is enabled if the user selected a position that qualifies as a user
        private void comboBoxEmpPos_SelectedIndexChanged(object sender, EventArgs e)
        {
            //If the employee is a user the password box is enabled
            switch (comboBoxEmpPos.SelectedIndex)
            {
                case 0:
                    groupBoxPassword.Enabled = true;
                    break;
                case 1:
                    groupBoxPassword.Enabled = true;
                    break;
                case 2:
                    groupBoxPassword.Enabled = true;
                    break;
                case 3:
                    groupBoxPassword.Enabled = true;
                    break;

                default:
                    groupBoxPassword.Enabled = false;
                    break;
            }                                  
        }                                           

        //Search                     
        private void buttonEmpSearch_Click(object sender, EventArgs e)
        {
            Employee aEmployee = new Employee();
            //Check if the user entered the information to be searched
            if (!string.IsNullOrEmpty(textBoxEmpSearch.Text))
            {
                switch (comboBoxEmpSearch.SelectedIndex)
                {
                    case -1:
                        MessageBox.Show("Please select a valid search option!");
                        return;
                    case 0:
                        int tempID;
                        //Check to see if the user entered a valid only digits ID
                        if (!int.TryParse(textBoxEmpSearch.Text, out tempID))
                        {
                            MessageBox.Show("Please enter a valid term to be searched.", "No search information!");
                            return;
                        }
                        aEmployee = EmployeeDA.SearchID(Convert.ToInt32(textBoxEmpSearch.Text));
                        break;
                    case 1:
                        aEmployee = EmployeeDA.SearchFirst(textBoxEmpSearch.Text);
                        break;
                    case 2:
                        aEmployee = EmployeeDA.SearchLast(textBoxEmpSearch.Text);
                        break;
                }

                if (aEmployee == null)
                {
                    MessageBox.Show("Employee not found!", "Employee not found!");
                    textBoxEmpSearch.Clear();
                    textBoxEmpSearch.Focus();
                    return;
                }

                EmployeeDA.ListSearchResult(aEmployee, listViewEmp);
                return;
            }
            MessageBox.Show("Please enter a valid term to be searched.", "No search information!");
            return;
             
                                                                
        }

        //List
        private void buttonEmpList_Click(object sender, EventArgs e)
        {
            EmployeeDA.ListEmployees(listViewEmp);
        }

        //Delete
        private void buttonEmpDelete_Click(object sender, EventArgs e)
        {                     
                DialogResult ans = MessageBox.Show("Are you sure you want to delete the employee information?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (ans == DialogResult.Yes) 
                {
                    EmployeeDA.Delete(Convert.ToInt32(textBoxEmpId.Text));
                    EmployeeDA.ListEmployees(listViewEmp);
                    UpdateComboBoxes();
                }                                    

        }

        // Update
        private void buttonEmpUpdate_Click(object sender, EventArgs e)
        {
            EmployeeDA.Update(createEmployee());
            EmployeeDA.ListEmployees(listViewEmp);
            UpdateComboBoxes();


        }

        //Method created to clear all the information in the textboxes
        private void empClearAll()
        {
            textBoxEmpId.Clear();
            textBoxEmpFName.Clear();
            textBoxEmpLName.Clear();
            textBoxEmpEmail.Clear();
            maskedTextBoxEmpPhone.Clear();
            return;  
        }

        //If the user clicks the item in the list view the fields of the form are automatically filled
        private void listViewEmp_MouseClick(object sender, MouseEventArgs e)
        {
            empClearAll();

            ListViewItem item = listViewEmp.SelectedItems[0];
            textBoxEmpId.Text = item.SubItems[0].Text;
            textBoxEmpFName.Text = item.SubItems[1].Text;
            textBoxEmpLName.Text = item.SubItems[2].Text;
            textBoxEmpEmail.Text = item.SubItems[5].Text;
            maskedTextBoxEmpPhone.Text = item.SubItems[4].Text;
            comboBoxEmpPos.Text = item.SubItems[3].Text;
            return;

        }             

        //=====================================================================================================================================
        //=============================END OF MIS MANAGER PROGRAM==============================================================================
        //=====================================================================================================================================


        //=====================================================================================================================================
        //=============================BEGINNING OF SALES MANAGER PROGRAM======================================================================
        //=====================================================================================================================================

        //Creates the object client
        private Client createClient()
        {
            Client aclient = new Client();

            aclient.ClientId = Convert.ToInt32(textBoxClID.Text);
            aclient.ClientName = textBoxClName.Text;
            aclient.ClientPhone = maskedTextBoxClPhone.Text;
            aclient.ClientFax = maskedTextBoxClFax.Text;
            aclient.ClientEmail = textBoxClEmail.Text;
            aclient.ClientCity = textBoxClCity.Text;
            aclient.ClientStreet = textBoxClStrt.Text;
            aclient.ClientPostCode = maskedTextBoxClPostCode.Text;             
            aclient.ClientCredit = Convert.ToDecimal(textBoxClCredit.Text);

            return aclient;
        }

        //Enable the buttons delete and update in case of a valid entered ID

        private void textBoxClID_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxClID.Text))
            {
                buttonClDel.Enabled = false;
                buttonClUpdate.Enabled = false;
                return;
            }

            if (ClientDA.SearchID(Convert.ToInt32(textBoxClID.Text)) != null)
            {
                buttonClDel.Enabled = true;
                buttonClUpdate.Enabled = true;
                return;
            }
            buttonClDel.Enabled = false;
            buttonClUpdate.Enabled = false;

        }
   
        //Add
        private void buttonClAdd_Click(object sender, EventArgs e)
        {
            Validator checker = new Validator();

            if (checker.isValidID(textBoxClID, 5) && checker.isNotNull(textBoxClName.Text, "Client Name") && checker.isNotNull(textBoxClEmail.Text, "E-mail") && checker.isNotNull(textBoxClStrt.Text, "Street") && checker.isNotNull(textBoxClCity.Text, "City") && checker.isNotNull(maskedTextBoxClPhone.Text, "Phone") && checker.isNotNull(maskedTextBoxClFax.Text, "Fax") && checker.isNotNull(maskedTextBoxClPostCode.Text, "Post Code"))
            {
                ClientDA.Save(createClient());
                UpdateComboBoxes();
            }           

        }

        //Delete
        private void buttonClDel_Click(object sender, EventArgs e)
        {

            DialogResult ans = MessageBox.Show("Are you sure you want to delete the Client information?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (ans == DialogResult.Yes)
            {
                if (OrderDA.SearchByClientID(Convert.ToInt32(textBoxClID.Text)) != null)
                {
                    MessageBox.Show("This client has open orders in the system, please verify the orders before deleting this client.", "ALERT!");
                    return;
                }
                ClientDA.Delete(Convert.ToInt32(textBoxClID.Text));
                ClientDA.List(listViewSales);
                UpdateComboBoxes();
            }

        }

        //Update
        private void buttonClUpdate_Click(object sender, EventArgs e)
        {
            DialogResult ans = MessageBox.Show("Are you sure you want to update the Client information?", "Update Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (ans == DialogResult.Yes)
            {
                ClientDA.Update(createClient());
            }
            ClientDA.List(listViewSales);
            UpdateComboBoxes();

        }

        //Search
        private void buttonClSearch_Click(object sender, EventArgs e)
        {
            Client aclient = new Client();
            if (!string.IsNullOrEmpty(textBoxClSearch.Text))
            {
                switch (comboBoxClSearch.SelectedIndex)
                {
                    case -1:
                        MessageBox.Show("Please select a valid search option!");
                        return;
                    case 0:
                        int tempId;
                        if (!int.TryParse(textBoxClSearch.Text,out tempId))
                        {
                            MessageBox.Show("Please enter a valid term to be searched.", "No search information!");
                            return;
                        }
                        aclient = ClientDA.SearchID(Convert.ToInt32(textBoxClSearch.Text));
                        break;
                    case 1:
                        aclient = ClientDA.SearchName(textBoxClSearch.Text);
                        break;
                }

                if (aclient == null)
                {
                    MessageBox.Show("Client not found!", "Not found!");
                    textBoxClSearch.Clear();
                    textBoxClSearch.Focus();
                    return;
                }
                
                ClientDA.ListSearch(aclient, listViewSales);
                return;
            }
            MessageBox.Show("Please enter a valid term to be searched.", "No search information!");
            return;
        }

        //List
        private void buttonClList_Click(object sender, EventArgs e)
        {
            ClientDA.List(listViewSales);
        }
        
        //Method to clear all the fields
        private void SalesClearAll()
        {
            textBoxClID.Clear();
            textBoxClName.Clear();
            maskedTextBoxClPhone.Clear();
            maskedTextBoxClFax.Clear();
            textBoxClEmail.Clear();
            textBoxClCity.Clear();
            textBoxClStrt.Clear();
            maskedTextBoxClPostCode.Clear();
            textBoxClCredit.Clear();
            return;
        }

        //If the user clicks the item in the list view the fields of the form are automatically filled
        private void listViewSales_MouseClick(object sender, MouseEventArgs e)
        {
            SalesClearAll();

            ListViewItem item = listViewSales.SelectedItems[0];
            textBoxClID.Text = item.SubItems[0].Text;
            textBoxClName.Text = item.SubItems[1].Text;
            maskedTextBoxClPhone.Text = item.SubItems[2].Text;
            maskedTextBoxClFax.Text = item.SubItems[3].Text;
            textBoxClEmail.Text = item.SubItems[7].Text;
            textBoxClCity.Text = item.SubItems[6].Text;
            textBoxClStrt.Text = item.SubItems[4].Text;
            maskedTextBoxClPostCode.Text = item.SubItems[5].Text;
            textBoxClCredit.Text = item.SubItems[8].Text;
            return;
        }



        //=====================================================================================================================================
        //=============================END OF SALES MANAGER PROGRAM============================================================================
        //=====================================================================================================================================


        //=====================================================================================================================================
        //=============================BEGINNING OF INVENTORY MANAGER PROGRAM==================================================================
        //=====================================================================================================================================


        //Author Management System=========================================================

        //Create object Author
        private Author createAuthor()
        {
            Author aAuthor = new Author();

            aAuthor.AutID = Convert.ToInt32(textBoxAutID.Text);
            aAuthor.AutFirstN = textBoxautFName.Text;
            aAuthor.AutLastN = textBoxAutLName.Text;
            aAuthor.AutEmail = textBoxAutEmail.Text;

            return aAuthor;                                  
        }

        //Enable the buttons delete and update in case of a valid entered ID

        private void textBoxAutID_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxAutID.Text))
            {
                buttonAutDel.Enabled = false;
                buttonAutUpd.Enabled = false;
                return;
            }
            if (AuthorDA.SearchID(Convert.ToInt32(textBoxAutID.Text)) != null)
            {
                buttonAutDel.Enabled = true;
                buttonAutUpd.Enabled = true;
                return;
            }
            buttonAutDel.Enabled = false;
            buttonAutUpd.Enabled = false;
        }

        //Add        
        private void buttonAutAdd_Click(object sender, EventArgs e)
        {
            Validator checker = new Validator();
            if (checker.isValidID(textBoxAutID, 5) && checker.isValidName(textBoxautFName, "First") && checker.isValidName(textBoxAutLName, "Last"))
            {
                AuthorDA.Save(createAuthor());
                UpdateComboBoxes();
            }
            

        }

        //Delete
        private void buttonAutDel_Click(object sender, EventArgs e)
        {
            DialogResult ans = MessageBox.Show("Are you sure you want to delete the Author information?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (ans == DialogResult.Yes)
            {
                if (BookDA.SearchAuthor(Convert.ToInt32(textBoxAutID.Text)) != null)
                {
                    MessageBox.Show("This author has one or more books registered in the inventory, please be sure that there are no books by this author before deleting it!", "ALERT!");
                    return;
                }
                AuthorDA.Delete(Convert.ToInt32(textBoxAutID.Text));
                AuthorDA.ListAuthors(listViewAuthor);
                UpdateComboBoxes();
            }

        }

        //Update
        private void buttonAutUpd_Click(object sender, EventArgs e)
        {
            AuthorDA.Update(createAuthor());
            AuthorDA.ListAuthors(listViewAuthor);

            UpdateComboBoxes();

        }
        
        //List
        private void buttonAutList_Click(object sender, EventArgs e)
        {
            AuthorDA.ListAuthors(listViewAuthor);      
        }
        
        //Search
        private void buttonAutSearch_Click(object sender, EventArgs e)
        {
            Author aAuthor = new Author();

            if (!string.IsNullOrEmpty(textBoxAutSearch.Text))
            {
                switch (comboBoxAutSearch.SelectedIndex)
                {
                    case -1:
                        MessageBox.Show("Please select a valid search option!");
                        return;
                    case 0:
                        int tempId;
                        if (!int.TryParse(textBoxAutSearch.Text, out tempId))
                        {
                            MessageBox.Show("Please enter a valid term to be searched.", "No search information!");
                            return;
                        }
                        aAuthor = AuthorDA.SearchID(Convert.ToInt32(textBoxAutSearch.Text));
                        break;
                    case 1:
                        aAuthor = AuthorDA.SearchFirst(textBoxAutSearch.Text);
                        break;
                    case 2:
                        aAuthor = AuthorDA.SearchLast(textBoxAutSearch.Text);
                        break;
                }

                if (aAuthor == null)
                {
                    MessageBox.Show("Author not found!", "Not found!");
                    textBoxAutSearch.Clear();
                    textBoxAutSearch.Focus();
                    return;
                }

                AuthorDA.ListSearchResult(aAuthor, listViewAuthor);
                return;
            }
            MessageBox.Show("Please enter a valid term to be searched.", "No search information!");
            return;

        }

        //Clear all fields
        private void authorClearAll()
        {
            textBoxAutID.Clear();
            textBoxautFName.Clear();
            textBoxAutLName.Clear();
            textBoxAutEmail.Clear();
        }

        //If the user clicks the item in the list view the fields of the form are automatically filled
        private void listViewAuthor_MouseClick(object sender, MouseEventArgs e)
        {
            authorClearAll();
            ListViewItem item = listViewAuthor.SelectedItems[0];
            textBoxAutID.Text = item.SubItems[0].Text;
            textBoxautFName.Text = item.SubItems[1].Text;
            textBoxAutLName.Text = item.SubItems[2].Text;
            textBoxAutEmail.Text = item.SubItems[3].Text;
        }

        //=====================================================================================================================================
        //Book Management System===============================================================================================================
        //=====================================================================================================================================

        //Add a new Publisher
        private void buttonAddPub_Click(object sender, EventArgs e)
        {
            Publisher apublisher = new Publisher();
            apublisher.PubName = textBoxBkPublisher.Text;
            PublisherDA.Save(apublisher);
            UpdateComboBoxes();
            return;
        }

        //Enable the buttons delete and update in case of a valid entered ID              
        private void textBoxBkISBN_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxBkISBN.Text))
            {
                buttonBkDel.Enabled = false;
                buttonBkUpdate.Enabled = false;
                return;
            }
            if (BookDA.SearchISBN(Convert.ToInt64(textBoxBkISBN.Text)) != null)
            {
                buttonBkDel.Enabled = true;
                buttonBkUpdate.Enabled = true;
                return;
            }
            buttonBkDel.Enabled = false;
            buttonBkUpdate.Enabled = false;
        }
            
        //Search
        private void buttonBkSearch_Click(object sender, EventArgs e)
        {
            Book abook = new Book();

            if (!string.IsNullOrEmpty(textBoxBkSearch.Text))
            {
                switch (comboBoxBkSearch.SelectedIndex)
                {
                    case -1:
                        MessageBox.Show("Please select a valid search option!");
                        return;
                    case 0:
                        long tempId;
                        if (!long.TryParse(textBoxBkSearch.Text, out tempId))
                        {
                            MessageBox.Show("Please enter a valid term to be searched.", "No search information!");
                            return;
                        }
                        abook = BookDA.SearchISBN(Convert.ToInt64(textBoxBkSearch.Text));
                        break;
                    case 1:
                        abook = BookDA.SearchTitle(textBoxBkSearch.Text);
                        break;
                }
                if (abook == null)
                {
                    MessageBox.Show("Book not found!", "Not found!");
                    textBoxBkSearch.Clear();
                    textBoxBkSearch.Focus();
                    return;
                }
                BookDA.ListSearchResult(abook, listViewBooks);
                return;
            }                    
            
            MessageBox.Show("Please enter a valid term to be searched.", "No search information!");
            return;
        }

        //Create object Book
        private Book createBook()
        {
            Book abook = new Book();

            abook.BkISBN = Convert.ToInt64(textBoxBkISBN.Text);
            abook.BkTitle = textBoxBktitle.Text;
            abook.BkQOH = Convert.ToInt32(textBoxBkQOH.Text);
            abook.BkDate = dateTimePickerBook.Value;
            abook.BkAuthor = AuthorDA.SearchID(Convert.ToInt32(textBoxBkAuthorID.Text));
            abook.BkCategory = textBoxBkCategory.Text;
            abook.BkUnitPrice = Convert.ToDecimal(textBoxBkUP.Text);
            abook.BkPublisher = comboBoxBkPub.Text;

            return abook;
          
        }
        
        //Change the author ISBN after choosing an Author from the Combobox
        private void comboBoxBkAuthor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string line = comboBoxBkAuthor.Text;
            string[] lName = line.Split(',');
            textBoxBkAuthorID.Text = AuthorDA.SearchLast(lName[0]).AutID.ToString();
        }
        
        //Add
        private void buttonBkAdd_Click(object sender, EventArgs e)
        {
            Validator checker = new Validator();

            if (checker.isValidLongID(textBoxBkISBN,13) && checker.isNotNull(textBoxBktitle.Text, "Title") && checker.isNotNull(textBoxBkAuthorID.Text, "Category") && checker.isNotNull(comboBoxBkPub.Text, "Publisher") && checker.isNotNull(textBoxBkQOH.Text, "QOH") && checker.isNotNull(textBoxBkUP.Text, "Unit Price") && checker.isNotNull(comboBoxBkAuthor.Text, "Author"))
            {
                BookDA.Save(createBook());
                UpdateComboBoxes();
            }                                
        }

        //Delete
        private void buttonBkDel_Click(object sender, EventArgs e)
        {
            DialogResult ans = MessageBox.Show("Are you sure you want to delete the Book information?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (ans == DialogResult.Yes)    
            {
                if (OrderDA.SearchByBook(Convert.ToInt64(textBoxBkISBN.Text)) != null)
                {
                    MessageBox.Show("This book has open orders in the system, please verify the orders before deleting this book.", "ALERT!");
                    return;
                }
                BookDA.Delete(createBook().BkISBN);
                BookDA.ListBooks(listViewBooks);
                UpdateComboBoxes();
            }        
        }

        //Update
        private void buttonBkUpdate_Click(object sender, EventArgs e)
        {
            DialogResult ans = MessageBox.Show("Are you sure you want to update the Book information?", "Update Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (ans == DialogResult.Yes)
            {
                BookDA.Update(createBook());
            }
            BookDA.ListBooks(listViewBooks);       
            UpdateComboBoxes(); 
        }

        //List
        private void buttonBkList_Click(object sender, EventArgs e)
        {
            BookDA.ListBooks(listViewBooks);             
        }

        //Clear all the fields
        private void bookClearAll()
        {
            textBoxBkISBN.Clear();
            textBoxBktitle.Clear();
            textBoxBkQOH.Clear();
            textBoxBkCategory.Clear();
            textBoxBkUP.Clear();
        }

        //If the user clicks the item in the list view the fields of the form are automatically filled
        private void listViewBooks_MouseClick(object sender, MouseEventArgs e)
        {
            bookClearAll();
            ListViewItem item = listViewBooks.SelectedItems[0];
            textBoxBkISBN.Text = item.SubItems[0].Text;
            textBoxBktitle.Text = item.SubItems[1].Text;
            textBoxBkQOH.Text = item.SubItems[2].Text;
            dateTimePickerBook.Text = item.SubItems[3].Text;
            comboBoxBkAuthor.Text = item.SubItems[4].Text;
            textBoxBkCategory.Text = item.SubItems[5].Text;
            comboBoxBkPub.Text = item.SubItems[6].Text;
            textBoxBkUP.Text = item.SubItems[7].Text;             

        }



        //=====================================================================================================================================
        //=============================END OF INVENTORY MANAGER PROGRAM========================================================================
        //=====================================================================================================================================


        //=====================================================================================================================================
        //=============================BEGINNING OF ORDER CLERKS PROGRAM=======================================================================
        //=====================================================================================================================================

        // Create object Order
        private Order createOrder()
        {
            Order aorder = new Order();
            Book abook = new Book();
            Client aclient = new Client();

            abook = BookDA.SearchISBN(Convert.ToInt64(comboBoxOrdISBN.Text));
            aclient = ClientDA.SearchName(comboBoxOrdClient.Text);

            aorder.OrdId = OrderDA.OrderID() + 1;
            aorder.OrdClient = aclient;
            aorder.OrdBbook = abook;
            aorder.OrdBkQuantity = Convert.ToInt32(textBoxOrdBkQt.Text);
            aorder.OrdCost = aorder.OrdBkQuantity * BookDA.SearchISBN(Convert.ToInt64(comboBoxOrdISBN.Text)).BkUnitPrice;
            aorder.OrdDate = Convert.ToDateTime(dateTimePickerOrder.Value);
            aorder.OrdType = comboBoxOrdMadeBy.Text;
                            
            textBoxOrId.Text = aorder.OrdId.ToString();
            return aorder;

        }
        
        //Add        
        private void buttonOrdAdd_Click(object sender, EventArgs e)
        {
            Validator checker = new Validator();
            if (checker.isNotNull(comboBoxOrdClient.Text, "Client") && checker.isNotNull(comboBoxOrdISBN.Text, "Book ISBN") && checker.isNotNull(textBoxOrdBkQt.Text, "Quantity") && checker.isNotNull(comboBoxOrdMadeBy.Text, "Order made by?"))
            {
                Order aorder = createOrder();
                Book abook = BookDA.SearchISBN(Convert.ToInt64(comboBoxOrdISBN.Text));
                Client aclient = ClientDA.SearchName(comboBoxOrdClient.Text);
                abook.BkQOH = abook.BkQOH - aorder.OrdBkQuantity;
                BookDA.Update(abook);
                aclient.ClientCredit = aclient.ClientCredit - aorder.OrdCost;
                ClientDA.Update(aclient);
                OrderDA.SaveOrder(aorder);
                UpdateComboBoxes();
            }       
        }

        //Delete
        private void buttonOrdDel_Click(object sender, EventArgs e)
        {
            DialogResult ans = MessageBox.Show("Are you sure you want to delete this Order?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (ans == DialogResult.Yes)
            {
                OrderDA.Delete(Convert.ToInt32(textBoxOrId.Text));
                int bkQuantity = OrderDA.SearchID(Convert.ToInt32(textBoxOrId.Text)).OrdBkQuantity;
                decimal ordCost = OrderDA.SearchID(Convert.ToInt32(textBoxOrId.Text)).OrdCost;

                Book abook = new Book();
                Client aclient = new Client();

                abook = BookDA.SearchISBN(Convert.ToInt64(comboBoxOrdISBN.Text));
                aclient = ClientDA.SearchName(comboBoxOrdClient.Text);

                abook.BkQOH = abook.BkQOH + bkQuantity;
                aclient.ClientCredit = aclient.ClientCredit + ordCost;

                OrderDA.ListOrder(listViewOrder);
                UpdateComboBoxes();
            }

        }

        //Search book by title
        private void buttonOrdSearchBk_Click(object sender, EventArgs e)
        {
            Book abook = BookDA.SearchTitle(textBoxOrdBkTitle.Text);
            if (abook != null)
            {
                MessageBox.Show("Book found, ISBN:" + abook.BkISBN.ToString(), "Book search result");
            }                         

        }

        //Selecting a book by ISBN changes the book title automatically
        private void comboBoxOrdISBN_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selection = comboBoxOrdISBN.SelectedItem;
            textBoxOrdBkTitle.Text = BookDA.SearchISBN(Convert.ToInt64(selection.ToString())).BkTitle;
            textBoxOrdBkUP.Text = BookDA.SearchISBN(Convert.ToInt64(selection.ToString())).BkUnitPrice.ToString();    
        }                             
        
        //If the user clicks the item in the list view the fields of the form are automatically filled        
                                                              
        private void listViewOrder_MouseClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = listViewOrder.SelectedItems[0];
            textBoxOrId.Text = item.SubItems[0].Text;
            comboBoxOrdClient.Text = ClientDA.SearchID(Convert.ToInt32(item.SubItems[1].Text)).ClientName;
            comboBoxOrdISBN.Text = item.SubItems[2].Text;
            textBoxOrdBkQt.Text = item.SubItems[5].Text;
            comboBoxOrdMadeBy.Text = item.SubItems[6].Text;
            dateTimePickerOrder.Text = item.SubItems[7].Text;
            textBoxOrdTPrice.Text = item.SubItems[8].Text;        

        }

        //List
        private void buttonOrdList_Click(object sender, EventArgs e)
        {
            OrderDA.ListOrder(listViewOrder);
        }

        //Update                     
        private void buttonOrdUpd_Click(object sender, EventArgs e)
        {
            Order aorder = createOrder();
            Order oldorder = OrderDA.SearchID(Convert.ToInt32(textBoxOrId.Text));

            Book abook = BookDA.SearchISBN(Convert.ToInt64(comboBoxOrdISBN.Text));
            Client aclient = ClientDA.SearchName(comboBoxOrdClient.Text);
          
            abook.BkQOH = (abook.BkQOH + oldorder.OrdBkQuantity) - aorder.OrdBkQuantity;
            BookDA.Update(abook);
           
            aclient.ClientCredit = (aclient.ClientCredit+aorder.OrdCost) - aorder.OrdCost;
            ClientDA.Update(aclient);        
                                               
            OrderDA.Update(aorder);
            OrderDA.ListOrder(listViewOrder);
            UpdateComboBoxes();

        }

        //Displays the selected client Credit
        private void comboBoxOrdClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxOrdClCredit.Text = ClientDA.SearchName(comboBoxOrdClient.Text).ClientCredit.ToString();
        }
        
        //Displays the total cost based on the quantity entered
        private void textBoxOrdBkQt_TextChanged(object sender, EventArgs e)
        {
            textBoxOrdTPrice.Text = (BookDA.SearchTitle(textBoxOrdBkTitle.Text).BkUnitPrice * Convert.ToInt32(textBoxOrdBkQt.Text)).ToString();
        }                 

        //Enable the buttons delete and update in case of a valid entered ID  
        private void textBoxOrId_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxOrId.Text))
            {
                buttonOrdDel.Enabled = false;
                buttonOrdUpd.Enabled = false;
                return;
            }
            if (OrderDA.SearchID(Convert.ToInt32(textBoxOrId.Text)) != null)
            {
                buttonOrdDel.Enabled = true;
                buttonOrdUpd.Enabled = true;
                return;
            }
            buttonOrdDel.Enabled = false;
            buttonOrdUpd.Enabled = false;
        }

        //Search button
        private void buttonOrdSearch_Click(object sender, EventArgs e)
        {
            Order aorder = new Order();

            if (!string.IsNullOrEmpty(textBoxOrdSearch.Text))
            {
                switch (comboBoxOrdSearch.SelectedIndex)
                {
                    case 0:
                        int tempId;
                        if (!int.TryParse(textBoxOrdSearch.Text, out tempId))
                        {
                            MessageBox.Show("Please enter a valid term to be searched.", "No search information!");
                            return;
                        }
                        aorder = OrderDA.SearchID(Convert.ToInt32(textBoxOrdSearch.Text));
                        break;
                    case 1:
                        if (!int.TryParse(textBoxOrdSearch.Text, out tempId))
                        {
                            MessageBox.Show("Please enter a valid term to be searched.", "No search information!");
                            return;
                        }
                        aorder = OrderDA.SearchByClientID(Convert.ToInt32(textBoxOrdSearch.Text));
                        break;
                    case 2:
                        aorder = OrderDA.SearchByClientName(textBoxOrdSearch.Text);
                        break;         

                }

                if (aorder == null)
                {
                    MessageBox.Show("Order not found!", "Not found!");
                    textBoxOrdSearch.Clear();
                    textBoxOrdSearch.Focus();
                    return;
                }

                OrderDA.ListSearch(aorder, listViewOrder);
                return;
            }
            MessageBox.Show("Please enter a valid term to be searched.", "No search information!");
            return;                         

        }
                 
    }
}


