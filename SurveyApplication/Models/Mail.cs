using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyApplication.Models
{
    public class Mail
    {

        [Required]
        public string Adi { get; set; }
        [Required]
        public string Soyadi { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Konu { get; set; }
        [Required]
        public string Mesaj { get; set; }
    }
}
