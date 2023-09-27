using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using HonbunNoAnkiApi.Models;

namespace HonbunNoAnkiApi.DBContext
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<WordCollection> WordCollections { get; set; }
        public DbSet<WordDefinition> WordDefinitions { get; set; }
        public DbSet<Reading> Readings { get; set; }
        public DbSet<Meaning> Meanings { get; set; }
        public DbSet<MeaningValue> MeaningValues { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WordCollection>().HasOne(x => x.User)
                .WithMany(x => x.WordCollections)
                .HasForeignKey(x => x.User_ID);

            modelBuilder.Entity<Word>().HasOne(x => x.Stage)
                .WithMany(x => x.Words)
                .HasForeignKey(x => x.Stage_ID)
                .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Word>().HasOne(x => x.WordCollection)
                .WithMany(x => x.Words)
                .HasForeignKey(x => x.WordCollection_ID);

            modelBuilder.Entity<WordDefinition>().HasOne(x => x.Word)
                .WithMany(x => x.WordDefinitions)
                .HasForeignKey(x => x.Word_ID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Meaning>().HasOne(x => x.WordDefinition)
                .WithMany(x => x.Meanings)
                .HasForeignKey(x => x.WordDefinition_ID);
            modelBuilder.Entity<MeaningValue>().HasOne(x => x.Meaning)
                .WithMany(x => x.MeaningValues)
                .HasForeignKey(x => x.Meaning_ID);

            modelBuilder.Entity<WordDefinition>().HasOne(x => x.Reading)
                .WithOne(x => x.WordDefinition)
                .HasForeignKey<Reading>(x => x.WordDefinition_ID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
