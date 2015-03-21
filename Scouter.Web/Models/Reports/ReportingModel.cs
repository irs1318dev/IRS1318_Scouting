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

        public string TotesMoved
        {
            get
            {
                double leftTotesMoved = 0.0;
                double centerTotesMoved = 0.0;
                double rightTotesMoved = 0.0;
                foreach (PointAllocation p in this.pa)
                {
                    if (p.RobotMode == RobotMode.Autonomous)
                    {
                        if (p.LeftToteMoved.Count > 0)
                            leftTotesMoved += p.LeftToteMoved.Count;
                        if (p.CenterToteMoved.Count > 0)
                            centerTotesMoved += p.CenterContainerMoved.Count;
                        if (p.RightToteMoved.Count > 0)
                            rightTotesMoved += p.RightToteMoved.Count;
                    }
                }
                return string.Format("{0} : {1} : {2}", leftTotesMoved, centerTotesMoved, rightTotesMoved);
            }
        }

        public string ContainersMoved
        {
            get
            {
                double l = 0.0;
                double c = 0.0;
                double r = 0.0;
                foreach (PointAllocation p in this.pa)
                {
                    if (p.RobotMode == RobotMode.Autonomous)
                    {
                        if (p.LeftContainerMoved.Count > 0)
                            l += p.LeftContainerMoved.Count;
                        if (p.CenterContainerMoved.Count > 0)
                            c += p.CenterContainerMoved.Count;
                        if (p.RightContainerMoved.Count > 0)
                            r += p.RightContainerMoved.Count;
                    }
                }
                return string.Format("{0} : {1} : {2}", l, c, r);
            }
        }
        #endregion Auto

        #region Teleop
        //
        // Robot Key Indicators
        //
        public string TotesPickup
        {
            get
            {
                double leftChutePickup = 0.0;
                double rightChutePickup = 0.0;
                double groundPickup = 0.0;
                foreach (PointAllocation p in this.pa)
                {
                    if (p.RobotMode == RobotMode.Teleop)
                    {
                        if (p.LeftChutePickUp.Count > 0)
                            leftChutePickup += p.LeftChutePickUp.Count;
                        if (p.RightChutePickUp.Count > 0)
                            rightChutePickup += p.RightChutePickUp.Count;
                        if (p.GroundPickUp.Count > 0)
                            groundPickup += p.GroundPickUp.Count;
                    }
                }
                return string.Format("{0} : {1} : {2}", leftChutePickup, rightChutePickup, groundPickup);
            }
        }

        public string ContainersPickup
        {
            get
            {
                double r = 0.0;
                double rc = 0.0;
                double lc = 0.0;
                double l = 0.0;
                foreach(PointAllocation p in this.pa)
                {
                    if (p.RobotMode == RobotMode.Teleop)
                    {
                        if (p.RightContainerFromStep.Count > 0)
                            r += p.RightContainerFromStep.Count;
                        if (p.RightCenterContainerFromStep.Count > 0)
                            rc += p.RightCenterContainerFromStep.Count;
                        if (p.LeftCenterContainerFromStep.Count > 0)
                            lc += p.LeftCenterContainerFromStep.Count;
                        if (p.LeftContainerFromStep.Count > 0)
                            l += p.LeftContainerFromStep.Count;
                    }
                }
                return string.Format("{0} : {1} : {2} : {3}", r, rc, lc, l);
            }
        }

        public string StackEfficiancy
        {
            get
            {
                double totesPickedUp = 0.0;
                double totalStacked = 0.0;
                foreach (PointAllocation p in this.pa)
                {
                    if (p.RobotMode == RobotMode.Teleop)
                    {
                        if (p.GroundPickUp.Count > 0 || p.LeftChutePickUp.Count > 0 || p.RightChutePickUp.Count > 0)
                            totesPickedUp += p.GroundPickUp.Count + p.LeftChutePickUp.Count + p.RightChutePickUp.Count;
                        if (p.TotesStacked.Count > 0)
                            totalStacked += p.TotesStacked.Count;
                    }
                }
                return string.Format("{0}/{1}", totalStacked, totesPickedUp);
            }
        }

        //
        // Human Key Indicators
        //
        public string HumanPlayerConflict
        {
            get
            {
                double n = 0.0;
                double d = 0.0;
                foreach(PointAllocation p in this.pa)
                {
                    if (p.ThrowShortOfOwnLandfill.Count > 0 || p.ThrowToOwnLandfill.Count > 0)
                        n += p.ThrowShortOfOwnLandfill.Count + p.ThrowToOwnLandfill.Count;
                    if (p.ThrowShortOfOwnLandfill.Count > 0 || p.ThrowToOwnLandfill.Count > 0 || p.ThrowToStep.Count > 0 || p.ThrowToOpponentLandfill.Count > 0 || p.ThrowPastOpponentLandfill.Count > 0 || p.Failure.Count > 0)
                        d += p.ThrowShortOfOwnLandfill.Count + p.ThrowToOwnLandfill.Count + p.ThrowToStep.Count + p.ThrowToOpponentLandfill.Count + p.ThrowPastOpponentLandfill.Count + p.Failure.Count;
                }
                return string.Format("{0} : {1}", n, d);
            }
        }
        #endregion Teleop
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
        
        // Robot Events
        public PointType BulldozeLitterToLandfill { get; set; }
        public PointType ClearLitter { get; set; }
        public PointType ClearTote { get; set; }
        public PointType ClearContainer { get; set; }
        public PointType CoopertitionToteOne { get; set; }
        public PointType CoopertitionToteTwo { get; set; }
        public PointType CoopertitionToteThree { get; set; }
        public PointType Foul { get; set; }
        public PointType GroundPickUp { get; set; }
        public PointType LeftCenterContainerFromStep { get; set; }
        public PointType LeftChutePickUp { get; set; }
        public PointType LeftContainerFromStep { get; set; }
        public PointType OrientTote { get; set; }
        public PointType OrientContainer { get; set; }
        public PointType RightCenterContainerFromStep { get; set; }
        public PointType RightChutePickUp { get; set; }
        public PointType RightContainerFromStep { get; set; }
        public PointType AutoAttemptClutter { get; set; }
        public PointType AutonomousMoved { get; set; }
        public PointType CenterContainerMoved { get; set; }
        public PointType CenterToteMoved { get; set; }
        public PointType LeftContainerMoved { get; set; }
        public PointType LeftToteMoved { get; set; }
        public PointType NoAutonomous { get; set; }
        public PointType RightContainerMoved { get; set; }
        public PointType RightToteMoved { get; set; }
        public PointType TotesStacked { get; set; }

        // Human Events
        public PointType Failure { get; set; }
        public PointType ThrowPastOpponentLandfill { get; set; }
        public PointType ThrowToOpponentLandfill { get; set; }
        public PointType ThrowToOwnLandfill { get; set; }
        public PointType ThrowShortOfOwnLandfill { get; set; }
        public PointType ThrowToStep { get; set; }

        // Stack Events
        public PointType StartingHeight { get; set; }
        public PointType NumTotesAdded { get; set; }
        public PointType IsContainerAdded { get; set; }
        public PointType IsLitterAdded { get; set; }

        public PointAllocation(int matchId)
        {
            this.matchId = matchId;
 
            // Robot Events
            BulldozeLitterToLandfill = new PointType("BulldozeLitterToLandfill", (int)RobotEventType.BulldozeLitterToLandfill, 1);
            ClearLitter = new PointType("ClearLitter", (int)RobotEventType.ClearLitter, 1);
            ClearTote = new PointType("ClearTote", (int)RobotEventType.ClearTote, 1);
            ClearContainer = new PointType("ClearContainer", (int)RobotEventType.ClearContainer, 1);
            CoopertitionToteOne = new PointType("CoopertitionToteOne", (int)RobotEventType.CoopertitionToteOne, 1);
            CoopertitionToteTwo = new PointType("CoopertitionToteTwo", (int)RobotEventType.CoopertitionToteTwo, 1);
            CoopertitionToteThree = new PointType("CoopertitionToteThree", (int)RobotEventType.CoopertitionToteThree, 1);
            Foul = new PointType("Foul", (int)RobotEventType.Foul, 1);
            GroundPickUp = new PointType("GroundPickUp", (int)RobotEventType.GroundPickUp, 1);
            LeftCenterContainerFromStep = new PointType("LeftCenterContainerFromStep", (int)RobotEventType.LeftCenterContainerFromStep, 1);
            LeftChutePickUp = new PointType("LeftChutePickUp", (int)RobotEventType.LeftChutePickUp, 1);
            LeftContainerFromStep = new PointType("LeftContainerFromStep", (int)RobotEventType.LeftContainerFromStep, 1);
            OrientTote = new PointType("OrientTote", (int)RobotEventType.OrientTote, 1);
            OrientContainer = new PointType("OrientContainer", (int)RobotEventType.OrientContainer, 1);
            RightCenterContainerFromStep = new PointType("RightCenterContainerFromStep", (int)RobotEventType.RightCenterContainerFromStep, 1);
            RightContainerFromStep = new PointType("RightContainerFromStep", (int)RobotEventType.RightContainerFromStep, 1);
            RightChutePickUp = new PointType("RightChutePickUp", (int)RobotEventType.RightChutePickUp, 1);
            AutoAttemptClutter = new PointType("AutoAttemptClutter", (int)RobotEventType.AutoAttemptClutter, 1);
            AutonomousMoved = new PointType("AutonomousMoved", (int)RobotEventType.AutonomousMoved, 1);
            CenterContainerMoved = new PointType("CenterContainerMoved", (int)RobotEventType.CenterContainerMoved, 1);
            CenterToteMoved = new PointType("CenterToteMoved", (int)RobotEventType.CenterToteMoved, 1);
            LeftContainerMoved = new PointType("LeftContainerMoved", (int)RobotEventType.LeftContainerMoved, 1);
            LeftToteMoved = new PointType("LeftToteMoved", (int)RobotEventType.LeftToteMoved, 1);
            NoAutonomous = new PointType("NoAutonomous", (int)RobotEventType.NoAutonomous, 1);
            RightContainerMoved = new PointType("RightContainerMoved", (int)RobotEventType.RightContainerMoved, 1);
            RightToteMoved = new PointType("RightToteMoved", (int)RobotEventType.RightToteMoved, 1);
            TotesStacked = new PointType("TotesStacked", (int)RobotEventType.TotesStacked, 1);

            // Human Events
            Failure = new PointType("Failure", (int)HumanEventType.Failure, 1);
            ThrowPastOpponentLandfill = new PointType("ThrowPastOpponentLandfill", (int)HumanEventType.ThrowPastOpponentLandfill, 1);
            ThrowToOpponentLandfill = new PointType("ThrowToOpponentLandfill", (int)HumanEventType.ThrowToOpponentLandfill, 1);
            ThrowToOwnLandfill = new PointType("ThrowToOwnLandfill", (int)HumanEventType.ThrowToOwnLandfill, 1);
            ThrowToStep = new PointType("ThrowToStep", (int)HumanEventType.ThrowToStep, 1);
            ThrowShortOfOwnLandfill = new PointType("ThrowShortOfOwnLandfill", (int)HumanEventType.ThrowShortOfOwnLandfill, 6);

            // Stack Events
            StartingHeight = new PointType("StartingHeight", 1, 1);
            NumTotesAdded = new PointType("NumTotesAdded", 2, 2);
            IsContainerAdded = new PointType("IsContainerAdded", 3, 4);
            IsLitterAdded = new PointType("IsLitterAdded", 4, 6);
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
        public string EventType { get; set; }
        public RobotMode RobotMode { get; set; }
        public int Match_Seq { get; set; }
        public int NumTotesAdded { get; set; }
        public bool IsContainerAdded { get; set; }
        public bool IsLitterAdded { get; set; }
        public int StartingHeight { get; set; }
    }
}