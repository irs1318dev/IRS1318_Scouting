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

        public int ContainersFromStep { get; set; } //same counter in A and T
        public int RightContainerMoved { get; set; }
        public int CenterContainerMoved { get; set; }
        public int LeftContainerMoved { get; set; }

        public Boolean AutonomousMoved { get; set; }
        public Boolean NoAutonomous { get; set; }
        public Boolean AutoAttemptClutter { get; set; }
        public int AutoFoul { get; set; } // different foul-counters between A and T

        public int RightChutePickUp { get; set; }
        public int LeftChutePickUp { get; set; }
        public int GroundPickUp { get; set; }
        public int DriveOverPlatform { get; set; }
        public int HumanPlayerShoots { get; set; }
        public int HumanPlayerFails { get; set; }

        //assist on step
        public int OrientContainer { get; set; }
        public int OrientTote { get; set; }
        public int ClearContainer { get; set; }
        public int ClearTote { get; set; }
        public int ClearLitter { get; set; }

        public int LitterPlacedAtHeight { get; set; }
        public int BulldozeLitterToLandfill { get; set; }
        public int TeleopFoul { get; set; }

        public IObservable<StackData> StacksList {get; set;}
        public IObservable<int> ContainerPlacedList { get; set; }
        
       
	}

    public class StackData
    {
        private int StartingHeight;
        private int NumTotes;

        public StackData(int height, int totes)
        {
            this.StartingHeight = height;
            this.NumTotes = totes;
        }

        public int getStartingHeight()
        {
            return this.StartingHeight;
        }
        public int getNumTotes()
        {
            return this.NumTotes;
        }

        public void setHeight(int height)
        {
            this.StartingHeight = height;
        }
        public void setNumTotes(int totes)
        {
            this.NumTotes = totes;
        }
        public void set(int height, int totes)
        {
            this.StartingHeight = height;
            this.NumTotes = totes;
        }
    }
}