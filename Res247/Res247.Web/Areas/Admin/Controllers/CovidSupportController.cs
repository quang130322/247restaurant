using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using Res247.Models.Common;
using Res247.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Res247.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CovidSupportController : Controller
    {
        private readonly IOrderServices _orderServices;
        private readonly IShipperService _shipperService;
        private readonly ICovidInfoServices _covidInfoServices;
        private readonly ICovidShipperService _covidShipperService;

        public CovidSupportController(IOrderServices orderServices, 
            IShipperService shipperService, 
            ICovidShipperService covidShipperService, 
            ICovidInfoServices covidInfoServices)
        {
            _orderServices = orderServices;
            _shipperService = shipperService;
            _covidShipperService = covidShipperService;
            _covidInfoServices = covidInfoServices;
        }

        private AccountManager _userManager;
        public AccountManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<AccountManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Admin/CovidSupport
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CustomerSupport(int? pageIndex = 1, int pageSize = 10)
        {
            ViewData["CurrentPageSize"] = pageSize;
            var covids = _covidInfoServices.GetPositiveCustomer();
            int pageNum = pageIndex ?? 1;
            return View(covids.ToPagedList(pageNum, pageSize));
        }

        public string GetCustomerNameById(string id)
        {
            var name = UserManager.FindById(id).Name;
            return name;
        }

        public string GetShipperNameById(int id)
        {
            var name = _shipperService.GetById(id).Name;
            return name;
        }

        public ActionResult ShipperSupport(int? pageIndex = 1, int pageSize = 10)
        {
            ViewData["CurrentPageSize"] = pageSize;
            var covids = _covidShipperService.GetPositiveShipper();
            int pageNum = pageIndex ?? 1;
            return View(covids.ToPagedList(pageNum, pageSize));
        }

        public ActionResult CusHistory(string id, int? pageIndex = 1, int pageSize = 10)
        {
            ViewData["CurrentPageSize"] = pageSize;
            var orders = _orderServices.GetShippedOrderHistory(id);
            int pageNum = pageIndex ?? 1;
            return View(orders.ToPagedList(pageNum, pageSize));
        }
    }
}