var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
#if (!NoHttps)
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
#endif
}

#if (!NoHttps)
app.UseHttpsRedirection();

#endif
#if (Framework == "net6.0" || Framework == "net7.0" || Framework == "net8.0")
app.UseStaticFiles();
#else
app.MapStaticAssets();
#endif

app.UseRouting();

app.MapBlazorHub();
#if (Framework != "net6.0" && Framework != "net7.0" && Framework != "net8.0")
app.MapRazorPages().WithStaticAssets();
#endif
app.MapFallbackToPage("/_Host");

app.Run();
