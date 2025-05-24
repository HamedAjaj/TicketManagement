using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketManagement.API.Filters;
using TicketManagement.API.PipeLineBehavior;
using TicketManagement.Infrastructure.DBContext;
using TicketManagement.Infrastructure.Repository.Implementation;
using TicketManagement.Infrastructure.Repository.Interface;
using TicketManagement.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddHostedService<AutoHandleService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddMediatR(option => option.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPipeLine<,>));
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddDbContext<TicketDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// this is if I want to make global filter
builder.Services.AddControllers(
    options => options.Filters.Add(new HandleErrorAttribute()));
builder.Services.AddCors(options =>
{
    options.AddPolicy("TicketOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:4200")  
              .AllowAnyHeader() 
              .AllowAnyMethod();  
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TicketDbContext>();
    dbContext.Database.Migrate();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // app.MapOpenApi();
}

app.UseCors("TicketOrigin");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
 