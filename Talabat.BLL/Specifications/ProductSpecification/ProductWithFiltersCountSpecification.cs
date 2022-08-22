using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.DAL.Entities;

namespace Talabat.BLL.Specifications.ProductSpecification
{
    public class ProductWithFiltersCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersCountSpecification(ProductSpecParams productSpecParams) :
           base(P =>
           (string.IsNullOrEmpty(productSpecParams.searchValue) || P.Name.ToLower().Contains(productSpecParams.searchValue)) &&
               (!productSpecParams.BrandId.HasValue || P.ProductBrandId == productSpecParams.BrandId) &&
               (!productSpecParams.TypeId.HasValue || P.ProductTypeId == productSpecParams.TypeId)
           )
        {

        }
    }
}
