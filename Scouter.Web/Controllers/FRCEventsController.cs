using Scouter.Data;
using Scouter.Web.Models;
using Scouter.Web.Models.FRCEvent;
using Scouter.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Scouter.Web.Controllers
{
    public class FRCEventsController : Controller
    {
        private ApplicationUnit _unit = new ApplicationUnit();

        public ActionResult Index()
        {
            FRCEventsListViewModel vm = new FRCEventsListViewModel();
            var query = from v in this._unit.FRCCompetitions.GetAll()
                        select new FRCEventDataTransfer() { Id = v.Id, Venue = v.Venue, FinishDate = v.FinishDate,
                            BeginDate = v.BeginDate, Name = v.Name, City = v.City, State = v.State};
            vm.FRCEvents = query.ToList();
            return View("Index", vm);
        }

        [ActionName("Edit")]
        public ActionResult GetForEdit(int id)
        {
            FRCEventViewModel vm = new FRCEventViewModel();
            vm.Event = this._unit.FRCCompetitions.GetById(id);

            if (vm.Event != null)
                return View("Edit", vm);

            return View("NotFound");
        }

        [ActionName("View")]
        public ActionResult Get(int id)
        {
            FRCEventViewViewModel vm = new FRCEventViewViewModel();
            vm.Event = this._unit.FRCCompetitions.GetById(id);
            //var query = this._unit.FRCMatches.GetAll().Where(p => p.FRCEventKey == vm.Event.Id).OrderBy(s => s.SequenceNumber);
            var q = from e in this._unit.FRCMatches.GetAll()
                    where e.FRCEvent.Id == id
                    select new FRCMatchDataTransfer()
                    {
                        BlueAlliance = new AllianceDataTransfer()
                        {
                            Color = e.BlueAlliance.Color,
                            Team1 = e.BlueAlliance.Team1,
                            Team2 = e.BlueAlliance.Team2,
                            Team3 = e.BlueAlliance.Team3
                        }
                        ,
                        Id = e.Id,
                        RedAlliance = new AllianceDataTransfer()
                        {
                            Color = e.RedAlliance.Color,
                            Team1 = e.RedAlliance.Team1,
                            Team2 = e.RedAlliance.Team2,
                            Team3 = e.RedAlliance.Team3
                        },
                        SequenceNumber = e.SequenceNumber
                    };
            vm.Matches = q.ToList();

            if (vm.Event != null)
                return View("View", vm);

            return View("NotFound");
        }
    }
}