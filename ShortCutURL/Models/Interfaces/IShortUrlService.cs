using System.Collections.Generic;
using ShortCutURL.Models.DataModel;

namespace ShortCutURL.Models.Interfaces
{
    public interface IShortUrlService
    {
        /// <summary>
        /// Создает сокращенный URL по полному.
        /// </summary>
        /// <param name="fullUrl">Полный URL.</param>
        /// <returns>Сокращенный URL.</returns>
        ShortUrl Create(string fullUrl);

        /// <summary>
        /// Изменяет короткий URL.
        /// </summary>
        /// <param name="urlView"></param>
        /// <returns></returns>
        bool Edit(ShortUrlView urlView);

        /// <summary>
        /// Удаляет URL по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор удаляемого URL.</param>
        /// <returns>Список URL, которые не являются удаленными.</returns>
        void Delete(int id);

        /// <summary>
        /// Возвращает список URL, которые не являются удаленными.
        /// </summary>
        /// <returns>Список URL, которые не являются удаленными</returns>
        List<ShortUrl> GetShortUrlList();

        /// <summary>
        /// Возвращает сокращенный URL по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сокращенного URL.</param>
        /// <returns>Сокращенный URL.</returns>
        ShortUrlView Get(int id);

        /// <summary>
        /// Обновляет количество переходов по сокращенному URL.
        /// </summary>
        /// <param name="shortUrlValue">Сокращенный URL.</param>
        /// <returns>Значение полного URL или NULL.</returns>
        string UpdateTransitions(string shortUrlValue);
    }
}
