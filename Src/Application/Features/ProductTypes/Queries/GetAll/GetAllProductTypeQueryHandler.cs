using Application.Contracts;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductTypes.Queries.GetAll
{
    public class GetAllProductTypeQueryHandler : IRequestHandler<GetAllProductTypeQuery, IEnumerable<ProductType>>
    {
        private readonly IUnitOWork _uow;

        public GetAllProductTypeQueryHandler(IUnitOWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<ProductType>> Handle(GetAllProductTypeQuery request,
            CancellationToken cancellationToken)
        {
            return await _uow.Repository<ProductType>().GetAllAsync(cancellationToken);
        }
    }
}
