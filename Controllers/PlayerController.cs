using Microsoft.AspNet.Identity;
using PickME.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PickME.Controllers
{
    //[/*Authorize*/]
    public class PlayerController : Controller
    {
        private PickMEEntities db = new PickMEEntities();
        private footballAPI api = new footballAPI();
        public ActionResult Index(string player1, string player2)
        {
            playerSearchViewModel viewModel = null;

            if (!string.IsNullOrWhiteSpace(player1) && !string.IsNullOrWhiteSpace(player2))
            {
                var playerOne = api.Search(player1).ToList();
                var playerTwo = api.Search(player2).ToList();
                viewModel = new playerSearchViewModel { playerOneList = playerOne, playerTwoList = playerTwo };

                if (playerOne.Count() == 1 && playerTwo.Count() == 1)
                {
                    
                    viewModel.isRightComp = true;
                }

                
            }

            

            return View(viewModel);
        }
        public ActionResult Confirmation(int playerOneId, int playerTwoId)
        {
            var existingComp = db.comps.Where(c => c.firstPlayerId == playerOneId && c.secondPlayerId == playerTwoId).FirstOrDefault(c => c.exp > DateTime.Now);
            if (existingComp != null)
            {
                return RedirectToAction("Details", "Home", new { id = existingComp.compId });
            }
            var comp = new comp
            {
                exp = DateTime.Today.AddDays((DayOfWeek.Tuesday - DateTime.Today.DayOfWeek + 6) % 7 + 1),
                firstPlayerId = playerOneId,
                secondPlayerId = playerTwoId,
                userId = User.Identity.GetUserId()
            };

            db.comps.Add(comp);
            db.SaveChanges();
            return RedirectToAction("Account", "Home");
        }
    }
}