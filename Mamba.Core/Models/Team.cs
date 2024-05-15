using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Core.Models;

public class Team:BaseEntity
{
    [Required]
    public string FullName { get; set; }=null!;

    [Required]
    public string Position { get; set; } = null!;
    public string? ImageUrl { get; set; }
    [NotMapped]
    public IFormFile? formFile { get; set; }

}
