using BigOferta.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BigOferta.API.Data
{
    public class DataContext : IdentityDbContext<User, Role, int,
        IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<UserOffer> UserOffers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>(userRole => {
                userRole.HasKey(ur => new {ur.UserId, ur.RoleId});

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.UserFeedbacks)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(m => m.Offer)
                .WithMany(o => o.Feedbacks)
                .HasForeignKey(m => m.OfferId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Offer>()
                .HasMany(o => o.Album)
                .WithOne(p => p.Offer)
                .HasForeignKey(p => p.OfferId);

            builder.Entity<User>()
                .HasOne(u => u.ProfilePhoto)
                .WithOne(p => p.User)
                .HasForeignKey<Photo>(p => p.UserId);
      
            builder.Entity<PurchaseOrder>()
                .HasOne(po => po.User)
                .WithOne(u => u.PurchaseOrder)
                .HasForeignKey<PurchaseOrder>(po => po.UserId);

            builder.Entity<UserOffer>(userOffer => {
                userOffer.HasKey(uf => new { uf.UserId, uf.OfferId });

                userOffer.HasOne(uf => uf.User)
                    .WithMany(u => u.OffersCart)
                    .HasForeignKey(uf => uf.UserId)
                    .IsRequired();

                userOffer.HasOne(uf => uf.Offer)
                    .WithMany(o => o.InterestedUsers)
                    .HasForeignKey(uf => uf.OfferId)
                    .IsRequired();
            });
                
        }

    }
}