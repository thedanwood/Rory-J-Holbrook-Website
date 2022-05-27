using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RJHWebsite.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RJHWebsite.Pages.Shared.Components.NewsPage
{
    public class NewsPageViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string newsArticlePageName)
        {
            string newsJson = File.ReadAllText("news-articles-v1.0.json").ToString();
            List<NewsInformationModel> newsInfo = JsonConvert.DeserializeObject<List<NewsInformationModel>>(newsJson);
            return View("NewsPage", newsInfo.Where(r=>r.PageName == newsArticlePageName).FirstOrDefault());
        }
    }
    
}
