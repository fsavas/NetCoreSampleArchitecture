using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Core.Domain.Users;

namespace Sample.Data.Mapping.Users
{
    public partial class UserRoleMappingMap : BaseEntityTypeConfiguration<UserRoleMapping>
    {
        #region Methods

        public override void Configure(EntityTypeBuilder<UserRoleMapping> builder)
        {
            builder.ToTable(nameof(UserRoleMapping));
            builder.HasKey(e => new { e.UserId, e.RoleId });

            builder.Property(e => e.UserId);
            builder.Property(e => e.RoleId);

            builder.HasOne(e => e.User)
                .WithMany(user => user.UserRoleMappings)
                .HasForeignKey(e => e.UserId)
                .IsRequired();

            builder.HasOne(e => e.Role)
                .WithMany()
                .HasForeignKey(e => e.RoleId)
                .IsRequired();

            builder.HasAlternateKey(e => new { e.UserId, e.RoleId });

            builder.Ignore(e => e.Id);

            base.Configure(builder);
        }

        #endregion Methods
    }
}