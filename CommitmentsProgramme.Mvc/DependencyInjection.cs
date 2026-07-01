using CommitmentsProgramme.Utilities.Extensions;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace CommitmentsProgramme.Mvc;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddControllersWithViews();

        //services.AddLocalizationConfig();
        services.AddAuthConfig(configuration);

        services
            .AddSessionConfig()
            .AddDatabaseConfig(configuration).
            AddMappsterrConfig();


        services.AddApplicationCookieConfig();


        // adding external authentication like google - github - facebook

        services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationClaimsPrincipalFactory>(); //Now every sign-in automatically includes these claims.

        services.AddSignalR();

        services.AddOptions<EmailSettings>()
                    .BindConfiguration(nameof(EmailSettings))
                    .ValidateDataAnnotations()
                    .ValidateOnStart();


        #region Adding Custome Services

        services.AddTransient<IEmailService, EmailService>();

        services.AddScoped<IDailyPlanService, DailyPlanService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<AuthService>();

        #endregion

        return services;
    }

   
    private static IServiceCollection AddAuthConfig(this IServiceCollection services, IConfiguration configuration)
    {
        //services.ConfigureApplicationCookie(

        services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
        /* enabling features like: password reset,email confirmation,change email,2FA codes*/

        services.Configure<IdentityOptions>(options =>
        {

            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedAccount = false;
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;

            options.Password.RequiredLength = 8;
            options.SignIn.RequireConfirmedEmail = false;  
            options.User.RequireUniqueEmail = true;

            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 3;
            options.Lockout.AllowedForNewUsers = true;


        });




        return services;
    }

    private static IServiceCollection AddDatabaseConfig(this IServiceCollection services, IConfiguration configuration)
    {

        var constr = configuration.GetConnectionString("constr") ??
            throw new InvalidOperationException("There is no Connection String For The 'constr' Key ");

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(constr);
        });

        return services;
    }

    private static IServiceCollection AddSessionConfig(this IServiceCollection services)
    {
        services.AddSession();
        services.AddHttpContextAccessor();
        services.AddDistributedMemoryCache(); // if any error happend at client , caching the session info at server not client

        return services;
    }

    private static IServiceCollection AddApplicationCookieConfig(this IServiceCollection services)
    {
        services.ConfigureApplicationCookie(option =>
        {
            option.LoginPath = "/Auth/Login";
            option.AccessDeniedPath = "/Auth/AccessDenied";

            option.Cookie.Name = "Cookie";
            option.Cookie.HttpOnly = true;
            option.ExpireTimeSpan = TimeSpan.FromDays(7);
            option.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            option.SlidingExpiration = true;
        });

        return services;
    }



    private static IServiceCollection AddMappsterrConfig(this IServiceCollection services)
    {
        var mappingConfigurations = TypeAdapterConfig.GlobalSettings;
        mappingConfigurations.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton<IMapper>(new Mapper(mappingConfigurations));

        return services;
    }
}

