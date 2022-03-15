using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Res247.Data;
using Res247.Models.Common;
using Res247.Services;
using Res247.Web.ViewModels;
using Microsoft.AspNet.Identity;

namespace Res247.Web.Controllers
{
    public class CovidInfoesController : Controller
    {
        private readonly ICovidInfoServices _covidInfoServices;

        public CovidInfoesController(ICovidInfoServices covidInfoServices)
        {
            _covidInfoServices = covidInfoServices;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetCovidInfo(CovidInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();

                var covidInfo = new CovidInfo()
                {
                    DateCreated = DateTime.Now,
                    HealthStatus = model.HealthStatus,
                    Vaccination = model.Vaccination,
                    HaveSymptoms = model.HaveSymptoms,
                    MeetCovidPatients = model.MeetCovidPatients,
                    TravelToOtherPlace = model.TravelToOtherPlace,
                    AccountId = userId
                };

                var result = _covidInfoServices.Add(covidInfo);
                if (result > 0)
                {
                    TempData["Message"] = "Cập nhật thành công.";
                    return RedirectToAction("GetCovidInfo", "CovidInfoes");
                }
            }
            return View(model);
        }
    }
}
