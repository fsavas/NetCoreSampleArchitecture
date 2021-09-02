using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Core.Domain.Security;

namespace Sample.Data.Mapping.Security
{
    public partial class RolePermissionMappingMap : BaseEntityTypeConfiguration<RolePermissionMapping>
    {
        #region Methods

        public override void Configure(EntityTypeBuilder<RolePermissionMapping> builder)
        {
            builder.ToTable(nameof(RolePermissionMapping));
            builder.HasKey(e => new { e.PermissionId, e.RoleId });

            builder.Property(e => e.PermissionId);
            builder.Property(e => e.RoleId);

            builder.HasOne(e => e.Role)
                .WithMany(role => role.RolePermissionMappings)
                .HasForeignKey(e => e.RoleId)
                .IsRequired();

            builder.HasOne(e => e.Permission)
                .WithMany(permission => permission.RolePermissionMappings)
                .HasForeignKey(e => e.PermissionId)
                .IsRequired();

            builder.HasAlternateKey(e => new { e.PermissionId, e.RoleId });

            builder.Ignore(e => e.Id);

            base.Configure(builder);
        }

        #endregion Methods
    }
}