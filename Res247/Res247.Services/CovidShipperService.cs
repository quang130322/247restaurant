using Res247.Data.Infrastructure;
using Res247.Models.Common;
using Res247.Services.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Res247.Services
{
    public class CovidShipperService : BaseServices<CovidShipper>, ICovidShipperService
    {
        public CovidShipperService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public CovidShipper GetCovidShipperByShipperId(int id)
        {
            return _unitOfWork.CovidShipperRepository.GetQuery().Where(x=>x.ShipperId == id)
                                                                        .OrderByDescending(o=>o.DateCreated)
                                                                        .FirstOrDefault();
        }

        public IEnumerable<CovidShipper> GetPositiveShipper()
        {
            DateTime previousDay = DateTime.Now.AddDays(-14);

            return _unitOfWork.CovidShipperRepository.GetQuery().Where(x => x.HealthStatus == true
                                                                    && x.DateCreated >= previousDay)
                                                                    .OrderByDescending(c => c.DateCreated).ToList();
        }
    }
}
