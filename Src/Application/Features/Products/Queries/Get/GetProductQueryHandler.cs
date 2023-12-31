﻿using Application.Contracts;
using Application.Dtos.Products;
using Application.Features.Products.Queries.GetAll;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.Get
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDto>
    {
        private readonly IUnitOWork _uow;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IUnitOWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetProductSpec(request.Id);
            var result = await _uow.Repository<Product>().GetEntityWithSpec(spec, cancellationToken);
            if (result == null) throw new NotFoundEntityException();
            return _mapper.Map<ProductDto>(result);
        }
    }
}
