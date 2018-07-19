using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Roomy.Models
{
    public class Category:BaseModel
    {
        [Required(ErrorMessage = "Nom obligatoire")]
        [StringLength(20, ErrorMessage = "Trop long")]
        [Display(Name = "Libellé")]
        public string Name { get; set; }
    }
}