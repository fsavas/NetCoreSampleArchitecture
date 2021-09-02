using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Core.Domain.Localizations;

namespace Sample.Data.Mapping.Localizations
{
    public partial class LocaleResourceMap : BaseEntityTypeConfiguration<LocaleResource>
    {
        #region Methods

        public override void Configure(EntityTypeBuilder<LocaleResource> builder)
        {
            builder.ToTable(nameof(LocaleResource));
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name).HasMaxLength(400).IsRequired();
            builder.Property(e => e.Value).IsRequired();

            builder.HasOne(e => e.Language)
                .WithMany()
                .HasForeignKey(e => e.LanguageId)
                .IsRequired();

            builder.HasAlternateKey(e => new { e.Name, e.LanguageId });

            base.Configure(builder);
        }

        #endregion Methods
    }
}