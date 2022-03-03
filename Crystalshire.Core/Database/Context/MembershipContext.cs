using Microsoft.EntityFrameworkCore;

using Crystalshire.Core.Model.Accounts;
using Crystalshire.Core.Model.Characters;

namespace Crystalshire.Core.Database.Context {
    public class MembershipContext : DbContext {
        public DbSet<Account>? Accounts { get; set; }
        public DbSet<AccountAuthentication>? AccountAuthentication { get; set; }
        public DbSet<AccountCupom>? AccountCupom { get; set; }
        public DbSet<AccountLock>? AccountLock { get; set; }
        public DbSet<AccountService>? AccountService { get; set; }

        public DbSet<Character>? Characters { get; set; }
        public DbSet<CharacterAchievement>? CharacterAchievement { get; set; }
        public DbSet<CharacterAchievementProgress>? CharacterAchievementProgress { get; set; }
        public DbSet<CharacterAppearance>? CharacterAppearance { get; set; }
        public DbSet<CharacterPrimaryAttribute>? CharacterPrimaryAttribute { get; set; }
        public DbSet<CharacterAttributeEffect>? CharacterAttributeEffect { get; set; }
        public DbSet<CharacterCraft>? CharacterCraft { get; set; }
        public DbSet<CharacterCurrency>? CharacterCurrency { get; set; }
        public DbSet<CharacterDeleteRequest>? CharacterDeleteRequest { get; set; }
        public DbSet<CharacterEquipment>? CharacterEquipment { get; set; }
        public DbSet<CharacterHeraldry>? CharacterHeraldry { get; set; }
        public DbSet<CharacterInventory>? CharacterInventory { get; set; }
        public DbSet<CharacterMail>? CharacterMail { get; set; }
        public DbSet<CharacterMailAttachItem>? CharacterMailAttachItem { get; set; }
        public DbSet<CharacterPassive>? CharacterPassive { get; set; }
        public DbSet<CharacterQuickSlot>? CharacterQuickSlot { get; set; }
        public DbSet<CharacterRecipe>? CharacterRecipe { get; set; }
        public DbSet<CharacterSettings>? CharacterSettings { get; set; }
        public DbSet<CharacterSkill>? CharacterSkill { get; set; }
        public DbSet<CharacterTitle>? CharacterTitle { get; set; }
        public DbSet<CharacterVital>? CharacterVital { get; set; }
        public DbSet<CharacterWarehouse>? CharacterWarehouse { get; set; }

        public MembershipContext(DbContextOptions<MembershipContext> options) : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Account>().Property(p => p.AccountKey).IsRequired(required: false);
            modelBuilder.Entity<Character>().Property(p => p.LastLoginDate).HasDefaultValue(new DateTime(1990, 1, 1));
            modelBuilder.Entity<Character>().Property(p => p.LastLogoutDate).HasDefaultValue(new DateTime(1990, 1, 1));
          //  modelBuilder.Entity<Character>().Property(p => p.X).HasConversion(x => x, x => Convert.ToSingle(x));
        }
    }
}