using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RJHWebsite.Pages.Locations
{
    public class IndexModel : PageModel
    {
        public string LocationName { get; set; }
        public void OnGet([FromQuery(Name = "location-name")] string locationName)
        {
            LocationName = locationName;
        }
    }
}
