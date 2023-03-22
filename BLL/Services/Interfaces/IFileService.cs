using BLL.DTOs;
using BLL.Models;

namespace BLL.Services.Interfaces
{
    public interface IFileService
    {
        Task SaveFile(FileDTO file);
    }
}
