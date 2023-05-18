﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pie.Connectors.Connector1c;
using Pie.Data.Models.Identity;
using Pie.Data.Models.Out;

namespace Pie.Data
{
    public static class DataSeed
    {
        public static void Initialize(ModelBuilder builder)
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

            builder.Entity<QueueNumber>().HasData(
                new QueueNumber { CharValue = 0, NumValue = 0, Value = "A000" }
                );

            builder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = "22919707-7d2c-450d-92e7-19f36935bcdb",
                    FirstName = "Админ",
                    LastName = null,
                    UserName = "igmo@dobroga.ru",
                    NormalizedUserName = "IGMO@DOBROGA.RU",
                    Email = "igmo@dobroga.ru",
                    NormalizedEmail = "IGMO@DOBROGA.RU",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEDgydLmvi4/0kDXZB6+ShJFMNIK8Xzgaawytbvp8IMJquSZ/4hO8sPu9mlXC5uS9IQ==",
                    SecurityStamp = "HCJOWYFSM63CJOZM5AZAGXSHEI257BCI",
                    ConcurrencyStamp = "2b68aa3c-d884-475f-8a7a-f72d5666f9ae",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    LockoutEnd = null,
                    AccessFailedCount = 0,
                    WarehouseId = null
                },
                new AppUser
                {
                    Id = "0b60c927-0e9f-4fa7-8422-e0e16e6fa5f4",
                    FirstName = nameof(Service1c),
                    LastName = null,
                    UserName = $"{nameof(Service1c)}@www",
                    NormalizedUserName = $"{nameof(Service1c).ToUpper()}@WWW",
                    Email = $"{nameof(Service1c)}@www",
                    NormalizedEmail = $"{nameof(Service1c).ToUpper()}@WWW",
                    EmailConfirmed = true,
                    //Password: Sevice1c#
                    PasswordHash = "AQAAAAIAAYagAAAAEAOaCAADpFz/XmkyKhEkd0FjnlPHtkUSiV7GdH10SvSKvK4eZhtJHFnWl66jxbydXw==",
                    SecurityStamp = "JCSGTYDEWVEPHPV7DDVTSY263NY4JDBP",
                    ConcurrencyStamp = "655eae0d-3ab4-4688-87ec-700d6c6c9567",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    LockoutEnd = null,
                    AccessFailedCount = 0,
                    WarehouseId = null
                }
                );

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "d6bfb7c2-9a45-45e5-b27a-3b7cba85527f", Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = null },
                new IdentityRole { Id = "9423e7b8-b496-41e8-b9c9-416b74823db9", Name = "User", NormalizedName = "USER", ConcurrencyStamp = null },
                new IdentityRole { Id = "049c2135-b769-4ea5-986a-a5231330fe46", Name = nameof(Service1c), NormalizedName = nameof(Service1c).ToUpper(), ConcurrencyStamp = null }
                );

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = "22919707-7d2c-450d-92e7-19f36935bcdb", RoleId = "9423e7b8-b496-41e8-b9c9-416b74823db9" },
                new IdentityUserRole<string> { UserId = "22919707-7d2c-450d-92e7-19f36935bcdb", RoleId = "d6bfb7c2-9a45-45e5-b27a-3b7cba85527f" },
                new IdentityUserRole<string> { UserId = "22919707-7d2c-450d-92e7-19f36935bcdb", RoleId = "049c2135-b769-4ea5-986a-a5231330fe46" },
                new IdentityUserRole<string> { UserId = "0b60c927-0e9f-4fa7-8422-e0e16e6fa5f4", RoleId = "049c2135-b769-4ea5-986a-a5231330fe46" }
                );
        }
    }
}
