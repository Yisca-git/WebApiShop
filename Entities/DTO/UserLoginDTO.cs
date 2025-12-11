using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public record UserLoginDTO
    (
        [EmailAddress, Required]
        string UserName,
        [Required]
        string UserPassword
    );
    
}
