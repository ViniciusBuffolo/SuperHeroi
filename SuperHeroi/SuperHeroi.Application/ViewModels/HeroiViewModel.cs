using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroi.Application.ViewModels
{
    public class HeroiViewModel
    {
        public HeroiViewModel()
        {
            HeroiId = Guid.NewGuid();
            Poderes = new Collection<PoderViewModel>();
        }

        [Key]
        public Guid HeroiId { get; set; }

        [Required(ErrorMessage = "Preencha o campo Nome")]
        [MaxLength(250, ErrorMessage = "Máximo {0} caracteres")]
        public String Nome { get; set; }

        public virtual ICollection<PoderViewModel> Poderes { get; set; }
    }
}
