using ShortCutURL.DAL;
using ShortCutURL.Models.DataModel;
using ShortCutURL.Models.Interfaces;
using ShortCutURL.Models.Repositories;
using ShortCutURL.Models.Services;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace ShortCutURL.Controllers
{
    public class HomeController : Controller
    {
        #region Properties

        private readonly ShortUrlContext shortUrlContext = new ShortUrlContext();

        private IShortUrlService ShortUrlService { get;} = new ShortUrlService(new ShortUrlRepository());

        #endregion

        [Route("Home/Index/{shortUrlValue}")]
        public ActionResult Index(string shortUrlValue)
        {
            string fullUrlValue = ShortUrlService.UpdateTransitions(shortUrlValue);

            if (fullUrlValue != null)
                return Redirect(fullUrlValue);

            ViewBag.NewUrl = shortUrlValue;

            return RedirectToAction("Create");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ShortUrl shortUrl = shortUrlContext.ShortUrls.Find(id);

            if (shortUrl == null)
            {
                return HttpNotFound();
            }

            return View(shortUrl);
        }

        public ActionResult Create()
        {
            ShortUrlView viewModel = new ShortUrlView();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShortUrlView model)
        {
            ShortUrl shortUrl = ShortUrlService.Create(model.UrlValue);

            if(shortUrl?.ID == null)
                return View("Create");

            return RedirectToAction("Details", shortUrl);
        }

        /// <summary>
        /// Изменяет короткий URL.
        /// </summary>
        /// <param name="shortUrlView"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(ShortUrlView shortUrlView)
        {
            bool result = false;

            if (ModelState.IsValid)
                result = ShortUrlService.Edit(shortUrlView);

            if(result)
                return RedirectToAction("Statistics");

            return View(shortUrlView);
        }

        /// <summary>
        /// Получает страницу редактирования короткого URL.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ShortUrlView shortUrlView = ShortUrlService.Get(id);

            return View("Edit", shortUrlView);
        }

        [HttpGet]
        public ViewResult Statistics()
        {
            List<ShortUrl> result = ShortUrlService.GetShortUrlList();

            return View("Statistics", result);
        }

        public ActionResult Delete(int id)
        {
            ShortUrlService.Delete(id);

            List<ShortUrl> result = ShortUrlService.GetShortUrlList();

            return View("Statistics", result);
        }
    }
}
