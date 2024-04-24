using Domain.Models;

namespace Application.Utilities
{
    public interface IOrderDiscount
    {
        float CalculateDiscount(List<Food> orderItems);
    }
}