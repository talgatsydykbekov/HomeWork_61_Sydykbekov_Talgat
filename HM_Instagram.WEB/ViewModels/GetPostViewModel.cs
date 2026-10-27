using System.ComponentModel.DataAnnotations;
using HM_Instagram.BLL.Models;

namespace HM_Instagram.WEB.ViewModels;

public class GetPostViewModel
{
    public int Id { get; set; }
    public DateTime Ceated_Datetime { get; set; }
    [Required(ErrorMessage = "")]
    public string Content { get; set; }
    public string? Desc { get; set; }
    public int UserId { get; set; }
    public List<Comments>? Comments { get; set; }
    public List<Reactions>? Reactions { get; set; }
    
    
    public MyUser? User { get; set; }
}