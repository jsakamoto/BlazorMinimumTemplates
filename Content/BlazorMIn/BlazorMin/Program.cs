#if (Interactivity == "WebAssembly" || Interactivity == "Auto")
using BlazorMin.Client.Pages;
#endif
using BlazorMin.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#if (Interactivity == "None")
builder.Services.AddRazorComponents();
#else
builder.Services.AddRazorComponents()
    #if (Interactivity == "Auto")
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
    #elif (Interactivity == "Server")
    .AddInteractiveServerComponents();
    #elif (Interactivity == "WebAssembly")
    .AddInteractiveWebAssemblyComponents();
    #endif
#endif

var app = builder.Build();

// Configure the HTTP request pipeline.
#if (Interactivity == "WebAssembly" || Interactivity == "Auto")
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
#else
if (!app.Environment.IsDevelopment())
#endif
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
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

#if (Interactivity == "None")
app.MapRazorComponents<App>();
#else
app.MapRazorComponents<App>()
    #if (Interactivity == "Server")
    .AddInteractiveServerRenderMode();
    #endif
    #if (Interactivity == "Auto")
    .AddInteractiveServerRenderMode()
    #endif
    #if (Interactivity == "WebAssembly" || Interactivity == "Auto")
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Home).Assembly);
    #endif
#endif

app.Run();
