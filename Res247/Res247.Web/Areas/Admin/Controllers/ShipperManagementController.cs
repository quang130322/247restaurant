using PagedList;
using Res247.Models.Common;
using Res247.Services;
using Res247.Web.Areas.Admin.ViewModels;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Res247.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ShipperManagementController : Controller
    {
        private readonly IShipperService _shipperService;
        private readonly ICovidShipperService _covidShipperService;
        private readonly IOrderServices _orderServices;

        public ShipperManagementController(IShipperService shipperService, 
            ICovidShipperService covidShipperService,
            IOrderServices orderServices)
        {
            _shipperService = shipperService;
            _covidShipperService = covidShipperService;
            _orderServices = orderServices;
        }

        // GET: Admin/ShipperManagement
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageIndex = 1, int pageSize = 10)
        {
            ViewData["CurrentPageSize"] = pageSize;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            Expression<Func<Shipper, bool>> filter = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                filter = c => c.Name.Contains(searchString);
            }

            Func<IQueryable<Shipper>, IOrderedQueryable<Shipper>> orderBy = null;

            switch (sortOrder)
            {
                case "name_desc":
                    orderBy = n => n.OrderByDescending(c => c.Name);
                    break;
                default:
                    orderBy = n => n.OrderBy(p => p.Name);
                    break;
            }

            var shippers = await _shipperService.GetAsync(filter: filter, orderBy: orderBy, pageIndex: pageIndex ?? 1, pageSize: pageSize);

            return View(shippers);
        }

        // GET: Admin/ShipperManagement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ShipperManagement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShipperViewModel shipperViewModel)
        {
            if (ModelState.IsValid)
            {
                var shipper = new Shipper
                {
                    Name = shipperViewModel.Name,
                    Status = shipperViewModel.Status
                };

                var shipResult = _shipperService.Add(shipper);

                var covid = new CovidShipper
                {
                    DateCreated = DateTime.Now,
                    HaveSymptoms = shipperViewModel.HaveSymptoms,
                    HealthStatus = shipperViewModel.HealthStatus,
                    MeetCovidPatients = shipperViewModel.MeetCovidPatients,
                    TravelToOtherPlace = shipperViewModel.TravelToOtherPlace,
                    Vaccination = shipperViewModel.Vaccination,
                    ShipperId = shipper.Id
                };
                var covidResult = _covidShipperService.Add(covid);

                if (shipResult > 0 && covidResult > 0)
                {
                    TempData["Message"] = "Tạo thành công.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Message"] = "Tạo thất bại. Thử lại sau nhé!";
                }
            }

            return View(shipperViewModel);
        }

        // GET: Admin/ShipperManagement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipper shipper =_shipperService.GetById((int)id);
            if (shipper == null)
            {
                return HttpNotFound();
            }
            var covid = _covidShipperService.GetCovidShipperByShipperId(shipper.Id);
            var shipperViewModel = new ShipperViewModel
            {
                Name = shipper.Name,
                HaveSymptoms = covid.HaveSymptoms,
                HealthStatus = covid.HealthStatus,
                MeetCovidPatients = covid.MeetCovidPatients,
                TravelToOtherPlace = covid.TravelToOtherPlace,
                Vaccination = covid.Vaccination,
                Status = shipper.Status
            };
            return View(shipperViewModel);
        }

        // POST: Admin/ShipperManagement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(ShipperViewModel model)
        {
            if (ModelState.IsValid)
            {
                var shipper = _shipperService.GetById(model.Id);
                if (shipper == null)
                {
                    return HttpNotFound();
                }

                shipper.Status = model.Status;
                shipper.Name = model.Name;

                var covid = new CovidShipper
                {
                    DateCreated = DateTime.Now,
                    HaveSymptoms = model.HaveSymptoms,
                    HealthStatus = model.HealthStatus,
                    MeetCovidPatients = model.MeetCovidPatients,
                    TravelToOtherPlace = model.TravelToOtherPlace,
                    Vaccination = model.Vaccination,
                    ShipperId = shipper.Id
                };

                if(covid.HealthStatus == true)
                {
                    shipper.Status = -1;
                }
                else
                {
                    shipper.Status = 0;
                }

                var shipResult = _shipperService.Update(shipper);

                var covidResult = _covidShipperService.Add(covid);

                if (shipResult && covidResult > 0)
                {
                    TempData["Message"] = "Tạo thành công.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Message"] = "Tạo thất bại. Thử lại sau nhé!";
                }

                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
