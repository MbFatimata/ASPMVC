using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Roomy.Models
{
    public class User : BaseModel
    {
        [Required(ErrorMessage ="Le champ {0} est obligatoire")]
        [Display(Name ="Nom")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Le champ {0} doit contenir entre {2} et {1} caractères")]
        public string Lastname { get; set; }

        [Display(Name = "Prenom")]
        public string Firstname { get; set; }

        [Required(ErrorMessage ="Le champ {0} est obligatoire")]
        [Display(Name ="Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                           @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                           @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                           ErrorMessage = "L'adresse mail n'est pas au bon format")]
        public string Mail { get; set; }

        [Required(ErrorMessage ="Le champ {0} est obligatoire")]
        [Display(Name ="Date de naissance")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Le champ {0} est obligatoire")]
        [Display(Name ="Mot de passe")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Le champ {0} doit contenir entre {2} et {1} caractères")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,15}$",
                           ErrorMessage = "Le {0} est invalide ")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Le champ {0} est obligatoire")]
        [Display(Name ="Confirmation du mot de passe")]
        [DataType(DataType.Password)]
        [Compare ("Password", ErrorMessage = "Erreur sur la {0}")]
        public string ConfirmedPassword { get; set; }

        public int CivilityID { get; set; }
        [ForeignKey("CivilityID")]
        public Civility Civility { get; set; }

    }
}