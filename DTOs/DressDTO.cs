using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public record DressDTO
    (
        [Required]
        int Id,
        [Required]
        string Size,
        [Required]
        int Price,
        string Note,
        [Required]
        string ModelName,
        [Required]
        string ModelImgUrl

    );
    
}
