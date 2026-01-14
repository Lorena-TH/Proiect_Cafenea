using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;




namespace Proiect_Cafenea.Models
{
    public class Produs
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nume { get; set; }

        [Range(0.01, 1000)] 
        public decimal Pret { get; set; }

        [DisplayName("Categorie")]
        public int CategorieId { get; set; }

        [ForeignKey("CategorieId")] 
        public Categorie? Categorie { get; set; }
        public ICollection<DetaliiComanda>? DetaliiComenzi { get; set; }
    }
}