using DevStore.Domain;
using DevStore.Infra.Mappings;
using System.Data.Entity;

namespace DevStore.DataAccess.DataContexts
{
    public class DevStoreDataContext : DbContext
    {
        public DevStoreDataContext() : 
            base("DevStoreConnectionString")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer<DevStoreDataContext>(new DevStoreDataContextInitializer());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public class DevStoreDataContextInitializer : DropCreateDatabaseIfModelChanges<DevStoreDataContext>
        {
            protected override void Seed(DevStoreDataContext context)
            {
                context.Categories.Add(new Category { Id = 1, Title = "Informática" });
                context.Categories.Add(new Category { Id = 2, Title = "Games" });
                context.Categories.Add(new Category { Id = 3, Title = "Papelaria" });
                context.SaveChanges();

                context.Products.Add(new Product { Id = 1, Title = "Mouse Logitech G620", Price = 100.00M, isActive = true, CategoryId = 1 });
                context.Products.Add(new Product { Id = 2, Title = "Notebook Gamer Acer FF517", Price = 4989.90M, isActive = true, CategoryId = 2 });
                context.Products.Add(new Product { Id = 3, Title = "Placa de Vídeo GTX 1080TI", Price = 2450.60M, isActive = true, CategoryId = 2 });
                context.Products.Add(new Product { Id = 4, Title = "Caneta Razer Kraken", Price = 50M, isActive = false, CategoryId = 3 });
                context.Products.Add(new Product { Id = 5, Title = "Caderno Tilibra The PowerPuff Girls", Price = 16.40M, isActive = true, CategoryId = 3 });
                context.SaveChanges();

                base.Seed(context);
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //remove plural dos nomes das tabelas
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
