using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTO
{

    public record ProductResponse
(Guid ProductID, string ProductName,
double UnitPrice,
int? QuantityInStock,
Category Category
)
    {
        public ProductResponse()
            : this(default,default, default, default, default)
        {
        }
    }
}

