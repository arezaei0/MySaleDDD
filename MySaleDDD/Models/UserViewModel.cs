using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MySaleDDD.Models
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            Claims = new List<string>();  //برای اینکه خطا نده وقتی فرم بالا میاد  و نمونه سازی شده باشد
        }

        public string Id { get; set; }
        [Display(Name =nameof(UserName), ResourceType =typeof(Resources.Messages))]
        [Required(AllowEmptyStrings =false,ErrorMessageResourceName ="RequiredField", ErrorMessageResourceType =typeof(Resources.Messages))]
        public string UserName { get; set; }
        [Display(Name = nameof(Email), ResourceType = typeof(Resources.Messages))]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = nameof(Mobile), ResourceType = typeof(Resources.Messages))]
        public string Mobile { get; set; }
        [Display(Name = nameof(PhoneNumber), ResourceType = typeof(Resources.Messages))]
        public string PhoneNumber { get; set; }
        [Display(Name = nameof(FirstName), ResourceType = typeof(Resources.Messages))]
        [Required]
        public string FirstName { get; set; }
        [Display(Name = nameof(LastName), ResourceType = typeof(Resources.Messages))]
        [Required]
        public string LastName { get; set; }
        [Display(Name = nameof(Password), ResourceType = typeof(Resources.Messages))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resources.Messages))]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = nameof(ConfirmPassword), ResourceType = typeof(Resources.Messages))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resources.Messages))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Display(Name = nameof(RoleType), ResourceType = typeof(Resources.Messages))]
        [Required]
        public RoleType  RoleType { get; set; }
        [Display(Name ="دسترسی ها")]
        public List<string> Claims { get; set; }  //برای دراپ داون های مالتی سلکت
    }
}
