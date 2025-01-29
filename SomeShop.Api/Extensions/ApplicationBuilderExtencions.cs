using Microsoft.EntityFrameworkCore;
using SomeShop.Api.Middleware;
using SomeShop.Infrastructure;

namespace SomeShop.Api.Extensions;

public static class ApplicationBuilderExtencions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        dbContext.Database.Migrate();
    }

    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<GlobalExceptionHandlingMiddlware>();
    }
}
