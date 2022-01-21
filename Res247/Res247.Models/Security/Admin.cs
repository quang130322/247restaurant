using Res247.Models.BaseEntities;

namespace Res247.Models.Security
{
    public class Admin : BaseEntity
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
