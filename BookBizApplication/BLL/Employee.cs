using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBizApplication.BLL
{
    class Employee
    {
        //==============Variables==============

        private int empId;
        private string empFName;
        private string empLName;
        private string empPhone;
        private string empEmail;
        private string empPosition;
        private string userPassword;
        private int userLevel;

        //===========Properties================

        public int EmpId
        {
            get
            {
                return empId;
            }

            set
            {
                empId = value;
            }
        }

        public string EmpFName
        {
            get
            {
                return empFName;
            }

            set
            {
                empFName = value;
            }
        }

        public string EmpLName
        {
            get
            {
                return empLName;
            }

            set
            {
                empLName = value;
            }
        }

        public string EmpPhone
        {
            get
            {
                return empPhone;
            }

            set
            {
                empPhone = value;
            }
        }

        public string EmpEmail
        {
            get
            {
                return empEmail;
            }

            set
            {
                empEmail = value;
            }
        }

        public string EmpPosition
        {
            get
            {
                return empPosition;
            }

            set
            {
                empPosition = value;
            }
        }

        public string UserPassword
        {
            get
            {
                return userPassword;
            }

            set
            {
                userPassword = value;
            }
        }

        public int UserLevel
        {
            get
            {
                return userLevel;
            }

            set
            {
                userLevel = value;
            }
        }

        //=============Methods==============

      
    }
}
