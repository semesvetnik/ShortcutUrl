using ShortCutURL.Models.DataModel;
using ShortCutURL.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ShortCutURL.Models.Services
{
    public class ShortUrlService : IShortUrlService
    {
        #region Constants

        /// <summary>
        /// Длина сокращенного URL. 
        /// </summary>
        public const int SHORT_URL_LENGTH = 7;

        #endregion

        #region Properties

        private IShortUrlRepository ShortUrlRepository { get; set; }

        #endregion

        #region Constructors

        public ShortUrlService(IShortUrlRepository shortUrlRepository)
        {
            ShortUrlRepository = shortUrlRepository;
        }

        #endregion

        /// <summary>
        /// Создает сокращенный URL по полному в БД.
        /// </summary>
        /// <param name="fullUrl">Полный URL.</param>
        /// <returns>Сокращенный URL.</returns>
        public ShortUrl Create(string fullUrl)
        {
            try
            {
                MvcApplication.Logger.Info($"{MethodBase.GetCurrentMethod()}, start.");

                if (!IsValid(fullUrl))
                {
                    throw new ArgumentException($"Некорректный Url {fullUrl}");
                }

                ShortUrl short_url = ShortUrlRepository.Get(fullUrl);

                if (short_url != null && !short_url.IsDeleted)
                    return short_url;

                string url_guid = Guid.NewGuid().ToString();

                while (ShortUrlRepository.FindShortUrl(url_guid))
                {
                    url_guid = Guid.NewGuid().ToString();
                }

                short_url = new ShortUrl
                {
                    ShortUrlValue = GetUrlTail(url_guid, SHORT_URL_LENGTH),//"http://Home/Index/" + 
                    FullUrlValue = fullUrl,
                    Count = 0,
                    IsDeleted = false,
                    Date = DateTime.Now
                };

                short_url.ID = ShortUrlRepository.Create(short_url);

                return short_url;
            }
            catch (ArgumentException ae)
            {
                MvcApplication.Logger.Error(ae.Message, ae.StackTrace);
                throw new ArgumentException(String.Join(MethodBase.GetCurrentMethod().ToString(), ae.Message, ae.StackTrace));
            }
            catch (Exception e)
            {
                MvcApplication.Logger.Error(e.Message, e.StackTrace);
                throw new Exception(String.Join(MethodBase.GetCurrentMethod().ToString(), e.Message, e.StackTrace));
            }
        }

        /// <summary>
        /// Изменяет короткий URL.
        /// </summary>
        /// <param name="urlView"></param>
        /// <returns></returns>
        public bool Edit(ShortUrlView urlView)
        {
            try
            {
                MvcApplication.Logger.Info($"{MethodBase.GetCurrentMethod()}, start.");

                if (!IsValid(urlView.UrlValue))
                    return false;

                bool ex_short_url = ShortUrlRepository.FindShortUrl(urlView.UrlValue);

                if (ex_short_url)
                    return false;

                ShortUrlRepository.Update(urlView);

                return true;
            }
            catch (Exception e)
            {
                MvcApplication.Logger.Error(e.Message, e.StackTrace);
                throw new Exception(String.Join(MethodBase.GetCurrentMethod().ToString(), e.Message, e.StackTrace));
            }
        }

        public void Delete(int id)
        {
            try
            {
                MvcApplication.Logger.Info($"{MethodBase.GetCurrentMethod()}, start.");

                ShortUrlRepository.Delete(id);
            }
            catch (Exception e)
            {
                MvcApplication.Logger.Error(e.Message, e.StackTrace);
                throw new Exception(String.Join(MethodBase.GetCurrentMethod().ToString(), e.Message, e.StackTrace));
            }
        }

        /// <summary>
        /// Возвращает хвост URL заданного размера.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="length">Длина хвоста URL.</param>
        /// <returns>Хвост URL.</returns>
        private string GetUrlTail(string url, int length)
        {
            try
            {
                MvcApplication.Logger.Info($"{MethodBase.GetCurrentMethod()}, start.");

                string result = url.Substring(url.Length - length, length);

                return result;
            }
            catch (Exception e)
            {
                MvcApplication.Logger.Error(e.Message, e.StackTrace);
                throw new Exception(String.Join(MethodBase.GetCurrentMethod().ToString(), e.Message, e.StackTrace));
            }
        }

        /// <summary>
        /// Проверяет, что заданный URL является валидным.
        /// </summary>
        /// <param name="strUrl">URL, который необходимо проверить.</param>
        /// <returns>Признак валидности URL.</returns>
        public bool IsValid(string strUrl)
        {
            try
            {
                MvcApplication.Logger.Info($"{MethodBase.GetCurrentMethod()}, start.");

                bool isValid = Uri.IsWellFormedUriString(strUrl, UriKind.Absolute);

                return isValid;
            }
            catch (Exception e)
            {
                MvcApplication.Logger.Error(e.Message, e.StackTrace);
                throw new Exception(String.Join(MethodBase.GetCurrentMethod().ToString(), e.Message, e.StackTrace));
            }
        }

        /// <summary>
        /// Возвращает список URL, которые не являются удаленными.
        /// </summary>
        /// <returns>Список URL, которые не являются удаленными.</returns>
        public List<ShortUrl> GetShortUrlList()
        {
            try
            {
                MvcApplication.Logger.Info($"{MethodBase.GetCurrentMethod()}, start.");

                List<ShortUrl> result = ShortUrlRepository.GetShortUrlList();

                return result;
            }
            catch (Exception e)
            {
                MvcApplication.Logger.Error(e.Message, e.StackTrace);
                throw new Exception(String.Join(MethodBase.GetCurrentMethod().ToString(), e.Message, e.StackTrace));
            }
        }

        /// <summary>
        /// Возвращает сокращенный URL по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сокращенного URL.</param>
        /// <returns>Сокращенный URL.</returns>
        public ShortUrlView Get(int id)
        {
            try
            {
                MvcApplication.Logger.Info($"{MethodBase.GetCurrentMethod()}, start.");

                ShortUrl shortUrl = ShortUrlRepository.Get(id);

                ShortUrlView shortUrlView = new ShortUrlView()
                {
                    ID = shortUrl.ID,
                    UrlValue = shortUrl.ShortUrlValue
                };

                return shortUrlView;
            }
            catch (Exception e)
            {
                MvcApplication.Logger.Error(e.Message, e.StackTrace);
                throw new Exception(String.Join(MethodBase.GetCurrentMethod().ToString(), e.Message, e.StackTrace));
            }
        }

        /// <summary>
        /// Обновляет количество переходов по сокращенному URL.
        /// </summary>
        /// <param name="shortUrlValue">Сокращенный URL.</param>
        /// <returns>Значение полного URL или NULL.</returns>
        public string UpdateTransitions(string shortUrlValue)
        {
            try
            {
                MvcApplication.Logger.Info($"{MethodBase.GetCurrentMethod()}, start.");

                string result = ShortUrlRepository.UpdateTransitions(shortUrlValue);
                return result;
            }
            catch (Exception e)
            {
                MvcApplication.Logger.Error(e.Message, e.StackTrace);
                throw new Exception(String.Join(MethodBase.GetCurrentMethod().ToString(), e.Message, e.StackTrace));
            }
        }
    }
}
