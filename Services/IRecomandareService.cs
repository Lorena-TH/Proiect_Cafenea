using System.Threading.Tasks;
using Proiect_Cafenea.Models;
namespace Proiect_Cafenea.Services
{
    public interface IRecomandareService
    {

        Task<float> PredictScoreAsync(RecomandareInput input);
    }
}
