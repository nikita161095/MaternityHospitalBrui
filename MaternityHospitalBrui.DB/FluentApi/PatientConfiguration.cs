using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MaternityHospitalBrui.DB.Entities;
using MaternityHospitalBrui.States;

namespace MaternityHospitalBrui.DB.FluentApi
{
    internal class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.NameId);
            builder.Property(x => x.Gender);
            builder.Property(x => x.BirthDate).IsRequired();
            builder.Property(x => x.Active);
        }
    }
}