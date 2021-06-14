using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmlakOfisi.Models
{
    public class UserSignUpViewModel
    {
        [Display(Name = "Kullanıcı Adı:")]
        [Required(ErrorMessage = "Kullanıcı adı boş geçilemez")]
        public string UserName { get; set; }
        [Display(Name = "Firma Adı:")]
        [Required(ErrorMessage = "Firma adı boş geçilemez")]
        public string CompanyName { get; set; }

    }
}
