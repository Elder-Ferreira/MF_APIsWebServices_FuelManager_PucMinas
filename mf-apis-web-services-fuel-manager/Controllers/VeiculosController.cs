using mf_apis_web_services_fuel_manager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mf_apis_web_services_fuel_manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VeiculosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var model = await _context.Veiculos.ToListAsync();
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Veiculo model)
        {
            if (model.AnoFabricacao <=0 || model.AnoModelo <=0)
            {
                return BadRequest(new {message = "Ano de Fabricação e Ano do Modelo são obrigatórios e devem ser maiores do que zero"});
            }

            _context.Veiculos.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new {id = model.Id}, model);   
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var model = await _context.Veiculos
                .FirstOrDefaultAsync(c => c.Id == Id);

            if (model == null) NotFound();

            return Ok(model);
            
        }
    }
}
