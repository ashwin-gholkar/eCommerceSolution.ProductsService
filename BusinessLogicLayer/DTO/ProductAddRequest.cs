using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTO
{
    public record ProductAddRequest
    (string ProductName,
        double UnitPrice,
        int? QuantityInStock,
        Category Category
    )
    {
        public ProductAddRequest()
            : this(default, default, default, default)
        {
        }
    }
}
