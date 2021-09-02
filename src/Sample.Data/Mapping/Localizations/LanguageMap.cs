using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Core.Domain.Localizations;

namespace Sample.Data.Mapping.Localizations
{
    public partial class LanguageMap : BaseEntityTypeConfiguration<Language>
    {
        #region Methods

        public override void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.ToTable(nameof(Language));
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
            builder.Property(e => e.Culture).HasMaxLength(100).IsRequired();
            builder.Property(e => e.IsDeleted);

            builder.HasAlternateKey(e => new { e.Name, e.Culture });

            base.Configure(builder);
        }

        #endregion Methods
    }
}