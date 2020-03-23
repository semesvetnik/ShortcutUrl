namespace CutUrl.Model.Interfaces
{
    /// <summary>
    /// Содержит методы работы с интернет-ссылкой.
    /// </summary>
    public interface IUrlRepository
    {
        /// <summary>
        /// Возвращает сокращенный URL по полному URL.
        /// </summary>
        /// <param name="fullURL">Полный URL.</param>
        /// <returns>Сокращенный URL или NULL, если отсутствует в БД.</returns>
        string GetShortURL(string fullURL);

        /// <summary>
        /// Проверяет наличие сокращенного URL в БД. Если найден, то возвращает true.
        /// </summary>
        /// <param name="shortURL">Сокращенный URL.</param>
        /// <returns>Признак того, что сокращенный URL уже используется в БД.</returns>
        bool FindShortURL(string shortURL);

        /// <summary>
        /// Создает сокращенный URL по полному.
        /// </summary>
        /// <param name="fullURL">Полный URL.</param>
        /// <param name="shortURL">Сокращенный URL.</param>
        int Create(string fullURL, string shortURL);


        void Delete(int id);
    }
}
