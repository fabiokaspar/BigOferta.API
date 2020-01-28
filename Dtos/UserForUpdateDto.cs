using System;
using System.ComponentModel.DataAnnotations;
using BigOferta.API.Models;

namespace BigOferta.API.Dtos
{
    public class UserForUpdateDto
    {
        [StringLength(50, MinimumLength=3, ErrorMessage="O nome deve conter entre 3 e 50 caracteres")]
        public string Name { get; set; }
        
        [StringLength(50, MinimumLength=6, ErrorMessage="O nome de usuário deve conter entre 6 e 50 caracteres")]
        public string UserName { get; set; }


        [StringLength(50, MinimumLength=6, ErrorMessage="A senha atual contém entre 6 e 50 caracteres")]
        public string CurrentPassword { get; set; }


        [StringLength(50, MinimumLength=6, ErrorMessage="A nova senha deve conter entre 6 e 50 caracteres")]
        public string NewPassword { get; set; }


        [StringLength(100, MinimumLength=3, ErrorMessage="Campo deve conter entre 3 e 100 caracteres")]
        public string City { get; set; }


        [StringLength(100, MinimumLength=3, ErrorMessage="Campo deve conter entre 3 e 100 caracteres")]
        public string Country { get; set; }


        [StringLength(100, MinimumLength=2, ErrorMessage="Campo deve conter entre 2 e 3 caracteres")]
        public string State { get; set; }


        [StringLength(100, MinimumLength=3, ErrorMessage="Campo deve conter entre 3 e 100 caracteres")]
        public string Street { get; set; }


        [StringLength(100, MinimumLength=3, ErrorMessage="Campo deve conter entre 3 e 100 caracteres")]
        public string District { get; set; }

        [StringLength(5, MinimumLength=1, ErrorMessage="Campo deve conter entre 1 e 5 digitos")]
        public string Number { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(20, MinimumLength = 8, ErrorMessage = "Phone number has to contain between 8 and 20 characters.")]
        public string PhoneNumber { get; set; }

        [StringLength(19, MinimumLength=19, ErrorMessage="O número do cartão deve conter 16+3(cvv) caracteres")]
        public string CardNumber { get; set; }
        
        public DateTime DateOfBirth { get; set; } 
    }
}