namespace WebApi
{
    using System;
    using System.Threading.Tasks;
    using Application.AutoMapper;
    using Application.Interfaces;
    using Application.Interfaces.ServicesInterfaces;
    using Application.Services;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.EF;
    using Infrastructure.Repository;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlServer().AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConntection")));

            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<DatabaseContext>();

            services.AddControllersWithViews();

            services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });

            services.AddScoped<IPizzaService, PizzaService>();
            services.AddScoped<IPizzaRepository, PizzaRepository>();

            services.AddScoped<IIngredientService, IngredientService>();
            services.AddScoped<IIngredientRepository, IngredientRepository>();

            services.AddScoped<ISizeService, SizeService>();
            services.AddScoped<ISizeRepository, SizeRepository>();

            services.AddScoped<IDoughService, DoughService>();
            services.AddScoped<IDoughRepository, DoughRepository>();

            services.AddScoped<IPizzaVariationService, PizzaVariationService>();
            services.AddScoped<IPizzaVariationRepository, PizzaVariationRepository>();

            services.AddScoped<IAdditionalIngredientService, AdditionalIngredientService>();
            services.AddScoped<IAdditionalIngredientRepository, AdditionalIngredientRepository>();

            services.AddScoped<IOrderLineService, OrderLineService>();
            services.AddScoped<IOrderLineRepository, OrderLineRepository>();

            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<IOrderStatusService, OrderStatusService>();
            services.AddScoped<IOrderStatusRepository, OrderStatusRepository>();

            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IBasketRepository, BasketRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DatabaseContext context, IServiceProvider services)
        {
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            context.Initialize();

            CreateUserRoles(services).Wait();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            AutoMapper.Initialize();
        }

        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new
               IdentityRole("admin"));
            }

            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }

            string adminEmail = "admin@mail.com";
            string adminPhone = "88888888888";
            string adminPassword = "Aa123456!";
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User
                {
                    Email = adminEmail,
                    Phone = adminPhone,
                    Name = "Admin",
                    Password = adminPassword,
                    UserName = adminPhone,
                    Basket = new Basket(),
                    Coins = 0,
                };
                IdentityResult result = await

                // userManager.CreateAsync(admin, adminPassword);
                userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }

            string userEmail = "user@mail.com";
            string userPhone = "89999999999";
            string userPassword = "Aa123456!";
            if (await userManager.FindByNameAsync(userEmail) == null)
            {
                User user = new User
                {
                    Email = userEmail,
                    Phone = userPhone,
                    Name = "Maksim",
                    Password = userPassword,
                    UserName = userPhone,
                    Basket = new Basket(),
                    Coins = 0,
                };
                IdentityResult result = await

                // userManager.CreateAsync(user, userPassword);
                userManager.CreateAsync(user, userPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "user");
                }
            }
        }
    }
}
