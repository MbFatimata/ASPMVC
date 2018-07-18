using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Roomy.Models
{
    public class Room:BaseModel
    {
        [Required(ErrorMessage="Le champ {0} est obligatoire.")]
        [StringLength(50)]
        [Display(Name="Libellé")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Le champ {0} est obligatoire.")]
        [Display(Name = "Nombre de place")]
        [Range(0,50)]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Le champ {0} est obligatoire.")]
        [Display(Name = "Tarif")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

    }
}