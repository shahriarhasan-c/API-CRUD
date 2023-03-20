using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeAPI.Models;

namespace PracticeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly BrandContext _db;
        public BrandController(BrandContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        {
            if (_db.Brands == null)
            {
                return NotFound();
            }
            return await _db.Brands.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>>GetBrandById(int id)
        {
            if(_db.Brands == null)
            {
                return NotFound();
            }
            var brand = await _db.Brands.FindAsync(id);
            if(brand == null)
            {
                return NotFound();
            }
            return Ok(brand);
        }

        [HttpPost]
        public async Task<ActionResult<Brand>>PostBrand(Brand brand)
        {
            _db.Brands.Add(brand);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBrandById), new { id =brand.Id},brand);
        }
    }
}
