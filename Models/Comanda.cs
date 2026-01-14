using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Proiect_Cafenea.Models
{

    public class Comanda
    {
        public int Id { get; set; }
        public DateTime DataComanda { get; set; } 

        public int ClientId { get; set; }
        public Client? Client { get; set; } 

        public ICollection<DetaliiComanda>? Detalii { get; set; }
    }
}