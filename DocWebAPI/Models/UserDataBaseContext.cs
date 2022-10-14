using Microsoft.EntityFrameworkCore;

namespace DocWebAPI.Models
{
    public partial class UserDataBaseContext : DbContext
    {
  
            public UserDataBaseContext()
            {
            }

            public UserDataBaseContext(DbContextOptions<UserDataBaseContext> options)
                : base(options)
            {
            }
            public virtual DbSet<User> Users { get; set; } = null!;

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
              
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<User>(entity =>
                {
                    entity.Property(e => e.Email)
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    entity.Property(e => e.FirstName)
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                   

                    entity.Property(e => e.LastName)
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    entity.Property(e => e.Password)
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);


                    entity.Property(e => e.PhoneNumber)
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsFixedLength(true);

                });

                OnModelCreatingPartial(modelBuilder);
            }
            partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        }
    }



