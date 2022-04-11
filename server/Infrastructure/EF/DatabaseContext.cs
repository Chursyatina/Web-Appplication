namespace Infrastructure.EF
{
    using Domain.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class DatabaseContext : IdentityDbContext<User>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
                 : base(options)
        {
        }

        public DbSet<PizzaVariation> PizzasVariations { get; set; }

        public DbSet<OrderLine> OrderLines { get; set; }

        public DbSet<Dough> Doughs { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<Order> Orders { get; set; }

        public virtual DbSet<Pizza> Pizzas { get; set; }

        public DbSet<Size> Sizes { get; set; }

        public DbSet<OrderStatus> OrderStatuses { get; set; }

        public DbSet<AdditionalIngredient> AdditionalIngredients { get; set; }

        public DbSet<Basket> Baskets { get; set; }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasOne(b => b.Basket)
                .WithOne(u => u.User)
                .HasForeignKey<User>(p => p.Id);
        }
    }
}
