using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RJHWebsite.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RJHWebsite.Pages.Shared.Components.NewsSnippet
{
    public class NewsSnippetViewComponent : ViewComponent
    {
        public class NewsSnippetViewModel
        {
            public List<NewsInformationModel> NewsInformationModel { get; set; }
            public int SnippetTypeEnum { get; set; }
            public int? NumberOfArticles { get; set; }
            public int CssStyleEnum { get; set; }
            public string Title { get; set; }
        }
        public IViewComponentResult Invoke(int snippetTypeEnum, int? numberOfArticles, int cssStyleEnum, string title, string currentNewsPage)
        {
            string newsJson = File.ReadAllText("news-articles-v1.0.json").ToString();
            List<NewsInformationModel> newsInfo = JsonConvert.DeserializeObject<List<NewsInformationModel>>(newsJson);
            newsInfo = newsInfo.OrderByDescending(r=>Convert.ToDateTime(r.PublishedDate)).Where(r=>r.Title != currentNewsPage).ToList();
            if (numberOfArticles != null)
            {
                newsInfo = newsInfo.Take((int)numberOfArticles).ToList();
            }
            NewsSnippetViewModel viewmodel = new NewsSnippetViewModel()
            {
                NewsInformationModel = newsInfo,
                NumberOfArticles = numberOfArticles,
                SnippetTypeEnum = snippetTypeEnum,
                CssStyleEnum = cssStyleEnum,
                Title = title,
            };
            return View("NewsSnippet", viewmodel);
        }
    }
}
