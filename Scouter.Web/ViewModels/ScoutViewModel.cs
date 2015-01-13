using Scouter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Scouter.Web.ViewModels
{
    public class ScoutViewModel
    {
        public int Color { get; set; }
        public Team Team { get; set; }
		public FRCMatch Match { get; set; }
		public int Scouter_Id { get; set; }
    }
}