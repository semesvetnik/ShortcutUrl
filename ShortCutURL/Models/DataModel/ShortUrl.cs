using System;
using System.ComponentModel.DataAnnotations;

namespace ShortCutURL.Models.DataModel
{
    /// <summary>
    /// Модель сокращенного URL.
    /// </summary>
    public class ShortUrl
    {
        /// <summary>
        /// Идентификатор ShortURL.
        /// </summary>
        [Key]
        [Display(Name = "ID", Prompt = "ID сокращенного URL", Description = "ID сокращенного URL")]
        public int? ID { get; set; }

        /// <summary>
        /// Длинный URL.
        /// </summary>
        [Display(Name = "URL", Prompt = "Введите URL", Description = "Длинный URL, который требуется сократить")]
        [Url(ErrorMessage = "Некорректный Url")]
        public string FullUrlValue { get; set; }

        /// <summary>
        /// Сокращенный URL.
        /// </summary>
        [Display(Name = "Сокращенный URL", Prompt = "Сокращенный URL", Description = "Сокращенный URL")]
        //[Url(ErrorMessage = "Некорректный Url")]
        public string ShortUrlValue { get; set; }

        /// <summary>
        /// Дата создания сокращенного URL.
        /// </summary>
        [Display(Name = "Дата создания", Prompt = "Дата создания сокращенного URL", Description = "Дата создания сокращенного URL")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Количество переходов по короткому URL.
        /// </summary>
        [Display(Name = "Количество переходов", Prompt = "Количество переходов по сокращенному URL", Description = "Количество переходов по сокращенному URL")]
        public int Count { get; set; }

        /// <summary>
        /// Признак того, что такой короткий URL был создан и больше не используется.
        /// </summary>
        [Display(Name = "URL используется", Prompt = "URL используется", Description = "Признак того, что такой короткий URL был создан и больше не используется")]
        public bool IsDeleted { get; set; }
    }
}
