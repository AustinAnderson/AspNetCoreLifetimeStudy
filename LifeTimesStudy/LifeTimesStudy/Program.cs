using LifeTimesStudy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped<ScopedDisposableDep>();
builder.Services.AddScoped<ScopedService>();
builder.Services.AddSingleton<ScopeWrapper<ScopedService>>();
builder.Services.AddSingletonWithInitializer<ConsumesScoped>(async consumesScoped => await consumesScoped.GetScopedsInstanceId());

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

app.MapControllers();

app.Run();
