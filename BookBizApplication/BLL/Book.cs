using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBizApplication.BLL
{
    class Book
    {
        //==============Variables==============

        private long bkISBN;
        private string bkTitle;
        private decimal bkUnitPrice;
        private DateTime bkDate;
        private int bkQOH;
        private string bkCategory;
        private string bkPublisher;
        private Author bkAuthor;
        private int bkOrdQuantity;

        //===========Properties================

 

        public string BkTitle
        {
            get
            {
                return bkTitle;
            }

            set
            {
                bkTitle = value;
            }
        }

        public decimal BkUnitPrice
        {
            get
            {
                return bkUnitPrice;
            }

            set
            {
                bkUnitPrice = value;
            }
        }

        public DateTime BkDate
        {
            get
            {
                return bkDate;
            }

            set
            {
                bkDate = value;
            }
        }

        public int BkQOH
        {
            get
            {
                return bkQOH;
            }

            set
            {
                bkQOH = value;
            }
        }

        public string BkCategory
        {
            get
            {
                return bkCategory;
            }

            set
            {
                bkCategory = value;
            }
        }

    
    
        public string BkPublisher
        {
            get
            {
                return bkPublisher;
            }

            set
            {
                bkPublisher = value;
            }
        }



        public int BkOrdQuantity
        {
            get
            {
                return bkOrdQuantity;
            }

            set
            {
                bkOrdQuantity = value;
            }
        }

        internal Author BkAuthor
        {
            get
            {
                return bkAuthor;
            }

            set
            {
                bkAuthor = value;
            }
        }

        public long BkISBN
        {
            get
            {
                return bkISBN;
            }

            set
            {
                bkISBN = value;
            }
        }

        //=============Methods==============
    }
}
