using EmlakOfisi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmlakOfisi.Entities.Concrete
{
    public class CompanyUser:IEntity
    {
        public int CompanyId { get; set; }
        public int UserId { get; set; }

        public virtual Company Company { get; set; }
        public virtual User User { get; set; }
    }
}
