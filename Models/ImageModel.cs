namespace RJHWebsite.Models
{
    public class ImageModel
    {
        public string ImageName { get; set; }
        public string ImageDescription { get; set; }
        public string ImageClassName { get; set; }
        public bool IncludeOtherSizes { get; set; } = true;
        public bool DisableLazyLoading { get; set; } = false;
    }
}
