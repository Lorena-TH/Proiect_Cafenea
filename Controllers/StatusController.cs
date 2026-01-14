using Microsoft.AspNetCore.Mvc;
using Grpc.Net.Client;
using Cafenea.GrpcStatus;
using Microsoft.EntityFrameworkCore;
using Proiect_Cafenea.Data; 

namespace Proiect_Cafenea.Controllers
{
    public class StatusController : Controller
    {
       
        private readonly CafeneaDbContext _context;

        public StatusController(CafeneaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            
            ViewBag.Produse = await _context.Produse.OrderBy(p => p.Nume).ToListAsync(); //incarca lista de produse pt dropdown
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Verifica(int orderId)
        {
            
            ViewBag.Produse = await _context.Produse.OrderBy(p => p.Nume).ToListAsync();

            var produs = await _context.Produse.FirstOrDefaultAsync(p => p.Id == orderId);
            ViewBag.NumeProdus = produs != null ? produs.Nume : orderId.ToString();

            var channel = GrpcChannel.ForAddress("https://localhost:7254");
            var client = new OrderStatus.OrderStatusClient(channel);

            try
            {
                var reply = await client.GetStatusAsync(new OrderRequest { OrderId = orderId });
                ViewBag.Rezultat = reply;
                ViewBag.Cautare = orderId;
            }
            catch
            {
                ViewBag.Eroare = "Nu am putut conecta serviciul gRPC.";
            }

            return View("Index");
        }
    }
}