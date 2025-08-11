using Microsoft.AspNetCore.Identity;
using RuedaYPatas.Constants; 
using RuedaYPatas.Models;

namespace RuedaYPatas.Data;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        // Definir roles
        string[] roleNames = { Roles.Admin, Roles.Cliente };

        foreach (var roleName in roleNames)
        {
            // Si el rol no existe, créalo
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Crear el usuario administrador si no existe
        var adminEmail = "admin@ruedaypata.com";
        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                Nombre = "Administrador del Sistema",
                Direccion = "N/A",
                EmailConfirmed = true // Importante para poder iniciar sesión directamente
            };

            var result = await userManager.CreateAsync(adminUser, "Admin123!");
            if (result.Succeeded)
            {
                // Asignar el rol de Administrador
                await userManager.AddToRoleAsync(adminUser, Roles.Admin);
            }
        }
    }
}