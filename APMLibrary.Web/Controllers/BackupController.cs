using APMLibrary.Bll.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APMLibrary.Web.Controllers
{
    [Route("/profile/admin"), Authorize(Policy = "Admin")]
    public class BackupController : ControllerBase
    {
        private readonly IDatabaseBackup databaseBackup = default!;
        public BackupController(IDatabaseBackup databaseBackup) : base()
        {
            this.databaseBackup = databaseBackup;
        }

        [HttpGet, Route("/backup", Name = "backup")]
        public async Task<IActionResult> Backup()
        {
            return this.File(await this.databaseBackup.BackupAsync(), "application/octet-stream", "backup.xml");
        }

        [HttpPost, Route("/restore", Name = "restore"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore([FromForm] IFormFile file)
        {
            if (!this.ModelState.IsValid) return this.BadRequest("Файл не установлен");
            using (var fileStream = new BinaryReader(file.OpenReadStream()))
            {
                await this.databaseBackup.RestoreAsync(fileStream.ReadBytes((int)file.Length));
            }
            return this.RedirectToPage("/AdminPages/PublishSettings");
        }
    }
}
