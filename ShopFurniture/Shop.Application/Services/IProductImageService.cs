using Shop.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services
{
    public interface IProductImageService
    {
        List<ProductImageDto> GetAll();
        ProductImageDto Get(int id);
        bool Add(ProductImageDto productImage);
        bool Update(ProductImageDto productImage);
        bool Delete(int id);
    }
}
