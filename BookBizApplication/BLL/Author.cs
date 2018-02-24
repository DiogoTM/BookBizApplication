using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBizApplication.BLL
{
    class Author
    {
        //==============Variables==============

        private int autID;
        private string autFirstN;
        private string autLastN;
        private string autEmail;

        //===========Properties================

        public int AutID
        {
            get
            {
                return autID;
            }

            set
            {
                autID = value;
            }
        }

        public string AutFirstN
        {
            get
            {
                return autFirstN;
            }

            set
            {
                autFirstN = value;
            }
        }

        public string AutLastN
        {
            get
            {
                return autLastN;
            }

            set
            {
                autLastN = value;
            }
        }

        public string AutEmail
        {
            get
            {
                return autEmail;
            }

            set
            {
                autEmail = value;
            }
        }

        //=============Methods==============
    }
}
