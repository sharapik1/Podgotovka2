using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podgotovka2
{
    public  interface IDataProvider
    {
        IEnumerable<Product> GetProducts();
        IEnumerable<ProductType> GetProductTypes();
    }
}
