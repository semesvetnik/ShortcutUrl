using System.ComponentModel.DataAnnotations;

namespace ShortCutURL.Models.DataModel
{
    /// <summary>
    /// URL.
    /// </summary>
    public class ShortUrlView
    {
        /// <summary>
        /// Идентификатор изменяемого URL.
        /// </summary>
        public int? ID { get; set; }

        /// <summary>
        /// Значение URL.
        /// </summary>
        [Display(Name = "URL", Prompt = "URL", Description = "Значение URL")]
        [Url(ErrorMessage = "Значение URL некорректно")]
        public string UrlValue { get; set; }
    }
}