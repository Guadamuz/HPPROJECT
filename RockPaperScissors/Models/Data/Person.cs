using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockPaperScissors.Models.Data
{
    public class Person
    {

        #region Field Space
        private string vUser;
        private string vFirstName;
        private string vSecondName;
        private string vStrategy;
        #endregion Field Space

        #region Properties Space

        public string Strategy
        {
            get
            {
                return this.vStrategy;
            }
            set
            {
                vStrategy = value;
            }
        }

        public string User
        {
            get
            {
                return this.vUser;
            }
            set
            {
                vUser = value;
            }
        }

        public string FirstName
        {
            get
            {
                return this.vFirstName;
            }
            set
            {
                vFirstName = value;
            }
        }

        public string SecondName
        {
            get
            {
                return this.vSecondName;
            }
            set
            {
                vSecondName = value;
            }
        }
        #endregion Properties Space

    }
}