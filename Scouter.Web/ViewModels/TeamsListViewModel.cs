using Scouter.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace Scouter.Web.ViewModels
{
    public class TeamsListViewModel : ViewModelBase
    {
        public IList<Team> Teams { get; set; }

        public string TeamsJSON
        {
            get
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();

                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                var teams = JsonConvert.SerializeObject(this.Teams, settings);
                return teams;
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