using Application.DTO;
using System.ComponentModel.DataAnnotations;

namespace Application.Validations
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property)]
    public class ValidateItemsCountInOrder : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var orderItems = (List<OrderItemPostDTO>)value;

                if (orderItems != null && orderItems.ToList().Count > 3)
                {
                    return new ValidationResult("Each order can have maximum 3 items");
                }

                return ValidationResult.Success!;
            }

            return new ValidationResult("List null");
        }
    }
}
