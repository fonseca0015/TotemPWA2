using TotemPWA.Data;
using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Mvc; // Geralmente não é necessário aqui
// using Microsoft.AspNetCore.Mvc.Rendering; // Geralmente não é necessário aqui
using TotemPWA.Models; // Geralmente não é necessário aqui
using TotemPWA.ViewModels; // Geralmente não é necessário aqui

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    // options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection"))
    options.UseSqlite(builder.Configuration.GetConnectionString("SQLLiteConnection"))
);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();  // Adiciona o Swagger

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();

    // Apaga o banco de dados completamente
    //context.Database.EnsureDeleted(); builder.Services.AddDbContext

    // Aplica as migrações do zero
    //context.Database.Migrate();

    // Executa o Seed (inicialização de dados)
    await DbInitializer.InitializeAsync(context);
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles(); // Esta linha já é responsável por servir arquivos estáticos como CSS, JS, imagens, etc.

app.UseSwagger();  // Habilita o Swagger
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TotemPWA API v1"));  // Interface do Swagger

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

// REMOVA ESTA LINHA: app.MapStaticAssets(); // Isso pode estar conflitando com o roteamento padrão

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    // REMOVA ESTA LINHA: .WithStaticAssets(); // Isso também pode estar conflitando

app.Run();