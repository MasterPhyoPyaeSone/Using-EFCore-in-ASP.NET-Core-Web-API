using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniPOS.Database.AppDbContextModels;


namespace MiniPOS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Program.cs မှာ register လုပ်ထားတဲ့ AppDbContext ကို Constructor ကနေ inject လုပ်ပြီး ယူသုံးပါမယ်။
        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            // EF Core ကနေ Data အားလုံးကို ToListAsync() နဲ့ ဆွဲထုတ်ပြီး return ပြန်ပေးပါမယ်။
            var lst = await _context.TblProducts.Where(x => x.DeleteFlag == false).ToListAsync();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            // FirstOrDefaultAsync() method နဲ့ ID ကိုက်ညီတဲ့ Data တစ်ကြောင်းကိုပဲ ရှာပါမယ်။
            var item = await _context.TblProducts.FirstOrDefaultAsync(x => x.ProductId == id && x.DeleteFlag == false);
            if (item is null)
            {
                return NotFound("No Data Found.");
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(TblProduct product)
        {
            // Request ကနေပါလာတဲ့ Product object ကို DbContext ထဲကိုထည့်ပါမယ်။
            await _context.TblProducts.AddAsync(product);
            // SaveChangesAsync() က Database မှာ အမှန်တကယ် သိမ်းဆည်းပေးတဲ့ command ဖြစ်ပါတယ်။
            var result = await _context.SaveChangesAsync();

            return Ok(result > 0 ? "Saving Successful." : "Saving Failed.");
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, TblProduct product)
        {
            // အရင်ဆုံး ပြင်ချင်တဲ့ Data ရှိမရှိ ID နဲ့ ရှာပါမယ်။
            var item = await _context.TblProducts.FirstOrDefaultAsync(x => x.ProductId == id && x.DeleteFlag == false);
            if (item is null)
            {
                return NotFound("No Data Found.");
            }

            // ရှိတယ်ဆိုရင် Request မှာပါလာတဲ့ Data အသစ်တွေနဲ့ အစားထိုးပါမယ်။
            item.ProductCode = product.ProductCode;
            item.ProductName = product.ProductName;
            item.Price = product.Price;

            // Database မှာ ပြန်သိမ်းပါမယ်။
            var result = await _context.SaveChangesAsync();
            return Ok(result > 0 ? "Updating Successful." : "Updating Failed.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            // ဖျက်ချင်တဲ့ Data ကို ID နဲ့ ရှာပါမယ်။
             var item = await _context.TblProducts.FirstOrDefaultAsync(x => x.ProductId == id && x.DeleteFlag == false);
            if (item is null)
            {
                return NotFound("No Data Found.");
            }
            
            // Data ကို အမှန်တကယ်မဖျက်ဘဲ DeleteFlag ကိုပဲ true ပြောင်းပါမယ် (Soft Delete)
            item.DeleteFlag = true;
            
            // Database မှာ ပြန်သိမ်းပါမယ်။
            var result = await _context.SaveChangesAsync();
            return Ok(result > 0 ? "Deleting Successful." : "Deleting Failed.");
        }
    }
}