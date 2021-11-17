using Microsoft.EntityFrameworkCore;
using NetCore6TestApplication.Data;
using NetCore6TestLibrary;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FirstDatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FirstDatabaseContext")));
builder.Services.AddDbContext<SecondDatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SecondDatabaseContext")));
builder.Services.AddDbContext<LibraryDatabaseContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryDatabaseContext")));
builder.Services.AddDbContext<InternalLibraryDatabaseContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("InternalLibraryDatabaseContext")));

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
