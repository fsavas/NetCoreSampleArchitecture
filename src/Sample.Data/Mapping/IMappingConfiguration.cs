using Microsoft.EntityFrameworkCore;

namespace Sample.Data.Mapping
{
    public partial interface IMappingConfiguration
    {
        void ApplyConfiguration(ModelBuilder modelBuilder);
    }
}