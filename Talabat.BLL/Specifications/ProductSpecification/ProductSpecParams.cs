using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.BLL.Specifications.ProductSpecification
{
    public class ProductSpecParams
    {
        private const int PageMaxSize = 50;

        public int PageIndex { get; set; } = 1;

        private int PageSize;

        public int pageSize
        {
            get { return PageSize; }
            set { PageSize = value > PageMaxSize ? PageMaxSize : value; }
        }

        public string Sort { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }

        private string SearchValue;

        public string searchValue
        {
            get { return SearchValue; }
            set { SearchValue = value.ToLower(); }
        }

    }
}
