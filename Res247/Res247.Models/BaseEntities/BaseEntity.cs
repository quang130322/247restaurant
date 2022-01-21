using System;
using System.ComponentModel.DataAnnotations;

namespace Res247.Models.BaseEntities
{
    public class BaseEntity : IBaseEntity
    {
        public BaseEntity()
        {
            IsDeleted = false;
        }

        public int Id { get; set; }

        [Display(Name = "Is Deleted")]
        public bool IsDeleted { get; set; }
    }
}
