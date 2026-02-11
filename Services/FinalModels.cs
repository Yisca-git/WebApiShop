using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;

namespace Services
{
    public class FinalModels
    {
        public List<ModelDTO> Models { get; set; }
        public int TotalCount { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrev { get; set; }
    }
}
