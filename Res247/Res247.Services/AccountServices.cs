using Res247.Data.Infrastructure;
using Res247.Models.Security;
using Res247.Services.BaseServices;

namespace Res247.Services
{
    public class AccountServices : BaseServices<Account>, IAccountServices
    {
        public AccountServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
