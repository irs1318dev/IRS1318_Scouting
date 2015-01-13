using Scouter.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using Scouter.Web.Models;
using Scouter.Web.Models.FRCMatch;

namespace Scouter.Web.ViewModels
{
    public class FRCMatchesListViewModel : ViewModelBase
    {
        public IList<FRCMatchDataTransfer> FRCMatches { get; set; }

        public string FRCMatchesJSON
        {
            get
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();

                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                var frcmatches = JsonConvert.SerializeObject(this.FRCMatches, settings);
                return frcmatches;
            }
        }
    }
}