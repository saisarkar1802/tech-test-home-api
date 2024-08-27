using HouseUnitAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HouseUnitAPI.Data
{
    public class HouseUnitDbContext:DbContext
    {
        public HouseUnitDbContext(DbContextOptions<HouseUnitDbContext> options) : base(options) { }

        public DbSet<HouseUnit> HouseUnits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var stringArrayComparer = new ValueComparer<string[]>(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.ToArray());


            modelBuilder.Entity<HouseUnit>()
                .Property(e => e.Features)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries))
                .Metadata.SetValueComparer(stringArrayComparer);

            modelBuilder.Entity<HouseUnit>()
                .Property(e => e.UnitType)
                .HasConversion<string>();
        }
    }
}
