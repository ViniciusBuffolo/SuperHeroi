using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroi.Domain.Entities
{
    public class HeroiPoder
    {
        public HeroiPoder()
        {
            HeroiPoderId = Guid.NewGuid();
        }

        public Guid HeroiPoderId { get; set; }
        public Guid HeroiId { get; set; }
        public Guid PoderId { get; set; }


        public virtual Heroi Heroi { get; set; }
        public virtual Poder Poder { get; set; }

    }
}
