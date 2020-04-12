using ShortCutURL.Models.DataModel;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ShortCutURL.DAL
{
    /// <summary>
    /// Класс контекста БД.
    /// </summary>
    public class ShortUrlContext: DbContext
    {
        /// <summary>
        /// Конструктор строк для DbContext с именем БД.
        /// Создает строку подключения для БД с помощью SQL Express.
        /// Строка подключения будет найдена в файле конфигурации.
        /// Если строка подключения с данным именем не найдена, будет выдано исключение.
        /// </summary>
        public ShortUrlContext(): base("name=ShortUrlContext") { }

        public DbSet<ShortUrl> ShortUrls { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Предотвращает множественное преобразование имен таблиц.
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}