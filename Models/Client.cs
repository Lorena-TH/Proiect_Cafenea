using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Proiect_Cafenea.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public string Email { get; set; }
        public ICollection<Comanda>? Comenzi { get; set; }
    }
}