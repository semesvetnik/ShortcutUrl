using NLog;
using ShortCutURL.DAL;
using ShortCutURL.Models.DataModel;
using ShortCutURL.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace ShortCutURL.Models.Repositories
{
    /// <summary>
    /// Содержит методы работы с сокращенным URL.
    /// </summary>
    public class ShortUrlRepository : IShortUrlRepository
    {
        #region Properties

        /// <summary>
        /// Контекст сокращенного URL.
        /// </summary>
        private ShortUrlContext shortUrlContext = new ShortUrlContext();

        #endregion

        #region Methods

        /// <summary>
        /// Создает сокращенный URL по полному в БД.
        /// </summary>
        /// <param name="shortUrl">Параметры сокращенного URL.</param>
        /// <returns>Идентификатор сокращенного URL.</returns>
        public int Create(ShortUrl shortUrl)
        {
            try
            {
                MvcApplication.Logger.Info($"{MethodBase.GetCurrentMethod()}, start.");

                shortUrlContext.ShortUrls.Add(shortUrl);
                shortUrlContext.SaveChanges();

                int? id = shortUrl.ID;
            
                if (!id.HasValue)
                    throw new Exception("Ошибка создания id сокращенного URL.");

                MvcApplication.Logger.Info($"{MethodBase.GetCurrentMethod()}, сокращенный URL id= {shortUrl.ID} создан.");
                return id.Value;
            }
            catch (Exception e)
            {
                MvcApplication.Logger.Error(e.Message, e.StackTrace);
                throw new Exception($"{ MethodBase.GetCurrentMethod() }, {e.Message}");
            }
        }

        /// <summary>
        /// Обновляет параметры сокращенного URL.
        /// </summary>
        /// <param name="shortUrl">Сокращенный URL.</param>
        public void Update(ShortUrl shortUrl)
        {
            try
            {
                MvcApplication.Logger.Info($"{MethodBase.GetCurrentMethod()}, start.");

                if (shortUrl.ID == null)
                {
                    MvcApplication.Logger.Info($"{MethodBase.GetCurrentMethod()}, сокращенный URL не обновлен.");

                    return;
                }

                ShortUrl result = shortUrlContext.ShortUrls.Find(shortUrl.ID.Value);

                if (result == null)
                    throw new Exception($"URL с указанным id = {shortUrl.ID} не найден.");

                result.ShortUrlValue = shortUrl.ShortUrlValue;
                result.FullUrlValue = shortUrl.FullUrlValue;
                result.Count = shortUrl.Count;
                result.IsDeleted = shortUrl.IsDeleted;
                result.Date = shortUrl.Date;

                shortUrlContext.SaveChanges();

                MvcApplication.Logger.Info($"{MethodBase.GetCurrentMethod()}, сокращенный URL id= {shortUrl.ID} обновлен.");
            }
            catch (Exception e)
            {
                MvcApplication.Logger.Error(e.Message, e.StackTrace);
                throw new Exception($"{ MethodBase.GetCurrentMethod() }, {e.Message}");
            }
        }

        /// <summary>
        /// Обновляет значение короткого URL.
        /// </summary>
        /// <param name="shortUrlView">Модель обновления сокращенного URL.</param>
        public void Update(ShortUrlView shortUrlView)
        {
            try
            {
                MvcApplication.Logger.Info($"{MethodBase.GetCurrentMethod()}, start.");

                ShortUrl result = shortUrlContext.ShortUrls.Find(shortUrlView.ID);

                if (result == null)
                    throw new Exception($"URL с указанным id = {shortUrlView.ID} не найден.");

                result.ShortUrlValue = shortUrlView.UrlValue;
                result.Date = DateTime.Now;
                result.Count = 0;

                shortUrlContext.SaveChanges();

                MvcApplication.Logger.Info($"{MethodBase.GetCurrentMethod()}, сокращенный URL id= {shortUrlView.ID} обновлен.");
            }
            catch (Exception e)
            {
                MvcApplication.Logger.Error(e.Message, e.StackTrace);
                throw new Exception($"{ MethodBase.GetCurrentMethod() }, {e.Message}");
            }
        }

        /// <summary>
        /// Удаляет URL по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор удаляемого URL.</param>
        public void Delete(int id)
        {
            try
            {
                MvcApplication.Logger.Info($"{MethodBase.GetCurrentMethod()}, start.");

                ShortUrl shortUrl = shortUrlContext.ShortUrls.Find(id);

                if (shortUrl != null)
                {
                    shortUrl.IsDeleted = true;
                    shortUrlContext.SaveChanges();
                    MvcApplication.Logger.Info($"{MethodBase.GetCurrentMethod()}, сокращенный URL id= {id} удален.");
                }

                MvcApplication.Logger.Info($"{MethodBase.GetCurrentMethod()}, сокращенный URL id= {id} не удален.");
            }
            catch (Exception e)
            {
                MvcApplication.Logger.Error(e.Message, e.StackTrace);
                throw new Exception($"{ MethodBase.GetCurrentMethod() }, {e.Message}");
            }
        }

        /// <summary>
        /// Возвращает сокращенный URL по значению полного из БД.
        /// </summary>
        /// <param name="fullUrl">Полный URL.</param>
        /// <returns>Сокращенный URL или NULL, если отсутствует в БД.</returns>
        public ShortUrl Get(string fullUrl)
        {
            try
            {
                MvcApplication.Logger.Info($"{MethodBase.GetCurrentMethod()}, start.");

                DbSet<ShortUrl> shortUrls = shortUrlContext.ShortUrls;

                ShortUrl result = shortUrls?.Where(el => el.FullUrlValue == fullUrl).FirstOrDefault();

                MvcApplication.Logger.Info($"{MethodBase.GetCurrentMethod()}, сокращенный URL fullUrl= {fullUrl} найден.");
                return result;
            }
            catch (Exception e)
            {
                MvcApplication.Logger.Error(e.Message, e.StackTrace);
                throw new Exception($"{ MethodBase.GetCurrentMethod() }, {e.Message}");
            }
        }

        /// <summary>
        /// Возвращает сокращенный URL по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сокращенного URL.</param>
        /// <returns>Сокращенный URL.</returns>
        public ShortUrl Get(int id)
        {
            try
            {
                MvcApplication.Logger.Info($"{MethodBase.GetCurrentMethod()}, start.");

                DbSet<ShortUrl> shortUrls = shortUrlContext.ShortUrls;

                ShortUrl result = shortUrls?.Where(el => el.ID == id).FirstOrDefault();

                MvcApplication.Logger.Info($"{MethodBase.GetCurrentMethod()}, сокращенный URL id= {id} найден.");
                return result;
            }
            catch (Exception e)
            {
                MvcApplication.Logger.Error(e.Message, e.StackTrace);
                throw new Exception($"{ MethodBase.GetCurrentMethod() }, {e.Message}");
            }
        }

        /// <summary>
        /// Возвращает список URL, которые не являются удаленными.
        /// </summary>
        /// <returns>Список URL, которые не являются удаленными</returns>
        public List<ShortUrl> GetShortUrlList()
        {
            try
            {
                MvcApplication.Logger.Info($"{MethodBase.GetCurrentMethod()}, start.");

                List<ShortUrl> result = shortUrlContext.ShortUrls.Where(el => !el.IsDeleted).ToList();
                MvcApplication.Logger.Info($"{MethodBase.GetCurrentMethod()}, список неудаленных URL создан.");

                return result;
            }
            catch (Exception e)
            {
                MvcApplication.Logger.Error(e.Message, e.StackTrace);
                throw new Exception($"{ MethodBase.GetCurrentMethod() }, {e.Message}");
            }
        }

        /// <summary>
        /// Проверяет наличие сокращенного URL в БД. Если найден, то возвращает true.
        /// </summary>
        /// <param name="shortUrl">Сокращенный URL.</param>
        /// <returns>Признак того, что сокращенный URL уже используется в БД.</returns>
        public bool FindShortUrl(string shortUrl)
        {
            try
            {
                MvcApplication.Logger.Info($"{MethodBase.GetCurrentMethod()}, start.");

                bool result = shortUrlContext.ShortUrls.Any(el => el.ShortUrlValue == shortUrl);
                MvcApplication.Logger.Info($"{MethodBase.GetCurrentMethod()}, сокращенный URL найден= {shortUrl}.");

                return result;
            }
            catch (Exception e)
            {
                MvcApplication.Logger.Error(e.Message, e.StackTrace);
                throw new Exception($"{ MethodBase.GetCurrentMethod() }, {e.Message}");
            }
        }

        /// <summary>
        /// Проверяет наличие полного URL в БД. Если найден, то возвращает true.
        /// </summary>
        /// <param name="fullUrl">Полный URL.</param>
        /// <returns>Признак того, что полный URL уже используется в БД.</returns>
        public bool FindFullUrl(string fullUrl)
        {
            try
            {
                MvcApplication.Logger.Info($"{MethodBase.GetCurrentMethod()}, start.");

                bool result = shortUrlContext.ShortUrls.Any(el => el.FullUrlValue == fullUrl);
                MvcApplication.Logger.Info($"{MethodBase.GetCurrentMethod()}, наличие полного URL= {result}.");

                return result;
            }
            catch (Exception e)
            {
                MvcApplication.Logger.Error(e.Message, e.StackTrace);
                throw new Exception($"{ MethodBase.GetCurrentMethod() }, {e.Message}");
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

                ShortUrl shortUrl = shortUrlContext.ShortUrls.FirstOrDefault(t => t.ShortUrlValue == shortUrlValue);

                if (shortUrl != default(ShortUrl))
                {
                    shortUrl.Count += 1;
                    shortUrlContext.SaveChanges();

                    MvcApplication.Logger.Info($"{MethodBase.GetCurrentMethod()}, количество переходов по сокращенному URL обновилось.");
                    return shortUrl.FullUrlValue;
                }

                MvcApplication.Logger.Warn($"{MethodBase.GetCurrentMethod()}, количество переходов по сокращенному URL НЕ обновилось.");
                return null;
            }
            catch (Exception e)
            {
                MvcApplication.Logger.Error(e.Message, e.StackTrace);
                throw new Exception($"{ MethodBase.GetCurrentMethod() }, {e.Message}");
            }
        }

        #endregion
    }
}
