using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniPOS.Database.AppDbContextModels;

namespace MiniPOS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var list = await _context.TblCategories.Where(x => x.DeleteFlag == false).ToListAsync();
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(TblCategory category)
        {
            await _context.TblCategories.AddAsync(category);
            await _context.SaveChangesAsync();
            return Ok("Category Created Successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var item = await _context.TblCategories.FirstOrDefaultAsync(x => x.CategoryId == id && x.DeleteFlag == false);
            if (item == null) return NotFound("Category not found.");

            item.DeleteFlag = true;
            await _context.SaveChangesAsync();
            return Ok("Category Deleted.");
        }
    }
}