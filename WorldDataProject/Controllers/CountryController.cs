using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WorldDataProject.Models;

namespace WorldDataProject.Controllers
{
    public class CountryController : Controller
    {
        [HttpGet("/countries")]
        public ActionResult Index()
        {
            List<Country> allCountries = Country.GetAll();
            return View(allCountries);
        }


    }
}
