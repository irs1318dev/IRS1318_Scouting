using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

using Scouter.Models;
using System.Collections.Generic;

namespace Scouter.Data.Configuration
{
#if CONSOLE
    public class CustomDatabaseInitializer : DropCreateDatabaseAlways<DataContext>
#else
    public class CustomDatabaseInitializer : CreateDatabaseIfNotExists<DataContext>
#endif
    {
        int nums;
		public static bool SeedMatches
		{
			get 
			{
				switch(Environment.MachineName.ToLower())
				{
					case "nephele":
						return true;
					case "a4970693":
						return true;
					case "irs001-pc":
                        return false;
                    case "prestonlaptop":
                        return true;
                    case "tali":
                        return true;
                    default:
						return false;
				}
			}
		}

        protected override void Seed(DataContext context)
        {
            Random rand = new Random();

            context.CurrentScoutData.Add(new CurrentScoutData());
            context.SaveChanges();

            Console.WriteLine("Creating Events");
            FRCCompetition Auburn = new FRCCompetition()
            {
                Name = "PNW FIRST Robotics Auburn Mountainview District Event",
                Venue = "Auburn Mountain View High School",
                City = "Auburn",
                State = "WA",
                Type = "District",
                BeginDate = new DateTime(2015, 2, 27, 11, 0, 0),
                FinishDate = new DateTime(2015, 2, 28, 17, 0, 0)
            };
            context.FRCCompetitions.Add(Auburn);

            FRCCompetition MtVernon = new FRCCompetition();
            MtVernon.Name = "PNW FIRST Robotics Mt. Vernon District Event";
            MtVernon.Venue = "Mount Vernon High School";
            MtVernon.City = "Mount Vernon";
            MtVernon.State = "WA";
            MtVernon.Type = "District";
            MtVernon.BeginDate = new DateTime(2014, 3, 14, 8, 0, 0);
            MtVernon.FinishDate = new DateTime(2014, 3, 15, 5, 0, 0);
            context.FRCCompetitions.Add(MtVernon);

            FRCCompetition Shorewood = new FRCCompetition();
            Shorewood.Name = "PNW FIRST Robotics Shorewood District Event";
            Shorewood.Venue = "Shorewood High School";
            Shorewood.City = "Shoreline";
            Shorewood.State = "WA";
            Shorewood.Type = "District";
            Shorewood.BeginDate = new DateTime(2014, 3, 21, 8, 0, 0);
            Shorewood.FinishDate = new DateTime(2014, 3, 22, 5, 0, 0);
            context.FRCCompetitions.Add(Shorewood);

            FRCCompetition Cheny = new FRCCompetition();
            Cheny.Name = "Autodesk PNW FRC Championship";
            Cheny.Venue = "Eastern Washington University";
            Cheny.City = "Cheney";
            Cheny.State = "WA";
            Cheny.Type = "District Championship";
            Cheny.BeginDate = new DateTime(2014, 4, 1, 8, 0, 0);
            Cheny.FinishDate = new DateTime(2014, 4, 4, 5, 0, 0);
            context.FRCCompetitions.Add(Cheny);

            FRCCompetition StLouis = new FRCCompetition();
            StLouis.Name = "FRC Championship";
            StLouis.Venue = "Edward Jones Dome";
            StLouis.City = "St. Louis";
            StLouis.State = "MO";
            StLouis.Type = "Championship";
            StLouis.BeginDate = new DateTime(2014, 4, 22, 8, 0, 0);
            StLouis.FinishDate = new DateTime(2014, 4, 25, 5, 0, 0);
            context.FRCCompetitions.Add(StLouis);

            Console.WriteLine("Saving Events");
            context.SaveChanges();

            Role role = new Role();
            role = new Role();
            role.RoleName = "Robot Scout";
            context.Roles.Add(role);

            role = new Role();
            role.RoleName = "Pit Scout";
            context.Roles.Add(role);

            role = new Role();
            role.RoleName = "Coordinator";
            context.Roles.Add(role);
            context.SaveChanges();

            context.CurrentScoutData.First().Event_ID = 1;

            /////////////Fill lookup tables////////////////////////
            for (int i = 0; i < (int)RobotEventType.MAX; i++)
            {
                context.RobotEventTypeLookups.Add(
                    new RobotEventTypeLookup()
                    {
                        Id = i + 1,
                        RobotEventTypeName = ((RobotEventType)i).ToString(),
                        RobotEventTypeValue = i
                    });
            }
            for (int i = 0; i < (int)HumanEventType.MAX; i++)
            {
                context.HumanEventTypeLookups.Add(
                    new HumanEventTypeLookup()
                    {
                        Id = i + 1,
                        RobotEventTypeName = ((HumanEventType)i).ToString(),
                        RobotEventTypeValue = i
                    });
            }

            /////////////SEED MATCHES//////////////////////////////
            if (SeedMatches)
            {
                Console.WriteLine("Creating Teams");
                List<Team> teams = new List<Team>();
                for (int i = 0; i < 40; i++)
                {
                    Team team = new Team()
                    {
                        Number = rand.Next(50 * i, 50 * (i + 1)),
                        Height = rand.Next(120, 600) / 10f,
                        Width = rand.Next(120, 240) / 10f,
                        Length = rand.Next(120, 240) / 10f,
                        Drivetrain = (DrivetrainType)rand.Next(5),
                        WheelCount = rand.Next(3, 9),
                        Weight = rand.Next(100, 1200) / 10f
                    };
                    team.Name = team.Number.ToString();
                    context.Teams.Add(team);
                    teams.Add(team);
                }
                Console.WriteLine("Saving Teams");
                context.SaveChanges();
                int matches = 86;
                Console.WriteLine("Creating Matches");
                for (int i = 0; i < matches; i++)
                {
                    FRCMatch m = new FRCMatch();
                    m.SequenceNumber = i + 1;

                    m.FRCEvent = Auburn;

                    context.FRCMatches.Add(m);
                }
                Console.WriteLine("Saving Matches");
                context.SaveChanges();

                Console.WriteLine("Creating Alliances and seeding robot events");
                Console.WriteLine("0%");
                DateTime startTime = DateTime.Now;
                int iteration = 0;
                foreach (FRCMatch m in Auburn.Matches)
                {
                    Alliance red, blue;
                    GenerateUniqueAlliances(m, teams, rand, context.Alliances, out red, out blue);

                    GenerateScores(m, blue.Team1, rand, context.RobotEvents);
                    GenerateScores(m, blue.Team2, rand, context.RobotEvents);
                    GenerateScores(m, blue.Team3, rand, context.RobotEvents);
                    GenerateScores(m, red.Team1, rand, context.RobotEvents);
                    GenerateScores(m, red.Team2, rand, context.RobotEvents);
                    GenerateScores(m, red.Team3, rand, context.RobotEvents);
                    context.SaveChanges();


                    double percent = ((iteration + 1) / (double)matches) * 100;
                    TimeSpan timeTook = DateTime.Now - startTime;
                    double percentLeft = 100 - percent;
                    double timePerPercent = timeTook.TotalMilliseconds / percent;
                    TimeSpan timeLeft = TimeSpan.FromMilliseconds(timePerPercent * percentLeft);
                    Console.CursorTop -= 1;
                    Console.WriteLine("{0}%  approximately {1} seconds remaining                                             ", Math.Round(percent, 1), Math.Round(timeLeft.TotalSeconds, 1));
                    iteration++;
                }
            }
            else
            {
                Console.WriteLine("SeedMatches is false so matches, teams, and events will not be seeded");
            }
        }

        protected void GenerateUniqueAlliances(FRCMatch match, List<Team> teams, Random rand, DbSet<Alliance> alliances, out Alliance red, out Alliance blue)
        {
            List<Team> teamsAvailable = new List<Team>(teams);
            
            blue = new Alliance() { Color = AllianceColor.Blue, Match = match, CreatedOn = DateTime.Now, ModifiedOn = DateTime.Now };
            red = new Alliance() { Color = AllianceColor.Red, Match = match, CreatedOn = DateTime.Now, ModifiedOn = DateTime.Now };
            int index = rand.Next(0, teamsAvailable.Count);
            blue.Team1 = teamsAvailable[index];
            teamsAvailable.RemoveAt(index);
            index = rand.Next(0, teamsAvailable.Count);
            blue.Team2 = teamsAvailable[index];
            teamsAvailable.RemoveAt(index);
            index = rand.Next(0, teamsAvailable.Count);
            blue.Team3 = teamsAvailable[index];
            teamsAvailable.RemoveAt(index);
            index = rand.Next(0, teamsAvailable.Count);
            red.Team1 = teamsAvailable[index];
            teamsAvailable.RemoveAt(index);
            index = rand.Next(0, teamsAvailable.Count);
            red.Team2 = teamsAvailable[index];
            teamsAvailable.RemoveAt(index);
            index = rand.Next(0, teamsAvailable.Count);
            red.Team3 = teamsAvailable[index];
            teamsAvailable.RemoveAt(index);

            red.Match = match;
            match.RedAlliance = red;
            blue.Match = match;
            match.BlueAlliance = blue;
            alliances.Add(blue);
            alliances.Add(red);
        }

        protected void GenerateScores(FRCMatch match, Team team, Random rand, DbSet<RobotEvent> events)
        {
            if (rand.Next(100) < 98)
            {
                RobotEvent evnt = new RobotEvent()
                {
                    Team = team,
                    Match = match,
                    Id = nums++,
                    RobotEventType = RobotEventType.AutonomousMoved,
                    RobotMode = RobotMode.Autonomous
                };
                events.Add(evnt);
            }
            int num = rand.Next(20, 200);
            for (int i = 0; i < num; i++)
            {
                RobotEvent evnt = new RobotEvent() { Team = team, Match = match, Id = nums++};
                evnt.RobotEventType = (RobotEventType)rand.Next((int)RobotEventType.MAX);
                events.Add(evnt);
            }
        }
    }
}
