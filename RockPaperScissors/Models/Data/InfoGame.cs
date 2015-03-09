using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockPaperScissors.Models.Data
{
    public class InfoGame
    {

        #region Field Space
        private string vUser;
        private string vResult;
        private string vIdGame;
        #endregion Field Space

        #region Properties Space

        public string Result
        {
            get
            {
                return this.vResult;
            }
            set
            {
                vResult = value;
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
        public string IdGame
        {
            get
            {
                return this.vIdGame;
            }
            set
            {
                vIdGame = value;
            }
        }

        #endregion Properties Space
    }
}