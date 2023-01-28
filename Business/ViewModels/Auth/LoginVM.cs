using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Auth;

public class LoginVM
{
   
    [Required, MaxLength(100)]
    public string UsernameOrEmail { get; set; }
   
    [Required, DataType(DataType.Password)]
    public string Password { get; set; }
    public bool RememberMe { get; set; }

}
