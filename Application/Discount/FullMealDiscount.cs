namespace Application.Discount
{
    internal class FullMealDiscount : IDiscountStrategy
    {
        public float ApplyDiscount(float totalPrice)
        {
            return totalPrice * 0.8f;
        }
    }
}
