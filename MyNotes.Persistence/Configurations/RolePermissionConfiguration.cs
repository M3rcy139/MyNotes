using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyNotes.Core.Enums;
using MyNotes.Persistence.Entities;

namespace MyNotes.Persistence.Configurations
{
    public partial class RolePermissionConfiguration 
        : IEntityTypeConfiguration<RolePermissionEntity>
    {

        public void Configure(EntityTypeBuilder<RolePermissionEntity> builder)
        {
            builder.HasKey(r => new { r.RoleId, r.PermissionId });

            builder.HasData(Create(Role.Admin, Permission.Create),
                    Create(Role.Admin, Permission.Read),
                    Create(Role.Admin, Permission.Update),
                    Create(Role.Admin, Permission.Delete),

                    Create(Role.User, Permission.Read));
        }

        private RolePermissionEntity Create(Role role, Permission permission)
            => new()
            {
                RoleId = (int)role,
                PermissionId = (int)permission
            };
    }
}
