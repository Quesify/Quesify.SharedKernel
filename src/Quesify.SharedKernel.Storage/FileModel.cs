using Microsoft.AspNetCore.Http;

namespace Quesify.SharedKernel.Storage;

public class FileModel
{
    public string Path { get; set; }

    public string? Name { get; set; }

    public IFormFile File { get; set; }

    public FileModel()
    {
        Path = null!;
        File = null!;
    }
}
