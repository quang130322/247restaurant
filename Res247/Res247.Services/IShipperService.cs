using Res247.Models.Common;
using Res247.Services.BaseServices;

namespace Res247.Services
{
    public interface IShipperService : IBaseService<Shipper>
    {
        Shipper GetShipperByAccId(string accId);
    }
}
