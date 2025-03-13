using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTH.Models;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context; // Đổi DbContext thành ApplicationDbContext

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int? categoryId)
    {
        // Lấy danh sách danh mục để hiển thị trong sidebar
        ViewBag.Categories = await _context.Categories.ToListAsync();

        // Lọc sản phẩm theo danh mục nếu có categoryId
        var products = _context.Products.AsQueryable();
        if (categoryId.HasValue)
        {
            products = products.Where(p => p.CategoryId == categoryId);
        }

        return View(await products.ToListAsync());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

