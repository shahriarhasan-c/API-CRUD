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
        public async Task<IActionResult> GetBrands()
        {
            if (_db.Brands == null)
            {
                return NotFound();
            }
            return Ok(await _db.Brands.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrandById(int id)
        {
            if (_db.Brands == null)
            {
                return NotFound();
            }
            var brand = await _db.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return Ok(brand);
        }

        [HttpPost]
        public async Task<IActionResult> PostBrand(Brand brand)
        {
            _db.Brands.Add(brand);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBrandById), new { id = brand.Id }, brand);
        }

        [HttpPut]
        public async Task<IActionResult>PutBrand(int id, Brand brand)
        {
            if(id != brand.Id)
            {
                return BadRequest();
            }
            _db.Entry(brand).State = EntityState.Modified;
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandAvailable(id))
                {
                    return NotFound();
                }
                else
                    throw;
            }
            return Ok();
        }

        private bool BrandAvailable(int id)
        {
            return (_db.Brands?.Any(x => x.Id == id)).GetValueOrDefault();
        }
    }
}
