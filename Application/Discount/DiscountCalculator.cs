using Domain.Models;

namespace Application.Discount
{
    public class DiscountCalculator
    {
        public float ApplyDiscount(List<Food> orderItems)
        {
            var priceBeforeDiscount = orderItems.Sum(x => x.Price);

            bool hasSandwich = orderItems.Where(x => x.Type == "Sandwich").Any();
            bool hasFries = orderItems.Where(x => x.Name == "Fries").Any();
            bool hasDrink = orderItems.Where(x => x.Name == "Soft drink").Any();

            DiscountContext context = (hasSandwich, hasFries, hasDrink) switch
            {
                (true, true, true) => new DiscountContext(new FullMealDiscount()), //Full meal discount
                (true, true, false) => new DiscountContext(new SandwichAndFriesDiscount()), // Sandwich and fries
                (true, false, true) => new DiscountContext(new SandwichAndDrinkDiscount()), // Sandwich and drink
                _ => new DiscountContext(new WhitoutDiscount()),
            };

            return context.CalculateDiscount(priceBeforeDiscount);
        }
    }
}
