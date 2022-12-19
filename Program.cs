﻿using Microsoft.EntityFrameworkCore;
using SuperSuperSimpleHelpDesk.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(Environment.GetEnvironmentVariable("CONNECTION_STRING")
    ?? builder.Configuration.GetConnectionString("DataContext") // apenas pra facilitar o comando add-migration (arrancar daqui depois)
    ?? throw new InvalidOperationException("Connection string 'DataContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

EnsureDatabaseCreated(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Tickets}/{action=Index}/{id?}");

app.Run();


void EnsureDatabaseCreated(WebApplication app)
{
    var serviceScopeFactory = app.Services.GetService<IServiceScopeFactory>();

    if (serviceScopeFactory is null) throw new NullReferenceException();

    using var serviceScope = serviceScopeFactory.CreateScope();
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<DataContext>();

    dbContext.Database.Migrate();
    dbContext.Ticket.AddRange(TempSeed.CreateTickets());
    dbContext.SaveChanges();

    serviceScope.Dispose();
}