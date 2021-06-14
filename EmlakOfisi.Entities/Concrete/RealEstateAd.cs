using EmlakOfisi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmlakOfisi.Entities.Concrete
{
    public class RealEstateAd : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string AdName { get; set; }
        public int SquareMeter { get; set; }
        public int NumberOfRoomsId { get; set; }
        public int YearBuilt { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual NumberOfRoom NumberOfRooms { get; set; }
        public virtual User User { get; set; }
    }
}
