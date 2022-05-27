using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RJHWebsite.Pages.Shared.Components.TileSnippet
{
    public class TileSnippetViewComponent : ViewComponent
    {
        public class TileSnippetViewModel
        {
            public string ImagePath { get; set; }
            public string ImageDescription { get; set; }
            public string Title { get; set; }
            public string ButtonText { get; set; }
            public string ButtonLink { get; set; }
            public int TileStyleEnum { get; set; }
        }
        public IViewComponentResult Invoke(TileSnippetViewModel viewmodel)
        {
            return View("TileSnippet", viewmodel);
        }
    }
}
