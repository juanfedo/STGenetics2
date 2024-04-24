using Application.DTO;
using Application.Utilities;
using AutoMapper;
using Domain.Models;
using Infrastructure.Repositories;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        readonly IOrderRepository _orderRepository;
        readonly IOrderItemsRepository _orderItemsRepository;
        readonly IMapper _mapper;
        readonly IOrderDiscount _orderDiscount;

        public OrderService(IOrderRepository orderRepository, IOrderItemsRepository orderItemsRepository, IMapper mapper, IOrderDiscount orderDiscount)
        {
            _orderRepository = orderRepository;
            _orderItemsRepository = orderItemsRepository;
            _mapper = mapper;
            _orderDiscount = orderDiscount;
        }

        public async Task<List<OrderGetDTO>> GetAllOrdersAsync(CancellationToken cancellationToken)
        {
            List<OrderGetDTO> ordersDTO = new List<OrderGetDTO>();
            var orders = await _orderRepository.GetAllOrdersAsync(cancellationToken);
            foreach (var element in orders)
            {
                var items = await _orderItemsRepository.GetOrderItemsByOrderIdAsync(element.Id, cancellationToken);
                var orderDTO = _mapper.Map<OrderGetDTO>(element);
                List<OrderItemGetDTO> itemsDTO = _mapper.Map<List<OrderItemGetDTO>>(items);
                orderDTO.Items.AddRange(itemsDTO);
                ordersDTO.Add(orderDTO);
            }

            return ordersDTO;
        }

        public async Task<float> CreateOrderAsync(OrderPostDTO orderDTO, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(orderDTO);
            string foodIds = string.Join(',', orderDTO.Items.Select(x => x.FoodId));
            order.TotalPrice = await GetPriceByOrder(foodIds, cancellationToken);
            var orderId = await _orderRepository.CreateOrderAsync(order, cancellationToken);
            foreach (var element in orderDTO.Items)
            {
                var orderItem = _mapper.Map<OrderItems>(element);
                orderItem.OrderId = orderId;
                await _orderItemsRepository.CreateOrderItemsAsync(orderItem, cancellationToken);
            }
            return order.TotalPrice;
        }

        public async Task DeleteOrderAsync(int id, CancellationToken cancellationToken)
        { 
            var rowsAffectedOrderItem = await _orderItemsRepository.DeleteOrderItemsAsync(id, cancellationToken);
            var rowsAffectedOrder = await _orderRepository.DeleteOrderAsync(id, cancellationToken);
            if (rowsAffectedOrderItem + rowsAffectedOrder == 0)
            {
                throw new Exception("0 rows deleted");
            }
        }

        public async Task UpdateOrderAsync(OrderPatchDTO orderDTO, CancellationToken cancellationToken)
        {
            int rowsAffectedOrderItem = 0;
            var order = _mapper.Map<Order>(orderDTO);
            string foodIds = string.Join(',', orderDTO.Items.Select(x => x.FoodId));
            order.TotalPrice = await GetPriceByOrder(foodIds, cancellationToken);
            var rowsAffectedOrder = await _orderRepository.UpdateOrderAsync(order, cancellationToken);
            foreach (var element in orderDTO.Items)
            {
                var orderItem = _mapper.Map<OrderItems>(element);
                rowsAffectedOrderItem += await _orderItemsRepository.UpdateOrderItemsAsync(orderItem, cancellationToken);
            }

            if (rowsAffectedOrderItem + rowsAffectedOrder == 0)
            {
                throw new Exception("0 rows affected");
            }
        }

        private async Task<float> GetPriceByOrder(string foodIds, CancellationToken cancellationToken)
        {
            var result = await _orderItemsRepository.GetPriceByOrderAsync(foodIds, cancellationToken);
            var price = _orderDiscount.CalculateDiscount(result);
            return (float)Math.Round(price, 2);
        }
    }
}
