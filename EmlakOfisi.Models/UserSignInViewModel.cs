using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmlakOfisi.Models
{
    public class UserSignInViewModel
    {
        [Display(Name = "Kullanıcı Adı:")]
        [Required(ErrorMessage = "Kullanıcı adı boş geçilemez")]
        public string UserName { get; set; }
        [Display(Name = "Şifre :")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre boş geçilemez")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

    }
}
