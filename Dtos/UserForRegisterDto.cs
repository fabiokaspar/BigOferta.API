using System;
using System.ComponentModel.DataAnnotations;

namespace BigOferta.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        [StringLength(50, MinimumLength=3, ErrorMessage="O nome deve conter entre 3 a 50 caracteres")]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength=6, ErrorMessage="O nome de usuário deve conter entre 6 e 50 caracteres")]
        public string Username { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength=6, ErrorMessage="A senha deve conter entre 6 e 50 caracteres")]
        public string Password { get; set; }
        
        // [Required]
        public DateTime DateOfBirth { get; set; }
        
        // [Required]
        public string City { get; set; }
        
        // [Required]
        public string Country { get; set; }

        public DateTime Created { get; set; }

        public UserForRegisterDto()
        {
            Created = DateTime.Now;
        }
    }
}