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
    public class OrderDetailService:IOrderDetailService
    {
        private readonly IOrderDetailRepo _repo;
        private readonly IMapper _mapper;
        public OrderDetailService(IOrderDetailRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public List<OrderDetailDto> GetAll()
        {
            return _mapper.Map<List<OrderDetailDto>>(_repo.GetAll());
        }
        public OrderDetailDto Get(int id)
        {
            return _mapper.Map<OrderDetailDto>(_repo.Get(id));
        }
        public bool Add(OrderDetailDto orderDetail) 
        {
            return _repo.Add(_mapper.Map<OrderDetail>(orderDetail));
        }
        public bool Update(OrderDetailDto orderDetail)
        {
            return _repo.Update(_mapper.Map<OrderDetail>(orderDetail));
        }
        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }
    }
}
