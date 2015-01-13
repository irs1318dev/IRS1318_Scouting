using Scouter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Scouter.Web.ViewModels
{
    public class FRCEventViewModel
    {
        public FRCEvent Event { get; set; }
        public bool IsNew { get; set; }

        public FRCEventViewModel()
        {
            this.Event = new FRCEvent();
        }
    }
}