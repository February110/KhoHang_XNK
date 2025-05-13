using KhoHang_XNK.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using QRCoder;
using System.Web;
using System.Text.Json;

namespace KhoHang_XNK.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(new AuthViewModel());
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AuthViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(
                model.Login.Email,
                model.Login.Password,
                model.Login.RememberMe,
                lockoutOnFailure: true);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Login.Email);
                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Contains("Admin"))
                {
                    // Sử dụng Session thay vì TempData để ổn định hơn
                    HttpContext.Session.SetString("Admin2FAEmail", user.Email);

                    if (!await _userManager.GetTwoFactorEnabledAsync(user))
                    {
                        return RedirectToAction("Activate2fa");
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (result.RequiresTwoFactor)
                {

                    // Người dùng đã bật 2FA, lưu email vào session để xác minh bước 2
                    var user = await _userManager.FindByEmailAsync(model.Login.Email);
                    HttpContext.Session.SetString("Admin2FAEmail", user.Email);
                    return RedirectToAction("Verify2fa");

                }
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Tài khoản bị khóa.");
                return View("Index", model);
            }
            // Sai mật khẩu hoặc email
            ModelState.AddModelError("", "Thông tin đăng nhập không đúng.");
            return View("Index", model);
        }

        [HttpGet]
        public async Task<IActionResult> Activate2fa()
        {
            var email = HttpContext.Session.GetString("Admin2FAEmail");
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Index");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return RedirectToAction("Index");
            }

            var authenticatorKey = await _userManager.GetAuthenticatorKeyAsync(user);
            if (string.IsNullOrEmpty(authenticatorKey))
            {
                await _userManager.ResetAuthenticatorKeyAsync(user);
                authenticatorKey = await _userManager.GetAuthenticatorKeyAsync(user);
            }

            return View(new Enable2faViewModel
            {
                SharedKey = authenticatorKey,
                AuthenticatorUri = GenerateQrCodeUri(user.Email, authenticatorKey)
            });
        }
        private string GenerateQrCodeUri(string email, string authenticatorKey)
        {
            return string.Format(
                "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6",
                Uri.EscapeDataString("WareTrack"),
                Uri.EscapeDataString(email),
                authenticatorKey);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Activate2fa(Enable2faViewModel model)
        {
            var email = HttpContext.Session.GetString("Admin2FAEmail");
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Index");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return RedirectToAction("Index");
            }

            if (!await _userManager.VerifyTwoFactorTokenAsync(
                user,
                _userManager.Options.Tokens.AuthenticatorTokenProvider,
                model.Code))
            {
                ModelState.AddModelError("Code", "Invalid verification code");
                return View(model);
            }

            // Kích hoạt 2FA và tạo recovery codes
            await _userManager.SetTwoFactorEnabledAsync(user, true);
            var recoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);

            // Lưu recovery codes vào Session
            HttpContext.Session.SetString("RecoveryCodes", JsonSerializer.Serialize(recoveryCodes));

            // Đăng nhập luôn sau khi kích hoạt thành công
            await _signInManager.SignInAsync(user, model.RememberMe);
            return RedirectToAction("ShowRecoveryCodes");
        }

        [HttpGet]
        public IActionResult ShowRecoveryCodes()
        {
            var codesJson = HttpContext.Session.GetString("RecoveryCodes");
            if (string.IsNullOrEmpty(codesJson))
            {
                return RedirectToAction("Index");
            }

            var codes = JsonSerializer.Deserialize<string[]>(codesJson);
            return View(codes); 
        }


        [HttpGet]
        public async Task<IActionResult> Verify2fa()
        {
            var email = HttpContext.Session.GetString("Admin2FAEmail");
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Index");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || !await _userManager.GetTwoFactorEnabledAsync(user))
            {
                return RedirectToAction("Index");
            }

            return View(new Verify2faViewModel { RememberMe = false });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Verify2fa(Verify2faViewModel model)
        {
            var email = HttpContext.Session.GetString("Admin2FAEmail");
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Index");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return RedirectToAction("Index");
            }

            var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(
                model.Code,
                model.RememberMe,
                model.RememberMachine);

            if (result.Succeeded)
            {
                HttpContext.Session.Remove("Admin2FAEmail");
                return RedirectToAction("Index", "Home");
            }

            // Xử lý lỗi...
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AuthViewModel model)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Register.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Register.Email", "Email đã được sử dụng.");
                return View("Index", model);
            }

            var user = new ApplicationUser
            {
                UserName = model.Register.Email,
                Email = model.Register.Email,
                FullName = model.Register.FullName,
                AvatarUrl = "/images/avatars/default.png",
            };


            var result = await _userManager.CreateAsync(user, model.Register.Password);
            if (result.Succeeded)
            {

                await _userManager.AddToRoleAsync(user, "User");
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View("Index", model);
        }

        [HttpGet]
        public IActionResult LoginWithGoogle(string returnUrl = null)
        {
            var redirectUrl = Url.Action("GoogleResponse", "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(
                "Google",
                redirectUrl);
            return Challenge(properties, "Google");
        }

        public async Task<IActionResult> GoogleResponse(string returnUrl = null)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                TempData["ErrorMessage"] = "Không thể lấy thông tin từ Google.";
                return RedirectToAction("Index");
            }

            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            var name = info.Principal.FindFirstValue(ClaimTypes.Name);
            var picture = info.Principal.FindFirstValue("picture");

            // === ĐOẠN CODE XỬ LÝ TÀI KHOẢN ĐÃ TỒN TẠI ===
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null)
            {
                // Kiểm tra nếu user đã có password
                if (await _userManager.HasPasswordAsync(existingUser))
                {
                    TempData["ErrorMessage"] = "Email đã được đăng ký. Vui lòng đăng nhập bằng mật khẩu.";
                    return RedirectToAction("Index");
                }
                else
                {
                    // Liên kết tài khoản Google với tài khoản hiện có
                    await _userManager.AddLoginAsync(existingUser, info);
                    await _signInManager.SignInAsync(existingUser, isPersistent: true);
                    return RedirectToLocal(returnUrl);
                }
            }
            // =============================================

            // Nếu không có tài khoản tồn tại, tạo mới
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FullName = name,
                AvatarUrl = picture,
                EmailConfirmed = true
            };

            var createResult = await _userManager.CreateAsync(user);
            if (!createResult.Succeeded)
            {
                TempData["ErrorMessage"] = "Tạo tài khoản thất bại.";
                return RedirectToAction("Index");
            }

            await _userManager.AddToRoleAsync(user, "User");
            await _userManager.AddLoginAsync(user, info);
            await _signInManager.SignInAsync(user, isPersistent: true);

            return RedirectToLocal(returnUrl);
        }

        [HttpGet]
        public IActionResult LoginWithFacebook(string returnUrl = null)
        {
            var redirectUrl = Url.Action("FacebookResponse", "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(
                "Facebook",
                redirectUrl);
            return Challenge(properties, "Facebook");
        }
        public async Task<IActionResult> FacebookResponse(string returnUrl = null)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                TempData["ErrorMessage"] = "Không thể lấy thông tin từ Facebook.";
                return RedirectToAction("Index");
            }

            // Lấy thông tin từ Facebook
            var facebookId = info.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
            var name = info.Principal.FindFirstValue(ClaimTypes.Name);

            // Trường hợp 1: Tài khoản Facebook không có email
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(email))
            {
                email = $"{facebookId}@facebook.com"; // Tạo email từ Facebook ID
            }

            var picture = info.Principal.FindFirstValue("picture") ??
                         $"https://graph.facebook.com/{facebookId}/picture?type=large";

            // Trường hợp 2: Tài khoản đã được đăng nhập trước đó (kiểm tra qua UserLogins)
            var existingLogin = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            if (existingLogin != null)
            {
                // Đăng nhập trực tiếp nếu đã liên kết trước đó
                await _signInManager.SignInAsync(existingLogin, isPersistent: true);
                return RedirectToLocal(returnUrl);
            }

            // Kiểm tra email đã tồn tại
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null)
            {
                if (await _userManager.HasPasswordAsync(existingUser))
                {
                    TempData["ErrorMessage"] = "Email đã được đăng ký. Vui lòng đăng nhập bằng mật khẩu.";
                    return RedirectToAction("Index");
                }

                await _userManager.AddLoginAsync(existingUser, info);
                await _signInManager.SignInAsync(existingUser, isPersistent: true);
                return RedirectToLocal(returnUrl);
            }

            // Tạo tài khoản mới
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FullName = name,
                AvatarUrl = picture,
                EmailConfirmed = !email.EndsWith("@facebook.com") // Chỉ confirm nếu là email thật
            };

            var createResult = await _userManager.CreateAsync(user);
            if (!createResult.Succeeded)
            {
                TempData["ErrorMessage"] = "Tạo tài khoản thất bại: " +
                    string.Join(", ", createResult.Errors.Select(e => e.Description));
                return RedirectToAction("Index");
            }

            await _userManager.AddToRoleAsync(user, "User");
            await _userManager.AddLoginAsync(user, info);
            await _signInManager.SignInAsync(user, isPersistent: true);

            return RedirectToLocal(returnUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction("Index", "Home");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> Manage()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var model = new ManageAccountViewModel
            {
                FullName = user.FullName,
                AvatarUrl = user.AvatarUrl
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manage(ManageAccountViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            if (model != null)
            {
                // Cập nhật tên
                user.FullName = model.FullName;

                // Cập nhật ảnh đại diện
                if (model.AvatarFile != null && model.AvatarFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine("wwwroot", "avatars");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var uniqueFileName = $"{Guid.NewGuid()}_{model.AvatarFile.FileName}";
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.AvatarFile.CopyToAsync(stream);
                    }

                    user.AvatarUrl = $"/avatars/{uniqueFileName}";
                }

                // Cập nhật mật khẩu nếu có
                if (!string.IsNullOrEmpty(model.NewPassword))
                {
                    var changePasswordResult = await _userManager.ChangePasswordAsync(
                        user,
                        model.OldPassword,
                        model.NewPassword);

                    if (!changePasswordResult.Succeeded)
                    {
                        foreach (var error in changePasswordResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return View(model);
                    }
                }

                var updateResult = await _userManager.UpdateAsync(user);
                if (updateResult.Succeeded)
                {
                    TempData["SuccessMessage"] = "Cập nhật thông tin thành công";
                    return RedirectToAction("Manage");
                }

                foreach (var error in updateResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }


        


    }
}