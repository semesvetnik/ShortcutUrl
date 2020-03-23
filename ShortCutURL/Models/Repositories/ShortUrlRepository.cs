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
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        
        /// <summary>
        /// Контекст сокращенного URL.
        /// </summary>
        private ShortUrlContext shortUrlContext = new ShortUrlContext();

        /// <summary>
        /// Создает сокращенный URL по полному в БД.
        /// </summary>
        /// <param name="shortUrl">Параметры сокращенного URL.</param>
        /// <returns>Идентификатор сокращенного URL.</returns>
        public int Create(ShortUrl shortUrl)
        {
            try
            {
                shortUrlContext.ShortUrls.Add(shortUrl);
                shortUrlContext.SaveChanges();

                int? id = shortUrl.ID;
            
                if (!id.HasValue)
                    throw new Exception("Ошибка создания id сокращенного URL.");

                logger.Info($"{MethodBase.GetCurrentMethod()}, сокращенный URL id= {shortUrl.ID} создан.");
                return id.Value;
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e.StackTrace);
                throw;
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
                if (shortUrl.ID == null)
                {
                    logger.Info($"{MethodBase.GetCurrentMethod()}, сокращенный URL не обновлен.");

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

                logger.Info($"{MethodBase.GetCurrentMethod()}, сокращенный URL id= {shortUrl.ID} обновлен.");
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e.StackTrace);
                throw;
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
                ShortUrl result = shortUrlContext.ShortUrls.Find(shortUrlView.ID);

                if (result == null)
                    throw new Exception($"URL с указанным id = {shortUrlView.ID} не найден.");

                result.ShortUrlValue = shortUrlView.UrlValue;
                result.Date = DateTime.Now;
                result.Count = 0;

                shortUrlContext.SaveChanges();

                logger.Info($"{MethodBase.GetCurrentMethod()}, сокращенный URL id= {shortUrlView.ID} обновлен.");
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e.StackTrace);
                throw;
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
                ShortUrl shortUrl = shortUrlContext.ShortUrls.Find(id);

                if (shortUrl != null)
                {
                    shortUrl.IsDeleted = true;
                    shortUrlContext.SaveChanges();
                    logger.Info($"{MethodBase.GetCurrentMethod()}, сокращенный URL id= {id} удален.");
                }
                
                logger.Info($"{MethodBase.GetCurrentMethod()}, сокращенный URL id= {id} не удален.");
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e.StackTrace);
                throw;
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
                DbSet<ShortUrl> shortUrls = shortUrlContext.ShortUrls;

                ShortUrl result = shortUrls?.Where(el => el.FullUrlValue == fullUrl).FirstOrDefault();

                logger.Info($"{MethodBase.GetCurrentMethod()}, сокращенный URL fullUrl= {fullUrl} найден.");
                return result;
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e.StackTrace);
                throw;
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
                DbSet<ShortUrl> shortUrls = shortUrlContext.ShortUrls;

                ShortUrl result = shortUrls?.Where(el => el.ID == id).FirstOrDefault();

                logger.Info($"{MethodBase.GetCurrentMethod()}, сокращенный URL id= {id} найден.");
                return result;
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e.StackTrace);
                throw;
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
                List<ShortUrl> result = shortUrlContext.ShortUrls.Where(el => !el.IsDeleted).ToList();
                logger.Info($"{MethodBase.GetCurrentMethod()}, список неудаленных URL создан.");

                return result;
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e.StackTrace);
                throw;
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
                bool result = shortUrlContext.ShortUrls.Any(el => el.ShortUrlValue == shortUrl);
                logger.Info($"{MethodBase.GetCurrentMethod()}, сокращенный URL найден= {shortUrl}.");

                return result;
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e.StackTrace);
                throw;
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
                bool result = shortUrlContext.ShortUrls.Any(el => el.FullUrlValue == fullUrl);
                logger.Info($"{MethodBase.GetCurrentMethod()}, наличие полного URL= {result}.");

                return result;
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e.StackTrace);
                throw;
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
                ShortUrl shortUrl = shortUrlContext.ShortUrls.FirstOrDefault(t => t.ShortUrlValue == shortUrlValue);

                if (shortUrl != default(ShortUrl))
                {
                    shortUrl.Count += 1;
                    shortUrlContext.SaveChanges();

                    logger.Info($"{MethodBase.GetCurrentMethod()}, количество переходов по сокращенному URL обновилось.");
                    return shortUrl.FullUrlValue;
                }

                logger.Warn($"{MethodBase.GetCurrentMethod()}, количество переходов по сокращенному URL НЕ обновилось.");
                return null;
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e.StackTrace);
                throw;
            }
        }
    }
}
