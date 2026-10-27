namespace HM_Instagram.BLL.DTOs;

public class UploadFileDto
{
    public string FileName { get; set; } = string.Empty;
    public Stream Content { get; set; } = Stream.Null;
}
