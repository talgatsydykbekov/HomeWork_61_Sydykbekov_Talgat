using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HomeWork_61_Instagram.Models;

public class MyUserContext : IdentityDbContext<MyUser, IdentityRole<int>, int>
{
    public DbSet<Post> Posts { get; set; }
    
    public MyUserContext(DbContextOptions<MyUserContext> options) : base(options) { }

}
