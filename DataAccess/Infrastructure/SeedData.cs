using DataAccess.Entities;
using Microsoft.Extensions.DependencyInjection;

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
                Title = "Видеокарта BIOSTAR GeForce RTX 3070 8GB GDDR6 VN3706RM82",
                Price = 1357,
                Description = "8 ГБ GDDR6 LHR, 1500 МГц / 1725 МГц, 5888sp, 46 RT-ядер, трассировка лучей, 256 бит, HDMI, DisplayPort",
                Type = ProductType.GPU,
                CreatedDate = DateTime.UtcNow
            },
            new()
            {
                Title = "Процессор Intel Celeron G1820",
                Price = 50.00m,
                Description = "Socket - LGA1150, Ядро - Haswell (2013), Количество ядер - 2, Техпроцесс - 22 нм, Тактовая частота - 2700 МГц, Системная шина - 1333 МГц, DMI, Коэффициент умножения - 27",
                Type = ProductType.CPU,
                CreatedDate = DateTime.UtcNow
            },
            new()
            {
                Title = "Материнская плата ASUS Prime H310M-K R2.0",
                Price = 137.73m,
                Description = "Socket - LGA1151 v2, Поддерживаемые процессоры - Intel Core i7/Core i5/Core i3/Pentium/Celeron, Поддержка многоядерных процессоров - есть, Чипсет - Intel H310, BIOS - AMI, Поддержка EFI - есть, Поддержка SLI/CrossFire - нет",
                Type = ProductType.MotherBoard,
                CreatedDate = DateTime.UtcNow
            },
            new()
            {
                Title = "Жесткий диск Toshiba P300 1TB [HDWD110UZSVA]",
                Price = 141.98m,
                Description = "Тип - HDD, Поддержка секторов размером 4 КБ - есть, Назначение - для настольного компьютера, Форм-фактор - 3.5\", Объем - 1000 ГБ, Объем буферной памяти - 64 МБ, Скорость вращения - 7200 rpm",
                Type = ProductType.HDD,
                CreatedDate = DateTime.UtcNow
            },
            new()
            {
                Title = "SSD Transcend SSD230S 2TB TS2TSSD230",
                Price = 417.14m,
                Description = "2.5\", SATA 3.0, микросхемы 3D TLC NAND, последовательный доступ: 560/520 MBps, случайный доступ: 85000/89000 IOps",
                Type = ProductType.SSD,
                CreatedDate= DateTime.UtcNow
            }
        };

        db.Products.AddRange(products);
        await db.SaveChangesAsync();
    }
}
