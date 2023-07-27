using AuthModule.Configration;
using AuthModule.Context;
using Microsoft.EntityFrameworkCore;
using SharedConfilgrations.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddDataAnnotationsLocalization(options =>
{
    options.DataAnnotationLocalizerProvider = (type, factory) =>
        factory.Create(typeof(SharedResource));
}); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

#region AuthConfigration
builder.Services.LoadAuthDependencies();
builder.Services.RunAuthConfigs();
#endregion



#region DataBaseConfig
builder.Services.AddDbContext<AuthContextDB>(optionsAction: options => options.UseSqlServer(builder.Configuration.GetConnectionString("AuthDB")));
builder.Services.AddDbContext<DBContext>(optionsAction: options => options.UseSqlServer(builder.Configuration.GetConnectionString("SAASDB")));
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
