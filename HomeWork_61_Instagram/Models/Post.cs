using System.ComponentModel.DataAnnotations;

namespace HomeWork_61_Instagram.Models;

public class Post
{
    public int Id { get; set; }
    
    public DateTime Ceated_Datetime { get; set; }
    [Required(ErrorMessage = "Выберите файл")]
    public string Content { get; set; }
    public string? Desc { get; set; }
    public int UserId { get; set; }
    public List<Reactions>? Reactions { get; set; }
    
    
    public MyUser? User { get; set; }
}