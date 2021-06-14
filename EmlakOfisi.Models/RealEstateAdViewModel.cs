using System;
using System.Collections.Generic;
using System.Text;

namespace EmlakOfisi.Models
{
    public class RealEstateAdFilterViewModel
    {
        public RealEstateAdFilterViewModel()
        {
            Items = new List<RealEstateAdViewModel>();
        }
        public List<RealEstateAdViewModel> Items { get; set; }
        public RealEstateAdFilterDefaults Filters { get; set; }
    }


    public class RealEstateAdViewModel
    {
        public int Id { get; set; }
        public string AdName { get; set; }
        public int SquareMeter { get; set; }
        public string NumberOfRoom { get; set; }
        public int YearBuilt { get; set; }
        public decimal Price { get; set; }
        public string PriceConverted { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateConverted { get; set; }
    }
    public class RealEstateAdFilterDefaults
    {
        public int MinSquareMeter { get; set; }
        public int MaxSquareMeter { get; set; }
        public int MinYearBuilt { get; set; }
        public int MaxYearBuilt { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public DateTime SinceDate { get; set; }
        public int SelectedNumberOfRooms { get; set; }
        public List<RealEstateAdNumberOfRoom> ExistingNumberOfRooms { get; set; }

    }
    public class RealEstateAdFilterInModel
    {
        public int MinSquareMeter { get; set; }
        public int MaxSquareMeter { get; set; }
        public int MinYearBuilt { get; set; }
        public int MaxYearBuilt { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public int SelectedNumberOfRooms { get; set; }

        public int PageCounter { get; set; }

    }

    public class RealEstateAdNumberOfRoom
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
