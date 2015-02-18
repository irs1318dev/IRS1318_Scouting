using Scouter.Data;
using Scouter.Models;
using Scouter.Web.Models.FRCEvent;
using Scouter.Web.Models.Scouting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Scouter.Web.Controllers.api
{
    public class ScouterApiController : ApiController
    {
        private ApplicationUnit _unit = new ApplicationUnit();

        /// <summary>
        /// Gets the current information about all the scouts, including status, match number, and team number
        /// </summary>
        [HttpGet]
        public ScoutDataTransfer GetScoutData()
        {
            var data = _unit.CurrentScoutData.GetById(1);


            return new ScoutDataTransfer()
            {
                Blue1 = data.Blue1,
                Blue1Status = data.Blue1Status,
                Blue2 = data.Blue2,
                Blue2Status = data.Blue2Status,
                Blue3 = data.Blue3,
                Blue3Status = data.Blue3Status,
                Red1 = data.Red1,
                Red1Status = data.Red1Status,
                Red2 = data.Red2,
                Red2Status = data.Red2Status,
                Red3 = data.Red3,
                Red3Status = data.Red3Status,
                Blue1Match = new Models.FRCEvent.FRCMatchDataTransfer()
                {
                    Id = (data.Blue1Match != null) ? data.Blue1Match.Id : 0,
                    SequenceNumber = (data.Blue1Match != null) ? data.Blue1Match.SequenceNumber : 0
                },
                Blue2Match = new Models.FRCEvent.FRCMatchDataTransfer()
                {
                    Id = (data.Blue2Match != null) ? data.Blue2Match.Id : 0,
                    SequenceNumber = (data.Blue2Match != null) ? data.Blue2Match.SequenceNumber : 0
                },
                Blue3Match = new Models.FRCEvent.FRCMatchDataTransfer()
                {
                    Id = (data.Blue3Match != null) ? data.Blue3Match.Id : 0,
                    SequenceNumber = (data.Blue3Match != null) ? data.Blue3Match.SequenceNumber : 0
                },
                Red1Match = new Models.FRCEvent.FRCMatchDataTransfer()
                {
                    Id = (data.Red1Match != null) ? data.Red1Match.Id : 0,
                    SequenceNumber = (data.Red1Match != null) ? data.Red1Match.SequenceNumber : 0
                },
                Red2Match = new Models.FRCEvent.FRCMatchDataTransfer()
                {
                    Id = (data.Red2Match != null) ? data.Red2Match.Id : 0,
                    SequenceNumber = (data.Red2Match != null) ? data.Red2Match.SequenceNumber : 0
                },
                Red3Match = new Models.FRCEvent.FRCMatchDataTransfer()
                {
                    Id = (data.Red3Match != null) ? data.Red3Match.Id : 0,
                    SequenceNumber = (data.Red3Match != null) ? data.Red3Match.SequenceNumber : 0
                },
                Match_ID = data.Match_ID,
                MatchNumber = _unit.FRCMatches.GetById(data.Match_ID).SequenceNumber
            };
        }

    }
}