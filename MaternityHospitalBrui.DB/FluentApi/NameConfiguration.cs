using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MaternityHospitalBrui.DB.Entities;
using MaternityHospitalBrui.States;

namespace MaternityHospitalBrui.DB.FluentApi
{
    internal class NameConfiguration : IEntityTypeConfiguration<Name>
    {
        public void Configure(EntityTypeBuilder<Name> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Use);
            builder.Property(x => x.Family).IsRequired();
            builder.Property(x => x.Given);
        }
    }
}