﻿using Application.Features.Products.Queries.Get;
using Application.Features.Products.Queries.GetAll;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Web.Common;

namespace Web.Controllers
{
    public class ProductsController : BaseApiController
    {
        [HttpGet]
       public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetAllProductsQuery(), cancellationToken));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> Get([FromRoute] int id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetProductQuery(id), cancellationToken));
        }

    }
}
