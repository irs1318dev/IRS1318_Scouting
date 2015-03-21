using Scouter.Data;
using Scouter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Scouter.Dump
{
    class Program
    {
        static void Main(string[] args)
        {
            int compId = 1;
            int currentMatch = 86;
            DataContext context = new DataContext();
            var Teams = context.Teams.ToList();
            Dictionary<Team, List<FRCMatch>> teamMatch = new Dictionary<Team, List<FRCMatch>>();
            Dictionary<Team, Score> teamScore = new Dictionary<Team, Score>();

            Console.WriteLine("Finding Teams");

            foreach(var team in context.Teams)
            {
                teamMatch.Add(team, new List<FRCMatch>());
                teamScore.Add(team, new Score());
            }

            Console.WriteLine("Finding Matches");

            var matches = context.FRCMatches.ToList();
            foreach(FRCMatch match in matches)
            {
                if (match.FRCEvent.Id != compId)
                    continue;
                if (match.SequenceNumber > currentMatch)
                    continue;

                teamMatch[match.RedAlliance.Team1].Add(match);
                teamMatch[match.RedAlliance.Team2].Add(match);
                teamMatch[match.RedAlliance.Team3].Add(match);
                teamMatch[match.BlueAlliance.Team1].Add(match);
                teamMatch[match.BlueAlliance.Team2].Add(match);
                teamMatch[match.BlueAlliance.Team3].Add(match);
            }

            Console.WriteLine("Finding last 4 matches of each team");

            foreach(var pair in teamMatch)
            {
                while(pair.Value.Count > 4)
                {
                    pair.Value.RemoveAt(0);
                }
            }


            Console.WriteLine("Counting events");

            var Events = context.RobotEvents.ToList();

            foreach(var evnt in Events)
            {
                bool useEvent = false;

                foreach(var match in teamMatch[evnt.Team])
                {
                    if(match == evnt.Match)
                    {
                        useEvent = true;
                        break;
                    }
                }

                if (!useEvent)
                    continue;

                Score score = teamScore[evnt.Team];

                switch (evnt.RobotEventType)
                {
                    case RobotEventType.TotesStacked:
                        if (evnt.RobotMode == RobotMode.Autonomous)
                            score.AutoStacked += 20;
                        break;
                    case RobotEventType.RightToteMoved:
                        if (evnt.RobotMode == RobotMode.Autonomous)
                            score.YellowToAuto += 2;
                        break;
                    case RobotEventType.CenterToteMoved:
                        if (evnt.RobotMode == RobotMode.Autonomous)
                            score.YellowToAuto += 2;
                        break;
                    case RobotEventType.LeftToteMoved:
                        if (evnt.RobotMode == RobotMode.Autonomous)
                            score.YellowToAuto += 2;
                        break;
                    case RobotEventType.CoopertitionToteOne:
                        score.StkHtYellowTotesOnStep += 4;
                        break;
                    case RobotEventType.CoopertitionToteTwo:
                        score.StkHtYellowTotesOnStep += 4;
                        break;
                    case RobotEventType.CoopertitionToteThree:
                        score.StkHtYellowTotesOnStep += 4;
                        break;
                    case RobotEventType.CoopertitionToteFour:
                        score.StkHtYellowTotesOnStep += 4;
                        break;
                    case RobotEventType.RightContainerFromStep:
                        if (evnt.RobotMode == RobotMode.Autonomous)
                            score.StepContainersToAllies += 8;
                        else if (evnt.RobotMode == RobotMode.Teleop)
                            score.StepRecycleToAllies += 8;
                        break;
                    case RobotEventType.RightCenterContainerFromStep:
                        if (evnt.RobotMode == RobotMode.Autonomous)
                            score.StepContainersToAllies += 8;
                        else if (evnt.RobotMode == RobotMode.Teleop)
                            score.StepRecycleToAllies += 8;
                        break;
                    case RobotEventType.LeftCenterContainerFromStep:
                        if (evnt.RobotMode == RobotMode.Autonomous)
                            score.StepContainersToAllies += 8;
                        else if (evnt.RobotMode == RobotMode.Teleop)
                            score.StepRecycleToAllies += 8;
                        break;
                    case RobotEventType.LeftContainerFromStep:
                        if (evnt.RobotMode == RobotMode.Autonomous)
                            score.StepContainersToAllies += 8;
                        else if (evnt.RobotMode == RobotMode.Teleop)
                            score.StepRecycleToAllies += 8;
                        break;
                    case RobotEventType.RightContainerMoved:
                        if (evnt.RobotMode == RobotMode.Autonomous)
                            score.MovedToAuto += 3;
                        break;
                    case RobotEventType.CenterContainerMoved:
                        if (evnt.RobotMode == RobotMode.Autonomous)
                            score.MovedToAuto += 3;
                        break;
                    case RobotEventType.LeftContainerMoved:
                        if (evnt.RobotMode == RobotMode.Autonomous)
                            score.MovedToAuto += 3;
                        break;
                    case RobotEventType.AutonomousMoved:
                        if (evnt.RobotMode == RobotMode.Autonomous)
                            score.RobotMovedToAuto += 2;
                        break;
                    case RobotEventType.NoAutonomous:
                        score.HasAuto = true;
                        break;
                    case RobotEventType.Foul:
                        score.Fouls -= 6;
                        break;
                    case RobotEventType.RightChutePickUp:
                        score.FeederPickup += 1;
                        break;
                    case RobotEventType.LeftChutePickUp:
                        score.FeederPickup += 1;
                        break;
                    case RobotEventType.GroundPickUp:
                        score.GroundPickup += 1;
                        break;
                    case RobotEventType.OrientContainer:
                        score.OrientRecycle += 4;
                        break;
                    case RobotEventType.OrientTote:
                        score.OrientTotes += 2;
                        break;
                    case RobotEventType.ClearContainer:
                        score.ClearRecycle += 2;
                        break;
                    case RobotEventType.ClearTote:
                        score.ClearTote += 2;
                        break;
                    case RobotEventType.ClearLitter:
                        score.ClearLitter += 2;
                        break;
                    default:
                        break;
                }
                teamScore[evnt.Team] = score;
            }


            Console.WriteLine("Counting stack Events");
            var StackEvents = context.StackEvents.ToList();

            foreach (var evnt in StackEvents)
            {
                bool useEvent = false;

                foreach (var match in teamMatch[evnt.Team])
                {
                    if (match == evnt.Match)
                    {
                        useEvent = true;
                        break;
                    }
                }

                if (!useEvent)
                    continue;

                Score score = teamScore[evnt.Team];

                if (evnt.StartingHeight == 0)
                    score.StkHtScoringPlatoform += 2 * evnt.NumTotesAdded;
                else
                    score.HtPlacedOnExistingStack += 2 * evnt.NumTotesAdded;

                if (evnt.IsContainerAdded)
                    score.Recycle += (4 * (evnt.StartingHeight + evnt.NumTotesAdded));
                if (evnt.IsLitterAdded)
                    score.Litter += 6;

                teamScore[evnt.Team] = score;
            }


            Console.WriteLine("Finding Average");

            var keys = teamScore.Keys.ToArray();
            for (int i = 0; i < keys.Length; i++)
            {
                var score = teamScore[keys[i]];
                score.AutoStacked /= 4;
                score.ClearLitter /= 4;
                score.ClearRecycle /= 4;
                score.ClearTote /= 4;
                score.FeederPickup /= 4;
                score.Fouls /= 4;
                score.GroundPickup /= 4;
                score.HtPlacedOnExistingStack /= 4;
                score.HtPlacedOnExistiongStkTopping /= 4;
                score.Litter /= 4;
                score.MovedToAuto /= 4;
                score.OrientRecycle /= 4;
                score.OrientTotes /= 4;
                score.Recycle /= 4;
                score.RobotMovedToAuto /= 4;
                score.StepContainersToAllies /= 4;
                score.StepRecycleToAllies /= 4;
                score.StkHtScoringPlatoform /= 4;
                score.StkHtYellowTotesOnStep /= 4;
                score.YellowToAuto /= 4;

                teamScore[keys[i]] = score;
            }


            Console.WriteLine("Writing Data to file");

            using(StreamWriter writer = new StreamWriter(System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/thing.txt"))
            {
                writer.Write("Team Number \t");
                writer.Write("AutoStacked \t");
                writer.Write("ClearLitter \t");
                writer.Write("ClearRecycle \t");
                writer.Write("ClearTote \t");
                writer.Write("FeederPickup \t");
                writer.Write("Fouls \t");
                writer.Write("GroundPickup \t");
                writer.Write("HtPlacedOnExistingStack \t");
                writer.Write("HtPlacedOnExistiongStkTopping \t");
                writer.Write("Litter \t");
                writer.Write("MovedToAuto \t");
                writer.Write("OrientRecycle \t");
                writer.Write("OrientTotes \t");
                writer.Write("Recycle \t");
                writer.Write("RobotMovedToAuto \t");
                writer.Write("StepContainersToAllies \t");
                writer.Write("StepRecycleToAllies \t");
                writer.Write("StkHtScoringPlatoform \t");
                writer.Write("StkHtYellowTotesOnStep \t");
                writer.Write("YellowToAuto \t");

                writer.WriteLine();

                foreach (var pair in teamScore)
                {
                    var score = teamScore[pair.Key];
                    writer.Write(pair.Key.Number + "\t");
                    writer.Write(score.AutoStacked + "\t");
                    writer.Write(score.ClearLitter + "\t");
                    writer.Write(score.ClearRecycle + "\t");
                    writer.Write(score.ClearTote + "\t");
                    writer.Write(score.FeederPickup + "\t");
                    writer.Write(score.Fouls + "\t");
                    writer.Write(score.GroundPickup + "\t");
                    writer.Write(score.HtPlacedOnExistingStack + "\t");
                    writer.Write(score.HtPlacedOnExistiongStkTopping + "\t");
                    writer.Write(score.Litter + "\t");
                    writer.Write(score.MovedToAuto + "\t");
                    writer.Write(score.OrientRecycle + "\t");
                    writer.Write(score.OrientTotes + "\t");
                    writer.Write(score.Recycle + "\t");
                    writer.Write(score.RobotMovedToAuto + "\t");
                    writer.Write(score.StepContainersToAllies + "\t");
                    writer.Write(score.StepRecycleToAllies + "\t");
                    writer.Write(score.StkHtScoringPlatoform + "\t");
                    writer.Write(score.StkHtYellowTotesOnStep + "\t");
                    writer.Write(score.YellowToAuto + "\t");

                    writer.WriteLine();
                }
            }

        }//end main
    }
}
