﻿using System;
using System.Collections.Generic;
using System.Text;

using TimeBank.Core.Enums;

namespace TimeBank.Core.Models
{
    public class User
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime InDate { get; set; }
        public DateTime OutDate { get; set; }
        public string Password { get; set; }
        public float Rating { get; set; }
        public bool Admin { get; set; }
        
        //one to one
        public Wallet Wallet { get; set; }

        public long AddressId { get; set; }
        public Address Address { get; set; }
        
        //one to many
        public List<Service> ProvideServices { get; set; }
        public List<Validation> Validations { get; set; }
        public List<Payment> Payments { get; set; }
        public List<Comment> Comments { get; set; }
    }
}