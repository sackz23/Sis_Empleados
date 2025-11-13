using Microsoft.EntityFrameworkCore;
using Sis_Empleados.Models;
using System.Globalization;
using Rotativa.AspNetCore;
using OfficeOpenXml;

var builder = WebApplication.CreateBuilder(args);

// Agregamos el contexto al contenedor de dependencias
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionEmpleados")));

builder.Services.AddControllersWithViews();

builder.Services.AddSession(); // Habilita sesiones

var app = builder.Build();

// CONFIGURACIÓN DE CULTURA PARA DECIMALES
var cultureInfo = new CultureInfo("en-US");
cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
cultureInfo.NumberFormat.NumberGroupSeparator = ",";

CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

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
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
