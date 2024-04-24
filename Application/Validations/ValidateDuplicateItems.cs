using Application.DTO;
using System.ComponentModel.DataAnnotations;

namespace Application.Validations
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property)]
    public class ValidateDuplicateItems: ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var order = (List<OrderItemPostDTO>)value;

                var duplicateItems = order?.GroupBy(f => new { f.FoodId })
                                       .Where(g => g.Count() > 1)
                                       .ToList();

                if (duplicateItems != null && duplicateItems.Any())
                {
                    return new ValidationResult("It's not allowed to duplicate items in the Order");
                }

                return ValidationResult.Success!;
            }

            return new ValidationResult("List null");
        }
    }
}
