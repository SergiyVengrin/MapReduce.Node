using BLL.Models;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BLL.DTOs;

namespace Node.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NodeController : ControllerBase
    {
        private readonly IFileService _fileService;

        public NodeController(IFileService fileService)
        {
            _fileService = fileService;
        }


        [HttpPost]
        public async Task<IActionResult> SaveFile(FileDTO file)
        {
            try
            {
                await _fileService.SaveFile(file);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return Ok("Files Saved");
        }
    }
}
