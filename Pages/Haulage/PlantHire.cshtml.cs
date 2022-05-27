using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RJHWebsite.Pages.Haulage
{
    public class PlantHireModel : PageModel
    {
        public string PlantType { get; set; }
        public void OnGet([FromQuery(Name = "plant-type")] string plantType)
        {
            PlantType = plantType;
        }
    }
}
