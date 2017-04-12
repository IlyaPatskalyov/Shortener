﻿using System.Web.Mvc;
using Shortener.Storage;

namespace Shortener.Front.Controllers
{
    public class MainController : Controller
    {
        private readonly ILinksRepository linksRepository;

        public MainController(ILinksRepository linksRepository)
        {
            this.linksRepository = linksRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Link(string key)
        {
            var link = linksRepository.GetByKeyAndIncrement(key);
            if (link != null)
                return Redirect(link.Url);
            return View("NotFound");
        }
    }
}