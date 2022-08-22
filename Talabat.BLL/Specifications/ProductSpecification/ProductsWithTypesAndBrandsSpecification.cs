using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.BLL.Specifications.ProductSpecification;
using Talabat.DAL.Entities;

namespace Talabat.BLL.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productSpecParams) :
            base(P =>
            (string.IsNullOrEmpty(productSpecParams.searchValue) || P.Name.ToLower().Contains(productSpecParams.searchValue)) &&
                (!productSpecParams.BrandId.HasValue || P.ProductBrandId == productSpecParams.BrandId) &&
                (!productSpecParams.TypeId.HasValue || P.ProductTypeId == productSpecParams.TypeId)
            )
        {
            AddIncludes(P => P.ProductType);
            AddIncludes(P => P.ProductBrand);

            AddPagination(productSpecParams.pageSize * (productSpecParams.PageIndex - 1), productSpecParams.pageSize);

            AddOrderBy(P => P.Name);

            if (!string.IsNullOrEmpty(productSpecParams.Sort))
            {
                switch (productSpecParams.Sort)
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
