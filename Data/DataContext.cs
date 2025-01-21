using Portfolio_Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Portfolio_Backend.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<AppContent> Content {get; set;}

    public DbSet<AppContentList> ContentList {get; set;}
    public DbSet<AppContentListContent> ContentListContent {get; set;}

    public DbSet<AppUser> User {get; set;}
    public DbSet<AppInquiry> Inquiry {get; set;}
}