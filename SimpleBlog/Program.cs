using Microsoft.EntityFrameworkCore;
using SimpleBlog.Contracts.Persistence;
using SimpleBlog.Data;
using SimpleBlog.Data.Repositories;
using SimpleBlog.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder
    .Services.AddRazorPages()
    .AddRazorPagesOptions(options =>
        options.Conventions.AddPageRoute("/Blogs/Index", string.Empty)
    );

builder.Services.AddDbContext<SimpleBlogContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SimpleBlogContext")
            ?? throw new InvalidOperationException(
                "Connection string 'SimpleBlogContext' not found."
            )
    )
);

builder
    .Services.AddDefaultIdentity<BlogUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<SimpleBlogContext>();

builder.Services.AddScoped<IBlogRepository, BlogRepository>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
