using AutoMapper;
using Shop.Application.Dto;
using Shop.Domain.Entities;
using Shop.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services
{
    public class ProductImageService:IProductImageService
    {
        private readonly IProductImageRepo _productImageRepo;
        private readonly IMapper _mapper;
        public ProductImageService(IProductImageRepo productImageRepo, IMapper mapper)
        {
            _productImageRepo = productImageRepo;
            _mapper = mapper;
        }
        public List<ProductImageDto> GetAll()
        {
            return _mapper.Map<List<ProductImageDto>>(_productImageRepo.GetAll());
        }
        public ProductImageDto Get(int id)
        {
            return _mapper.Map<ProductImageDto>(_productImageRepo.Get(id));
        }
        public bool Add(ProductImageDto productImage)
        {
            return _productImageRepo.Add(_mapper.Map<ProductImage>(productImage));
        }
        public bool Update(ProductImageDto productImage)
        {
            return _productImageRepo.Update(_mapper.Map<ProductImage>(productImage));
        }
        public bool Delete(int id)
        {
            return _productImageRepo.Delete(id);
        }
    }
}
