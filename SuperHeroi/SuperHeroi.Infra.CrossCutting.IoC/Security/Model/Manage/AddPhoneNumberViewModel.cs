using System.ComponentModel.DataAnnotations;

namespace SuperHeroi.Infra.CrossCutting.IoC.Security.Model.Manage
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }
}