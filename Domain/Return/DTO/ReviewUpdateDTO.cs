using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Return.DTO
{
    public class ReviewUpdateDTO
    {
        public int ReviewId { get; set; }
        public string Content { get; set; }
        public double Rating { get; set; }
    }
}
