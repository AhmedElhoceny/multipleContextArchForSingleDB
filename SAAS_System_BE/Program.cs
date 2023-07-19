using AuthModule.Configration;
using AuthModule.Context;
using Microsoft.EntityFrameworkCore;
using OrderModule.Configration;
using OrderModule.Context;
using SharedConfilgrations.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region AuthConfigration
builder.Services.LoadAuthDependencies();
#endregion

#region OrderConfigration
builder.Services.LoadOrderDependencies();
#endregion

#region DataBaseConfig
builder.Services.AddDbContext<AuthContextDB>(optionsAction: options => options.UseSqlServer(builder.Configuration.GetConnectionString("AuthDB")));
builder.Services.AddDbContext<OrderContextDb>(optionsAction: options => options.UseSqlServer(builder.Configuration.GetConnectionString("OrdersDB")));
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
