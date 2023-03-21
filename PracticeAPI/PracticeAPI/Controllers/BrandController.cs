using AutoMapper;
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
        private readonly IMapper _mapper;
        public BrandController(BrandContext db,IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
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
        public async Task<IActionResult> PutBrand(int id, BrandDto brand)
        {
            var data = _db.Brands.FirstOrDefault(x=> x.Id == id);
            if (data == null)
            {
                return NotFound();
            }
            _mapper.Map(brand, data);
            _db.Brands.Update(data);
            if (await _db.SaveChangesAsync() > 0)
            {
                return Ok(brand);
            }
            return BadRequest();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
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
            _db.Brands.Remove(brand);
            await _db.SaveChangesAsync();
            return Ok(); ;
        }
    }
}
