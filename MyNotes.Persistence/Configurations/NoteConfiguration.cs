using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyNotes.Persistence.Entities;


namespace MyNotes.Persistence.Configurations
{
    public partial class NoteConfiguration : IEntityTypeConfiguration<NoteEntity>
    {
        public void Configure(EntityTypeBuilder<NoteEntity> builder)
        {
            builder.HasKey(n => n.Id);

            builder.HasOne(u => u.User)
                .WithMany(n => n.Notes);
        }
    }
}
