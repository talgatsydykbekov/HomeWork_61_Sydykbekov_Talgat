using HM_Instagram.BLL.DTOs;

namespace HM_Instagram.BLL.Interfaces;

public interface IImageStorage
{
    Task<string> SaveAsync(UploadFileDto file, string folder);
    Task DeleteAsync(string relativePath);
}
