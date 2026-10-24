using System.ComponentModel.DataAnnotations;
using HM_Instagram.WEB.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace HM_Instagram.WEB.Models;

public class MyUser : IdentityUser<int>
{
    
     [Required(ErrorMessage = "Выберите файл для профиля")]
     public string Avatar { get; set; }
     public string? Desc { get; set; }
   
     public string? Name { get; set; }
     public Gender? Gender { get; set; }

     public List<Post>? Posts { get; set ;}
     public List<UserFollow>? Following { get; set ;}
     public List<UserFollow>? Followers { get; set; }
     public List<Reactions>? Reactions { get; set; }
}