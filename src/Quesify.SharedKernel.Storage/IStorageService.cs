namespace Quesify.SharedKernel.Storage;

public interface IStorageService
{
    Task<(string Path, string Name)> UploadAsync(FileModel model);

    void Delete(string path, string name);

    FileInfo[] GetFiles(string path);

    bool HasFile(string path, string name);
}
