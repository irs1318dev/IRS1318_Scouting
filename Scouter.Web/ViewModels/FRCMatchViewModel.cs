using Scouter.Models;

namespace Scouter.Web.ViewModels
{
    public class FRCMatchViewModel :ViewModelBase
        {
        public FRCMatch MatchYO { get; set; }
        public bool IsNew { get; set; }
        public FRCMatchViewModel()
        {
            this.MatchYO = new FRCMatch();
        }
    }
}