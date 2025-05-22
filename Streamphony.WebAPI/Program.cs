using Microsoft.EntityFrameworkCore;
using Streamphony.Infrastructure.Persistence.Contexts;
using Streamphony.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServices();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

app.UseCors("ClientPermission");
app.UseHttpsRedirection();
app.UseRequestTiming();
app.UseExceptionHandling();
app.UseDbTransaction();
app.MapControllers();

app.Run();

public partial class Program
{
}
