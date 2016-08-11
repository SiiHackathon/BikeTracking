using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BikeTracker.Models {

    public class NewsViewModel {

        [AllowHtml]
        [UIHint("tinymce_full_compressed")]
        public string Content { get; set; }

        public string Title { get; set; }
    }

    public static class NewsList
    {
        private static List<NewsViewModel> _news = new List<NewsViewModel>();
        public static List<NewsViewModel> News { get { return _news; } }
    }
}