using ShortCutURL.Models.DataModel;
using System.Collections.Generic;

namespace ShortCutURL.Models.Interfaces
{
    /// <summary>
    /// Содержит методы работы с сокращенным URL.
    /// </summary>
    public interface IShortUrlRepository
    {
        /// <summary>
        /// Возвращает сокращенный URL по полному из БД.
        /// </summary>
        /// <param name="fullUrl">Полный URL.</param>
        /// <returns>Сокращенный URL или NULL, если отсутствует в БД.</returns>
        ShortUrl Get(string fullUrl);

        /// <summary>
        /// Возвращает сокращенный URL по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сокращенного URL.</param>
        /// <returns>Сокращенный URL.</returns>
        ShortUrl Get(int id);

        /// <summary>
        /// Возвращает список URL, которые не являются удаленными.
        /// </summary>
        /// <returns></returns>
        List<ShortUrl> GetShortUrlList();

        /// <summary>
        /// Проверяет наличие сокращенного URL в БД. Если найден, то возвращает true.
        /// </summary>
        /// <param name="shortUrl">Сокращенный URL.</param>
        /// <returns>Признак того, что сокращенный URL уже используется в БД.</returns>
        bool FindShortUrl(string shortUrl);

        /// <summary>
        /// Проверяет наличие полного URL в БД. Если найден, то возвращает true.
        /// </summary>
        /// <param name="fullUrl">Полный URL.</param>
        /// <returns>Признак того, что полный URL уже используется в БД.</returns>
        bool FindFullUrl(string fullUrl);

        /// <summary>
        /// Создает сокращенный URL по полному в БД.
        /// </summary>
        /// <param name="shortUrl">Параметры сокращенного URL.</param>
        /// <returns>Идентификатор сокращенного URL.</returns>
        int Create(ShortUrl shortUrl);

        /// <summary>
        /// Обновляет параметры сокращенного URL.
        /// </summary>
        /// <param name="shortUrl">Сокращенный URL.</param>
        void Update(ShortUrl shortUrl);

        /// <summary>
        /// Обновляет значение короткого URL.
        /// </summary>
        /// <param name="shortUrlView">Модель обновления сокращенного URL.</param>
        void Update(ShortUrlView shortUrlView);

        /// <summary>
        /// Удаляет URL по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор удаляемого URL.</param>
        void Delete(int id);

        /// <summary>
        /// Обновляет количество переходов по сокращенному URL.
        /// </summary>
        /// <param name="shortUrlValue">Сокращенный URL.</param>
        /// <returns>Значение полного URL или NULL.</returns>
        string UpdateTransitions(string shortUrlValue);
    }
}
