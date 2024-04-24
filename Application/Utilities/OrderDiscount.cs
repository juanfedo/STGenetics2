using Domain.Models;

namespace Application.Utilities
{
    public class OrderDiscount : IOrderDiscount
    {
        public float CalculateDiscount(List<Food> orderItems)
        {
            var priceBeforeDiscount = orderItems.Sum(x => x.Price);

            bool hasSandwich = orderItems.Where(x => x.Type == "Sandwich").Any();
            bool hasFries = orderItems.Where(x => x.Name == "Fries").Any();
            bool hasDrink = orderItems.Where(x => x.Name == "Soft drink").Any();

            var result = (hasSandwich, hasFries, hasDrink) switch
            {
                (true, true, true) => priceBeforeDiscount * 0.8f, //Full meal discount
                (true, true, false) => priceBeforeDiscount * 0.9f, // Sandwich and fries
                (true, false, true) => priceBeforeDiscount * 0.85f, // Sandwich and drink
                _ => priceBeforeDiscount,
            };

            return result;
        }
    }
}
