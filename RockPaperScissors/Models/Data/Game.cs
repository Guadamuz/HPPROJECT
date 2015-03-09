using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockPaperScissors.Models.Data
{
    public class Game
    {
        #region Field Space

        private string vUserWin;
        private string vObjectFist;
        private string vSecondUser;
        private string vObjectSecond;
        private string vIdGame;
        private DateTime vDateGame;
        #endregion Field Space

        #region Properties Space
        public DateTime DateGame
        {
            get
            {
                return this.vDateGame;
            }
            set
            {
                vDateGame = value;
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
        public string ObjectSecond
        {
            get
            {
                return this.vObjectSecond;
            }
            set
            {
                vObjectSecond = value;
            }
        }
        public string SecondUser
        {
            get
            {
                return this.vSecondUser;
            }
            set
            {
                vSecondUser = value;
            }
        }
        public string UserWin
        {
            get
            {
                return this.vUserWin;
            }
            set
            {
                vUserWin = value;
            }
        }
        public string ObjectFist
        {
            get
            {
                return this.vObjectFist;
            }
            set
            {
                vObjectFist = value;
            }
        }
        #endregion Properties Space
    }
}