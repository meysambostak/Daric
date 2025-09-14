//namespace DaricTemplate.Infra.Data.Commands.Common;

//public static class SqlServiceCollectionExtensions
//{
//    public static void UseConfiguredMsSql(
//        this DbContextOptionsBuilder optionsBuilder, SiteSettings siteSettings, IServiceProvider serviceProvider)
//    {
//        if (optionsBuilder == null)
//        {
//            throw new ArgumentNullException(nameof(optionsBuilder));
//        }

//        if (siteSettings == null)
//        {
//            throw new ArgumentNullException(nameof(siteSettings));
//        }

//        var connectionString = siteSettings.ConnectionStrings.SqlServer.ApplicationDbContextConnection;
//        optionsBuilder.UseSqlServer(
//            connectionString,
//            sqlServerOptionsBuilder =>
//            {
//                sqlServerOptionsBuilder.CommandTimeout((int)TimeSpan.FromMinutes(3).TotalSeconds);
//                sqlServerOptionsBuilder.EnableRetryOnFailure(10,
//                    TimeSpan.FromSeconds(7),
//                    null);
//                sqlServerOptionsBuilder.MigrationsAssembly(typeof(SqlServiceCollectionExtensions).Assembly.FullName);
//            });

//        optionsBuilder.AddInterceptors(
//            new PersianYeKeCommandInterceptor(),
//            serviceProvider.GetRequiredService<AddOutBoxEventItemInterceptor>(),
//            serviceProvider.GetRequiredService<AuditableEntitiesInterceptor>()

//            );


//        optionsBuilder.ConfigureWarnings(warnings =>
//        {
//            warnings.Log(
//                (CoreEventId.LazyLoadOnDisposedContextWarning, LogLevel.Warning),
//                (CoreEventId.DetachedLazyLoadingWarning, LogLevel.Warning),
//                (CoreEventId.ManyServiceProvidersCreatedWarning, LogLevel.Warning),
//                (CoreEventId.SensitiveDataLoggingEnabledWarning, LogLevel.Information)
//            );
//        });
//        optionsBuilder.EnableSensitiveDataLogging().EnableDetailedErrors();
//    }

//}
