using System;
using System.Collections.Generic;
using BigOferta.API.Models;

namespace BigOferta.API.Dtos
{
    public class UserForReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime Created { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string Number { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string CardNumber { get; set; }
        public virtual PhotoForReturnDto ProfilePhoto { get; set; }
        public virtual PurchaseOrderDto Purchase { get; set; }
    }
}