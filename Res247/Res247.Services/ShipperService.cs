using Res247.Data.Infrastructure;
using Res247.Models.Common;
using Res247.Services.BaseServices;
using System.Linq;

namespace Res247.Services
{
    public class ShipperService : BaseServices<Shipper>, IShipperService
    {
        public ShipperService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Shipper GetShipperByAccId(string accId)
        {
            return _unitOfWork.ShipperRepository.GetQuery().FirstOrDefault(x=>x.Account.Id == accId);
        }
    }
}
