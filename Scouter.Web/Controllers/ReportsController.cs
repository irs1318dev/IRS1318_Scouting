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
    public class ReportsController : Controller
    {
        private readonly ApplicationUnit _unit = new ApplicationUnit();

        /// <summary>
        /// Supports the list of matches for a given team
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Index(int eventId = -1, int id = -1)
        {
            if (eventId == -1)
                eventId = CurrentEvent();

            if (id == -1)
                id = 1318;

            var upcomingMatches = from m in _unit.FRCMatches.GetAll().ToList()
                                  where
                                    (m.BlueAlliance.Team1.Number == id ||
                                    m.BlueAlliance.Team2.Number == id ||
                                    m.BlueAlliance.Team3.Number == id ||
                                    m.RedAlliance.Team1.Number == id ||
                                    m.RedAlliance.Team2.Number == id ||
                                    m.RedAlliance.Team3.Number == id) &&
                                    m.FRCEvent.Id == eventId
                                  orderby m.SequenceNumber
                                  select new
                                  {
                                      EventID = eventId,
                                      match_Seq = m.SequenceNumber,
                                      blue1 = m.BlueAlliance.Team1.Number,
                                      blue2 = m.BlueAlliance.Team2.Number,
                                      blue3 = m.BlueAlliance.Team3.Number,
                                      red1 = m.RedAlliance.Team1.Number,
                                      red2 = m.RedAlliance.Team2.Number,
                                      red3 = m.RedAlliance.Team3.Number
                                  };
            List<AllMatches> ams = new List<AllMatches>();
            
            foreach(var m in upcomingMatches)
            {
                AllMatches am = new AllMatches();
                am.MatchSeq = m.match_Seq;
                am.Blue1 = m.blue1;
                am.Blue2 = m.blue2;
                am.Blue3 = m.blue3;
                am.Red1 = m.red1;
                am.Red2 = m.red2;
                am.Red3 = m.red3;
                ams.Add(am);
            }

            ReportViewModel um = new ReportViewModel();
            um.TeamNumber = id;
            um.AllFRCEvents = (from e in _unit.FRCCompetitions.GetAll() orderby e.BeginDate select new AllEvents{ EventId = e.Id, Name = e.Name }).ToList();
            um.CurrentEventID = eventId;
            um.CurrentMatchSeq = CurrentMatch();
            um.AllIRSMatches = ams;

            return View("Index", um);
        }

        //
        // GET: /UpcomingMatch/
        [ActionName("UpcomingMatch")]
        [HttpGet]
        public ActionResult UpcomingMatch(int eventId = -1 , int id = -1) // id is Match SequenceNumber
        {
            if (eventId == -1)
            {
                eventId = CurrentEvent();
            }

            if (id == -1)
            {
                id = CurrentMatch();
            }

            // get the teams that are participating in match sequence = id
            var upMatch = from m in _unit.FRCMatches.GetAll()
                                where m.SequenceNumber == id &&
                                      m.FRCEvent.Id == eventId
                                select new
                                {
                                    seq = m.SequenceNumber,
                                    blue1 = m.BlueAlliance.Team1.Number,
                                    blue2 = m.BlueAlliance.Team2.Number,
                                    blue3 = m.BlueAlliance.Team3.Number,
                                    red1 = m.RedAlliance.Team1.Number,
                                    red2 = m.RedAlliance.Team2.Number,
                                    red3 = m.RedAlliance.Team3.Number
                                };

            // prime the data classes that we depend on
            var robotevents = _unit.RobotEvents.GetAll().Where(e => e.Match.FRCEvent.Id == eventId).ToList();
            var upcomingMatch = upMatch.ToList();

            // get the top three events list for each team from matches the precede the input match sequence number
            var b1 = BuildScorableList(upcomingMatch[0].blue1, upcomingMatch[0].seq, robotevents);
            var b2 = BuildScorableList(upcomingMatch[0].blue2, upcomingMatch[0].seq, robotevents);
            var b3 = BuildScorableList(upcomingMatch[0].blue3, upcomingMatch[0].seq, robotevents);
            var r1 = BuildScorableList(upcomingMatch[0].red1, upcomingMatch[0].seq, robotevents);
            var r2 = BuildScorableList(upcomingMatch[0].red2, upcomingMatch[0].seq, robotevents);
            var r3 = BuildScorableList(upcomingMatch[0].red3, upcomingMatch[0].seq, robotevents);

            // join the teams from each alliance into one list
            var eventinfo = b1.Union(b2.Union(b3.Union(r1.Union(r2.Union(r3)))));

            // score the teams
            List<TeamScore> sixteams = BuildTeamScores(eventinfo);
            List<TeamScore> allTeams = AllTeamRanker(eventId);

            var q = from t in sixteams
                    join a in allTeams on t.Team equals a.Team
                    select new { team = t, HelperRank = a.HelperRank, Offense1Rank = a.Offense1Rank, Offense2Rank = a.Offense2Rank };

            var ql = q.ToList();

            List<TeamScore> tss = new List<TeamScore>();

            foreach (var o in ql)
            {
                o.team.HelperRank = o.HelperRank;
                o.team.Offense1Rank = o.Offense1Rank;
                o.team.Offense2Rank = o.Offense2Rank;
                tss.Add(o.team);
            }


            // create the object required by the viewmodel
            UpcomingMatch um = new UpcomingMatch(eventId, id);

            // now unwind the scored list back into the alliance format
            foreach (var teamScore in sixteams)
            {
                if (teamScore.Team == upcomingMatch[0].blue1)
                    um.Blue1 = teamScore;
                else if (teamScore.Team == upcomingMatch[0].blue2)
                    um.Blue2 = teamScore;
                else if (teamScore.Team == upcomingMatch[0].blue3)
                    um.Blue3 = teamScore;
                else if (teamScore.Team == upcomingMatch[0].red1)
                    um.Red1 = teamScore;
                else if (teamScore.Team == upcomingMatch[0].red2)
                    um.Red2 = teamScore;
                else if (teamScore.Team == upcomingMatch[0].red3)
                    um.Red3 = teamScore;
            }

            ReportViewModel vm = new ReportViewModel();
            vm.IRSMatches = um;
            vm.CurrentEventName = (from e in _unit.FRCCompetitions.GetAll() where e.Id == eventId select e.Name).First();
            return View("UpcomingMatch", vm);
        }

        public ActionResult TeamRankings(int id = -1) // id = eventId
        {
            if (id == -1)
                id = CurrentEvent();

            List<TeamScore> tss = AllTeamRanker(id);

            ReportViewModel vm = new ReportViewModel();
            vm.TeamRankings = tss;
            vm.CurrentEventName = (from e in _unit.FRCCompetitions.GetAll() where e.Id == id select e.Name).First();
            return View("TeamRankings", vm);
        }

        #region Helpers

        private List<TeamScore> AllTeamRanker(int eventId)
        {
            var eventinfo = from t in _unit.Teams.GetAll().ToList()
                            join e in _unit.RobotEvents.GetAll().ToList() on t.Id equals e.Team.Id
                            where e.Match.FRCEvent.Id == eventId
                            //&& t.Number == 4911
                            orderby t.Number
                            select new ScoringList { Team_Number = e.Team.Number, Picture = e.Team.ImageName, Team_Description = e.Team.Description, RobotEventType = e.RobotEventType, RobotMode = e.RobotMode, Match_Seq = e.Match.SequenceNumber };

            List<TeamScore> tss = BuildTeamScores(eventinfo);
            tss = RankTeamScores(tss);
            return tss;
        }

        private IEnumerable<ScoringList> BuildScorableList(int teamNumber, int matchSeq, List<RobotEvent> robotevents)
        {
            // filter the events to one that apply to the team and matches that have smaller sequence to the given match sequence id
            var eFor = from r in robotevents
                         where
                             r.Team.Number == teamNumber &&
                             r.Match.SequenceNumber < matchSeq
                         orderby r.Match.SequenceNumber descending
                         select new { r.Match.SequenceNumber, TeamNumber = teamNumber };
            eFor.ToList(); // force execute the query otherwise the final to prevent the following queries from taking forever to run

            // get a distinct list of the match sequences and take only the last three
            var eForDist = from r in eFor
                             group r by new { r.SequenceNumber, r.TeamNumber } into g
                             orderby g.Key.SequenceNumber descending
                             select g.Key;
            var tak3 = eForDist.Take(3);
            tak3.ToList(); // force execute just in case

            // using the three found above return all the events
            var eFor3 = from e in robotevents
                          join l in tak3 on e.Match.SequenceNumber equals l.SequenceNumber
                          where
                            e.Team.Number == l.TeamNumber
                        select new ScoringList { Team_Number = e.Team.Number, Picture = e.Team.ImageName, Team_Description = e.Team.Description, RobotEventType = e.RobotEventType, RobotMode = e.RobotMode, Match_Seq = e.Match.SequenceNumber };
            eFor3.ToList(); // force execute just in case

            return eFor3;
        }

        private List<TeamScore> BuildTeamScores(IEnumerable<ScoringList> eventinfo)
        {

            // Model: UpcomingMatch has TeamScores has PointAllocation has PointType
            // loop through all the RobotEvents for each of the six teams.
            // create a PointAllocation for each team/match combination
            // create a TeamScore for each team
            // store the TeamScores in a list then find out which alliance slots they have for the upcoming match
            List<TeamScore> tss = new List<TeamScore>();
            TeamScore ts = null;
            List<PointAllocation> pas = new List<PointAllocation>();
            PointAllocation pa = null;

            int loopMatchId = -1;
            int loopTeamId = -1;
            foreach (var e in eventinfo)
            {
                int tempMatchId = e.Match_Seq;
                if (tempMatchId != loopMatchId)
                {
                    if (pa != null)
                    {
                        pas.Add(pa); // point allocation for this team in this match is complete
                    }
                    pa = new PointAllocation(tempMatchId);
                    loopMatchId = tempMatchId;
                }

                int tempTeamId = e.Team_Number;
                if (tempTeamId != loopTeamId)
                {
                    if (ts != null)
                    {
                        tss.Add(ts);
                    }
                    pas = new List<PointAllocation>();
                    ts = new TeamScore(e.Team_Number, e.Picture, e.Team_Description, pas);
                    loopTeamId = tempTeamId;
                }

                string n = e.RobotEventType.ToString();

                if (e.RobotMode == RobotMode.Autonomous)
                {
                    if (n == pa.AutoAttemptClutter.Name)
                        pa.AutoAttemptClutter.Count++;
                    else if (n == pa.AutonomousMoved.Name)
                        pa.AutonomousMoved.Count++;
                    else if (n == pa.RightToteMoved.Name)
                        pa.RightToteMoved.Count++;
                    else if (n == pa.CenterToteMoved.Name)
                        pa.CenterToteMoved.Count++;
                    else if (n == pa.LeftToteMoved.Name)
                        pa.LeftToteMoved.Count++;
                    else if (n == pa.RightContainerMoved.Name)
                        pa.RightContainerMoved.Count++;
                    else if (n == pa.CenterContainerMoved.Name)
                        pa.CenterContainerMoved.Count++;
                    else if (n == pa.LeftContainerMoved.Name)
                        pa.LeftContainerMoved.Count++;
                    else if (n == pa.TotesStacked.Name)
                        pa.TotesStacked.Count++;
                    else if (n == pa.Foul.Name)
                        pa.Foul.Count++;
                    else if (n == pa.LeftContainerFromStep.Name)
                        pa.LeftContainerFromStep.Count++;
                    else if (n == pa.LeftCenterContainerFromStep.Name)
                        pa.LeftCenterContainerFromStep.Count++;
                    else if (n == pa.RightCenterContainerFromStep.Name)
                        pa.RightCenterContainerFromStep.Count++;
                    else if (n == pa.RightContainerFromStep.Name)
                        pa.RightContainerFromStep.Count++;
                    else if (n == pa.NoAutonomous.Name)
                        pa.NoAutonomous.Count++;
                }
                else
                {
                    if (n == pa.TotesStacked.Name)
                        pa.TotesStacked.Count++;
                    else if (n == pa.ScoredLow.Name)
                        pa.ScoredLow.Count++;
                    else if (n == pa.MissedHigh.Name)
                        pa.MissedHigh.Count++;
                    else if (n == pa.MissedLow.Name)
                        pa.MissedLow.Count++;
                    else if (n == pa.BlockedShot.Name)
                        pa.BlockedShot.Count++;
                    else if (n == pa.StartedInGoalBox.Name)
                        pa.StartedInGoalBox.Count++;
                    else if (n == pa.BlockedRobot.Name)
                        pa.BlockedRobot.Count++;
                    else if (n == pa.BlockedPass.Name)
                        pa.BlockedPass.Count++;
                    else if (n == pa.Pass.Name)
                        pa.Pass.Count++;
                    else if (n == pa.TechFoul.Name)
                        pa.TechFoul.Count++;
                    else if (n == pa.Truss.Name)
                        pa.Truss.Count++;
                    else if (n == pa.TrussCatch.Name)
                        pa.TrussCatch.Count++;
                    else if (n == pa.Inbound.Name)
                        pa.Inbound.Count++;
                    else if (n == pa.MissedInbound.Name)
                        pa.MissedInbound.Count++;
                }
            }
            tss.Add(ts);

            return tss;
        }

        private List<TeamScore> RankTeamScores(List<TeamScore> tss)
        {
            var q = from t in tss
                    orderby t.imEffectivness descending
                    select t;
            List<TeamScore> ql = q.ToList();

            for (int x = 1; x < ql.Count() + 1; x++)
            {
                ql[x - 1].HelperRank = x;
            }

            var q2 = from t in ql
                     orderby t.Offense1Rank descending
                     select t;
            ql = q2.ToList();

            for (int x = 1; x < ql.Count() + 1; x++)
            {
                ql[x - 1].Offense1Rank = x;
            }

            var q3 = from t in ql
                     orderby t.gmEffectivness descending
                     select t;
            ql = q3.ToList();

            for (int x = 1; x < ql.Count() + 1; x++)
            {
                ql[x - 1].Offense2Rank = x;
            }
            
            return ql;
        }

        private int CurrentEvent()
        {
            return _unit.CurrentScoutData.GetById(1).Event_ID;
        }

        private int CurrentMatch()
        {
            var mid = _unit.CurrentScoutData.GetById(1).Match_ID;
            var seq = from m in _unit.FRCMatches.GetAll()
                      where m.Id == mid
                      select m.SequenceNumber;
            return seq.First<int>();
        }

        #endregion Helpers
    }
}