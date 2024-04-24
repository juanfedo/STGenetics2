namespace Application.Discount
{
    public interface IDiscountStrategy
    {
        float ApplyDiscount(float totalPrice);
    }
}
