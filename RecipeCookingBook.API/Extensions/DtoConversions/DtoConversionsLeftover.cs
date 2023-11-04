using RecipeCookingBook.API.Models;
using RecipeCookingBookDto.Library.Dto;

namespace RecipeCookingBook.API.Extensions.DtoConversions
{
    public static class DtoConversionsLeftover
    {
        public static LeftoverDto ConvertToOneLeftOverDto(this Leftover? leftover)
        {
            if (leftover == null)
            {
                throw new ArgumentNullException(nameof(leftover), "Leftover is null");
            }

            return new LeftoverDto
            {
                Id = leftover.Id,
                Name = leftover.Name,
                Amount = leftover.Amount
            };
        }

        public static IList<LeftoverDto> ConvertAllLeftoverDto(this IList<Leftover> leftover)
        {
            return (from leftovers in leftover
                select new LeftoverDto()
                {
                    Id = leftovers.Id,
                    Name = leftovers.Name,
                    Amount = leftovers.Amount
                }).ToList();
        }
    }
}