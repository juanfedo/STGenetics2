namespace Application.Discount
{
    public class WhitoutDiscount : IDiscountStrategy
    {
        float IDiscountStrategy.ApplyDiscount(float totalPrice)
        {
            return totalPrice;
        }
    }
}
