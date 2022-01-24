using Res247.Data.Infrastructure;
using Res247.Models.Common;
using Res247.Services.BaseServices;

namespace Res247.Services
{
    public class ShipperServices : BaseServices<Shipper>, IShipperServices
    {
        public ShipperServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
