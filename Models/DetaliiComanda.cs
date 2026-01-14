using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace Proiect_Cafenea.Models
{

public class DetaliiComanda
{
    public int Id { get; set; }

    public int ComandaId { get; set; }
    public Comanda? Comanda { get; set; }

    public int ProdusId { get; set; }
    public Produs? Produs { get; set; }

    [Range(1, int.MaxValue)]
    public int Cantitate { get; set; }  
}
}