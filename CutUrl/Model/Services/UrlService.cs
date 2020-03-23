using System;
using CutUrl.Model.Interfaces;

namespace CutUrl.Model.Services
{
    public class UrlService : IUrlService
    {
        /// <summary>
        /// Длина сокращенного URL. 
        /// </summary>
        public const int SHORT_URL_LENGTH = 7;

        #region Properties

        private IUrlRepository _urlRepository { get; set; }

        #endregion

        #region Constructors

        public UrlService(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        #endregion

        /// <summary>
        /// Создает сокращенный URL по полному.
        /// </summary>
        /// <param name="fullURL">Полный URL.</param>
        /// <returns>Сокращенный URL.</returns>
        public string Create(string fullURL)
        {
            string short_url = _urlRepository.GetShortURL(fullURL);

            if (short_url == null)
            {
                string url_id = Guid.NewGuid().ToString();
                string short_url_id = GetTail(url_id);

                while (_urlRepository.FindShortURL(short_url_id))
                {
                    url_id = Guid.NewGuid().ToString();
                    short_url_id = GetTail(url_id);
                }

                _urlRepository.Create(fullURL, url_id);

                short_url = url_id;
            }

            return GetTail(short_url);
        }

        private string GetTail(string value)
        {
            return value.Substring(value.Length - SHORT_URL_LENGTH, SHORT_URL_LENGTH);
        }
    }
}
