using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Role : BaseEntity
    {
        [Key]
        public int RoleId { get; set; }
        [Required]
        public string RoleName { get; set; }

        public string Description { get; set; }

        public ICollection<UserRole> userRoles { get; set; }

    }
}
