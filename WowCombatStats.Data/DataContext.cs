using Microsoft.EntityFrameworkCore;
using WowCombatStats.Models.DataModels;
using File = WowCombatStats.Models.DataModels.File;

namespace WowCombatStats.Data;

public class DataContext : DbContext
{
    public DbSet<Boss> Bosses { get; set; }
    public DbSet<ClassType> ClassTypes { get; set; }
    public DbSet<FactionType> FactionTypes { get; set; }
    public DbSet<File> Files { get; set; }
    public DbSet<Gear> Gears { get; set; }
    public DbSet<GenderType> GenderTypes { get; set; }
    public DbSet<Guild> Guilds { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<ItemType> ItemTypes { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<PlayerAttr> PlayerAttrs { get; set; }
    public DbSet<RaceType> RaceTypes { get; set; }
    public DbSet<Raid> Raids { get; set; }
    public DbSet<RaidComposition> RaidCompositions { get; set; }
    public DbSet<RaidLoot> RaidLoots { get; set; }
    public DbSet<RaidType> RaidTypes { get; set; }
    public DbSet<RarityType> RarityTypes { get; set; }
    public DbSet<Server> Servers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<VersionType> VersionTypes { get; set; }
    public DbSet<PlayerStatType> PlayerStatType { get; set; }
    public DbSet<PlayerStat> PlayerStats { get; set; }

    public DataContext()
    {

    }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Dictionaries

        modelBuilder.Entity<ClassType>(etp =>
        {
            etp.HasKey(c => c.ClassTypeId);
            etp.Property(c => c.ClassTypeId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<RaceType>(etp =>
        {
            etp.HasKey(r => r.RaceTypeId);
            etp.Property(r => r.RaceTypeId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<FactionType>(etp =>
        {
            etp.HasKey(f => f.FactionTypeId);
            etp.Property(f => f.FactionTypeId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<GenderType>(etp =>
        {
            etp.HasKey(g => g.GenderTypeId);
            etp.Property(g => g.GenderTypeId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<RaidType>(etp =>
        {
            etp.HasKey(r => r.RaidTypeId);
            etp.Property(r => r.RaidTypeId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<ItemType>(etp =>
        {
            etp.HasKey(i => i.ItemTypeId);
            etp.Property(i => i.ItemTypeId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<RarityType>(etp =>
        {
            etp.HasKey(r => r.RarityTypeId);
            etp.Property(r => r.RarityTypeId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<VersionType>(etp =>
        {
            etp.HasKey(v => v.VersionTypeId);
            etp.Property(v => v.VersionTypeId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Guild>(etp =>
        {
            etp.HasKey(g => g.GuildId);
            etp.Property(g => g.GuildId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<PlayerStatType>(etp =>
        {
            etp.HasKey(psti => psti.PlayerStatTypeId);
            etp.Property(psti => psti.PlayerStatTypeId).ValueGeneratedOnAdd();
        });

        #endregion

        #region Entities

        modelBuilder.Entity<Boss>(etp =>
        {
            etp.HasKey(b => b.BossId);
            etp.Property(b => b.BossId).ValueGeneratedOnAdd();

            etp.HasOne<RaidType>().WithMany().HasForeignKey(b => b.RaidTypeId);
        });

        modelBuilder.Entity<File>(etp =>
        {
            etp.HasKey(f => f.FileId);
            etp.Property(f => f.FileId).ValueGeneratedOnAdd();

            etp.HasOne<User>().WithMany().HasForeignKey(f => f.UserId);
            etp.HasOne<Raid>().WithMany().HasForeignKey(f => f.RaidId);
        });

        modelBuilder.Entity<Gear>(etp =>
        {
            etp.HasKey(g => g.GearId);
            etp.Property(g => g.GearId).ValueGeneratedOnAdd();

            etp.HasOne<Player>().WithMany().HasForeignKey(g => g.PlayerId);
        });

        modelBuilder.Entity<Item>(etp =>
        {
            etp.HasKey(i => i.ItemId);
            etp.Property(i => i.ItemId).ValueGeneratedOnAdd();

            etp.HasOne<RarityType>().WithMany().HasForeignKey(i => i.RarityTypeId);
            etp.HasOne<ItemType>().WithMany().HasForeignKey(i => i.ItemTypeId);
        });

        modelBuilder.Entity<Player>(etp =>
        {
            etp.HasKey(p => p.PlayerId);
            etp.Property(p => p.PlayerId).ValueGeneratedOnAdd();
            etp.HasIndex(p => p.PlayerName).IsUnique();

            etp.HasOne<RaceType>().WithMany().HasForeignKey(p => p.RaceTypeId);
            etp.HasOne<ClassType>().WithMany().HasForeignKey(p => p.ClassTypeId);
            etp.HasOne<FactionType>().WithMany().HasForeignKey(p => p.FactionId);
            etp.HasOne<Guild>().WithMany().HasForeignKey(p => p.GuildId);
            etp.HasOne<GenderType>().WithMany().HasForeignKey(p => p.GenderTypeId);
            // 1 to 1
            etp.HasOne(p => p.PlayerAttr)
                .WithOne(pa => pa.Player)
                .HasForeignKey<PlayerAttr>(f => f.PlayerId);
        });

        modelBuilder.Entity<PlayerAttr>(etp =>
        {
            etp.HasKey(pa => pa.PlayerAttrId);
            etp.Property(pa => pa.PlayerAttrId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<PlayerStat>(etp =>
        {
            etp.HasKey(ps => ps.PlayerStatId);
            etp.Property(ps => ps.PlayerStatId).ValueGeneratedOnAdd();

            etp.HasOne<Raid>().WithMany().HasForeignKey(p => p.RaidId);
            etp.HasOne<Player>().WithMany().HasForeignKey(p => p.PlayerId);
            etp.HasOne<PlayerStatType>().WithMany().HasForeignKey(p => p.PlayerStatTypeId);
        });

        modelBuilder.Entity<Raid>(etp =>
        {
            etp.HasKey(r => r.RaidId);
            etp.Property(r => r.RaidId).ValueGeneratedOnAdd();

            etp.HasOne<RaidType>().WithMany().HasForeignKey(r => r.RaidTypeId);
            etp.HasOne<Server>().WithMany().HasForeignKey(r => r.ServerId);
        });

        modelBuilder.Entity<RaidComposition>(etp =>
        {
            etp.HasKey(rc => rc.RaidCompositionId);
            etp.Property(rc => rc.RaidCompositionId).ValueGeneratedOnAdd();

            etp.HasOne<Raid>().WithMany().HasForeignKey(rc => rc.RaidId);
            etp.HasOne<Player>().WithMany().HasForeignKey(rc => rc.PlayerId);
        });

        modelBuilder.Entity<RaidLoot>(etp =>
        {
            etp.HasKey(r => r.RaidLootId);
            etp.Property(r => r.RaidLootId).ValueGeneratedOnAdd();

            etp.HasOne<Raid>().WithMany().HasForeignKey(rl => rl.RaidId);
            etp.HasOne<Player>().WithMany().HasForeignKey(rl => rl.PlayerId);
        });

        modelBuilder.Entity<Server>(etp =>
        {
            etp.HasKey(s => s.ServerId);
            etp.Property(s => s.ServerId).ValueGeneratedOnAdd();

            etp.HasOne<VersionType>().WithMany().HasForeignKey(s => s.VersionTypeId);
        });

        modelBuilder.Entity<User>(etp =>
        {
            etp.HasKey(u => u.UserId);
            etp.Property(u => u.UserId).ValueGeneratedOnAdd();

            etp.HasIndex(u => u.UserName).IsUnique();
            etp.HasIndex(u => u.Email).IsUnique();
        });
        #endregion

        base.OnModelCreating(modelBuilder);
    }
}