using DataAccess.Context;
using DataAccess.Entities;
using Microsoft.Extensions.DependencyInjection;
using Shared.Enums;

namespace DataAccess.Infrastructure;

public static class SeedData
{
    public static async Task AddSeedDataAsync(IServiceProvider serviceProvider)
    {
        var db = serviceProvider.GetRequiredService<ApplicationDbContext>();

        if (db.Products.Any()) return;

        await CreateProductsAsync(db);
    }

    private static async Task CreateProductsAsync(ApplicationDbContext db)
    {
        var products = new Product[]
        {
            new()
            {
                Title = "PELADN AMD Radeon RX6650XT 8GB GDDR6 PCI Express 4.0 Video Card",
                Price = 1357,
                Description = "8GB 128-Bit GDDR6",
                Type = ProductType.GPU,
                CreatedDate = DateTime.UtcNow
            },
            new()
            {
                Title = "Intel Core i5-12400F",
                Price = 50.00m,
                Description = "Intel 7 Alder Lake Processor Base Power: 65W Maximum Turbo Power: 117W 18MB L3 Cache 7.5MB L2 Cache Windows 11 Supported Intel Laminar RM1 CPU Cooler",
                Type = ProductType.CPU,
                CreatedDate = DateTime.UtcNow
            },
            new()
            {
                Title = "ASUS ROG STRIX Z790-H Gaming",
                Price = 137.73m,
                Description = "Intel LGA 1700 socket: Ready for 13th Gen Intel Core, and 12th Gen Intel Core, Pentium Gold, and Celeron® processors.",
                Type = ProductType.MotherBoard,
                CreatedDate = DateTime.UtcNow
            },
            new()
            {
                Title = "Seagate Exos X20 ST20000NM007D ",
                Price = 141.98m,
                Description = "7200 RPM 256MB Cache SATA 6.0Gb For Enterprise Storage",
                Type = ProductType.HDD,
                CreatedDate = DateTime.UtcNow
            },
            new()
            {
                Title = "SABRENT 500GB Rocket Nvme PCIe 4.0 M.2 2280",
                Price = 417.14m,
                Description = "NVMe M.2 PCIe Gen4 x4 Interface. PCIe 4.0 Compliant / NVMe 1.3 Compliant.\r\nPower Management Support for APST / ASPM / L1.2.",
                Type = ProductType.SSD,
                CreatedDate= DateTime.UtcNow
            }
        };

        db.Products.AddRange(products);
        await db.SaveChangesAsync();
    }
}
