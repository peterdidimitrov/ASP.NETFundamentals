﻿using Microsoft.AspNetCore.Identity;

namespace MvcMovie.Models
{
    public class SampleUser : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

    }
}
