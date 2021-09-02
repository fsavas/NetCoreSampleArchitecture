using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Core.Domain.Security;

namespace Sample.Data.Mapping.Security
{
    public partial class RoleMap : BaseEntityTypeConfiguration<Role>
    {
        #region Methods

        public override void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable(nameof(Role));
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Code).HasMaxLength(250);
            builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
            builder.Property(e => e.IsDeleted);

            builder.HasAlternateKey(e => e.Code);

            base.Configure(builder);
        }

        #endregion Methods
    }
}