using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public record UserDTO
    (
        int Id,
        [EmailAddress, Required]
        string Name,
        [Required]
        string FirstName,
        [Required]
        string LastName,
        [Required]
        string Password
    );
}
