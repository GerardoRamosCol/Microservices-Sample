using AutoMapper;
using ECommerce.API.Products.Db;
using ECommerce.API.Products.Interfaces;
using ECommerce.API.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext dbcontext;        
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;

        public ProductsProvider(ProductsDbContext dbcontext, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            this.dbcontext = dbcontext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!dbcontext.Products.Any())
            {
                dbcontext.Products.Add(new Db.Product() { Id = 1, Name = "Keyboard", Price = 20, Inventory = 100 });
                dbcontext.Products.Add(new Db.Product() { Id = 2, Name = "Mouse", Price = 5, Inventory = 200 });
                dbcontext.Products.Add(new Db.Product() { Id = 3, Name = "Monitor", Price = 150, Inventory = 150 });
                dbcontext.Products.Add(new Db.Product() { Id = 4, Name = "CPU", Price = 200, Inventory = 50 });
                dbcontext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Product> Products, string ErrorMessage)> GerProductsAsync()
        {
            try
            {
                var products = await dbcontext.Products.ToListAsync();
                if (products != null && products.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Product>, IEnumerable<Models.Product>>(products);

                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.Product Product, string ErrorMessage)> GerProductAsync(int id)
        {
            try
            {
                var product = await dbcontext.Products.FirstAsync(p=> p.Id == id);
                if (product != null)
                {
                    var result = mapper.Map<Db.Product, Models.Product>(product);

                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
