using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using RockPaperScissors.Models.Data;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.Configuration;
using System.Web.Script.Serialization;


namespace RockPaperScissors.Models
{



    public class UserModel
    {

        #region Field Space
        List<Person> vPersonData;
        List<InfoGame> vInfoGame;
        #endregion Field Space

        #region Constructor Space
        public UserModel()
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

        public List<Person> UserPerson
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

        [HttpPost]
        public string CreateUser(string pUser, string pFirstName, string pSecondName)
        {

            string vResult = string.Empty;

            try
            {
                Person vPerson = new Person();
                vPerson.User = pUser.ToUpper();
                vPerson.FirstName = pFirstName.ToUpper();
                vPerson.SecondName = pSecondName.ToUpper();
                UserPerson = new List<Person>();
                UserPerson.Add(vPerson);
                this.XmlUser(UserPerson, ref vResult);
            }
            catch (Exception ex)
            {
                vResult = ex.Message;
            }

            return vResult;


        }

        protected void XmlUser(List<Person> pPersons, ref string pResult)
        {
            pResult = string.Empty;
            bool vExist = false;
            string vPath = string.Concat(ConfigurationManager.AppSettings["PATH"].ToString(), "Persons.xml");
            XmlDocument document = new XmlDocument();
            document.Load(vPath);


            XmlNode nodeFirst = document.DocumentElement;

            XElement root = XElement.Load(vPath);
            IEnumerable<XElement> Users =
                from user in root.Elements("Person")
                select user;
            foreach (XElement user in Users)
            {
                foreach (Person vItem in pPersons)
                {
                    if (string.Equals(user.Element("User").Value, vItem.User))
                    {
                        vExist = true;
                        break;
                    }
                }
            }


            if (!vExist)
            {
                foreach (Person vItem in pPersons)
                {
                    XmlElement vPerson = document.CreateElement("Person");

                    XmlElement vUser = document.CreateElement("User");
                    vUser.InnerText = vItem.User;
                    vPerson.AppendChild(vUser);


                    XmlElement vFirstName = document.CreateElement("FirstName");
                    vFirstName.InnerText = vItem.FirstName;
                    vPerson.AppendChild(vFirstName);


                    XmlElement vSecondName = document.CreateElement("SecondName");
                    vSecondName.InnerText = vItem.SecondName;
                    vPerson.AppendChild(vSecondName);

                    nodeFirst.InsertAfter(vPerson, nodeFirst.LastChild);

                    document.Save(vPath);

                }
            }
            else
            {
                pResult = "Existe usuario";
            }
        }

        //[HttpGet]
        //public string GetUser(string pUser)
        //{

        //    string vPath = string.Concat(ConfigurationManager.AppSettings["PATH"].ToString(), "Persons.xml");
        //    string vPerson = string.Empty;
        //    InfoGame vUser = new InfoGame();

        //    string[] vUsers = pUser.Split(',');

        //    InfoUserGame = new List<InfoGame>();
        //    XElement root = XElement.Load(vPath);
        //    IEnumerable<XElement> Users =
        //        from user in root.Elements("Person")
        //        select user;

        //    GameModel vGame = new GameModel();
        //    //vGame.GameUser = vGame.MakeGame(pUser);

        //    foreach (string vUserGame in vUsers)
        //    {
        //        string[] vinfo = vUserGame.Split('|');
        //        foreach (XElement user in Users)
        //        {
        //            if (string.Equals(user.Element("User").Value, vinfo[0]))
        //            {
        //                vUser = new InfoGame();
        //                vUser.User = user.Element("User").Value;
        //                vUser.FirstName = user.Element("FirstName").Value;
        //                vUser.SecondName = user.Element("SecondName").Value;

        //                foreach (Game vResultGame in vGame.GameUser)
        //                {
        //                    if (string.Equals(vResultGame.UserWin, vinfo[0]))
        //                    {
        //                        vUser.Result = "WINNER";
        //                    }
        //                    else
        //                    {
        //                        vUser.Result = "LOSER";
        //                    }
        //                }
        //                InfoUserGame.Add(vUser);
        //            }
        //        }
        //    }
        //    vPerson = new JavaScriptSerializer().Serialize(InfoUserGame);
        //    return vPerson;
        //}
        #endregion Methods Space

    }


}
