using Microsoft.AspNetCore.Mvc;

namespace RJHWebsite.Pages.Shared.Components.AggregateAdvertSnippet
{
    public class AggregateAdvertSnippetViewComponent : ViewComponent
    {
        public class AggregateAdvertViewModel
        {
            public string title { get; set; }
            public string subTitle { get; set; }
        }
        public IViewComponentResult Invoke(AggregateAdvertViewModel vm)
        {
            return View("AggregateAdvertSnippet", vm);
        }
    }
}
