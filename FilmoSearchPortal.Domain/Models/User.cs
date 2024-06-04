﻿using Microsoft.AspNetCore.Identity;

namespace FilmoSearchPortal.Domain.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public IEnumerable<Review>? Reviews { get; set; }
    }
}
