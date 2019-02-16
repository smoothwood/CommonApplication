using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Entities
{
    public class Category:BaseEntity
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public int  ParentCategoryId { get; set; }
    }
}
