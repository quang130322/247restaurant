using Res247.Models.Common;
using Res247.Services.BaseServices;
using System.Collections.Generic;

namespace Res247.Services
{
    public interface ICovidInfoServices : IBaseService<CovidInfo>
    {
        CovidInfo GetCovidInfoByAccountId(string accountId);
        IEnumerable<CovidInfo> GetPositiveCustomer();
    }
}
