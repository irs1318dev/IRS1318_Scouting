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
        #endregion Auto

        #region Teleop
        public string StackEfficiancy
        {
            get
            {
                double totesPickedUp = 0.0;
                double totalStacked = 0.0;
                foreach (PointAllocation p in this.pa)
                {
                    if (p.RobotMode == RobotMode.Teleop && (p.GroundPickUp.Count > 0 || p.LeftChutePickUp.Count > 0 || p.RightChutePickUp.Count > 0))
                        totesPickedUp++;

                    totalStacked += p.TotesStacked.Count;
                }
                return string.Format("{0}/{1}", totalStacked, totesPickedUp);
            }
        }

        public string StacksInformation
        {
            get
            {
                double initialStackHeight = 0.0;
                double totesAddedThisTime = 0.0;
                double isContainerOnTop = 0.0;
                double isLitterInContainer = 0.0;
                foreach(PointAllocation p in this.pa)
                {
                    if (p.RobotMode == RobotMode.Teleop & (p.TotesStacked.Count > 0))
                    {
                        initialStackHeight += p.StartingHeight.Count;
                        totesAddedThisTime += p.NumTotesAdded.Count;
                        isContainerOnTop += p.IsContainerAdded.Count;
                        isLitterInContainer += p.IsLitterAdded.Count;
                    }
                }
                return string.Format("S{0}A{1}C{2}L{3}", initialStackHeight, totesAddedThisTime, isContainerOnTop, isLitterInContainer);
            }
        }

        public string TotesPickup
        {
            get
            {
                double leftChutePickup = 0.0;
                double rightChutePickup = 0.0;
                double groundPickup = 0.0;
                foreach(PointAllocation p in this.pa)
                {
                    if (p.RobotMode == RobotMode.Teleop & p.LeftChutePickUp.Count > 0)
                        leftChutePickup += p.LeftChutePickUp.Count;
                    
                    if (p.RobotMode == RobotMode.Teleop & p.RightChutePickUp.Count > 0)
                        rightChutePickup += p.RightChutePickUp.Count;

                    if (p.RobotMode == RobotMode.Teleop & p.GroundPickUp.Count > 0)
                        groundPickup += p.GroundPickUp.Count;
                }
                return string.Format("L{0}R{1}G{2}", leftChutePickup, rightChutePickup, groundPickup)
            }
        }

        public string LitterMovedToLandfill
        {
            get
            {
                double litterMovedToLandfill = 0.0;
                foreach(PointAllocation p in this.pa)
                {
                    if (p.RobotMode == RobotMode.Teleop && p.BulldozeLitterToLandfill.Count > 0)
                        litterMovedToLandfill += p.BulldozeLitterToLandfill.Count;
                }
                return string.Format("{0}", litterMovedToLandfill);
            }
        }

        public string Orientation
        {
            get
            {
                double orientContainer = 0.0;
                double orientTote = 0.0;
                foreach(PointAllocation p in this.pa)
                {
                    if (p.RobotMode == RobotMode.Teleop && p.OrientContainer.Count > 0)
                        orientContainer += p.OrientContainer.Count;

                    if (p.RobotMode == RobotMode.Teleop && p.OrientTote.Count > 0)
                        orientTote += p.OrientTote.Count;
                }
                return string.Format("C{0}T{1}", orientContainer, orientTote);
            }
        }

        public string ClearedAway
        {
            get
            {
                double clearedContainer = 0.0;
                double clearedTote = 0.0;
                double clearedLitter = 0.0;
                foreach(PointAllocation p in this.pa)
                {
                    if (p.RobotMode == RobotMode.Teleop && p.ClearContainer.Count > 0)
                        clearedContainer += p.ClearContainer.Count;
                    if (p.RobotMode == RobotMode.Teleop && p.ClearTote.Count > 0)
                        clearedTote += p.ClearTote.Count;
                    if (p.RobotMode == RobotMode.Teleop && p.ClearLitter.Count > 0)
                        clearedLitter += p.ClearLitter.Count;
                }
                return string.Format("C{0}T{1}L{2}", clearedContainer, clearedTote, clearedLitter);
            }
        }

        public string ContainersTakenFromStep
        {
            get
            {
                double takenLeft = 0.0;
                double takenCenterLeft = 0.0;
                double takenCenterRight = 0.0;
                double takenRight = 0.0;
                foreach(PointAllocation p in this.pa)
                {
                    if (p.RobotMode == RobotMode.Teleop && p.LeftContainerFromStep.Count > 0)
                        takenLeft += p.LeftContainerFromStep.Count;
                    else if (p.RobotMode == RobotMode.Teleop && p.LeftCenterContainerFromStep.Count > 0)
                        takenCenterLeft += p.LeftCenterContainerFromStep.Count;
                    else if (p.RobotMode == RobotMode.Teleop && p.RightCenterContainerFromStep.Count > 0)
                        takenCenterRight += p.RightCenterContainerFromStep.Count;
                    else if (p.RobotMode == RobotMode.Teleop && p.RightContainerFromStep.Count > 0)
                        takenRight += p.RightContainerFromStep.Count;
                }
                return string.Format("L{0}CL{1}CR{2}R{3}", takenLeft, takenCenterLeft, takenCenterRight, takenRight);
            }
        }
        #endregion Teleop

        #region KeyIndicators

        /// <summary>
        /// By default hold the accumlation of Inbound, MissedInboud and Pass counts but allows for an override for sorting purposes
        /// </summary>
        //public int HelperRank { get; set; }

        //public double imEffectivness
        //{
        //    get
        //    {
        //        double perc = 0.0;
        //        double g = 0.0;
        //        double m = 0.0;
        //        foreach(PointAllocation p in this.pa)
        //        {
        //            g += p. + p.Pass.Count + (p.BlockedPass.Count + p.BlockedRobot.Count + p.BlockedShot.Count)/3;
        //            m += p.MissedInbound.Count;
        //        }
        //        if (g + m > 0)
        //            perc = Math.Round((g / (double)(g + m)) * g, 3);
        //        else
        //            perc = 0.0;
        //        return perc;
        //    }
        //}

        //private int offense1Rank = -1;
        ///// <summary>
        ///// By default hold the accumlation of Truss and TrussCatch counts but allows for an override for sorting purposes
        ///// </summary>
        //public int Offense1Rank
        //{
        //    get
        //    {
        //        if (offense1Rank > -1)
        //            return offense1Rank;

        //        int accum = 0;
        //        foreach(PointAllocation p in this.pa)
        //        {
        //            if (p.RobotMode == RobotMode.Teleop)
        //            {
        //                accum += p.TrussCatch.Count;
        //                accum += p.Truss.Count;
        //            }
        //        }
        //        return accum;
        //    }
        //    set { offense1Rank = value; }
        //}

        //public double gmEffectivness
        //{
        //    get
        //    {
        //        double perc = 0.0;
        //        int g = 0;
        //        int m = 0;
        //        foreach(PointAllocation p in this.pa)
        //        {
        //            g += p.ScoredHigh.Count + p.ScoredLow.Count;
        //            m += p.MissedHigh.Count + p.MissedLow.Count;

        //            g += p.AutonomousMoved.Count;
        //            g += p.AutoScoredHigh.Count + p.AutoScoredLow.Count;
        //            m += p.AutoMissedHigh.Count + p.AutoMissedLow.Count;
        //        }
        //        if (g + m > 0)
        //            perc = Math.Round((g / (double)(g + m)) * g, 3);
        //        else
        //            perc = 0.0;
        //        return perc;
        //    }
        //}

        ///// <summary>
        ///// set by sorting method
        ///// </summary>
        //public int Offense2Rank { get; set; }

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

        // Stack Events
        public PointType StartingHeight { get; set; }
        public PointType NumTotesAdded { get; set; }
        public PointType IsContainerAdded { get; set; }
        public PointType IsLitterAdded { get; set; }

        public PointAllocation(int matchId)
        {
            this.matchId = matchId;
 
            // Robot Events
            BulldozeLitterToLandfill = new PointType("BulldozeLitterToLandfill", (int)RobotEventType.BulldozeLitterToLandfill, 0);
            ClearLitter = new PointType("ClearLitter", (int)RobotEventType.ClearLitter, 0);
            ClearTote = new PointType("ClearTote", (int)RobotEventType.ClearTote, 0);
            ClearContainer = new PointType("ClearContainer", (int)RobotEventType.ClearContainer, 0);
            CoopertitionToteOne = new PointType("CoopertitionToteOne", (int)RobotEventType.CoopertitionToteOne, 0);
            CoopertitionToteTwo = new PointType("CoopertitionToteTwo", (int)RobotEventType.CoopertitionToteTwo, 0);
            CoopertitionToteThree = new PointType("CoopertitionToteThree", (int)RobotEventType.CoopertitionToteThree, 0);
            Foul = new PointType("Foul", (int)RobotEventType.Foul, 0);
            GroundPickUp = new PointType("GroundPickup", (int)RobotEventType.GroundPickUp, 0);
            LeftCenterContainerFromStep = new PointType("LeftCenterContainerFromStep", (int)RobotEventType.LeftCenterContainerFromStep, 0);
            LeftChutePickUp = new PointType("LeftChutePickUp", (int)RobotEventType.LeftChutePickUp, 0);
            LeftContainerFromStep = new PointType("LeftContainerFromStep", (int)RobotEventType.LeftContainerFromStep, 0);
            OrientTote = new PointType("OrientTote", (int)RobotEventType.OrientTote, 0);
            OrientContainer = new PointType("ClearContainer", (int)RobotEventType.OrientContainer, 0);
            RightCenterContainerFromStep = new PointType("", (int)RobotEventType.RightCenterContainerFromStep, 0);
            RightChutePickUp = new PointType("RightChutePickUp", (int)RobotEventType.RightChutePickUp, 0);
            AutoAttemptClutter = new PointType("AutoAttemptClutter", (int)RobotEventType.AutoAttemptClutter, 0);
            AutonomousMoved = new PointType("AutonomousMoved", (int)RobotEventType.AutonomousMoved, 0);
            CenterContainerMoved = new PointType("CenterContainerMoved", (int)RobotEventType.CenterContainerMoved, 0);
            CenterToteMoved = new PointType("CenterToteMoved", (int)RobotEventType.CenterToteMoved, 0);
            LeftContainerMoved = new PointType("LeftContainerMoved", (int)RobotEventType.LeftContainerMoved, 0);
            LeftToteMoved = new PointType("LeftToteMoved", (int)RobotEventType.LeftToteMoved, 0);
            NoAutonomous = new PointType("NoAutonomous", (int)RobotEventType.NoAutonomous, 0);
            RightContainerMoved = new PointType("RightContainerMoved", (int)RobotEventType.RightContainerMoved, 0);
            RightToteMoved = new PointType("RightToteMoved", (int)RobotEventType.RightToteMoved, 0);
            TotesStacked = new PointType("TotesStacked", (int)RobotEventType.TotesStacked, 0);

            // Human Events
            Failure = new PointType("Failure", (int)HumanEventType.Failure, 0);
            ThrowPastOpponentLandfill = new PointType("ThrowPastOpponentLandfill", (int)HumanEventType.ThrowPastOpponentLandfill, 0);
            ThrowToOpponentLandfill = new PointType("", (int)HumanEventType.ThrowToOpponentLandfill, 0);
            ThrowToOwnLandfill = new PointType("ThrowToOwnLandfill", (int)HumanEventType.ThrowToOwnLandfill, 0);

            // Stack Events
            StartingHeight = new PointType("StartingHeight", 1, 0);
            NumTotesAdded = new PointType("NumTotesAdded", 2, 0);
            IsContainerAdded = new PointType("IsContainerAdded", 3, 0);
            IsLitterAdded = new PointType("IsLitterAdded", 4, 0);
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
        public HumanEventType HumanEventType { get; set; }
        public RobotMode RobotMode { get; set; }
        public int Match_Seq { get; set; }
    }
}