namespace RecipeCookingBookDto.Library.Dto
{
    public record LeftoverDto
    {
        public int Id { get; init; }

        public string Name { get; set; } = string.Empty;

        public int Amount { get; set; }
    }
}