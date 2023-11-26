using Bigon.Infrastructure.Entities;
using Bigon.Infrastructure.Repositories;
using Bigon.Infrastructure.Services.Abstracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigon.Business.Modules.ShopModule.Commands.ProductEditCommand
{
    class ProductEditRequestHandler : IRequestHandler<ProductEditRequest, Product>
    {
        private readonly IProductRepository productRepository;
        private readonly IFileService fileService;

        public ProductEditRequestHandler(IProductRepository productRepository, IFileService fileService)
        {
            this.productRepository = productRepository;
            this.fileService = fileService;
        }

        public async Task<Product> Handle(ProductEditRequest request, CancellationToken cancellationToken)
        {
            var product = productRepository.Get(m => m.Id == request.Id);

            product.Name = request.Name;
            product.StockKeepingUnit = request.StockKeepingUnit;
            product.Price = request.Price;
            product.ShortDescription = request.ShortDescription;
            product.Description = request.Description;
            product.BrandId = request.BrandId;
            product.CategoryId = request.CategoryId;
            product.CategoryId = request.CategoryId;
            productRepository.Save();


            if (request.Images != null && request.Images.Length > 0)
            {
                var imageItems = await productRepository.GetImages(m => m.ProductId == request.Id && m.DeletedAt == null).ToListAsync(cancellationToken);

                //left join
                var imageCombinations = from inMemory in request.Images
                                        join inDb in imageItems on inMemory.Id equals inDb.Id into inDbLeftJoin
                                        from lj in inDbLeftJoin.DefaultIfEmpty()
                                        select new
                                        {
                                            InMemory = inMemory, //formdan gelen
                                            Entity = lj  //database-de olan eger varsa
                                        };

                foreach (var item in imageCombinations)
                {
                    if (item.Entity == null && item.InMemory?.File != null) //insert
                    {
                        var productImage = new ProductImage
                        {
                            IsMain = item.InMemory.IsMain,
                            Name = fileService.Upload(item.InMemory.File)
                        };

                        await productRepository.AddProductImageAsync(product.Id, productImage, cancellationToken);
                    }
                    else if (item.Entity != null && string.IsNullOrWhiteSpace(item.InMemory?.TempPath)) //remove
                    {
                        productRepository.RemoveProductImage(item.Entity);
                    }
                    else if(item.Entity != null)
                    {
                        item.Entity.IsMain = item.InMemory.IsMain;
                    }
                }
                productRepository.Save();
            }



            if (request.Catalog != null && request.Catalog.Length > 0)
            {
                #region formdan gelir ama db-de yoxdur : insert lazimdi
                foreach (var catalog in request.Catalog.Where(m => m.Id == 0))
                {
                    var productCatalog = new ProductCatalog
                    {
                        ColorId = catalog.ColorId,
                        SizeId = catalog.SizeId,
                        MaterialId = catalog.MaterialId,
                        Price = catalog.Price,
                    };
                    await productRepository.AddProductCatalogItemAsync(product.Id, productCatalog, cancellationToken);
                }
                #endregion

                var catalogItems = await productRepository.GetCatalog(m => m.ProductId == product.Id).ToListAsync(cancellationToken);

                //left join
                var catalogCombinations = from inDb in catalogItems
                                          join inMemory in request.Catalog.Where(m => m.Id != 0) on inDb.Id equals inMemory.Id into inDbLeftJoin
                                          from lj in inDbLeftJoin.DefaultIfEmpty()
                                          select new
                                          {
                                              Entity = inDb, //database-de olan
                                              InMemory = lj  //formdan gelen eger gelibse
                                          };


                foreach (var combination in catalogCombinations)
                {
                    if (combination.InMemory == null) //formdan gelmeyib demeli silmek lazimdi
                    {
                        productRepository.RemoveProductCatalogItem(combination.Entity);
                    }
                    else
                    {
                        combination.Entity.ColorId = combination.InMemory.ColorId;
                        combination.Entity.SizeId = combination.InMemory.SizeId;
                        combination.Entity.MaterialId = combination.InMemory.MaterialId;
                        combination.Entity.Price = combination.InMemory.Price;
                    }
                }
                productRepository.Save();
            }

            return product;
        }
    }
}
