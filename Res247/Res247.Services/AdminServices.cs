using Res247.Data.Infrastructure;
using Res247.Models.Security;
using Res247.Services.BaseServices;

namespace Res247.Services
{
    public class AdminServices : BaseServices<Admin>, IAdminServices
    {
        public AdminServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
