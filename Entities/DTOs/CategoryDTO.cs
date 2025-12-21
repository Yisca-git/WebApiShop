using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public record CategoryDTO
    (
        
        int Id,
        [Required]
        string Name
   
    );

}
