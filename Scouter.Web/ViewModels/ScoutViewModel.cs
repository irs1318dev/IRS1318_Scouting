using Scouter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace Scouter.Web.ViewModels
{
    public class ScoutViewModel
    {
        public int Color { get; set; }
        public Team Team { get; set; }
		public FRCMatch Match { get; set; }
		public int Scouter_Id { get; set; }

        public string RobotEventTypes 
        { 
            get
            {
                Dictionary<string, int> types = new Dictionary<string, int>();
                foreach(var type in Enum.GetNames(typeof(RobotEventType)))
                {
                    types.Add(type, (int)Enum.Parse(typeof(RobotEventType), type));
                }
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Serialize(types).Replace("&quot;", "\"");
            }
        }

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