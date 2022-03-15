using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecificationCounts : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecificationCounts(ProductSpecParams prodParams)
        : base (x=> 
         (string.IsNullOrEmpty(prodParams.Search) || x.Name.Contains(prodParams.Search)) && 
            (!prodParams.BrandId.HasValue || x.ProductBrandId == prodParams.BrandId) && 
            (!prodParams.TypeId.HasValue || x.ProductTypeId == prodParams.TypeId)
        )
        {

        }
    }
}