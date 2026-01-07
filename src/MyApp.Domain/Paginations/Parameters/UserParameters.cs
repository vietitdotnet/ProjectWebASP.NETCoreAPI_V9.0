namespace MyApp.Domain.Parameters
{
    public class UserParameters : PagingParameters
    {
        public string Slug { get; set; }

        public string UrlAction { get; set; }

        public string RouterId { get; set; }

        public string KeySearch { get; set; }

        public string SortOrder { get; set; }

    }
}
