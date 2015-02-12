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
						return true;
					default:
						return false;
				}
			}
		}

        protected override void Seed(DataContext context)
        {
            Random rand = new Random();
            //string[] teamdescriptions = new string[10] {
            //    "Nice team with friendly members.",
            //    "A truly beautiful robot!",
            //    "From a school on a nice quiet street",
            //    "The school is freeway accessible with a hugh green lawn",
            //    "Lots of members and a big pit.",
            //    "Robot is well-kept by team",
            //    "Includes frisbee thrower, pyramid climber and a cup holder.",
            //    "The kit needs some work (cardboard!) but the design is good",
            //    "Includes a huge recliner for a good first persion point of view",
            //    "From a powerhouse school"
            //};

            //for (int i = 9; i > 0; i--)
            //{
            //    Team team = new Team();
            //    team.Name = string.Format("Team {0}", i);
            //    team.Description = teamdescriptions[i];
            //    team.ImageName = "noimg.jpg";
            //    context.Teams.Add(team);
            //}

			context.CurrentScoutData.Add(new CurrentScoutData());
			context.SaveChanges();

            Console.WriteLine("Creating Events");
			FRCEvent Auburn = new FRCEvent()
			{
				Name = "PNW FIRST Robotics Auburn Mountainview District Event",
				Venue = "Auburn Mountain View High School",
				City = "Auburn",
				State = "WA",
				Type = "District",
				BeginDate = new DateTime(2014, 2, 28, 11, 0, 0),
				FinishDate = new DateTime(2014, 3, 1, 17, 0, 0)
			};
			context.FRCEvents.Add(Auburn);

            FRCEvent GlacierPeak = new FRCEvent();
            GlacierPeak.Name = "PNW FIRST Robotics Glacier Peak District Event";
            GlacierPeak.Venue = "Glacier Peak High School";
            GlacierPeak.City = "Snohomish";
            GlacierPeak.State = "WA";
            GlacierPeak.Type = "District";
            GlacierPeak.BeginDate = new DateTime(2014, 3, 6, 8, 0, 0);
            GlacierPeak.FinishDate = new DateTime(2014, 3, 8, 5, 0, 0);
            context.FRCEvents.Add(GlacierPeak);

            FRCEvent Portland = new FRCEvent();
            Portland.Name = "Autodesk PNW FRC Championship";
            Portland.Venue = "Memorial Coliseum";
            Portland.City = "Portland";
            Portland.State = "OR";
            Portland.Type = "District Championship";
            Portland.BeginDate = new DateTime(2014, 4, 10, 8, 0, 0);
            Portland.FinishDate = new DateTime(2014, 4, 12, 5, 0, 0);
            context.FRCEvents.Add(Portland);

            Console.WriteLine("Saving Events");
            context.SaveChanges();
            //Role role = new Role();
            //role.RoleName = "Ball Scout";
            //context.Roles.Add(role);

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
                        Ball = Convert.ToBoolean(rand.Next(0,2)),
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

					m.FRCEvent = GlacierPeak;

					context.FRCMatches.Add(m);
				}
				Console.WriteLine("Saving Matches");
				context.SaveChanges();

				Console.WriteLine("Creating Alliances and seeding robot events");
				Console.WriteLine("0%");
				DateTime startTime = DateTime.Now;
				int iteration = 0;
				foreach (FRCMatch m in GlacierPeak.Matches)
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
            RobotEvent autonEvent = new RobotEvent() { Team = team, Match = match, Id = nums++, RobotMode = RobotMode.Autonomous};
            events.Add(autonEvent);
            switch (rand.Next(3))
            {
                case 0:
                    autonEvent.RobotEventType = (RobotEventType)0;
                    if (rand.Next(2) == 1)
                        autonEvent.GoalWasHot = true;
                    break;
                case 1:
                    autonEvent.RobotEventType = (RobotEventType)0;
                    if (rand.Next(2) == 1)
                        autonEvent.GoalWasHot = true;
                    break;
            }
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
                switch (rand.Next(5))
                {
                    case 0:
                        evnt.RobotEventType = (RobotEventType)0;
                        break;
                    case 1:
                        evnt.RobotEventType = (RobotEventType)0;
                        break;
                    case 2:
                        evnt.RobotEventType = (RobotEventType)0;
                        break;
                    case 3:
                        evnt.RobotEventType = (RobotEventType)0;
                        break;
                    case 4:
                        evnt.RobotEventType = (RobotEventType)0;
                        break;
                }
                events.Add(evnt);
            }
        }
    }
}
