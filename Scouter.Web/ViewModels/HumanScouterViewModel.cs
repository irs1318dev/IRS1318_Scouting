using Scouter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace Scouter.Web.ViewModels
{
    public class HumanScouterViewModel
    {
        public int Color { get; set; }
        public Team Team1 { get; set; }
        public Team Team2 { get; set; }
        public Team Team3 { get; set; }
        public FRCMatch Match { get; set; }
        public int Scouter_Id { get; set; }


        public string HumanEventTypes
        {
            get
            {
                Dictionary<string, int> types = new Dictionary<string, int>();
                foreach (var type in Enum.GetNames(typeof(HumanEventType)))
                {
                    types.Add(type, (int)Enum.Parse(typeof(HumanEventType), type));
                }
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Serialize(types).Replace("&quot;", "\"");
            }
        }
    }
}