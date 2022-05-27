using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RJHWebsite.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RJHWebsite.Pages.Shared.Components.TestimonialsSnippet
{
    public class TestimonialsSnippetViewComponent : ViewComponent
    {
        public class TestimonialsViewModel
        {
            public int VariationEnum { get; set; }
            public int SnippetTypeEnum { get; set; }
            public int CssStyleEnum { get; set; }
            public List<TestimonialsModel> Testimonials { get; set; }
        }
        public IViewComponentResult Invoke(int snippetTypeEnum, int cssStyleEnum, int variationEnum)
        {
            string testimonialsJson = File.ReadAllText("testimonials-v1.0.json").ToString();
            TestimonialsViewModel viewmodel = JsonConvert.DeserializeObject<TestimonialsViewModel>(testimonialsJson);
            viewmodel.SnippetTypeEnum = snippetTypeEnum;
            viewmodel.VariationEnum = variationEnum;
            viewmodel.CssStyleEnum = cssStyleEnum;
            return View("TestimonialsSnippet", viewmodel);
        }
    }
}
