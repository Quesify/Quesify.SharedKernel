using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Quesify.SharedKernel.Guids;
using Xunit;

namespace Quesify.SharedKernel.Storage.Local.Tests;

public class LocalStorageTests
{
    public IServiceCollection Services { get; set; }

    public LocalStorageTests()
    {
        Services = new ServiceCollection();
        Services.AddStorage(storageOptions =>
        {
            storageOptions.UseLocalStorage(Services);
        });
        Services.AddGuid(guidOptions => guidOptions.UseCustomGuidGenerator(Services));
    }

    [Fact]
    public async Task UploadAsync_ShouldReturnUploadedFilePathAndName()
    {
        var serviceProvider = Services.BuildServiceProvider();
        var storageService = serviceProvider.GetRequiredService<IStorageService>();

        var content = "Hello World from a Test File";
        var fileName = "test.pdf";
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        await writer.WriteAsync(content);
        await writer.FlushAsync();
        stream.Position = 0;

        IFormFile file = new FormFile(stream, 0, stream.Length, "files", fileName);
        
        var result = await storageService.UploadAsync(new FileModel()
        {
            File = file,
            Path = @"C:\Users\Enes\Desktop\Quesify\backend\Quesify.SharedKernel\tests\Quesify.SharedKernel.Storage.Local.Tests\Files"
        });

        Assert.NotEqual(result.Name, string.Empty);
        Assert.NotEqual(result.Path, string.Empty);
    }

    [Fact]
    public void Delete_ShouldDeleteAFile()
    {
        var serviceProvider = Services.BuildServiceProvider();
        var storageService = serviceProvider.GetRequiredService<IStorageService>();
        var name = "ff2ce3fa-2ff6-4743-a70b-c96334182889.pdf";
        var path = @"C:\Users\Enes\Desktop\Quesify\backend\Quesify.SharedKernel\tests\Quesify.SharedKernel.Storage.Local.Tests\Files";
        storageService.Delete(path, name);
    }

    [Fact]
    public void GetFiles_ShouldGetFiles()
    {
        var serviceProvider = Services.BuildServiceProvider();
        var storageService = serviceProvider.GetRequiredService<IStorageService>();
        var path = @"C:\Users\Enes\Desktop\Quesify\backend\Quesify.SharedKernel\tests\Quesify.SharedKernel.Storage.Local.Tests\Files";
        var files = storageService.GetFiles(path);
        Assert.Equal(2, files.Length);
    }

    [Fact]
    public void HasFile_ShouldReturnTrue()
    {
        var serviceProvider = Services.BuildServiceProvider();
        var storageService = serviceProvider.GetRequiredService<IStorageService>();
        var path = @"C:\Users\Enes\Desktop\Quesify\backend\Quesify.SharedKernel\tests\Quesify.SharedKernel.Storage.Local.Tests\Files";
        var name = "15b85abd-bb94-4924-b1b5-84a2c66fe472.pdf";
        var hasFile = storageService.HasFile(path, name);
        Assert.True(hasFile);
    }
}
