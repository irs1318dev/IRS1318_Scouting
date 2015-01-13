using Scouter.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace Scouter.Web.ViewModels
{
    public class AlliancesListViewModel : ViewModelBase
    {
        public IList<Alliance> Alliances { get; set; }

        public string AlliancesJSON
        {
            get
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();
                
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                var alliances = JsonConvert.SerializeObject(this.Alliances, settings);
                return alliances;
            }
        }
    }
}