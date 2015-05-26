using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperHeroi.Domain.Entities;

namespace SuperHeroi.Infra.Data.EntityConfig
{
    public class HeroiPoderConfiguration : EntityTypeConfiguration<HeroiPoder>
    {
        public HeroiPoderConfiguration()
        {
            HasKey(f => f.HeroiPoderId);

            Property(f => f.HeroiId)
                .IsRequired();

            Property(f => f.PoderId)
                .IsRequired();

            this.HasRequired(t => t.Heroi)
                .WithMany(t => t.HeroisPoderes)
                .HasForeignKey(d => d.HeroiId);

            this.HasRequired(t => t.Poder)
                .WithMany(t => t.HeroisPoderes)
                .HasForeignKey(d => d.PoderId);



            // MAPEAMENTO HEROI - PODER -> N-N
            //HasMany(f => f.PoderList)
            //    .WithMany(e => e.HeroiList)
            //    .Map(me =>
            //    {
            //        me.MapLeftKey("HeroiId");
            //        me.MapRightKey("PoderId");
            //        me.ToTable("HeroiPoder");
            //    });
        }
    }
}
