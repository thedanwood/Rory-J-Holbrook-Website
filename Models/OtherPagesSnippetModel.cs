using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RJHWebsite.Models
{
    public class OtherPagesSnippetModel
    {
        public int SnippetTypeEnum { get; set; }
        public string CurrentPage { get; set; }
        public string ParentPage { get; set; }
        public string Title { get; set; }
        public int CssStyleEnum { get; set; }
    }
}
