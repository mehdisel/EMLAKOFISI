using EmlakOfisi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmlakOfisi.Entities.Concrete
{
    public class NumberOfRoom:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RealEstateAd> RealEstateAds { get; set; }
    }
}
