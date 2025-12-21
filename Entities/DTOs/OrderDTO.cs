using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public record OrderDTO
    (
        int Id,
        DateOnly Date,
        [Required]
        int Sum,        
        List<OrderItemDTO> OrderItems
    );
    
}
