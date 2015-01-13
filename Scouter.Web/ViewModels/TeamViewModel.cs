using Scouter.Models;

namespace Scouter.Web.ViewModels
{
    public class TeamViewModel : ViewModelBase
    {
        public Team Team { get; set; }
        public bool IsNew { get; set; }

        public string ImageUrlPrefix
        {
            get { return Scouter.Web.Config.ImagesUrlPrefix; }
        }

        public TeamViewModel()
        {
            this.Team = new Team();
        }
    }
}