namespace Application.Discount
{
    public class DiscountContext
    {
        private readonly IDiscountStrategy _discountStrategy;

        public DiscountContext(IDiscountStrategy discountStrategy)
        {
            _discountStrategy = discountStrategy;
        }

        public float CalculateDiscount(float totalPrice)
        {
            return _discountStrategy.ApplyDiscount(totalPrice);
        }
    }
}
