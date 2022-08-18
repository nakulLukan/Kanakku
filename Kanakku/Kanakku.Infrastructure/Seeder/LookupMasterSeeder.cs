using Kanakku.Domain.Lookup;
using Kanakku.Shared;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Infrastructure.Seeder
{
    public static class LookupMasterSeeder
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            List<LookupMaster> lookupMasters = new List<LookupMaster>();
            lookupMasters.Add(new()
            {
                Id = PKValue[LookupMasterInternalName.STATE],
                InternalName = LookupMasterInternalName.STATE,
                IsActive = true,
                CreatedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
            });
            lookupMasters.Add(new()
            {
                Id = PKValue[LookupMasterInternalName.DISTRICT],
                InternalName = LookupMasterInternalName.DISTRICT,
                DependentLookupMasterId = PKValue[LookupMasterInternalName.STATE],
                IsActive = true,
                CreatedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
            });

            foreach (var seed in lookupMasters)
            {
                modelBuilder.Entity<LookupMaster>().HasData(seed);
            }
        }

        public static IDictionary<string, int> PKValue { get; } = new Dictionary<string, int>
        {
            { LookupMasterInternalName.STATE, 1},
            { LookupMasterInternalName.DISTRICT, 2},
        };
    }
}
