using Res247.Models.Common;
using Res247.Services.BaseServices;
using System.Collections.Generic;

namespace Res247.Services
{
    public interface ICovidShipperService : IBaseService<CovidShipper>
    {
        CovidShipper GetCovidShipperByShipperId(int id);
        IEnumerable<CovidShipper> GetPositiveShipper();
    }
}
