using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Scouter.Web.Models.Scouting
{
	//include ...Count in name... nvm
    public class ScoutCounter
	{
        public Boolean TotesStacked { get; set; }
        public Boolean RightToteMoved { get; set; }
        public Boolean CenterToteMoved { get; set; }
        public Boolean LeftToteMoved { get; set; }

        //should change into four events
        //public int YellowTotesMovedToStep { get; set; }
        public Boolean CoopertitionToteOne { get; set; }
        public Boolean CoopertitionToteTwo { get; set; }
        public Boolean CoopertitionToteThree { get; set; }
        public Boolean CoopertitionToteFour { get; set; }

        public Boolean LeftContainerFromStep { get; set; } //SAME counters for A and T
        public Boolean LeftCenterContainerFromStep { get; set; }
        public Boolean RightCenterContainerFromStep { get; set; }
        public Boolean RightContainerFromStep { get; set; }
        public Boolean CenterContainerMoved { get; set; }
        public Boolean LeftContainerMoved { get; set; }


        public Boolean RightContainerMoved { get; set; }
        public Boolean AutonomousMoved { get; set; }
        public Boolean NoAutonomous { get; set; }
        public Boolean AutoAttemptClutter { get; set; }
        public int Foul { get; set; } // different foul-counters between A and T

        public int RightChutePickUp { get; set; }
        public int LeftChutePickUp { get; set; }
        public int GroundPickUp { get; set; }
        //public int DriveOverPlatform { get; set; }

        public int OrientContainer { get; set; }
        public int OrientTote { get; set; }
        public int ClearContainer { get; set; }
        public int ClearTote { get; set; }
        public int ClearLitter { get; set; }

        public int BulldozeLitterToLandfill { get; set; }
	}
}