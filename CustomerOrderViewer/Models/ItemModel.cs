using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOrderViewer.Models
{
    class ItemModel
    {
        public int ItemId { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
