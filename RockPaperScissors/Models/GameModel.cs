using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RockPaperScissors.Models.Data;
using System.Web.Mvc;
using System.Xml;
using System.Configuration;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.Security.Cryptography;
using System.Text;

namespace RockPaperScissors.Models
{
    public class GameModel
    {
        #region Field Space
        List<Game> vGameData;
        List<Person> vPersonData;
        List<InfoGame> vInfoGame;

        #endregion Field Space

        #region Constructor Space
        public GameModel()
            : base()
        {
        }
        #endregion Constructor Space

        #region Properties Space

        public List<InfoGame> InfoUserGame
        {
            get
            {
                return vInfoGame;
            }
            set
            {
                vInfoGame = value;
            }
        }

        private List<Game> GameUser
        {
            get
            {
                return vGameData;
            }
            set
            {
                vGameData = value;
            }
        }

        private List<Person> UserPerson
        {
            get
            {
                return vPersonData;
            }
            set
            {
                vPersonData = value;
            }
        }


        #endregion Properties Space

        #region Methods Space

        [HttpGet]
        public string ExecuteGame(string pFile)
        {
            
            string vIdGame = string.Concat(DateTime.Now.ToFileTimeUtc().ToString(),DateTime.Now.Millisecond);
            bool vUserInvalid = false;
            string vInformation = string.Empty;
            UserPerson = new List<Person>();
            int vFrecuentUser = 0;

          
            try
            {
                XElement root = XElement.Load(pFile);
                IEnumerable<XElement> Users =
                    from user in root.Elements("Person")
                    select user;

                if (Users.Count() > 10)
                {
                    foreach (XElement user in Users)
                    {
                        vFrecuentUser = 0;
                        foreach (XElement SecondUser in Users)
                        {
                            if (string.Equals(user.Element("User").Value, SecondUser.Element("User").Value))
                            {
                                vFrecuentUser++;
                                if (vFrecuentUser > 2)
                                {
                                    vUserInvalid = true;
                                }
                            }

                        }
                    }

                    foreach (XElement user in Users)
                    {
                        if (!vUserInvalid)
                        {
                                if ((string.Equals(user.Element("Strategy").Value.ToUpper(), "P"))
                                    || (string.Equals(user.Element("Strategy").Value.ToUpper(), "R"))
                                    || (string.Equals(user.Element("Strategy").Value.ToUpper(), "S")))
                                {
                                    Person vPerson = new Person();
                                    vPerson.User = user.Element("User").Value.ToUpper();
                                    vPerson.FirstName = user.Element("FirstName").Value.ToUpper();
                                    vPerson.SecondName = user.Element("SecondName").Value.ToUpper();
                                    vPerson.Strategy = user.Element("Strategy").Value.ToUpper();
                                    UserPerson.Add(vPerson);
                                }
                                else
                                {
                                    vUserInvalid = true;
                                }
                        }
                    }
                }
                else
                {
                    vUserInvalid = true;
                }





                if (!vUserInvalid)
                {
                    this.MakeGame(UserPerson, vIdGame);
                    vInformation = this.GetInfoGame(vIdGame, pFile);

                }

            }
            catch (Exception ex)
            {
                vUserInvalid = true;
                vInformation = string.Empty;
            }
            return vInformation;
        }

        private bool ValidateShowDown(string pidGame, string pFirstUser, string pSecondUser)
        {
            bool vExitShowDown = false;
            string vPath = string.Concat(ConfigurationManager.AppSettings["PATH"].ToString(), "Game.xml");
            XElement vRoot = XElement.Load(vPath);
            IEnumerable<XElement> Games =
                from vGame in vRoot.Elements("Game")
                where (string)vGame.Element("IdGame") == pidGame && (string)vGame.Element("UserWin") == pFirstUser
                && (string)vGame.Element("SecondUser") == pSecondUser
                select vGame;

            if (Games.Count() > 0)
            {
                vExitShowDown = true;
            }
            else
            {

                IEnumerable<XElement> SecondGames =
                  from vGame in vRoot.Elements("Game")
                  where (string)vGame.Element("IdGame") == pidGame && (string)vGame.Element("UserWin") == pSecondUser
                  && (string)vGame.Element("SecondUser") == pFirstUser
                  select vGame;

                if (SecondGames.Count() > 0)
                {
                    vExitShowDown = true;
                }
            }
            return vExitShowDown;
        }

        private int UserPoint(string pidGame, string pUser)
        {
            int vPoints = 0;
            string vPath = string.Concat(ConfigurationManager.AppSettings["PATH"].ToString(), "Game.xml");
            XElement vRoot = XElement.Load(vPath);
            IEnumerable<XElement> Games =
                from vGame in vRoot.Elements("Game")
                where (string)vGame.Element("IdGame") == pidGame && (string)vGame.Element("UserWin") == pUser
                select vGame;

            vPoints = Games.Count() * 3;

            return vPoints;
        }

        protected void MakeGame(List<Person> pUsers, string pIdGame)
        {

            string vResult = string.Empty;
            string vUserWin = string.Empty;
            string vUserLoser = string.Empty;

            foreach (Person vFirstUser in pUsers)
            {
                foreach (Person vSecondUser in pUsers)
                {
                    if (!string.Equals(vFirstUser.User, vSecondUser.User))
                    {
                        if (!ValidateShowDown(pIdGame, vFirstUser.User, vSecondUser.User))
                        {
                            if (!string.Equals(vFirstUser.Strategy, vSecondUser.Strategy))
                            {

                                switch (vFirstUser.Strategy)
                                {

                                    case "R":
                                        switch (vSecondUser.Strategy)
                                        {
                                            case "P":
                                                vUserWin = vSecondUser.User;
                                                vUserLoser = vFirstUser.User;
                                                break;
                                            case "S":
                                                vUserWin = vFirstUser.User;
                                                vUserLoser = vSecondUser.User;
                                                break;
                                        }
                                        break;
                                    case "P":
                                        switch (vSecondUser.Strategy)
                                        {
                                            case "R":
                                                vUserWin = vFirstUser.User;
                                                vUserLoser = vSecondUser.User;
                                                break;
                                            case "S":
                                                vUserWin = vSecondUser.User;
                                                vUserLoser = vFirstUser.User;
                                                break;
                                        }
                                        break;
                                    case "S":
                                        switch (vSecondUser.Strategy)
                                        {
                                            case "R":
                                                vUserWin = vSecondUser.User;
                                                vUserLoser = vFirstUser.User;
                                                break;
                                            case "P":
                                                vUserWin = vFirstUser.User;
                                                vUserLoser = vSecondUser.User;
                                                break;
                                        }
                                        break;
                                }

                            }
                            else
                            {
                                if (this.UserPoint(pIdGame, vFirstUser.User) >= this.UserPoint(pIdGame, vSecondUser.User))
                                {
                                    vUserWin = vFirstUser.User;
                                    vUserLoser = vSecondUser.User;
                                }
                                else
                                {
                                    vUserWin = vSecondUser.User;
                                    vUserLoser = vFirstUser.User;
                                }
                            }//Compare Strategy
                        } //ValidateShowDown

                        Game vGame = new Game();
                        vGame.DateGame = DateTime.Now;
                        vGame.UserWin = vUserWin;
                        vGame.ObjectFist = vFirstUser.Strategy;
                        vGame.ObjectSecond = vSecondUser.Strategy;
                        vGame.SecondUser = vUserLoser;
                        vGame.IdGame = pIdGame;
                        GameUser = new List<Game>();
                        GameUser.Add(vGame);

                        this.XmlGame(GameUser);
                    }//Compare same user
                }//For SecondUser
            }//For FirstUser    
        }

        protected void XmlGame(List<Game> pGames)
        {

            string vPath = string.Concat(ConfigurationManager.AppSettings["PATH"].ToString(), "Game.xml");
            XmlDocument document = new XmlDocument();
            document.Load(vPath);


            XmlNode nodeFirst = document.DocumentElement;


            foreach (Game vItem in pGames)
            {

                XmlElement vGame = document.CreateElement("Game");

                XmlElement vDateTime = document.CreateElement("DateGame");
                vDateTime.InnerText = vItem.DateGame.ToShortTimeString();
                vGame.AppendChild(vDateTime);


                XmlElement vUserWin = document.CreateElement("UserWin");
                vUserWin.InnerText = vItem.UserWin;
                vGame.AppendChild(vUserWin);


                XmlElement vObjectFirst = document.CreateElement("ObjectFirst");
                vObjectFirst.InnerText = vItem.ObjectFist;
                vGame.AppendChild(vObjectFirst);

                XmlElement vObjectSecond = document.CreateElement("ObjectSecond");
                vObjectSecond.InnerText = vItem.ObjectSecond;
                vGame.AppendChild(vObjectSecond);

                XmlElement vSecondUser = document.CreateElement("SecondUser");
                vSecondUser.InnerText = vItem.SecondUser.ToString();
                vGame.AppendChild(vSecondUser);

                XmlElement vIdGame = document.CreateElement("IdGame");
                vIdGame.InnerText = vItem.IdGame;
                vGame.AppendChild(vIdGame);

                nodeFirst.InsertAfter(vGame, nodeFirst.LastChild);

                document.Save(vPath);

            }

        }

        protected string GetInfoGame(string pIdGame,string pFile)
        {
            string vInfoGame = string.Empty;
            InfoGame vUser = new InfoGame();
            InfoUserGame = new List<InfoGame>();
            int vNumWin = 0;

            XElement root = XElement.Load(pFile);
            IEnumerable<XElement> Users =
                from user in root.Elements("Person")
                select user;

            string vPath = string.Concat(ConfigurationManager.AppSettings["PATH"].ToString(), "Game.xml");
            XElement vRoot = XElement.Load(vPath);
            IEnumerable<XElement> Games =
                from vGame in vRoot.Elements("Game")
                where (string)vGame.Element("IdGame") == pIdGame
                select vGame;


            foreach (XElement User in Users)
            {
                vNumWin = 0;
                foreach (XElement Game in Games)
                {

                    if (string.Equals(User.Element("User").Value, Game.Element("UserWin").Value))
                    {
                        vNumWin++;
                    }      
                }
                vUser = new InfoGame();
                vUser.User = User.Element("User").Value;
                vUser.Result = vNumWin.ToString();
                vUser.IdGame = pIdGame;
                InfoUserGame.Add(vUser);
            }

            vInfoGame = new JavaScriptSerializer().Serialize(InfoUserGame);

            return vInfoGame;
        }
        #endregion Methods Space
    }
}