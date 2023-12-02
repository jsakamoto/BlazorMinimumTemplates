using BlazorMin.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#if (!UseServer && !UseWebAssembly)
builder.Services.AddRazorComponents();
#else
builder.Services.AddRazorComponents()
    #if (UseServer && UseWebAssembly)
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
    #elif (UseServer)
    .AddInteractiveServerComponents();
    #elif (UseWebAssembly)
    .AddInteractiveWebAssemblyComponents();
    #endif
#endif

var app = builder.Build();

// Configure the HTTP request pipeline.
#if (UseWebAssembly)
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
#else
if (!app.Environment.IsDevelopment())
#endif
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
app.UseStaticFiles();
app.UseAntiforgery();

#if (!UseServer && !UseWebAssembly)
app.MapRazorComponents<App>();
#else
app.MapRazorComponents<App>()
    #if (UseServer && UseWebAssembly)
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode();
    #elif (UseServer)
    .AddInteractiveServerRenderMode();
    #elif (UseWebAssembly)
    .AddInteractiveWebAssemblyRenderMode();
    #endif
#endif

app.Run();
