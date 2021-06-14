using EmlakOfisi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmlakOfisi.Entities.Concrete
{
    public class Company:IEntity
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }

        public virtual ICollection<CompanyUser> CompanyUsers { get; set; }
    }
}
