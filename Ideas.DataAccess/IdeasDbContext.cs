using Ideas.DataAccess.Entities;
using Ideas.DataAccess.Entities.Identity;
using Ideas.DataAccess.EntitiesConfig;
using Ideas.DataAccess.EntitiesConfig.Identity;
using Ideas.DataAccess.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ideas.DataAccess
{
    public class IdeasDbContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<IdeaCategory> IdeaCategories { get; set; }

        public DbSet<IdeaSubcategory> IdeaSubcategories { get; set; }

        public DbSet<Idea> Ideas { get; set; }

        public DbSet<AssignedIdeaSubcategory> AssignedIdeaSubcategories { get; set; }

        public DbSet<ApiClient> ApiClients { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }


        public IdeasDbContext(DbContextOptions<IdeasDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //configure identity entities
            builder.AddConfiguration<User, UserConfig>();
            builder.AddConfiguration<Role, RoleConfig>();
            builder.AddConfiguration<IdentityUserClaim<int>, IdentityUserClaimConfig>();
            builder.AddConfiguration<IdentityUserLogin<int>, IdentityUserLoginConfig>();
            builder.AddConfiguration<IdentityRoleClaim<int>, IdentityRoleClaimConfig>();
            builder.AddConfiguration<IdentityUserRole<int>, IdentityUserRoleConfig>();
            builder.AddConfiguration<IdentityUserToken<int>, IdentityUserTokenConfig>();

            //configure domain entities
            builder.AddConfiguration<IdeaCategory, IdeaCategoryConfig>();
            builder.AddConfiguration<IdeaSubcategory, IdeaSubcategoryConfig>();
            builder.AddConfiguration<Idea, IdeaConfig>();
            builder.AddConfiguration<AssignedIdeaSubcategory, AssignedIdeaSubcategoryConfig>();
            builder.AddConfiguration<ApiClient, ApiClientConfig>();
            builder.AddConfiguration<RefreshToken, RefreshTokenConfig>();
        }
    }
}
