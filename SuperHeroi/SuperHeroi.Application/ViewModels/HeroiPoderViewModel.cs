using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroi.Application.ViewModels
{
    public class HeroiPoderViewModel
    {
        public HeroiPoderViewModel()
        {
            HeroiPoderId = Guid.NewGuid();
        }

        [Key]
        public Guid HeroiPoderId { get; set; }

        [Required(ErrorMessage = "Preencha o campo HeroiId")]
        public Guid HeroiId { get; set; }

        [Required(ErrorMessage = "Preencha o campo PoderId")]
        public Guid PoderId { get; set; }


        public virtual HeroiViewModel HeroiViewModel { get; set; }
        public virtual PoderViewModel PoderViewModel { get; set; }

    }
}
