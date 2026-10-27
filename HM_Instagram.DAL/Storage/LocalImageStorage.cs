using HM_Instagram.BLL.DTOs;
using HM_Instagram.BLL.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace HM_Instagram.DAL.Storage;

public class LocalImageStorage : IImageStorage
{
    private readonly IWebHostEnvironment _environment;

    public LocalImageStorage(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<string> SaveAsync(UploadFileDto file, string folder)
    {
        if (folder != "avatars" && folder != "posts")
            throw new ArgumentException("Unknown image folder.", nameof(folder));

        var directory = Path.Combine(_environment.WebRootPath, "images", folder);
        Directory.CreateDirectory(directory);
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var path = Path.Combine(directory, fileName);
        try
        {
            await using var output = new FileStream(path, FileMode.CreateNew);
            await file.Content.CopyToAsync(output);
        }
        catch
        {
            File.Delete(path);
            throw;
        }
        return $"images/{folder}/{fileName}";
    }

    public Task DeleteAsync(string relativePath)
    {
        var root = Path.GetFullPath(Path.Combine(_environment.WebRootPath, "images"))
                   + Path.DirectorySeparatorChar;
        var path = Path.GetFullPath(Path.Combine(_environment.WebRootPath, relativePath));
        if (!path.StartsWith(root, StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException("Invalid image path.", nameof(relativePath));

        File.Delete(path);
        return Task.CompletedTask;
    }
}
