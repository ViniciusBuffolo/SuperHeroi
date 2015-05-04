using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperHeroi.Domain.Entities;

namespace SuperHeroi.Infra.Data.EntityConfig
{
    public class HeroiConfiguration : EntityTypeConfiguration<Heroi>
    {
        public HeroiConfiguration()
        {
            HasKey(f => f.HeroiId);

            Property(f => f.Nome)
                .IsRequired()
                .HasMaxLength(250);


            // MAPEAMENTO HEROI - PODER -> N-N
            HasMany(f => f.PoderList)
                .WithMany(e => e.HeroiList)
                .Map(me =>
                {
                    me.MapLeftKey("HeroiId");
                    me.MapRightKey("PoderId");
                    me.ToTable("HeroiPoder");
                });
        }
    }
}
