using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Roomy.Models
{
    public class RoomFile:BaseModel
    {
        [Required ( ErrorMessage ="Champ {0} obligatoire")]
        [StringLength(254)]
        public string Name { get; set; }

        [Required(ErrorMessage="Champ {0} obligatoire")]
        [StringLength(100)]
        public string ContentType { get; set; }

        [Required(ErrorMessage = "Champ {0} obligatoire")]
        public byte[] Content { get; set; }

        public int RoomID { get; set; }
        [ForeignKey("RoomID")]
        public Room Room { get; set; }
    }
}