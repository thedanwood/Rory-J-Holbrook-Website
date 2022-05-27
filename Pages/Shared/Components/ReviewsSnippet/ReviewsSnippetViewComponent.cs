using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace RJHWebsite.Pages.Shared.Components.ReviewsSnippet
{
    public class ReviewsSnippetViewComponent : ViewComponent
    {
        public class Review
        {
            public string ReviewBody { get; set; }
            public string ReviewName { get; set; }
        }
        public class ReviwsViewModel
        {
            public int CssStyleEnum { get; set; }
            public string Title { get; set; }
            public string SubTitle { get; set; }
            public List<Review> AllReviews { get; set; }
        }
        public IViewComponentResult Invoke(ReviwsViewModel vm)
        {
            vm.AllReviews = new List<Review>()
            {
                new Review()
                {
                    ReviewBody = "Great service and price",
                    ReviewName = "Daniel Wood"
                },
                new Review()
                {
                    ReviewBody = "Great staff very friendly and a good price",
                    ReviewName = "Mark Berry"
                },
                new Review()
                {
                    ReviewBody = "Very prompt delivery .friendly drivers good telephone manner and crushed concrete was fab",
                    ReviewName = "Marcus Bailey"
                },
            };
            return View("ReviewsSnippet", vm);
        }
    }
}
