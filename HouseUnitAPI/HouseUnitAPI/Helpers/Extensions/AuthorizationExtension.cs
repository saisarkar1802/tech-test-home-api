namespace HouseUnitAPI.Helpers.Extensions
{
    public static class AuthorizationExtension
    {
        public static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("USER", policy => policy.RequireRole("USER"));
                options.AddPolicy("ADMIN", policy => policy.RequireRole("ADMIN"));
            });

            return services;
        }
    }
}
