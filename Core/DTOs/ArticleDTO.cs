using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs
{
    public class ArticleDTO
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ContentUrl { get; set; }
        public string ImageUrl { get; set; }

    }
}
