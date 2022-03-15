using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams prodParams)
        : base (x=> 
            (string.IsNullOrEmpty(prodParams.Search) || x.Name.ToLower().Contains(prodParams.Search)) && 
            (!prodParams.BrandId.HasValue || x.ProductBrandId == prodParams.BrandId) && 
            (!prodParams.TypeId.HasValue || x.ProductTypeId == prodParams.TypeId)
        )
        {

            var yy= this.Criteria;
            AddInclude(x=>x.ProductType);
            AddInclude(x=>x.ProductBrand);
            AddPaging((prodParams.PageIndex-1)*prodParams.PageSize,prodParams.PageSize);

            if(!string.IsNullOrEmpty(prodParams.Sort))
            {
                switch (prodParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(x=>x.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDecending(x=>x.Price);
                        break;
                    case "nameAsc":
                        AddOrderBy(x=>x.Name);
                        break;
                    case "nameDesc":
                        AddOrderByDecending(x=>x.Name);
                        break;
                    default:
                        AddOrderBy(x=>x.Name);
                        break;

                } 
            }

            
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x=>x.ProductType);
            AddInclude(x=>x.ProductBrand);
        }
    }
}