using Connect4.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.AspNetCore.Identity;
using IdentityServer4.EntityFramework.Extensions;
using Connect4.Extensions;
using System.Collections.Generic;

namespace Connect4.Data
{
    public class ApplicationDbContext : KeyApiAuthorizationDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<PlayerGameState> PlayerStates { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Game
            //builder.Entity<Game>().HasMany<ApplicationUser>(p => p.Players);
            builder.Entity<Game>().Property(p => p.CardDrawPile).HasConversion(ConversionExtension.DbIntListConverter());
            builder.Entity<Game>().Property(p => p.CardDiscardPile).HasConversion(ConversionExtension.DbIntListConverter());
            builder.Entity<Game>().Property(p => p.GameBoard).HasConversion(ConversionExtension.JsonConverter<BoardTile[,]>());
            builder.Entity<Game>().Property(p => p.Players).HasConversion(ConversionExtension.JsonConverter<PlayerGameState[]>());

            //PlayerGameState
            //builder.Entity<PlayerGameState>().HasOne<Game>(p => p.Game).WithMany(prop => prop.PlayerStates);
            //builder.Entity<PlayerGameState>().HasOne<ApplicationUser>(p => p.Player);
            //builder.Entity<PlayerGameState>().Property(p => p.Cards).HasConversion(ConversionExtension.DbIntListConverter());

            //ApplicationUser aka player
            builder.Entity<ApplicationUser>().HasMany<Game>(p => p.CurrentGames);

            /* TODO: Refactor and have the models themselves create ther conversion
            var parameters = new object[] { builder };
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var method = entityType.ClrType.GetMethod("OnModelCreating", BindingFlags.Static | BindingFlags.NonPublic);
                if (method != null)
                    method.Invoke(null, parameters);
            }
            */
        }
    }

    public class KeyApiAuthorizationDbContext<TUser, TRole, TKey> : IdentityDbContext<TUser, TRole, TKey>, IPersistedGrantDbContext
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly IOptions<OperationalStoreOptions> _operationalStoreOptions;
        public DbSet<PersistedGrant> PersistedGrants { get; set; }
        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }

        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();
        public KeyApiAuthorizationDbContext(DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options)
        {
            _operationalStoreOptions = operationalStoreOptions;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ConfigurePersistedGrantContext(_operationalStoreOptions.Value);
        }
    }
}
