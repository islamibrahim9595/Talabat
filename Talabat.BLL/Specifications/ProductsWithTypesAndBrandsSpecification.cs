using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.DAL.Entities;

namespace Talabat.BLL.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(string sort, int? brandId, int? typeId):
            base(P =>
                (!brandId.HasValue || P.ProductBrandId == brandId) &&
                (!typeId.HasValue || P.ProductTypeId == typeId)
            )
        {
            AddIncludes(P => P.ProductType);
            AddIncludes(P => P.ProductBrand);

            AddOrderBy(P => P.Name);

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "PriceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDescending(P => P.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(P => P.Id == id)
        {
            AddIncludes(P => P.ProductType);
            AddIncludes(P => P.ProductBrand);
        }
    }
}
