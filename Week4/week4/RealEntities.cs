using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace week4
{
    public partial class RealEntities : DbContext
    {
        public RealEntities()
            : base("name=RealEstateEntities")
        {
        }

        public virtual DbSet<Flat> Flats { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flat>()
                .Property(e => e.Code)
                .IsFixedLength();

            modelBuilder.Entity<Flat>()
                .Property(e => e.Side)
                .IsFixedLength();
        }
    }
}
