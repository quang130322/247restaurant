using Res247.Data.Infrastructure;
using Res247.Models.Common;
using Res247.Services.BaseServices;

namespace Res247.Services
{
    public class CovidInfoServices : BaseServices<CovidInfo>, ICovidInfoServices
    {
        public CovidInfoServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
