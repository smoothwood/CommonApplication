using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class UserRole : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Role")]
  
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
