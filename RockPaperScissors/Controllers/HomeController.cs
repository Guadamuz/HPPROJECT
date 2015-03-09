using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RockPaperScissors.Models;
using RockPaperScissors.Models.Data;
using System.Xml.Linq;

namespace RockPaperScissors.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "ROCK PAPER SCISSORS";
            ViewData["INFO"] = " <Persons><Person><User>LUGUA</User><FirstName>Luis</FirstName><SecondName>Guadamuz</SecondName><Strategy>P</Strategy></Person></Persons>";

            return View();
        }


        public class InfoGame
        {

            public string User { get; set; }
            public string Object { get; set; }

        }

        [HttpGet]
        public JsonResult ExecuteGame(string info)
        {
            GameModel vModel = new GameModel();
            vModel.ExecuteGame(info);
            return Json(vModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]  
        public JsonResult GetUser(string info)
        {
            UserModel vModel = new UserModel();
            //vModel.GetUser(info);
            return Json(vModel,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ServicesPage(string User,string FirstName,string SecondName)
        {
            UserModel vModel = new UserModel();
            vModel.CreateUser(User, FirstName, SecondName);

            return Json(new {sucess=true});
            
        }
    }
}
