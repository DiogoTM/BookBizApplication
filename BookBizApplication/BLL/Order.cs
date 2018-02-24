using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBizApplication.DAL;
using System.IO;

namespace BookBizApplication.BLL
{
    class Order
    {
        //==============Variables==============

        private Book ordBbook;
        private Client ordClient;
        private int ordBkQuantity;
        private decimal ordCost;
        private int ordId;
        private DateTime ordDate;
        private string ordType;
        
        //===========Properties================         
        
  
        public decimal OrdCost
        {
            get
            {
                return ordCost;
            }

            set
            {
                ordCost = value;
            }
        }

       
        public int OrdId
        {
            get
            {
                return ordId;
            }

            set
            {
                ordId = value;
            }
        }

        public DateTime OrdDate
        {
            get
            {
                return ordDate;
            }

            set
            {
                ordDate = value;
            }
        }

        internal Book OrdBbook
        {
            get
            {
                return ordBbook;
            }

            set
            {
                ordBbook = value;
            }
        }

        internal Client OrdClient
        {
            get
            {
                return ordClient;
            }

            set
            {
                ordClient = value;
            }
        }

        public int OrdBkQuantity
        {
            get
            {
                return ordBkQuantity;
            }

            set
            {
                ordBkQuantity = value;
            }
        }

        public string OrdType
        {
            get
            {
                return ordType;
            }

            set
            {
                ordType = value;
            }
        }



        //=============Methods==============





    }
}
