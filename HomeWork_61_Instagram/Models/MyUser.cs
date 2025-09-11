using System.ComponentModel.DataAnnotations;
using HomeWork_61_Instagram.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace HomeWork_61_Instagram.Models;

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
}