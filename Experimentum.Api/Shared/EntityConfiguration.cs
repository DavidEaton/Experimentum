using Experimentum.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Experimentum.Api.Shared
{
    public class EntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : Entity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
        }
    }
}
