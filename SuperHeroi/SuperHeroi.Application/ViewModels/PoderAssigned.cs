using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroi.Application.ViewModels
{
    public class PoderAssigned
    {
        public Guid PoderId { get; set; }
        public string Descricao { get; set; }
        public bool Assigned { get; set; }
    }
}
