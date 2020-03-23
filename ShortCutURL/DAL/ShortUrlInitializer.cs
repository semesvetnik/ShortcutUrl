using ShortCutURL.Models.DataModel;
using System;
using System.Collections.Generic;

namespace ShortCutURL.DAL
{
    public class ShortUrlInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ShortUrlContext>
    {
        /// <summary>
        /// Добавляет новые сущности в БД.
        /// </summary>
        /// <param name="context">Объект контекста базы данных.</param>
        protected override void Seed(ShortUrlContext context)
        {
            var urls = new List<ShortUrl>
            {
                new ShortUrl
                {
                    Count = 0,
                    Date = DateTime.Now,
                    FullUrlValue =
                        "https://ru.wikipedia.org/wiki/%D0%A1%D0%BE%D0%BA%D1%80%D0%B0%D1%89%D0%B5%D0%BD%D0%B8%D0%B5_URL",
                    IsDeleted = false,
                    ShortUrlValue = "123"
                },
                new ShortUrl
                {
                    Count = 0,
                    Date = DateTime.Now,
                    FullUrlValue =
                        "https://docs.microsoft.com/ru-ru/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-an-entity-framework-data-model-for-an-asp-net-mvc-application",
                    IsDeleted = false,
                    ShortUrlValue = "456"
                }
            };

            urls.ForEach(url => context.ShortUrls.Add(url));
            context.SaveChanges();
        }
    }
}