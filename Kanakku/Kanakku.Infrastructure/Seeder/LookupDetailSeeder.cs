using Kanakku.Domain.Lookup;
using Kanakku.Shared;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Infrastructure.Seeder
{
    public static class LookupDetailSeeder
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            int id = 1;

            List<LookupDetail> lookupDetails = new List<LookupDetail>();
            lookupDetails.Add(new()
            {
                Id = id++,
                LookupMasterId = LookupMasterSeeder.PKValue[LookupMasterInternalName.STATE],
                IsActive = true,
                CreatedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                Value = "Kerala"
            });
            var keralaId = id - 1;

            lookupDetails.Add(new()
            {
                Id = id++,
                LookupMasterId = LookupMasterSeeder.PKValue[LookupMasterInternalName.STATE],
                IsActive = true,
                CreatedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                Value = "Tamil Nadu"
            });
            var tamilNaduId = id - 1;

            lookupDetails.Add(new()
            {
                Id = id++,
                LookupMasterId = LookupMasterSeeder.PKValue[LookupMasterInternalName.DISTRICT],
                IsActive = true,
                CreatedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                DependentLookupDetailId = keralaId,
                Value = "Palakkad"
            });
            lookupDetails.Add(new()
            {
                Id = id++,
                LookupMasterId = LookupMasterSeeder.PKValue[LookupMasterInternalName.DISTRICT],
                IsActive = true,
                CreatedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                DependentLookupDetailId = keralaId,
                Value = "Ernakulam"
            });
            lookupDetails.Add(new()
            {
                Id = id++,
                LookupMasterId = LookupMasterSeeder.PKValue[LookupMasterInternalName.DISTRICT],
                IsActive = true,
                CreatedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                DependentLookupDetailId = keralaId,
                Value = "Trissur"
            });
            lookupDetails.Add(new()
            {
                Id = id++,
                LookupMasterId = LookupMasterSeeder.PKValue[LookupMasterInternalName.DISTRICT],
                IsActive = true,
                CreatedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                DependentLookupDetailId = keralaId,
                Value = "Malappuram"
            });
            lookupDetails.Add(new()
            {
                Id = id++,
                LookupMasterId = LookupMasterSeeder.PKValue[LookupMasterInternalName.DISTRICT],
                IsActive = true,
                CreatedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                DependentLookupDetailId = keralaId,
                Value = "Kozhikode"
            });
            lookupDetails.Add(new()
            {
                Id = id++,
                LookupMasterId = LookupMasterSeeder.PKValue[LookupMasterInternalName.DISTRICT],
                IsActive = true,
                CreatedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                DependentLookupDetailId = keralaId,
                Value = "Alappuzha"
            });
            lookupDetails.Add(new()
            {
                Id = id++,
                LookupMasterId = LookupMasterSeeder.PKValue[LookupMasterInternalName.DISTRICT],
                IsActive = true,
                CreatedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                DependentLookupDetailId = keralaId,
                Value = "Idukki"
            });
            lookupDetails.Add(new()
            {
                Id = id++,
                LookupMasterId = LookupMasterSeeder.PKValue[LookupMasterInternalName.DISTRICT],
                IsActive = true,
                CreatedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                DependentLookupDetailId = keralaId,
                Value = "Kannur"
            });
            lookupDetails.Add(new()
            {
                Id = id++,
                LookupMasterId = LookupMasterSeeder.PKValue[LookupMasterInternalName.DISTRICT],
                IsActive = true,
                CreatedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                DependentLookupDetailId = keralaId,
                Value = "Kasaragod"
            });
            lookupDetails.Add(new()
            {
                Id = id++,
                LookupMasterId = LookupMasterSeeder.PKValue[LookupMasterInternalName.DISTRICT],
                IsActive = true,
                CreatedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                DependentLookupDetailId = keralaId,
                Value = "Alappuzha"
            });
            lookupDetails.Add(new()
            {
                Id = id++,
                LookupMasterId = LookupMasterSeeder.PKValue[LookupMasterInternalName.DISTRICT],
                IsActive = true,
                CreatedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                DependentLookupDetailId = keralaId,
                Value = "Kollam"
            });
            lookupDetails.Add(new()
            {
                Id = id++,
                LookupMasterId = LookupMasterSeeder.PKValue[LookupMasterInternalName.DISTRICT],
                IsActive = true,
                CreatedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                DependentLookupDetailId = keralaId,
                Value = "Kottayam"
            });
            lookupDetails.Add(new()
            {
                Id = id++,
                LookupMasterId = LookupMasterSeeder.PKValue[LookupMasterInternalName.DISTRICT],
                IsActive = true,
                CreatedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                DependentLookupDetailId = keralaId,
                Value = "Pathanamthitta"
            });
            lookupDetails.Add(new()
            {
                Id = id++,
                LookupMasterId = LookupMasterSeeder.PKValue[LookupMasterInternalName.DISTRICT],
                IsActive = true,
                CreatedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                DependentLookupDetailId = keralaId,
                Value = "Thiruvananthapuram"
            });
            lookupDetails.Add(new()
            {
                Id = id++,
                LookupMasterId = LookupMasterSeeder.PKValue[LookupMasterInternalName.DISTRICT],
                IsActive = true,
                CreatedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                DependentLookupDetailId = keralaId,
                Value = "Wayanad"
            });

            lookupDetails.Add(new()
            {
                Id = id++,
                LookupMasterId = LookupMasterSeeder.PKValue[LookupMasterInternalName.DISTRICT],
                IsActive = true,
                CreatedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                DependentLookupDetailId = tamilNaduId,
                Value = "Coimbatore"
            });
            lookupDetails.Add(new()
            {
                Id = id,
                LookupMasterId = LookupMasterSeeder.PKValue[LookupMasterInternalName.DISTRICT],
                IsActive = true,
                CreatedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 08, 14, 12, 0, 0, 0, DateTimeKind.Utc),
                DependentLookupDetailId = tamilNaduId,
                Value = "Chennai"
            });

            foreach (var seed in lookupDetails)
            {
                modelBuilder.Entity<LookupDetail>().HasData(seed);
            }
        }
    }
}
