using Microsoft.EntityFrameworkCore;
using Pie.Data.Models;

namespace Pie.Data
{
    public static class InitialData
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<QueueOut>().HasData(
                new QueueOut { Id = Guid.Parse("7E83260A-316F-4A1F-BE9A-BF353B118536"), Key = 10, Active = true, Name = "Живая очередь" },
                new QueueOut { Id = Guid.Parse("3558D2BA-FFB6-4F08-9891-F7F1E8853C83"), Key = 20, Active = true, Name = "Собрать к дате" },
                new QueueOut { Id = Guid.Parse("D964FCAD-D71D-480A-BDEB-0B2C045FCD90"), Key = 30, Active = true, Name = "Собственная доставка" },
                new QueueOut { Id = Guid.Parse("8BDC656E-8A2C-4AEF-9422-E0A419608190"), Key = 40, Active = true, Name = "Очередность не указана" }
                );

            builder.Entity<StatusOut>().HasData(
                new StatusOut { Id = Guid.Parse("C2C5935D-B332-4D84-B1FD-309AD8A65356"), Key = 0, Active = true, Name = "Подготовлено" },
                new StatusOut { Id = Guid.Parse("E1A4C395-F7A3-40AF-82AB-AD545E51ECA7"), Key = 1, Active = true, Name = "КОтбору" },
                new StatusOut { Id = Guid.Parse("BD1AE241-D787-4A6D-B920-029BC6577364"), Key = 2, Active = false, Name = "КПроверке" },
                new StatusOut { Id = Guid.Parse("17CEE206-E06F-47D8-824D-14EECEAF394A"), Key = 3, Active = false, Name = "ВПроцессеПроверки" },
                new StatusOut { Id = Guid.Parse("E911589B-613C-42AD-AD56-7083C481C4B4"), Key = 4, Active = false, Name = "Проверен" },
                new StatusOut { Id = Guid.Parse("7C2BD6BE-CF81-4B1A-9ACF-D4EBF416F4D3"), Key = 5, Active = true, Name = "КОтгрузке" },
                new StatusOut { Id = Guid.Parse("9EBA20CE-9245-4109-92CB-A9875801FBB4"), Key = 6, Active = true, Name = "Отгружен" }
                );
        }
    }
}
