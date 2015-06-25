﻿using System.ComponentModel.DataAnnotations;

namespace SuperHeroi.Infra.CrossCutting.IoC.Security.Model.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}