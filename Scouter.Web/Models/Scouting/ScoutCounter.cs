using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Scouter.Web.Models.Scouting
{
	//include ...Count in name
    public class ScoutCounter
	{
        public int TotesStacked { get; set; }
        public int RightToteMoved { get; set; }
        public int CenterToteMoved { get; set; }
        public int LeftToteMoved { get; set; }
        public int YellowTotesMovedToStep { get; set; }

        public Boolean LeftContainerFromStep { get; set; } //SAME counters for A and T
        public Boolean LeftCenterContainerFromStep { get; set; }
        public Boolean RightCenterContainerFromStep { get; set; }
        public Boolean RightContainerFromStep { get; set; }

        public int RightContainerMoved { get; set; }
        public int CenterContainerMoved { get; set; }
        public int LeftContainerMoved { get; set; }

        public Boolean AutonomousMoved { get; set; }
        public Boolean NoAutonomous { get; set; }
        public Boolean AutoResultClutter { get; set; }
        public int Foul { get; set; } // different foul-counters between A and T

        public int RightChutePickUp { get; set; }
        public int LeftChutePickUp { get; set; }
        public int GroundPickUp { get; set; }
        public int DriveOverPlatform { get; set; }
        public int HumanPlayerShoots { get; set; }
        public int HumanPlayerFails { get; set; }

        public int OrientContainer { get; set; }
        public int OrientTote { get; set; }
        public int ClearContainer { get; set; }
        public int ClearTote { get; set; }
        public int ClearLitter { get; set; }

        public int LitterPlacedAtHeight { get; set; }
        public int BulldozeLitterToLandfill { get; set; }

        public int InitialStackHeight { get; set; } 
        public int TotesAddedToStack { get; set; }
        public Boolean ContainerAddedToStack { get; set; }        
	}
}