using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Scouter.Models;
using Scouter.Web.Models;
using Scouter.Web.Models.FRCEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Scouter.Web.ViewModels
{
    public class FRCEventViewViewModel : FRCEventViewModel
    {
        public IList<FRCMatchDataTransfer> Matches { get; set;}

        public string MatchesJSON
        {
            get
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();

                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                var matches = JsonConvert.SerializeObject(this.Matches, settings);
                return matches;
            }
        }
    }
}