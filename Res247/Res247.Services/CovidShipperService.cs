using Res247.Data.Infrastructure;
using Res247.Models.Common;
using Res247.Services.BaseServices;
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
            return _unitOfWork.CovidShipperRepository.GetQuery().FirstOrDefault(x=>x.ShipperId == id);
        }
    }
}
