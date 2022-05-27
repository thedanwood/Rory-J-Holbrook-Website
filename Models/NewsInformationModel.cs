using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RJHWebsite.Models
{
    public class NewsInformationModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string PublishedDate { get; set; }
        public string ImagePath { get; set; }
        public string ImageDescription { get; set; }
        public string SourceName { get; set; }
        public string SourceLink { get; set; }
        public string PageName { get; set; }
    }
}
