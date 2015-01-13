using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Scouter.Models;

namespace Scouter.Web.Models.Scouting
{
    public class AllEvents
    {
        public int EventId { get; set; }
        public string Name { get; set; }
    }

    public class AllMatches
    {
        public int EventId { get; set; }
        public int MatchSeq { get; set; }
        public int Blue1 { get; set; }
        public int Blue2 { get; set; }
        public int Blue3 { get; set; }
        public int Red1 { get; set; }
        public int Red2 { get; set; }
        public int Red3 { get; set; }
    }

    /// <summary>
    /// Holds the six teams for a given match
    /// </summary>
    public class UpcomingMatch
    {
        private readonly int eventId;
        private readonly int upcomingMatchSeq;
        public int EventId { get { return eventId; } }
        public int UpcomingMatchSeq { get { return this.upcomingMatchSeq; } }

        public TeamScore Blue1 { get; set; }
        public TeamScore Blue2 { get; set; }
        public TeamScore Blue3 { get; set; }
        public TeamScore Red1 { get; set; }
        public TeamScore Red2 { get; set; }
        public TeamScore Red3 { get; set; }

        public UpcomingMatch(int eventId, int upcomingMatchSeq)
        {
            this.eventId = eventId;
            this.upcomingMatchSeq = upcomingMatchSeq;
        }
    }

    /// <summary>
    /// Holds all point allocations over a list of matches for a team and also calculates a final score for the team.
    /// </summary>
    public class TeamScore
    {
        private readonly int team;
        private readonly string teamPicture;
        private readonly string teamDescription;
        private readonly List<PointAllocation> pa;

        public TeamScore(int team, string picture, string description, List<PointAllocation> points)
        {
 
            this.team = team;
            this.teamPicture = picture;
            this.teamDescription = HttpUtility.JavaScriptStringEncode(description);
            this.pa = points;
        }

        public int Team { get { return team; } }

        public string Picture { get { return teamPicture; } }

        public string Description { get { return teamDescription; } }

        #region Auto
        /// <summary>
        /// total count of success (high + low)/miss (high + low)
        /// 
        /// </summary>
        public string AutonomousOffensiveCountRatio
        {
            get
            {
                int scored = 0;
                int missed = 0;
                foreach (PointAllocation p in this.pa)
                {
                    scored += p.AutoScoredHigh.Count + p.AutoScoredLow.Count;
                    missed += p.AutoMissedHigh.Count + p.AutoMissedLow.Count;
                }

                return string.Format("{0}/{1}", scored, missed);
            }
        }

        /// <summary>
        /// total count of attempts high (sucess + miss) /low (success + miss)
        /// </summary>
        public string AutonomousOffensiveHiLoRatio
        { 
            get
            {
                int high = 0;
                int low = 0;
                foreach(PointAllocation p in this.pa)
                {
                    high += p.AutoScoredHigh.Count + p.AutoMissedHigh.Count;
                    low += p.AutoScoredLow.Count + p.AutoMissedLow.Count;
                }

                return string.Format("{0}/{1}", high, low);
            }
        }

        public string AutonomousDriveForward
        {
            get
            {
                bool drive = false;
                foreach (PointAllocation p in this.pa)
                {
                    if (p.AutonomousMoved.Count > 0)
                        drive = true;
                }
                return drive ? "D": string.Empty;
            }
        }

        public string AutonomousStartedInGoalbox
        {
            get
            {
                bool startedInGoalbox = false;
                foreach (PointAllocation p in this.pa)
                {
                    if (p.AutoStartedInGoalBox.Count > 0)
                        startedInGoalbox = true;
                }
                return startedInGoalbox ? "G" : string.Empty;
            }
        }

        public int AutonomousStartedInGoalBoxCount
        {
            get
            {
                int accum = 0;
                foreach(PointAllocation p in this.pa)
                    accum += p.AutoStartedInGoalBox.Count;
                return accum;
            }
        }

        public int AutonomousBlockedRobotCount
        {
            get
            {
                int accum = 0;
                foreach (PointAllocation p in this.pa)
                    accum += p.AutoBlockedRobot.Count;
                return accum;
            }
        }
      
        public int AutonomousBlockedShotCount
        {
            get
            {
                int accum = 0;
                foreach (PointAllocation p in this.pa)
                        accum += p.AutoBlockedShot.Count;
                return accum;
            }
        }

        #endregion Auto

        #region Teleop

        public string TeleopOffensiveCountRatio
        {
            get
            {
                int scored = 0;
                int missed = 0;
                foreach (PointAllocation p in this.pa)
                {
                    if (p.RobotMode == RobotMode.Teleop)
                    {
                        scored += p.ScoredHigh.Count + p.ScoredLow.Count;
                        missed += p.MissedHigh.Count + p.MissedLow.Count;
                    }
                }

                return string.Format("{0}/{1}", scored, missed);
            }
        }

        public string TeleopOffensiveInboundCountRatio
        {
            get
            {
                int success = 0;
                int missed = 0;
                foreach(PointAllocation p in this.pa)
                {
                    if (p.RobotMode == RobotMode.Teleop)
                    {
                        success += p.Inbound.Count;
                        missed += p.MissedInbound.Count;
                    }
                }

                return string.Format("{0}/{1}", success, missed);
            }
        }


        /// <summary>
        /// total count of attempts high (sucess + miss) /low (success + miss)
        /// </summary>
        public string TeleopOffensiveHiLoRatio
        {
            get
            {
                int high = 0;
                int low = 0;
                foreach (PointAllocation p in this.pa)
                {
                    if (p.RobotMode == RobotMode.Teleop)
                    {
                        high += p.ScoredHigh.Count + p.MissedHigh.Count;
                        low += p.ScoredLow.Count + p.MissedLow.Count;
                    }
                }

                return string.Format("{0}/{1}", high, low);
            }
        }

        public int TeleopTrussCount
        {
            get
            {
                int tries = 0;
                foreach (PointAllocation p in this.pa)
                {
                    if (p.RobotMode == RobotMode.Teleop)
                    {
                        tries += p.Truss.Count;
                    }
                }
                return tries;
            }
        }

        public int TeleopTrussCatchCount
        {
            get
            {
                int tries = 0;
                foreach (PointAllocation p in this.pa)
                {
                    if (p.RobotMode == RobotMode.Teleop)
                    {
                        tries += p.TrussCatch.Count;
                    }
                }
                return tries;
            }
        }

        public int TeleopPassesCount
        {
            get
            {
                int passes = 0;
                foreach (PointAllocation p in this.pa)
                {
                    if (p.RobotMode == RobotMode.Teleop)
                    {
                        passes += p.Pass.Count;
                    }
                }
                return passes;
            }
        }
        public int TeleopStartedInGoalBoxCount
        {
            get
            {
                int accum = 0;
                foreach (PointAllocation p in this.pa)
                    if (p.RobotMode == RobotMode.Teleop)
                        accum += p.StartedInGoalBox.Count;
                return accum;
            }
        }

        public int TeleopBlockedRobotCount
        {
            get
            {
                int accum = 0;
                foreach (PointAllocation p in this.pa)
                    if (p.RobotMode == RobotMode.Teleop)
                        accum += p.BlockedRobot.Count;
                return accum;
            }
        }

        public int TeleopBlockedShotCount
        {
            get
            {
                int accum = 0;
                foreach (PointAllocation p in this.pa)
                    if (p.RobotMode == RobotMode.Teleop)
                        accum += p.BlockedShot.Count;
                return accum;
            }
        }

        public int TeleopDefensiveCount
        {
            get
            {
                int accum = 0;
                foreach (PointAllocation p in this.pa)
                    accum += p.BlockedPass.Count + p.BlockedRobot.Count + p.BlockedShot.Count;
                return accum;
            }
        }
        #endregion Teleop

        #region KeyIndicators

        /// <summary>
        /// By default hold the accumlation of Inbound, MissedInboud and Pass counts but allows for an override for sorting purposes
        /// </summary>
        public int HelperRank { get; set; }

        public double imEffectivness
        {
            get
            {
                double perc = 0.0;
                double g = 0.0;
                double m = 0.0;
                foreach(PointAllocation p in this.pa)
                {
                    g += p.Inbound.Count + p.Pass.Count + (p.BlockedPass.Count + p.BlockedRobot.Count + p.BlockedShot.Count)/3;
                    m += p.MissedInbound.Count;
                }
                if (g + m > 0)
                    perc = Math.Round((g / (double)(g + m)) * g, 3);
                else
                    perc = 0.0;
                return perc;
            }
        }

        private int offense1Rank = -1;
        /// <summary>
        /// By default hold the accumlation of Truss and TrussCatch counts but allows for an override for sorting purposes
        /// </summary>
        public int Offense1Rank
        {
            get
            {
                if (offense1Rank > -1)
                    return offense1Rank;

                int accum = 0;
                foreach(PointAllocation p in this.pa)
                {
                    if (p.RobotMode == RobotMode.Teleop)
                    {
                        accum += p.TrussCatch.Count;
                        accum += p.Truss.Count;
                    }
                }
                return accum;
            }
            set { offense1Rank = value; }
        }

        public double gmEffectivness
        {
            get
            {
                double perc = 0.0;
                int g = 0;
                int m = 0;
                foreach(PointAllocation p in this.pa)
                {
                    g += p.ScoredHigh.Count + p.ScoredLow.Count;
                    m += p.MissedHigh.Count + p.MissedLow.Count;

                    g += p.AutonomousMoved.Count;
                    g += p.AutoScoredHigh.Count + p.AutoScoredLow.Count;
                    m += p.AutoMissedHigh.Count + p.AutoMissedLow.Count;
                }
                if (g + m > 0)
                    perc = Math.Round((g / (double)(g + m)) * g, 3);
                else
                    perc = 0.0;
                return perc;
            }
        }

        /// <summary>
        /// set by sorting method
        /// </summary>
        public int Offense2Rank { get; set; }

        #endregion KeyIndicators
    }

    /// <summary>
    /// Holds the point allocation, where the internal PointType tracks the count of each allocation for a match.
    /// This is largely meant to hold one team in one match.
    /// </summary>
    public class PointAllocation
    {
        private readonly int matchId;
        public int MatchId { get { return this.matchId; } }

        public RobotMode RobotMode { get; set; }

        public PointType GoalWasHot { get; set; }
        public PointType StartedInGoalBox { get; set; }
        public PointType AutoStartedInGoalBox { get; set; }
        public PointType ScoredLow { get; set; }
        public PointType ScoredHigh { get; set; }
        public PointType MissedLow { get; set; }
        public PointType MissedHigh { get; set; }
        public PointType AutoScoredLow { get; set; }
        public PointType AutoScoredHigh { get; set; }
        public PointType AutoMissedLow { get; set; }
        public PointType AutoMissedHigh { get; set; }
        public PointType Pass { get; set; }
        public PointType TrussCatch { get; set; }
        public PointType Truss { get; set; }
        public PointType AutonomousMoved { get; set; }
        public PointType BlockedShot { get; set; }
        public PointType AutoBlockedShot { get; set; }
        public PointType BlockedPass { get; set; }
        public PointType BlockedRobot { get; set; }
        public PointType AutoBlockedRobot { get; set; }
        public PointType Foul { get; set; }
        public PointType TechFoul { get; set; }
        public PointType Inbound { get; set; }
        public PointType MissedInbound {get; set; }

        public PointAllocation(int matchId)
        {
            this.matchId = matchId;
            GoalWasHot = new PointType("GoalWasHot", -1, 5); // use this one additively instead of as a multiplier (i.e. 5 extra points)
            StartedInGoalBox = new PointType("StartedInGoalBox", 0, 0);
            AutoStartedInGoalBox = new PointType("StartedInGoalBox", 0, 0);
            ScoredLow = new PointType("ScoredLow", 1, 1);
            ScoredHigh = new PointType("ScoredHigh", 2, 10);
            MissedLow = new PointType("MissedLow", 3, 0);
            MissedHigh = new PointType("MissedHigh", 4, 0);
            AutoScoredLow = new PointType("ScoredLow", 1, 1);
            AutoScoredHigh = new PointType("ScoredHigh", 2, 10);
            AutoMissedLow = new PointType("MissedLow", 3, 0);
            AutoMissedHigh = new PointType("MissedHigh", 4, 0);
            Pass = new PointType("Pass", 5, 0);
            TrussCatch = new PointType("TrussCatch", 6, 10);
            Truss = new PointType("Truss", 7, 10);
            AutonomousMoved = new PointType("AutonomousMoved", 8, 5);
            BlockedShot = new PointType("BlockedShot", 9, 1);
            AutoBlockedShot = new PointType("BlockedShot", 9, 1);
            BlockedPass = new PointType("BlockedPass", 10, 1);
            BlockedRobot = new PointType("BlockedRobot", 11, 1);
            AutoBlockedRobot = new PointType("BlockedRobot", 11, 1);
            Foul = new PointType("Foul", 12, 0);
            TechFoul = new PointType("TechFoul", 13, 0);
            Inbound = new PointType("Inbound", 14, 1);
            MissedInbound = new PointType("MissedInbound", 15, 0);
        }
    }

    /// <summary>
    /// Holds the event type and the count for it
    /// </summary>
    public class PointType
    {
        private readonly string name;
        private readonly int robotEventType;
        private readonly double weight;
        public string Name { get { return name; } }
        public int RobotEventType { get { return robotEventType; } }
        public double Weight { get { return weight; } }
        public int Count { get; set; }
        public PointType(string name, int robotEventType, double weight)
        {
            this.name = name;
            this.robotEventType = robotEventType;
            this.weight = weight;
        }
    }

    /// <summary>
    ///  This is the output type of method BuildScorableList and the input type of method BuildTeamScores
    /// </summary>
    public class ScoringList
    {
        public int Team_Number {get; set; }
        public string Picture { get; set; }
        public string Team_Description { get; set; }
        public RobotEventType RobotEventType { get; set; }
        public bool GoalWasHot { get; set; }
        public RobotMode RobotMode { get; set; }
        public int Match_Seq { get; set; }
    }
}