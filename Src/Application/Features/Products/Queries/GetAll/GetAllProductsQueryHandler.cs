using Application.Contracts;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetAll
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
    {
        private readonly IUnitOWork _unow;
        public GetAllProductsQueryHandler(IUnitOWork unow)
        {
            _unow=unow;
        }
        public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetProductSpec();
            return await _unow.Repository<Product>().ListAsyncSpec(spec, cancellationToken);
            //return await _unow.Repository<Product>().GetAllAsync(cancellationToken);
        }
    }
}
