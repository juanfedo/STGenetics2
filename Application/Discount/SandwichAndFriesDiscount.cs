namespace Application.Discount
{
    public class SandwichAndFriesDiscount : IDiscountStrategy
    {
        public float ApplyDiscount(float totalPrice)
        {
            return totalPrice * 0.9f;
        }
    }
}
