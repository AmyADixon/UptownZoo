using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UptownZoo.Models {
    public class Animals {
        [Key]
        public int AnimalID { get; set; }

        [Required]
        [StringLength(100)] // 100 Characters for any name
        [Column("Name", TypeName ="varchar") ]
        public string Species { get; set; } 
    }
}