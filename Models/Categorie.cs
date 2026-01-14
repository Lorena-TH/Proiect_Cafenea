using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Proiect_Cafenea.Models
{
    public class Categorie
    {
        [Key] 
        public int Id { get; set; }

        [Required] 
        [Display(Name = "Nume Categorie")] 
        public string Nume { get; set; }

        public ICollection<Produs>? Produse { get; set; } = new List<Produs>();
    }
}