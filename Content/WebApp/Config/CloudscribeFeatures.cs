using Microsoft.AspNetCore.Hosting;
#if (KvpCustomRegistration)
using cloudscribe.UserProperties.Models;
using cloudscribe.UserProperties.Services;
#endif
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CloudscribeFeatures
    {
        
        public static IServiceCollection SetupDataStorage(
            this IServiceCollection services,
            IConfiguration config,
            IHostingEnvironment env
            )
        {
#if (!NoDb && !SQLite)
            var connectionString = config.GetConnectionString("EntityFrameworkConnection");

#endif
#if (SQLite)
            var dbName = config.GetConnectionString("SQLiteDbName");
            var dbPath = Path.Combine(env.ContentRootPath, dbName);
            var connectionString = $"Data Source={dbPath}";
            services.AddCloudscribeCoreEFStorageSQLite(connectionString);
#endif
#if (!NoDb && !SQLite)

#if (MSSQL)
            services.AddCloudscribeCoreEFStorageMSSQL(connectionString);
#endif
#if (MySql)
            services.AddCloudscribeCoreEFStorageMySql(connectionString);
#endif
#if (pgsql)
            services.AddCloudscribeCorePostgreSqlStorage(connectionString);
#endif

#endif

#if(NoDb)
            services.AddCloudscribeCoreNoDbStorage();
#endif
#if (KvpCustomRegistration)
#if (NoDb)
            services.AddCloudscribeKvpNoDbStorage();
#endif
#if (SQLite)
            services.AddCloudscribeKvpEFStorageSQLite(connectionString);
#endif
#if (MSSQL)
            services.AddCloudscribeKvpEFStorageMSSQL(connectionString);
#endif
#if (MySql)
            services.AddCloudscribeKvpEFStorageMySql(connectionString);
#endif
#if (pgsql)
            services.AddCloudscribeKvpPostgreSqlStorage(connectionString);
#endif
#endif
#if (Logging)
#if (NoDb)
            services.AddCloudscribeLoggingNoDbStorage(config);
#endif
#if (SQLite)
            services.AddCloudscribeLoggingEFStorageSQLite(connectionString);
#endif
#if (MSSQL)
            services.AddCloudscribeLoggingEFStorageMSSQL(connectionString);
#endif
#if (MySql)
            services.AddCloudscribeLoggingEFStorageMySQL(connectionString);
#endif
#if (pgsql)
            services.AddCloudscribeLoggingPostgreSqlStorage(connectionString);
#endif
            
#endif
#if (SimpleContentConfig != "z")
#if (NoDb)
            services.AddNoDbStorageForSimpleContent();
#endif
#if (SQLite)
            services.AddCloudscribeSimpleContentEFStorageSQLite(connectionString);
#endif
#if (MSSQL)
            services.AddCloudscribeSimpleContentEFStorageMSSQL(connectionString);
#endif
#if (MySql)
            services.AddCloudscribeSimpleContentEFStorageMySQL(connectionString);
#endif
#if (pgsql)
            services.AddCloudscribeSimpleContentPostgreSqlStorage(connectionString);
#endif
#endif


#if (DynamicPolicy)
#if (NoDb)
            services.AddNoDbStorageForDynamicPolicies(config);
#endif
#if (SQLite)
            services.AddDynamicPolicyEFStorageSQLite(connectionString);
#endif
#if (MSSQL)
            services.AddDynamicPolicyEFStorageMSSQL(connectionString);
#endif
#if (MySql)
            services.AddDynamicPolicyEFStorageMySql(connectionString);
#endif
#if (pgsql)
            services.AddDynamicPolicyPostgreSqlStorage(connectionString);
#endif       
#endif

            return services;
        }

        public static IServiceCollection SetupCloudscribeFeatures(
            this IServiceCollection services,
            IConfiguration config
            )
        {

#if (Logging)
            services.AddCloudscribeLogging(config);

#endif
#if (KvpCustomRegistration)
            services.Configure<ProfilePropertySetContainer>(config.GetSection("ProfilePropertySetContainer"));
            services.AddCloudscribeKvpUserProperties();
#endif


            services.AddScoped<cloudscribe.Web.Navigation.INavigationNodePermissionResolver, cloudscribe.Web.Navigation.NavigationNodePermissionResolver>();
#if (SimpleContentConfig != "z")
            services.AddScoped<cloudscribe.Web.Navigation.INavigationNodePermissionResolver, cloudscribe.SimpleContent.Web.Services.PagesNavigationNodePermissionResolver>();
#endif
            services.AddCloudscribeCoreMvc(config);
#if (SimpleContentConfig != "z")
            services.AddCloudscribeCoreIntegrationForSimpleContent(config);
            services.AddSimpleContentMvc(config);
            services.AddContentTemplatesForSimpleContent(config);
            
            services.AddMetaWeblogForSimpleContent(config.GetSection("MetaWeblogApiOptions"));
            services.AddSimpleContentRssSyndiction();
#endif
#if (ContactForm)
            services.AddCloudscribeSimpleContactFormCoreIntegration(config);
            services.AddCloudscribeSimpleContactForm(config);
#endif


#if (DynamicPolicy)
            services.AddCloudscribeDynamicPolicyIntegration(config);
            services.AddDynamicAuthorizationMvc(config);
#endif


            return services;
        }

    }
}
