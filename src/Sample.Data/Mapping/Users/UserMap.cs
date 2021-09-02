using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Core.Domain.Users;

namespace Sample.Data.Mapping.Users
{
    public partial class UserMap : BaseEntityTypeConfiguration<User>
    {
        #region Methods

        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Username).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Key).IsRequired();
            builder.Property(e => e.Salt).IsRequired();
            builder.Property(e => e.IsDeleted);

            builder.Ignore(e => e.Password);
            builder.Ignore(e => e.Roles);

            builder.HasAlternateKey(e => e.Username);

            base.Configure(builder);
        }

        #endregion Methods
    }
}