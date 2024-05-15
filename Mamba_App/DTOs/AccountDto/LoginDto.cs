using System.ComponentModel.DataAnnotations;

namespace Mamba_App.DTOs.AccountDto;

public class LoginDto
{
    [Required]
    public string EmailOrUsername { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public bool IsRemember { get; set; }
}
