using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace RJHWebsite.Pages.Haulage
{
    public class TipperHireModel : PageModel
    {
        public class Tippers
        {
            public string TipperName { get; set; }
            public string DeliveryInfo { get; set; }
            public string Capabilities { get; set; }
            public string ImageName { get; set; }
            public string ImageDescription { get; set; }
        }
        public List<Tippers> AllTippers { get; set; }
        public string TipperType { get; set; }
        public void OnGet([FromQuery(Name = "tipper-type")] string tipperType)
        {
            string tipperJson = System.IO.File.ReadAllText("tippers-v1.0.json").ToString();
            AllTippers = JsonConvert.DeserializeObject<List<Tippers>>(tipperJson);
            TipperType = tipperType;
        }
    }
}
