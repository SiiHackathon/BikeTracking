using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System;

namespace BikeTracker.Models
{
    public class NewsDetailsViewModel
    {
        public long NewsId { get; set; }

        [AllowHtml]
        [UIHint("tinymce_full_compressed")]
        public string Content { get; set; }

        public string Title { get; set; }
        public DateTime AddedOn { get; set; }
    }
}