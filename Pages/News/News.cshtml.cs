using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RJHWebsite.Models;
using Newtonsoft.Json;

namespace RJHWebsite.Pages.News
{
    public class NewsModel : PageModel
    {
        public List<NewsInformationModel> NewsInformationModel { get; set; }
        public void OnGet()
        {
            string newsJson = System.IO.File.ReadAllText("news-articles-v1.0.json").ToString();
            NewsInformationModel = JsonConvert.DeserializeObject<List<NewsInformationModel>>(newsJson);
            NewsInformationModel = NewsInformationModel.OrderByDescending(r => Convert.ToDateTime(r.PublishedDate)).ToList();
            //return View("NewsSnippet", newsInfo);
        }
    }
}
