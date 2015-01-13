using Scouter.Data;
using Scouter.Models;
using Scouter.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Scouter.Web.Models.Scouting;

namespace Scouter.Web.Controllers
{
    public class ScoutingController : Controller
    {
        private ApplicationUnit _unit = new ApplicationUnit();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Auto(int id)
        {
            ScoutViewModel vm = new ScoutViewModel();
            var mid = _unit.CurrentScoutData.GetById(1).Match_ID;
            vm.Match = _unit.FRCMatches.GetById(mid);
            vm.Scouter_Id = id;
            switch(id)
            {
                case 1://red1
                    vm.Color = (int)AllianceColor.Red;
                    vm.Team = vm.Match.RedAlliance.Team1;
                    break;
                case 2://red2
                    vm.Color = (int)AllianceColor.Red;
                    vm.Team = vm.Match.RedAlliance.Team2;
                    break;
                case 3://red3
                    vm.Color = (int)AllianceColor.Red;
                    vm.Team = vm.Match.RedAlliance.Team3;
                    break;
                case 4://blue1
                    vm.Color = (int)AllianceColor.Blue;
                    vm.Team = vm.Match.BlueAlliance.Team1;
                    break;
                case 5://blue2
                    vm.Color = (int)AllianceColor.Blue;
                    vm.Team = vm.Match.BlueAlliance.Team2;
                    break;
                case 6://blue3
                    vm.Color = (int)AllianceColor.Blue;
                    vm.Team = vm.Match.BlueAlliance.Team3;
                    break;
            }
            return View("Auto", vm);
        }

        public ActionResult Teleop(int id)
        {
            ScoutViewModel vm = new ScoutViewModel();
            var mid = _unit.CurrentScoutData.GetById(1).Match_ID;
            vm.Match = _unit.FRCMatches.GetById(mid);
            vm.Scouter_Id = id;
            switch (id)
            {
                case 1://red1
                    vm.Color = (int)AllianceColor.Red;
                    vm.Team = vm.Match.RedAlliance.Team1;
                    break;
                case 2://red2
                    vm.Color = (int)AllianceColor.Red;
                    vm.Team = vm.Match.RedAlliance.Team2;
                    break;
                case 3://red3
                    vm.Color = (int)AllianceColor.Red;
                    vm.Team = vm.Match.RedAlliance.Team3;
                    break;
                case 4://blue1
                    vm.Color = (int)AllianceColor.Blue;
                    vm.Team = vm.Match.BlueAlliance.Team1;
                    break;
                case 5://blue2
                    vm.Color = (int)AllianceColor.Blue;
                    vm.Team = vm.Match.BlueAlliance.Team2;
                    break;
                case 6://blue3
                    vm.Color = (int)AllianceColor.Blue;
                    vm.Team = vm.Match.BlueAlliance.Team3;
                    break;
            }
            return View("Teleop", vm);
        }

		public ActionResult Notes(int id)
		{
			ScoutViewModel vm = new ScoutViewModel();
			var mid = _unit.CurrentScoutData.GetById(1).Match_ID;
			vm.Match = _unit.FRCMatches.GetById(mid);
			vm.Scouter_Id = id;
			switch (id)
			{
				case 1://red1
					vm.Color = (int)AllianceColor.Red;
					vm.Team = vm.Match.RedAlliance.Team1;
					break;
				case 2://red2
					vm.Color = (int)AllianceColor.Red;
					vm.Team = vm.Match.RedAlliance.Team2;
					break;
				case 3://red3
					vm.Color = (int)AllianceColor.Red;
					vm.Team = vm.Match.RedAlliance.Team3;
					break;
				case 4://blue1
					vm.Color = (int)AllianceColor.Blue;
					vm.Team = vm.Match.BlueAlliance.Team1;
					break;
				case 5://blue2
					vm.Color = (int)AllianceColor.Blue;
					vm.Team = vm.Match.BlueAlliance.Team2;
					break;
				case 6://blue3
					vm.Color = (int)AllianceColor.Blue;
					vm.Team = vm.Match.BlueAlliance.Team3;
					break;
			}
			return View("Notes", vm);
		}

        public ActionResult Admin()
        {
            ScouterAdminViewModel vm = new ScouterAdminViewModel();

            return View("Admin", vm);
        }
    }
}