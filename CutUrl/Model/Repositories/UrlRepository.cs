using CutUrl.Model.Interfaces;
using System;

namespace CutUrl.Model.Repositories
{
    /// <summary>
    /// Содержит методы работы с интернет-ссылкой.
    /// </summary>
    public class UrlRepository : IUrlRepository
    {
        /// <summary>
        /// Создает сокращенный URL по полному.
        /// </summary>
        /// <param name="fullURL">Полный URL.</param>
        /// <returns>Сокращенный URL.</returns>
        public int Create(string fullURL, string shortURL)
        {
            string cut_url = Guid.NewGuid().ToString();

            return 21;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public string GetShortURL(string fullURL)
        {
            throw new NotImplementedException();
        }

        public bool FindShortURL(string shortURL)
        {
            throw new NotImplementedException();
        }
    }
}
