using Application.Contracts.Specification;
using Application.Wrappers;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetAll
{
    public class GetProductSpec :BaseSpecification<Product>
    {
        public GetProductSpec(GetAllProductsQuery specParams) : base(x=> 
        (!specParams.BrandId.HasValue || x.ProductBrandId ==specParams.BrandId)
        && (!specParams.TypeId.HasValue || x.ProductTypeId == specParams.TypeId)
        )
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);

            //sort
            if (specParams.TypeSort == TypeSort.Desc)
                switch (specParams.Sort)
                {
                    case 1:
                        AddOrderByDesc(x => x.Title);
                        break;
                    case 2:
                        AddOrderByDesc(x => x.ProductType.Title);
                        break;
                    case 3:
                        AddOrderByDesc(x => x.Price);
                        break;
                    default:
                        AddOrderByDesc(x => x.Title);
                        break;
                }
            else
                switch (specParams.Sort)
                {
                    case 1:
                        AddOrderBy(x => x.Title);
                        break;
                    case 2:
                        AddOrderBy(x => x.ProductType.Title);
                        break;
                    case 3:
                        AddOrderBy(x => x.Price);
                        break;
                    default:
                        AddOrderBy(x => x.Title);
                        break;
                }
            
           
        }
        public GetProductSpec(int id):base(x=>x.Id==id)
        
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
        }
    }
}
