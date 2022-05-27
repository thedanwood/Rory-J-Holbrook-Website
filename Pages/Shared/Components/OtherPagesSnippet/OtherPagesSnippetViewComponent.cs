using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RJHWebsite.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RJHWebsite.Pages.Shared.Components.OtherPagesSnippet
{
    public class OtherPagesSnippetViewComponent:ViewComponent
    {
        public class OtherPagesSnipperViewModel
        {
            public List<SiteStructureModel> SiteStructure { get; set; }
            public int SnippetTypeEnum { get; set; }
            public string Title { get; set; }
            public int CssStyleEnum { get; set; }
        }
        public IViewComponentResult Invoke(OtherPagesSnippetModel model)
        {
            string siteStructureJson = File.ReadAllText("site-structure-v1.0.json").ToString();
            OtherPagesSnipperViewModel jsonResult = JsonConvert.DeserializeObject<OtherPagesSnipperViewModel> (siteStructureJson);
            jsonResult.SiteStructure = jsonResult.SiteStructure.Where(r => r.PageLevel == 2 && r.ParentPageName.ToLower() == model.ParentPage.ToLower() && r.PageName.ToLower() != model.CurrentPage.ToLower()).ToList();
            return View("OtherPagesSnippet", new OtherPagesSnipperViewModel()
            {
                SnippetTypeEnum = model.SnippetTypeEnum,
                SiteStructure = jsonResult.SiteStructure,
                Title = model.Title,
                CssStyleEnum = model.CssStyleEnum
            });
        }
    }
}
