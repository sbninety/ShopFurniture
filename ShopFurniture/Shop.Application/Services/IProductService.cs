﻿using Shop.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services
{
    public interface IProductService
    {
        List<ProductDto> GetAll();
        ProductDto Get(int id);
        bool Add(ProductDto product);
        bool Update(ProductDto product);
        bool Delete(int id);
    }
}
