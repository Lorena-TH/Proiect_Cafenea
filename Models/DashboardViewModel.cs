namespace Proiect_Cafenea.Models
{
    public class DashboardViewModel
    {
        
        public int TotalClienti { get; set; }
        public int TotalComenzi { get; set; }
        public int TotalProduse { get; set; }

        public List<CategorieStat> CategoriiStats { get; set; } = new();
    }

    public class CategorieStat
    {
        public string NumeCategorie { get; set; } = string.Empty;
        public double NumarProduse { get; set; }
    }
}