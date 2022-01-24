using Res247.Data.Infrastructure;
using Res247.Models.Common;
using Res247.Services.BaseServices;

namespace Res247.Services
{
    public class CustomerServices : BaseServices<Customer>, ICustomerServices
    {
        public CustomerServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
