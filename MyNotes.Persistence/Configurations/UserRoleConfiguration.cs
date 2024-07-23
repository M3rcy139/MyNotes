using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyNotes.Persistence.Entities;

namespace MyNotes.Persistence.Configurations
{
    public partial class UserRoleConfiguration : IEntityTypeConfiguration<UserRoleEntity>
    {
        public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
        {
            builder.HasKey(r => new { r.UserId, r.RoleId });
        }
    }
}
