using BLL.DTOs;
using BLL.Models;
using BLL.POCOs;
using BLL.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace BLL.Services.Implementation
{
    public sealed class FileService : IFileService
    {
        private readonly IOptions<NodeConfig> _options;

        public FileService(IOptions<NodeConfig> options)
        {
            _options = options;
        }


        public async Task SaveFile(FileDTO file)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(file.NodeFolder)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(file.NodeFolder));
                }
                if (File.Exists(file.NodeFolder))
                {
                    var directory = Path.GetDirectoryName(file.NodeFolder);
                    var fileName = Path.GetFileNameWithoutExtension(file.NodeFolder);
                    var indexedFileName = SetIndexToFileName(fileName)+Path.GetExtension(fileName);
                    
                    await File.WriteAllTextAsync(file.NodeFolder, Path.Combine(directory, indexedFileName));
                    return;
                }

                await File.WriteAllTextAsync(file.NodeFolder, file.Text);

            }
            catch { throw; }
        }

        private string SetIndexToFileName(string fileName)
        {
            string regexPattern = @"\[\d{1,}\]";
            var match = Regex.Match(fileName, regexPattern);

            if (match.Success)
            {
                int index = int.Parse(match.ToString().Replace("[", "").Replace("]", ""));
                index++;
                return Regex.Replace(fileName, regexPattern, index.ToString()).ToString();
            }

            return fileName + "1";

            return fileName;
        }
    }
}
