using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Scouter.Web.ViewModels
{
    public class ManifestViewModel : ViewModelBase
    {
        readonly List<string> imageList;
        public ManifestViewModel(string root)
        {
            imageList = BuildImageList(root);
        }

        public List<string> ImageList
        {
            get
            {
                return imageList;
            }
        }
        
        private List<string> BuildImageList(string root)
        {
            List<string> images = new List<string>();

            DirectoryInfo di = new DirectoryInfo(root);
            var filesInfo = di.EnumerateFiles("*");

            foreach (var file in filesInfo)
            {
                images.Add(Config.ImagesUrlPrefix + file.Name);
            }

            return images;
        }
    }
}