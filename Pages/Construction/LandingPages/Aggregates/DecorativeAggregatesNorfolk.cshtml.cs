using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace RJHWebsite.Pages.Construction.LandingPages.Aggregates
{
    public class DecorativeAggregatesNorfolkModel : PageModel
    {
        public List<AggregateItemModel> Aggregates { get; set; }
        public void OnGet()
        {
            string aggregatesJson = System.IO.File.ReadAllText("aggregates-v1.0.json").ToString();
            List<AggregateItemModel> aggregates = JsonConvert.DeserializeObject<List<AggregateItemModel>>(aggregatesJson);
            Aggregates = aggregates.Where(r => r.Category == "decorative").ToList();
        }
    }
}
