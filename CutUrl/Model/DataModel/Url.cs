using System;

namespace CutUrl.Model.DataModel
{
    /// <summary>
    /// Интернет-ссылка.
    /// </summary>
    public class Url
    {
        public int ID { get; set; }

        /// <summary>
        /// Длинный URL.
        /// </summary>
        public string FullURL { get; set; }

        /// <summary>
        /// Сокращенный URL.
        /// </summary>
        public string ShortURL { get; set; }

        /// <summary>
        /// Дата создания сокращенного URL.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Количество переходов по короткому URL.
        /// </summary>
        public int Count { get; set; }
    }
}
