using Quesify.SharedKernel.Guids;

namespace Quesify.SharedKernel.Storage.Local;

public class LocalStorageService : IStorageService
{
    private readonly IGuidGenerator _guidGenerator;

    public LocalStorageService(IGuidGenerator guidGenerator)
    {
        _guidGenerator = guidGenerator;
    }

    public async Task<(string Path, string Name)> UploadAsync(FileModel model)
    {
        var name = model.Name ?? _guidGenerator.Generate().ToString();
        var extension = Path.GetExtension(model.File.FileName);
        string nameWithExtension = $"{name}{extension}";
        var path = $"{model.Path}\\{nameWithExtension}";

        using var stream = new FileStream(path, FileMode.Create);
        await model.File.CopyToAsync(stream);

        return new(model.Path, $"{nameWithExtension}");
    }

    public void Delete(string path, string name)
    {
        File.Delete($"{path}\\{name}");
    }

    public FileInfo[] GetFiles(string path)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(path);
        return directoryInfo.GetFiles();
    }

    public bool HasFile(string path, string name)
    {
        return File.Exists($"{path}\\{name}");
    }
}
