using CompanyWebApp.Domain;
using CompanyWebApp.Domain.Repositories.Abstract;
using CompanyWebApp.Domain.Repositories.EntityFramework;
using CompanyWebApp.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Serilog;



namespace CompanyWebApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            //Подключаем в конфигурацию файл appsettings.json
            IConfigurationBuilder configBuild = new ConfigurationBuilder()
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            //Оборачиваем секцию Project в объектную форму для удобства
            IConfiguration configuration = configBuild.Build();
            AppConfig config = configuration.GetSection("Project").Get<AppConfig>()!;

            //Подключаем контекст БД
            builder.Services.AddDbContext<AppDbContext>(x => x.UseNpgsql(config.Database.ConnectionString)
                //На момент создания сайта в данной версии EF есть баг, поэтому подавляем предупреждения
                .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning)));

            builder.Services.AddTransient<IServiceCategoriesRepository, EFServiceCategoriesRepository>();
            builder.Services.AddTransient<IServicesRepository, EFServicesRepository>();
            builder.Services.AddTransient<IServiceTypesRepository, EFServiceTypesRepository>();
            builder.Services.AddTransient<DataManager>();

            //Настраиваем Identity систему
            builder.Services.AddIdentity<IdentityUser,  IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            //Настраиваем Auth cookie
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "CompanyWebAppAuth";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "/admin/accessdenied";
                options.SlidingExpiration = true;
            });

            //Подключаем функционал контроллеров
            builder.Services.AddControllersWithViews();

            //Подключаем логи
            builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

            //Собираем конфигурацию 
            WebApplication app = builder.Build();

            //Порядок следования middleware очень важен, они будут выполняться согласно нему

            //Сразу же используем логирование
            app.UseSerilogRequestLogging();

            //Далее подключаем обработку исключений
            if(app.Environment.IsDevelopment())
                app.UseDeveloperExceptionPage();

            //Подключаем использование статичных файлов (js, css, любых)
            app.UseStaticFiles();

            //Подключаем систему маршрутизации
            app.UseRouting();

            //Подключаем аутентификацию и авторизацию
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            //Регистрируем нужные нам маршруты
            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

            await app.RunAsync();
        }
    }
}
