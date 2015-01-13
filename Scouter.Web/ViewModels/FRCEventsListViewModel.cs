using Scouter.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using Scouter.Web.Models;
using Scouter.Web.Models.FRCEvent;

namespace Scouter.Web.ViewModels
{
    public class FRCEventsListViewModel : ViewModelBase
    {
        public IList<FRCEventDataTransfer> FRCEvents { get; set; }

        public string FRCEventsJSON
        {
            get
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();

                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                var frcevents = JsonConvert.SerializeObject(this.FRCEvents, settings);
                return frcevents;
            }
        }
    }
}