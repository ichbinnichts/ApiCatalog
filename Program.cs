using APICatalog.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

/* ---------- Add this configuration in AddControllers because the new get
 * in categories that get
 * products in categories give a object cycle problem, to handle this, this configuration
 * ignore all cycles in the controller
 * */
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ----- Creating my var with my psql connection -----
var psqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
// ----- Using builder to add the db context to the services -----
builder.Services.AddDbContext<APICatalogContext>(options => options.UseNpgsql(psqlConnection));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
