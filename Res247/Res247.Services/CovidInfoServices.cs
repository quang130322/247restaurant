using Res247.Data.Infrastructure;
using Res247.Models.Common;
using Res247.Services.BaseServices;
using System.Linq;

namespace Res247.Services
{
    public class CovidInfoServices : BaseServices<CovidInfo>, ICovidInfoServices
    {
        public CovidInfoServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public CovidInfo GetCovidInfoByAccountId(string accountId)
        {
            return _unitOfWork.CovidInfoRepository.GetQuery().Where(x=>x.AccountId == accountId).OrderByDescending(c=>c.DateCreated).FirstOrDefault();
        }
    }
}
