using Microsoft.AspNetCore.Authorization;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Authorization
    {
        public static AuthorizationOptions SetupAuthorizationPolicies(this AuthorizationOptions options)
        {
            //https://docs.asp.net/en/latest/security/authorization/policies.html

            
#if (Logging)
            options.AddCloudscribeLoggingDefaultPolicy();
#endif

#if (DynamicPolicy)

            options.AddPolicy(
                "ServerAdminPolicy",
                authBuilder =>
                {
                    authBuilder.RequireRole("ServerAdmins");

                });

             // probably best to not let anyone change the main admin policy from the UI
            options.AddPolicy(
                "AdminPolicy",
                authBuilder =>
                {
                    authBuilder.RequireRole("ServerAdmins", "Administrators");
                });

            // you could comment this out if you want admins from any site to be able
            // to edit globally shared country state data
            // by commenting this out the policy could be managed per tenant from the UI
            // but it is probably best to only let this be managed from the master tenant
            options.AddPolicy(
                "CoreDataPolicy",
                authBuilder =>
                {
                    authBuilder.RequireRole("ServerAdmins");
                });

            // probably best and recommended to not let this policy be managed from the UI
            // since this policy controls who can manage policies from the UI
            options.AddPolicy(
                "PolicyManagementPolicy",
                authBuilder =>
                {
                    authBuilder.RequireRole("Administrators");
                });

           

#else
           options.AddCloudscribeCoreDefaultPolicies();

#if (SimpleContentConfig != "z")
            options.AddCloudscribeCoreSimpleContentIntegrationDefaultPolicies();
            
#endif

            options.AddPolicy(
                "FileManagerPolicy",
                authBuilder =>
                {
                    authBuilder.RequireRole("Administrators", "Content Administrators");
                });

            options.AddPolicy(
                "FileUploadPolicy",
                authBuilder =>
                {
                    authBuilder.RequireRole("Administrators", "Content Administrators");
                });

            options.AddPolicy(
                "FileManagerDeletePolicy",
                authBuilder =>
                {
                    authBuilder.RequireRole("Administrators", "Content Administrators");
                });

#if (IdentityServer)
            options.AddPolicy(
                    "IdentityServerAdminPolicy",
                    authBuilder =>
                    {
                        authBuilder.RequireRole("Administrators");
                    });
#endif


#endif

            
            // add other policies here 


            return options;
        }

    }
}
