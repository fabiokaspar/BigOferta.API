using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BigOferta.API.Models
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime Created { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string Number { get; set; }
        public int ReputationGrade { get; set; }
        public string CardNumber { get; set; }
        public virtual Photo ProfilePhoto { get; set; }
        public virtual ICollection<Message> UserFeedbacks { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual PurchaseOrder<UserOffer> CartOffers { get; set; }
    }
}