using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperHeroi.Domain.Entities;

namespace SuperHeroi.Infra.Data.EntityConfig
{
    public class PoderConfiguration : EntityTypeConfiguration<Poder>
    {
        public PoderConfiguration()
        {
            HasKey(c => c.PoderId);

            Property(c => c.Descricao)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
