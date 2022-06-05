using System;
using System.ComponentModel.DataAnnotations;

namespace CommonLayer
{
    public class RegisterModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Key]
        public int UserID { get; set; }
    }
}
