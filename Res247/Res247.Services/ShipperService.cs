using Res247.Data.Infrastructure;
using Res247.Models.Common;
using Res247.Services.BaseServices;
using System.Collections.Generic;
using System.Linq;

namespace Res247.Services
{
    public class ShipperService : BaseServices<Shipper>, IShipperService
    {
        public ShipperService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<Shipper> GetAllActive()
        {
            return _unitOfWork.ShipperRepository.GetQuery().Where(x => x.Status == 0);
        }
    }
}
