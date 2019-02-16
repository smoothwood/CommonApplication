using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    public class Article : BaseEntity
    {
        [Key]
        public int ArticleId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string ContentUrl { get; set; }
        public string ImageUrl { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
