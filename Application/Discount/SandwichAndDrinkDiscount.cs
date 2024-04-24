namespace Application.Discount
{
    public class SandwichAndDrinkDiscount : IDiscountStrategy
    {
        public float ApplyDiscount(float totalPrice)
        {
            return totalPrice * 0.85f;
        }
    }
}
