using Scouter.Models;
using Scouter.Web.Models.Scouting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace Scouter.Web.ViewModels
{
    public class ReportViewModel : ViewModelBase
    {
        public int TeamNumber { get; set; }

        public List<AllEvents> AllFRCEvents { get; set; }

        public int CurrentEventID { get; set; }
        public string CurrentEventName { get; set; }
        public int CurrentMatchSeq { get; set; }

        public List<AllMatches> AllIRSMatches { get; set; }
        public UpcomingMatch IRSMatches { get; set; }

        public List<TeamScore> TeamRankings { get; set; }

        public string AllFRCEventsJSON
        {
            get
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();

                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                var events = JsonConvert.SerializeObject(this.AllFRCEvents, settings);
                return events;
            }
        }

        public string AllIRSMatchesJSON
        {
            get
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();

                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                var matches = JsonConvert.SerializeObject(this.AllIRSMatches, settings);
                return matches;
            }
        }

        public string IRSMatchesJSON
        {
            get
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();

                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                var matches = JsonConvert.SerializeObject(this.IRSMatches, settings);
                return matches;
            }
        }

        public string TeamRankingsJSON
        {
            get
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();

                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                var matches = JsonConvert.SerializeObject(this.TeamRankings, settings);
                return matches;
            }
        }

        public string ImageUrlPrefix
        {
            get
            {
                return Scouter.Web.Config.ImagesUrlPrefix;
            }
        }

    }
}