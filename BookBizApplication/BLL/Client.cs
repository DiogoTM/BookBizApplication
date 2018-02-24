using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBizApplication.BLL
{
    class Client
    {
        //==============Variables==============

        private int clientId;
        private string clientName;
        private string clientStreet;
        private string clientEmail;
        private string clientCity;
        private string clientPostCode;
        private string clientPhone;
        private string clientFax;
        private decimal clientCredit;

        //===========Properties================

        public int ClientId
        {
            get
            {
                return clientId;
            }

            set
            {
                clientId = value;
            }
        }

        public string ClientName
        {
            get
            {
                return clientName;
            }

            set
            {
                clientName = value;
            }
        }

        public string ClientStreet
        {
            get
            {
                return clientStreet;
            }

            set
            {
                clientStreet = value;
            }
        }

        public string ClientCity
        {
            get
            {
                return clientCity;
            }

            set
            {
                clientCity = value;
            }
        }

        public string ClientPostCode
        {
            get
            {
                return clientPostCode;
            }

            set
            {
                clientPostCode = value;
            }
        }

        public string ClientPhone
        {
            get
            {
                return clientPhone;
            }

            set
            {
                clientPhone = value;
            }
        }

        public string ClientFax
        {
            get
            {
                return clientFax;
            }

            set
            {
                clientFax = value;
            }
        }

        public decimal ClientCredit
        {
            get
            {
                return clientCredit;
            }

            set
            {
                clientCredit = value;
            }
        }

        public string ClientEmail
        {
            get
            {
                return clientEmail;
            }

            set
            {
                clientEmail = value;
            }
        }

        //=============Methods==============
    }
}
