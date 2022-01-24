using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WowCombatStats.Domain;
using WowCombatStats.Models.VIewModels;

namespace WowCombatStats.App.Controllers
{
    /// <summary>
    /// https://docs.microsoft.com/ru-ru/aspnet/core/mvc/models/file-uploads?view=aspnetcore-6.0
    /// </summary>

    [Authorize]
    public class AccountController : Controller
    {
        private readonly AccountBuilder _accountBuilder;
        private readonly Auth _auth;

        public static int Progress { get; set; } = 0;
        public static bool IsUpload { get; set; } = false;

        public AccountController(AccountBuilder accountBuilder, Auth auth)
        {
            _accountBuilder = accountBuilder;
            _auth = auth;
        }

        #region Upload

        [HttpGet]
        public async Task<IActionResult> Upload()
        {
            var uploadVM = new UploadVM
            {
                Servers = _accountBuilder.GetServerList()
            };

            return View(uploadVM);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(UploadVM uploadVM)
        {
            if (ModelState.IsValid)
            {
                IsUpload = false;
                long size = uploadVM.File.Length;
                var fileName = Guid.NewGuid();

                var userToken = _auth.GetAuthToken(HttpContext.User);
                using (var memoryStream = new MemoryStream())
                {
                    await uploadVM.File.CopyToAsync(memoryStream);

                    var result = await _accountBuilder.SaveFile(memoryStream.ToArray(), userToken, fileName);
                }

                long totalBytes = uploadVM.File.Length;
                long totalReadBytes = 0;

                try
                {
                    int readBytes;
                    byte[] buffer = new byte[16 * 1024];
                    using (FileStream output = System.IO.File.Create(_accountBuilder.GetPath(fileName)))
                    {
                        using (Stream input = uploadVM.File.OpenReadStream())
                        {
                            while ((readBytes = input.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                await output.WriteAsync(buffer, 0, readBytes);
                                totalReadBytes += readBytes;
                                Progress = (int)((float)totalReadBytes / (float)totalBytes * 100.0);
                                //await Task.Delay(10); // It is only to make the process slower
                            }
                        }
                    }

                    IsUpload = true;
                    TempData["SuccessMessage"] = $"Data Uploaded Successfully!";
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Error: {ex.Message}.";
                    return RedirectToAction("Upload");
                }

            }
            return RedirectToAction("Upload");
        }

        [HttpPost]
        public void SetProgress()
        {
            Progress = Progress + 5;
            Console.WriteLine($"Progress = {Progress}");
        }

        [HttpPost]
        public IActionResult GetStatus()
        {
            Console.WriteLine($"Call method 'GetStatus()'");
            return this.Content(IsUpload.ToString());
        }

        [HttpPost]
        public IActionResult GetProgress()
        {
            Console.WriteLine($"Call method 'GetProgress()'");
            return this.Content(Progress.ToString());
        }
        #endregion

        #region Settings

        [HttpGet]
        public async Task<IActionResult> FileUpload()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ChangePasswordOrEmail()
        {
            return View();
        }
        #endregion
    }
}
