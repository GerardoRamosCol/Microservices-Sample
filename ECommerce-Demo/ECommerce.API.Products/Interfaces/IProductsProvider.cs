using ECommerce.API.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Products.Interfaces
{
    public interface IProductsProvider
    {
        Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMessage)> GerProductsAsync();
        Task<(bool IsSuccess, Product Product , string ErrorMessage)> GerProductAsync(int id);
    }
}
