﻿
namespace Beis.WebApplication.Models
{
    public class FullNameViewModel
    {
        [Required(ErrorMessage = "Enter your full name")]

        public string Name { get; set; }
    }
}