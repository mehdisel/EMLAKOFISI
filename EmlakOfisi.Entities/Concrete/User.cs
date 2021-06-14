using EmlakOfisi.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmlakOfisi.Entities.Concrete
{
    public class User:IdentityUser<int>,IEntity
    {
        public virtual ICollection<CompanyUser> CompanyUsers { get; set; }
        public virtual ICollection<RealEstateAd> RealEstateAds { get; set; }
    }
}
