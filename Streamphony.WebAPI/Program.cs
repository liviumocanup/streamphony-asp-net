using Streamphony.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServices();

var app = builder.Build();

if (app.Environment.IsDevelopment()) app.UseSwaggerDocumentation();

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
