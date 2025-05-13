using KhoHang_XNK.Models;
using KhoHang_XNK.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KhoHang_XNK.Controllers
{
    [Authorize]  // Requires authentication for all actions
    public class UserController : Controller
    {
        private readonly IKhoHangRepository _khoHangRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(
            IKhoHangRepository khoHangRepository,
            UserManager<ApplicationUser> userManager)
        {
            _khoHangRepository = khoHangRepository;
            _userManager = userManager;
        }

        // GET: User/Index
        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = _userManager.GetUserId(User);

                if (string.IsNullOrEmpty(userId))
                {
                    TempData["ErrorMessage"] = "You must be logged in to view warehouses";
                    return RedirectToAction("Login", "Account");
                }

                var khoHangs = await _khoHangRepository.GetAllKhoHangsForUserAsync(userId);
                return View(khoHangs);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error loading warehouses: {ex.Message}";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new KhoHang());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KhoHang khoHang, IFormFile imageUrl)
        {
            try
            {
                // Handle image upload if provided
                if (imageUrl != null && imageUrl.Length > 0)
                {
                    // Validate image file
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var fileExtension = Path.GetExtension(imageUrl.FileName).ToLowerInvariant();

                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        ModelState.AddModelError("imageUrl", "Only image files (JPG, PNG, GIF) are allowed.");
                        return View(khoHang);
                    }

                    if (imageUrl.Length > 5 * 1024 * 1024) // 5MB limit
                    {
                        ModelState.AddModelError("imageUrl", "The image file is too large (max 5MB).");
                        return View(khoHang);
                    }

                    khoHang.ImageUrl = await SaveImage(imageUrl);
                }

                await _khoHangRepository.AddKhoHangForUserAsync(khoHang);
                TempData["SuccessMessage"] = "Warehouse created successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {


                TempData["ErrorMessage"] = $"Error creating warehouse: {ex.Message}";
                return View(khoHang);
            }
        }

        private async Task<string> SaveImage(IFormFile image)
        {
            var uploadsFolder = Path.Combine("wwwroot", "images", "warehouses");
            var absoluteUploadsPath = Path.Combine(Directory.GetCurrentDirectory(), uploadsFolder);

            // Ensure directory exists
            Directory.CreateDirectory(absoluteUploadsPath);

            // Generate unique filename
            var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
            var filePath = Path.Combine(absoluteUploadsPath, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            return $"/images/warehouses/{uniqueFileName}";
        }
    
    }
}