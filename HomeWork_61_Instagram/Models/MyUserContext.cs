using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HomeWork_61_Instagram.Models;

public class MyUserContext : IdentityDbContext<MyUser, IdentityRole<int>, int>
{
    public DbSet<Post> Posts { get; set; }
    public  DbSet<UserFollow> UserFollows { get; set; }
    public DbSet<Reactions> Reactions { get; set; }
    public DbSet<Comments> Comments { get; set; }
    
    public MyUserContext(DbContextOptions<MyUserContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);    
        
        builder.Entity<UserFollow>()
            .HasOne(uf => uf.Follower)
            .WithMany(u => u.Following)
            .HasForeignKey(uf => uf.FollowerId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.Entity<UserFollow>()
            .HasOne(uf => uf.Followed)
            .WithMany(u => u.Followers)
            .HasForeignKey(uf => uf.FollowedId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
