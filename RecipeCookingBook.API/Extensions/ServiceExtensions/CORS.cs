namespace RecipeCookingBook.API.Extensions.ServiceExtensions
{
    public static class Cors
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(policies =>
            {
                policies.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetIsOriginAllowed((_) => true)
                        .WithExposedHeaders("X-Pagination"));
            });
    }
}