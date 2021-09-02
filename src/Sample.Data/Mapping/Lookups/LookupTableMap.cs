using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Core.Domain.Lookups;

namespace Sample.Data.Mapping.Lookups
{
    public partial class LookupTableMap : BaseEntityTypeConfiguration<LookupTable>
    {
        #region Methods

        public override void Configure(EntityTypeBuilder<LookupTable> builder)
        {
            builder.ToTable(nameof(LookupTable));
            builder.HasKey(e => e.Id);

            builder.Property(e => e.LookupType).IsRequired();
            builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
            //builder.Property(e => e.Value).IsRequired();
            builder.Property(e => e.Description).IsRequired().HasMaxLength(100);
            builder.Property(e => e.IsActive);

            builder.HasAlternateKey(e => new { e.LookupType, e.Name });

            base.Configure(builder);
        }

        #endregion Methods
    }
}