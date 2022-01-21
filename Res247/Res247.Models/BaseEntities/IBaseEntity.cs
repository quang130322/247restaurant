using System;

namespace Res247.Models.BaseEntities
{
    public interface IBaseEntity
    {
        int Id { get; set; }

        bool IsDeleted { get; set; }
    }
}
