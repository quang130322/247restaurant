﻿using Res247.Models.Common;
using Res247.Services.BaseServices;

namespace Res247.Services
{
    public interface ICovidShipperService : IBaseService<CovidShipper>
    {
        CovidShipper GetCovidShipperByShipperId(int id);
    }
}
