using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RJHWebsite.Models
{
    public class SiteStructureModel
    {
        public int PageLevel { get; set; }
        public string ParentPageName { get; set; }
        public string PageName { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string ImageDescription { get; set; }
        public string ButtonText { get; set; }
        public string ButtonLink { get; set; }
        public string TilePath { get; set; }
    }
}
