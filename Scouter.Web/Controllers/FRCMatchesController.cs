using Scouter.Data;
using Scouter.Web.Models.FRCMatch;
using Scouter.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AllianceDataTransfer = Scouter.Web.Models.FRCEvent.AllianceDataTransfer;

namespace Scouter.Web.Controllers
{
    public class FRCMatchesController : Controller
    {
        private ApplicationUnit _unit = new ApplicationUnit();

        public ActionResult Index()
        {
            FRCMatchesListViewModel vm = new FRCMatchesListViewModel();
            var query = from m in this._unit.FRCMatches.GetAll()
                        orderby new { m.FRCEvent.Id, m.SequenceNumber }
                        select new FRCMatchDataTransfer() 
                        {
                            Id = m.Id,
                            SequenceNumber = m.SequenceNumber,
                            FRCEventName = m.FRCEvent.Name,
                            BlueAlliance = new AllianceDataTransfer()
                            {
                                Color = m.BlueAlliance.Color,
                                Team1 = m.BlueAlliance.Team1,
                                Team2 = m.BlueAlliance.Team2,
                                Team3 = m.BlueAlliance.Team3
                            },
                            RedAlliance = new AllianceDataTransfer()
                            {
                                Color = m.RedAlliance.Color,
                                Team1 = m.RedAlliance.Team1,
                                Team2 = m.RedAlliance.Team2,
                                Team3 = m.RedAlliance.Team3
                            },
                        };
            vm.FRCMatches = query.ToList();
                        

            return View("Index", vm);
        }


    }
}
