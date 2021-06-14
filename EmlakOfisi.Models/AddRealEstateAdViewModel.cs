using EmlakOfisi.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmlakOfisi.Models
{
    public class AddRealEstateAdViewModel
    {
        public int UserId { get; set; }
        [Display(Name = "İlan Adı:")]
        [Required(ErrorMessage = "İlan adı boş geçilemez")]
        public string AdName { get; set; }
        [Display(Name = "Metrekaresi:")]
        [Required(ErrorMessage = "Metrekare alanı boş geçilemez")]
        public int SquareMeter { get; set; }
        [Display(Name = "Oda Sayısı:")]
        [Required(ErrorMessage = "Oda Sayısı alanı boş geçilemez")]
        public int NumberOfRoomsId { get; set; }
        public IEnumerable<SelectListItem> NumberOfRoomsItems { get; set; }

        [Display(Name = "Konut Yaşı:")]
        [Required(ErrorMessage = "Konut Yaşı alanı boş geçilemez")]
        public int YearBuilt { get; set; }
        [Display(Name = "Fiyatı::")]
        [Required(ErrorMessage = "Fiyat alanı boş geçilemez")]
        public decimal Price { get; set; }

        [Display(Name = "İlan Resmi:")]
        public string ImagePath { get; set; }
        public IFormFile Image { get; set; }

 
    }
}
