using Domain.Models;

namespace Application.Validations
{
    public class ValidateOrder : IValidateOrder
    {
        public void ValidateSandwichCountInOrder(List<Food> orderItems)
        {
            var result = orderItems.Where(x => x.Type == "Sandwich")
                                    .GroupBy(f => f.Id)
                                    .Where(g => g.Count() > 1).ToList();

            if (result != null && result.Count != 0)
            {
                throw new Exception("Only 1 kind of Sandwich is allowed");
            }
        }
    }
}
