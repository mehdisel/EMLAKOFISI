using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmlakOfisi.Models
{
    public class UserChangePasswordViewModel
    {
        [Display(Name = "Eski Şifre :")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Eski Şifre boş geçilemez")]
        public string OldPassword { get; set; }
        [Display(Name = "Yeni Şifre :")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Yeni Şifre boş geçilemez")]
        public string Password { get; set; }
        [Display(Name = "Yeni Şifre Tekrar :")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Yeni Şifre Tekrar boş geçilemez")]
        [Compare("Password", ErrorMessage = "Parolalar eşleşmiyor")]
        public string ConfirmPassword { get; set; }
    }
}
