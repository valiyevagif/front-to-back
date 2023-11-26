using Bigon.Infrastructure.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigon.Business.Modules.ShopModule.Queries.ProductGetByIdQuery
{
    public class ProductGetByIdRequest : IRequest<ProductGetByIdDto>
    {
        public int Id { get; set; }
    }
}
