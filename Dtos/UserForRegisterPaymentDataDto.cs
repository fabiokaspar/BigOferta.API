using System;
using System.ComponentModel.DataAnnotations;

namespace BigOferta.API.Dtos
{
    public class UserForRegisterPaymentDataDto
    {
        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string District { get; set; }

        [Required]
        [StringLength(5, MinimumLength=1, ErrorMessage="Campo deve conter entre 1 e 5 digitos")]
        public string Number { get; set; }


        [Required]
        [StringLength(19, MinimumLength=19, ErrorMessage="O número do cartão deve conter 16+3(cvv) caracteres")]
        public string CardNumber { get; set; }
        

        [StringLength(20, MinimumLength = 8, ErrorMessage = "Phone number has to contain between 8 and 20 characters.")]
        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }    
    }
}