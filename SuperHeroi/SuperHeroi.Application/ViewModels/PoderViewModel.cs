using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroi.Application.ViewModels
{
    public class PoderViewModel
    {
        public PoderViewModel()
        {
            PoderId = Guid.NewGuid();
        }

        [Key]
        public Guid PoderId { get; set; }

        [Required(ErrorMessage = "Preencha o campo Descriçao")]
        [MaxLength(50, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(3, ErrorMessage = "Mínimo {0} caracteres")]
        public string Descricao { get; set; }
    }
}
