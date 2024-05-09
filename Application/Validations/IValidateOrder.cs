using Domain.Models;

namespace Application.Validations
{
    public interface IValidateOrder
    {
        void ValidateSandwichCountInOrder(List<Food> orderItems);
    }
}