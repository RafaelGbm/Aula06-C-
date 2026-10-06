using System.Text.Json.Serialization;
using System.Threading.Tasks;
using LojaGraphQL.Api.Data;
using LojaGraphQL.Api.GraphQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LojaGraphQL.Api;

public partial class Program
{
    public static async Task Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddControllers()
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.ReferenceHandler =
                    ReferenceHandler.IgnoreCycles);
        builder.Services.AddOpenApi();
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(
                builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddAuthorization();
        builder.Services
            .AddIdentityApiEndpoints<IdentityUser>()
            .AddEntityFrameworkStores<AppDbContext>();

        builder.Services
            .AddGraphQLServer()
            .AddQueryType<Query>()
            .AddProjections()
            .AddFiltering()
            .AddSorting();

        WebApplication app = builder.Build();

        app.MapOpenApi();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapIdentityApi<IdentityUser>();
        app.MapControllers();
        app.MapGraphQL();

        await DbSeeder.SeedAsync(app.Services);
        await app.RunAsync();
    }
}
