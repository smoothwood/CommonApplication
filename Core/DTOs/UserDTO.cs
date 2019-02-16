using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string LoginId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Portait { get; set; }
        public bool? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string[] Roles { get; set; }
    }
}
