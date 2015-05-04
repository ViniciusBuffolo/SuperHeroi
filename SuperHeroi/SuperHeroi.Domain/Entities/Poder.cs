﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroi.Domain.Entities
{
    public class Poder
    {
        public Poder()
        {
            PoderId = Guid.NewGuid();
        }

        public Guid PoderId { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Heroi> HeroiList { get; set; }
    }
}