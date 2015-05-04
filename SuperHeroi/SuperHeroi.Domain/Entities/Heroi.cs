﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroi.Domain.Entities
{
    public class Heroi
    {
        public Heroi()
        {
            HeroiId = Guid.NewGuid();
            PoderList = new List<Poder>();
        }

        public Guid HeroiId { get; set; }
        public string Nome { get; set; }


        public virtual ICollection<Poder> PoderList { get; set; }
    }
}